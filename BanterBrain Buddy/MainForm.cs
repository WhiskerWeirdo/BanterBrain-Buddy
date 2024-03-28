using Gma.System.MouseKeyHook;
using OpenAI_API;
using OpenAI_API.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CSCore.MediaFoundation;
using CSCore.SoundOut;
using CSCore.SoundIn;
using CSCore;
using CSCore.DMO;
using CSCore.CoreAudioAPI;
using CSCore.Streams;
using CSCore.Codecs.WAV;
using System.Speech.AudioFormat;
using OpenAI_API.Moderation;
using System.Threading;
using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Client.Events;
using TwitchLib.Communication.Interfaces;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TwitchLib.Api.Helix.Models.Soundtrack;
using TwitchLib.Api.Auth;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Reflection;
using static System.Net.WebRequestMethods;

namespace BanterBrain_Buddy
{
    public partial class BBB : Form
    {
        //PTT hotkey hook
        private IKeyboardMouseEvents m_GlobalHook;

        //used for PTT checking
        private bool HotkeyCalled = false;
        // check if SST is finished yet
        private bool STTDone = false;
        //Hotkey Storage
        private List<Keys> SetHotkeys = new List<Keys>();
        //check if the GPT LLM is donestop audio capture
        private bool GPTDone = false;

        public BBB()
        {

            InitializeComponent();
            LoadSettings();
            Subscribe();
            GetAudioDevices();

            TextLog.AppendText("Program Starting...\r\n");
            TextLog.AppendText("PPT hotkey: " + MicrophoneHotkeyEditbox.Text + "\r\n");
        }

        public void GetAudioDevices()
        {
            var CaptureDevices = WaveInDevice.EnumerateDevices();
            foreach (var device in CaptureDevices)
            {
                SoundInputDevices.Items.Add(device.Name);
            }
            //output devices
            var OutputDevices = WaveOutDevice.EnumerateDevices();

            foreach (var device in WaveOutDevice.EnumerateDevices())
            {
                TTSAudioOutputComboBox.Items.Add(device.Name);
            }
        }

        private async void STTTestButton_Click(object sender, EventArgs e)
        {
            if (STTTestButton.Text == "Test")
            {
                STTTestOutput.Text = "";
                TextLog.AppendText("Test Microphone on\r\n");
                STTTestButton.Text = "Recording";
                String SelectedProvider = STTProviderBox.GetItemText(STTProviderBox.SelectedItem);
                TextLog.AppendText(SelectedProvider + "\r\n");
                STTDone = false;
                if (SelectedProvider == "Native")
                {
                    TextLog.AppendText("Test Native STT calling\r\n");
                    InputStreamtoWav();
                    while (!STTDone)
                    {
                        await Task.Delay(500);
                    }
                }
            }
            else
            {
                STTTestButton.Text = "Test";
                TextLog.AppendText("Test stopped recording\r\n");
                STTTestOutput.BackColor = SystemColors.Control;
                StopWavCapture();
            }
        }

        private void STTProviderBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String SelectedProvider = STTProviderBox.GetItemText(STTProviderBox.SelectedItem);
            if (SelectedProvider == "Native")
            {
                TextLog.AppendText("Native STT\r\n");
                STTAPIKeyEditbox.Enabled = false;
                STTRegionEditbox.Enabled = false;
                STTTestOutput.Text = "Hint: For better native Speech-To-Text always train your voice at least once in Control Panel\\Ease of Access\\Speech Recognition";
            }
            else if (SelectedProvider == "Azure")
            {
                TextLog.AppendText("Azure\r\n");
                STTAPIKeyEditbox.Enabled = true;
                STTRegionEditbox.Enabled = true;
            }
            else if (SelectedProvider == "Google")
            {
                TextLog.AppendText("Google\r\n");
                STTAPIKeyEditbox.Enabled = true;
                STTRegionEditbox.Enabled = true;
            }
        }

        //help with selected inputdevice
        private IWaveSource _finalSource;
        public MMDevice SelectedDevice
        {
            get { return _selectedDevice; }
            set
            {
                _selectedDevice = value;
            }
        }

        // Saving data from specific input device into a .wav file for Speech recognition
        private MMDevice _selectedDevice;
        private WasapiCapture _soundIn;
        private IWriteable _writer;
        private string tmpWavFile = Path.GetDirectoryName(Application.ExecutablePath) + "\\tmp.wav";
        private void InputStreamtoWav()
        {

            var devices = MMDeviceEnumerator.EnumerateDevices(DataFlow.Capture, DeviceState.Active);
            foreach (var device in devices)
            {
                if (device.FriendlyName == SoundInputDevices.Text)
                {
                    SelectedDevice = device;
                }
            }
            _soundIn = new WasapiCapture();
            _soundIn.Device = SelectedDevice;
            _soundIn.Initialize();
            var soundInSource = new SoundInSource(_soundIn) { FillWithZeros = false };
            var singleBlockNotificationStream = new SingleBlockNotificationStream(soundInSource.ToSampleSource());

            //speech recognition is painful, like life
            //this has to be this setting or Native TTS function will throw an error
            _finalSource = singleBlockNotificationStream.ToMono().ChangeSampleRate(16000).ToWaveSource(16); 
            _writer = new WaveWriter(tmpWavFile, _finalSource.WaveFormat);
            soundInSource.DataAvailable += (s, e) =>
            {
                int read;
                byte[] buffer = new byte[_finalSource.WaveFormat.BytesPerSecond / 2];
                while ((read = _finalSource.Read(buffer, 0, buffer.Length)) > 0)
                    _writer.Write(buffer, 0, read);
            };
            _soundIn.Start();
            TextLog.AppendText("STT microphone start. -- SPEAK NOW -- \r\n");
        }

        private void StopWavCapture()
        {
            TextLog.AppendText("Stopping capture to WAV file\r\n");
            if (_soundIn != null)
            {
                _soundIn.Stop();
                _soundIn.Dispose();
                _soundIn = null;
                _finalSource.Dispose();
                if (_writer is IDisposable)
                    ((IDisposable)_writer).Dispose();

            }
            STTDone = false;
            //give the disk a moment to catch up
            Thread.Sleep(1000);
            //now lets convert the saved .wav to Text
            NativeSTTfromWAV();
        }

        private async void NativeSTTfromWAV()
        {
            // Create an in-process speech recognizer for the en-US locale.  
            SpeechRecognitionEngine recognizer2 = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            // Create and load a dictation grammar.  
            recognizer2.LoadGrammar(new DictationGrammar());
            recognizer2.SetInputToWaveFile(tmpWavFile);
            // Attach event handlers for the results of recognition.  
            recognizer2.SpeechRecognized +=
              new EventHandler<SpeechRecognizedEventArgs>(SpeechRecognized);
            recognizer2.RecognizeCompleted +=
              new EventHandler<RecognizeCompletedEventArgs>(RecognizeCompletedHandler);

            TextLog.AppendText("Starting asynchronous recognition... on " +tmpWavFile +"\r\n");
            STTDone = false;
            recognizer2.RecognizeAsync(RecognizeMode.Multiple);
            while (!STTDone)
            {
                await Task.Delay(1000);
            }
            TextLog.AppendText("STT done.\r\n");
            recognizer2.Dispose();
        }



        // Handle the SpeechHypothesized event.  
        private void SpeechHypothesizedHandler(object sender, SpeechHypothesizedEventArgs e)
        {
            TextLog.AppendText(" In SpeechHypothesizedHandler:+\r\n");
            Console.WriteLine("in hypothesishandler");
            string grammarName = "<not available>";
            string resultText = "<not available>";
            if (e.Result != null)
            {
                if (e.Result.Grammar != null)
                {
                    grammarName = e.Result.Grammar.Name;
                }
                resultText = e.Result.Text;
            }

            Console.WriteLine(" - Grammar Name = {0}; Result Text = {1}",
              grammarName, resultText);
        }

        private void RecognizeCompletedHandler(object sender, RecognizeCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                TextLog.AppendText("  Error encountered, "+ e.Error.GetType().Name+" : " +e.Error.Message+"\r\n");
            }
            if (e.Cancelled)
            {
                TextLog.AppendText("  Operation cancelled\r\n");
            }
            if (e.InputStreamEnded)
            {
                TextLog.AppendText("STT recognize Stopped.\r\n");
            }
            
            STTDone = true;
        }

        // Handle the SpeechRecognized event.  
        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result != null && e.Result.Text != null)
            {
                TextLog.AppendText("Recognized text: " + e.Result.Text + "\r\n");
                STTTestOutput.AppendText(e.Result.Text + "\r\n");
            }
            else
            {
                STTTestOutput.AppendText("  Recognized text not available.");
            }
        }

        private void SpeechDetectedHandler(object sender, SpeechDetectedEventArgs e)
        {
            TextLog.AppendText(" In SpeechDetectedHandler:\r\n");
            TextLog.AppendText(" - AudioPosition = " + e.AudioPosition + "\r\n");
        }

        private async void TalkToOpenAIGPT(String UserInput)
        {
            TextLog.AppendText("Sending to GPT: " +UserInput+ "\r\n");
            GPTDone = false;
            OpenAIAPI api = new OpenAIAPI(LLMAPIKeyTextBox.Text);
            var chat = api.Chat.CreateConversation();
            chat.Model = Model.ChatGPTTurbo;
            chat.RequestParameters.Temperature = 0;
            chat.RequestParameters.MaxTokens = 100;

            //mood is setting the system text description
            TextLog.AppendText("SystemRole: " + LLMRoleTextBox.Text + "\r\n");
            chat.AppendSystemMessage(LLMRoleTextBox.Text);

            chat.AppendUserInput(UserInput);
            try
            {
                TextLog.AppendText("ChatGPT response: ");
                await chat.StreamResponseFromChatbotAsync(res =>
                 {
                     TextLog.AppendText(res);
                     LLMTestOutputbox.AppendText(res);
                 });
                TextLog.AppendText("\r\nGPT Response done\r\n");
                GPTDone = true;
            }
            catch (System.Security.Authentication.AuthenticationException ex)
            {
                MessageBox.Show(ex.Message, "GPT API Auth error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void GPTTestButton_Click(object sender, EventArgs e)
        {
            LLMTestOutputbox.Text = "";
            GPTTestButton.Enabled = false;
            GPTTestButton.Text = "Wait...";
            if (LLMProviderComboBox.Text == "OpenAI ChatGPT")
            {
                TextLog.AppendText("Using ChatGPT\r\n");
                TalkToOpenAIGPT("How are you?");
            }
            GPTTestButton.Text = "Test";
            GPTTestButton.Enabled = true;
        }

        //output to the selected audio device
        private void OutputStream(MemoryStream stream)
        {
            var Devices = WaveOutDevice.EnumerateDevices();
            int deviceID = 0;
            foreach (var device in WaveOutDevice.EnumerateDevices())
            {
                if (device.Name == TTSAudioOutputComboBox.Text)
                {
                    deviceID = device.DeviceId;
                }
            }   
            var waveOut = new WaveOut { Device = new WaveOutDevice(deviceID) };
            using (var waveSource = new MediaFoundationDecoder(stream))
            {
                waveOut.Initialize(waveSource);
                waveOut.Play();
                waveOut.WaitForStopped();
            }
        }
 
        private async void TTSNativeSpeakToOutput(String TTSText)
        {
            TextLog.AppendText("Saying text with Native TTS\r\n");
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            var stream = new MemoryStream();
            synthesizer.SetOutputToWaveStream(stream);
            synthesizer.Speak(TTSText);
            //OutputStream(stream);
            var Devices = WaveOutDevice.EnumerateDevices();
            int deviceID = 0;
            foreach (var device in WaveOutDevice.EnumerateDevices())
            {
                if (device.Name == TTSAudioOutputComboBox.Text)
                {
                    deviceID = device.DeviceId;
                }
            }
            var waveOut = new WaveOut { Device = new WaveOutDevice(deviceID) };
            using (var waveSource = new MediaFoundationDecoder(stream))
            {
                waveOut.Initialize(waveSource);
                waveOut.Play();
                while (waveOut.PlaybackState != PlaybackState.Stopped)
                {
                    await Task.Delay(500);
                }
            }
        }

        private void TTSTestButton_Click(object sender, EventArgs e)
        {
            if (TTSProviderComboBox.Text == "Native")
            {
                TTSNativeSpeakToOutput(TTSTestTextBox.Text);
            }
        }

        private async void ProgramFlowTest_Click(object sender, EventArgs e)
        {
            //first, lets call STT
            if (ProgramFlowTest.Text == "Start")
            {
                STTTestOutput.Text = "";
                ProgramFlowTest.Text = "Recording";
                String SelectedProvider = STTProviderBox.GetItemText(STTProviderBox.SelectedItem);

                if (SelectedProvider == "Native")
                {
                    TextLog.AppendText("ProframFlow Native STT calling\r\n");
                    InputStreamtoWav();
                    while (!STTDone)
                    {
                        await Task.Delay(500);
                    }
                }
                
                Thread.Sleep(500); 
                //now the STT text is in STTTestOutput.Text, lets pass that to ChatGPT
                if (STTTestOutput.Text.Length > 1)
                {
                    LLMTestOutputbox.Text = "";
                    if (LLMProviderComboBox.Text == "OpenAI ChatGPT")
                    {
                        TextLog.AppendText("Using ChatGPT\r\n");
                        TalkToOpenAIGPT(STTTestOutput.Text);
                    }
                    //lets wait for GPT to be done
                    while (!GPTDone)
                    {
                        await Task.Delay(500);
                    }

                    //result text is in LLMTestOutputbox.Text, lets pass that to TTS
                    if (TTSProviderComboBox.Text == "Native")
                    {
                        TTSNativeSpeakToOutput(LLMTestOutputbox.Text);
                    }
                }
                else
                    TextLog.AppendText("No audio recorded");
            }
            else
            {
                ProgramFlowTest.Text = "Start";
                StopWavCapture();

            }
        }

        private void LoadSettings()
        {
            SoundInputDevices.Text = Properties.Settings.Default.VoiceInput;
            MicrophoneHotkeyEditbox.Text = Properties.Settings.Default.PTTHotkey;
            STTProviderBox.Text = Properties.Settings.Default.STTProvider;
            LLMProviderComboBox.Text = Properties.Settings.Default.LLMProvider;
            LLMModelComboBox.Text = Properties.Settings.Default.LLMModel;
            LLMRoleTextBox.Text = Properties.Settings.Default.LLMRoleText;
            TTSProviderComboBox.Text = Properties.Settings.Default.TTSProvider;
            TTSAudioOutputComboBox.Text = Properties.Settings.Default.TTSAudioOutput;
            TTSOutputVoice.Text = Properties.Settings.Default.TTSAudioVoice;
            TTSOutputVoiceOptions.Text = Properties.Settings.Default.TTSAudioVoiceOptions;
            STTAPIKeyEditbox.Text = Properties.Settings.Default.STTAPIKey;
            STTRegionEditbox.Text = Properties.Settings.Default.STTAPIRegion;
            LLMAPIKeyTextBox.Text = Properties.Settings.Default.LLMAPIKey;
            TTSOutputVoice.Enabled = Properties.Settings.Default.TTSAudioVoiceEnabled;
            TTSOutputVoiceOptions.Enabled = Properties.Settings.Default.TTSAudioVoiceOptionsEnabled;
            STTAPIKeyEditbox.Enabled = Properties.Settings.Default.STTAPIKeyEnabled;
            STTRegionEditbox.Enabled = Properties.Settings.Default.STTAPIRegionEnabled;
            TwitchUsername.Text= Properties.Settings.Default.TwitchUsername;
            TwitchAccessToken.Text = Properties.Settings.Default.TwitchAccessToken;
            TwitchChannel.Text = Properties.Settings.Default.TwitchChannel;
            TwitchCommandTrigger.Text = Properties.Settings.Default.TwitchCommandTrigger;
            //load HotkeyList into SetHotKeys
            foreach (String key in Properties.Settings.Default.HotkeyList)
            {
                Keys tmpKey = (Keys)Enum.Parse(typeof(Keys), key, true);
                SetHotkeys.Add(tmpKey);
             }
        }

        private void BBB_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.VoiceInput = this.SoundInputDevices.Text;
            Properties.Settings.Default.PTTHotkey = this.MicrophoneHotkeyEditbox.Text;
            Properties.Settings.Default.STTProvider = STTProviderBox.Text;
            Properties.Settings.Default.LLMProvider = LLMProviderComboBox.Text;
            Properties.Settings.Default.LLMModel = LLMModelComboBox.Text;
            Properties.Settings.Default.LLMRoleText = LLMRoleTextBox.Text;
            Properties.Settings.Default.TTSProvider = TTSProviderComboBox.Text;
            Properties.Settings.Default.TTSAudioOutput = TTSAudioOutputComboBox.Text;
            Properties.Settings.Default.TTSAudioVoice = TTSOutputVoice.Text;
            Properties.Settings.Default.TTSAudioVoiceOptions = TTSOutputVoiceOptions.Text;
            Properties.Settings.Default.STTAPIKey = STTAPIKeyEditbox.Text;
            Properties.Settings.Default.STTAPIRegion = STTRegionEditbox.Text;
            Properties.Settings.Default.LLMAPIKey = LLMAPIKeyTextBox.Text;
            Properties.Settings.Default.TTSAudioVoiceEnabled = TTSOutputVoice.Enabled;
            Properties.Settings.Default.TTSAudioVoiceOptionsEnabled = TTSOutputVoiceOptions.Enabled;
            Properties.Settings.Default.STTAPIKeyEnabled = STTAPIKeyEditbox.Enabled;
            Properties.Settings.Default.STTAPIRegionEnabled = STTRegionEditbox.Enabled;
            Properties.Settings.Default.TwitchUsername = TwitchUsername.Text;
            Properties.Settings.Default.TwitchAccessToken = TwitchAccessToken.Text;
            Properties.Settings.Default.TwitchChannel  = TwitchChannel.Text;
            Properties.Settings.Default.TwitchCommandTrigger = TwitchCommandTrigger.Text;
            //add the hotkeys in settings list, not in text
            Properties.Settings.Default.HotkeyList.Clear();
            foreach (Keys key in SetHotkeys)
            {
                Properties.Settings.Default.HotkeyList.Add(key.ToString());
            }
            Properties.Settings.Default.Save();
            //remove hotkey hooks
            Unsubscribe();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        public void ShowHotkeyDialogBox()
        {
            
            HotkeyForm HotkeyDialog = new HotkeyForm();
            var result = HotkeyDialog.ShowDialog(this);
            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (result == DialogResult.OK && HotkeyDialog.ReturnValue1 != null)
            {
                SetHotkeys.Clear();
                this.MicrophoneHotkeyEditbox.Text = "";
                List<Keys> HotKeys = HotkeyDialog.ReturnValue1;
                //ok now we got the keys, parse them and put them in the index box
                // and the global list for hotkeys

                for (var i=0; i< HotKeys.Count; i++)
                {
                    //add to the current hotkey list for keyup event checks
                    SetHotkeys.Add(HotKeys[i]);

                    //add to the text box
                    if (i < HotKeys.Count - 1)
                        this.MicrophoneHotkeyEditbox.Text += HotKeys[i].ToString() + " + ";
                    else
                        this.MicrophoneHotkeyEditbox.Text += HotKeys[i].ToString();
                }
            }
            TextLog.AppendText("Hotkey set to " + MicrophoneHotkeyEditbox.Text + "\r\n");
            HotkeyDialog.Dispose();
            //bind the new value 
            Subscribe();
        }

        private void MicrophoneHotkeySet_Click(object sender, EventArgs e)
        {
            Unsubscribe();
            ShowHotkeyDialogBox();
        }

        //Keyboard hooks
        public void Unsubscribe()
        {
            m_GlobalHook.KeyDown -= GlobalHookKeyDown;
            m_GlobalHook.KeyUp -= GlobalHookKeyUp;
            //It is recommened to dispose it
            m_GlobalHook.Dispose();
        }
        public void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.KeyDown += GlobalHookKeyDown;
            m_GlobalHook.KeyUp  += GlobalHookKeyUp;
        }

        private async void HandleHotkeyButton()
        {
            if (!HotkeyCalled)
            {
                if (ProgramFlowTest.Text == "Start")
                {
                    ProgramFlowTest_Click(null,null);
                    HotkeyCalled = true;
                    //ok lets wait 1 sec before checking shit again
                    await Task.Delay(1000);
                }
            }
        }

        private async void GlobalHookKeyUp(object sender, KeyEventArgs e)
        {
            //if microphone is on (hotkeycalled = true) and of the hotkeys are in the keyup event turn off the microphone
            //current hotkeys are in SetHotkeys
            if (HotkeyCalled)
            {
                //if one of the keys in the sethotkeys is detected as UP, give it a second then stop recording
                //foreach (Keys key in SetHotkeys)
                if (SetHotkeys.Contains(e.KeyCode))
                {
                    ProgramFlowTest_Click(null, null);
                    await Task.Delay(1000);
                    HotkeyCalled = false;
                }
            }

        }

        //handle the current hotkey setting
        private void GlobalHookKeyDown(object sender, KeyEventArgs e)
        {
            var map = new Dictionary<Combination, Action>
            {
               {Combination.FromString(MicrophoneHotkeyEditbox.Text),  () => HandleHotkeyButton() }
            };

            m_GlobalHook.OnCombination(map);
        }

        private async void TwitchTestButton_Click(object sender, EventArgs e)
        {

        }
   

        private async void TwitchAuthorizeButton_Click(object sender, EventArgs e)
        {
            //lets not block everything.
            await GetTwitchAuthToken();
        }

        //authorizations te token has to have for what we want to do
        private static List<string> scopes = new List<string> { "chat:read", "whispers:read", "whispers:edit", "chat:edit", "channel:moderate" };
        private async Task GetTwitchAuthToken()
        {

            //read the api secret from secret.json cos we dont want to have it in the github
            //even if its not really all that secret
            string SecretsJson = null;
            using (StreamReader r = new StreamReader(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\settings.json"))
            {
                string json = r.ReadToEnd();
                Console.WriteLine($"{json}");
                dynamic data = JObject.Parse(json);
                SecretsJson = data.TwitchAPISecret;
            }
           
            string TwitchAuthRedirect = "http://localhost:8080/redirect/";
            string TwitchAuthClientId = "osrlyqidmp7hea761h146r0h5ggkq2";
            // create twitch api instance
            var api = new TwitchLib.Api.TwitchAPI();
            api.Settings.ClientId = TwitchAuthClientId;

            // start local web server
            var server = new TwitchAuthWebserver(TwitchAuthRedirect);

             //spawn browser for authing
             var t = new Thread(() => Process.Start($"{getAuthorizationCodeUrl(TwitchAuthClientId, TwitchAuthRedirect, scopes)}"));
             t.Start();

            // listen for incoming requests
            var auth = await server.Listen();

            // exchange auth code for oauth access/refresh uses "secret"
            var resp = await api.Auth.GetAccessTokenFromCodeAsync(auth.Code, SecretsJson, TwitchAuthRedirect);

            // update TwitchLib's api with the recently acquired access token
            api.Settings.AccessToken = resp.AccessToken;

            // get the auth'd user
            var user = (await api.Helix.Users.GetUsersAsync()).Users[0];

            // print out all the data we've got
            Console.WriteLine($"Authorization success!\n\nUser: {user.DisplayName} (id: {user.Id})\nAccess token: {resp.AccessToken}\nRefresh token: {resp.RefreshToken}\nExpires in: {resp.ExpiresIn}\nScopes: {string.Join(", ", resp.Scopes)}");

            // refresh token
            var refresh = await api.Auth.RefreshAuthTokenAsync(resp.RefreshToken, SecretsJson);
            api.Settings.AccessToken = refresh.AccessToken;

            // confirm new token works
            user = (await api.Helix.Users.GetUsersAsync()).Users[0];

            // print out all the data we've got
            Console.WriteLine($"Authorization success!\n\nUser: {user.DisplayName} (id: {user.Id})\nAccess token: {refresh.AccessToken}\nRefresh token: {refresh.RefreshToken}\nExpires in: {refresh.ExpiresIn}\nScopes: {string.Join(", ", refresh.Scopes)}");
            
            //we should clean up the browser thread
            t.Abort();
        }

        private static string getAuthorizationCodeUrl(string clientId, string redirectUri, List<string> scopes)
        {
            var scopesStr = String.Join("+", scopes);

            return "https://id.twitch.tv/oauth2/authorize?" +
                   $"client_id={clientId}&" +
                   $"redirect_uri={redirectUri}&" +
                   "response_type=code&" +
                   $"scope={scopesStr}";
        }
    }
}
