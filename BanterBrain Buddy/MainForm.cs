using Gma.System.MouseKeyHook;
using OpenAI_API;
using OpenAI_API.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Speech.Recognition;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CSCore.MediaFoundation;
using CSCore.SoundOut;
using CSCore.SoundIn;
using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.Streams;
using CSCore.Codecs.WAV;
using System.Threading;
using System.Diagnostics;
using System.Runtime.Versioning;
using TwitchLib.Api.Helix.Models.Users.GetUserFollows;
using TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus;
using TwitchLib.Api.Helix;
using Microsoft.AspNetCore.Components;
using System.Reflection.Emit;

/// <summary>
/// CODING RULES:
/// •	Local variables, private instance, static fields and method parameters should be camelCase.
/// •	Methods, constants, properties, events and classes should be PascalCase.
/// •	Global private instance fields should be in camelCase prefixed with an underscore.
/// </summary>

namespace BanterBrain_Buddy
{
    public partial class BBB : Form
    {
        //set logger
        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //PTT hotkey hook
        private IKeyboardMouseEvents m_GlobalHook;

        //used for PTT checking
        private bool _hotkeyCalled = false;
        // check if SST is finished yet
        private bool _sTTDone = false;

        [SupportedOSPlatform("windows6.1")]
        //Hotkey Storage
        readonly private List<Keys> _setHotkeys = [];

        //check if the GPT LLM is donestop audio capture
        private bool _gPTDone = false;

        //error checker for async events. If true, stop execution of whatever you're doing
        private bool _bigError = false;

        //Global Twitch API class
        //we need this for the hourly /validate check
        private TwitchAPIESub _globalTwitchAPI;
        private bool _twitchValidateCheckStarted;

        [SupportedOSPlatform("windows6.1")]
        public BBB()
        {
            _twitchValidateCheckStarted = false;

            InitializeComponent();
            LoadSettings();
            GetAudioDevices();

            _bBBlog.Info("Program Starting...");
            _bBBlog.Info("PPT hotkey: " + MicrophoneHotkeyEditbox.Text);

            UpdateTextLog("Program Starting...");
            UpdateTextLog("PPT hotkey: " + MicrophoneHotkeyEditbox.Text + "\r\n");
            if (TwitchEnableCheckbox.Checked && TwitchCheckAuthAtStartup.Checked)
                SetTwitchValidateTokenTimer();
            else
                TwitchCheckAuthAtStartup.Enabled = false;
        }

        /// <summary>
        /// Twitch requires you to validate your access token every hour. This starts this timer when Twitch is enabled.
        /// </summary>
        [SupportedOSPlatform("windows6.1")]
        /// <summary>
        public async void SetTwitchValidateTokenTimer()
        {
            if (!_twitchValidateCheckStarted && TwitchEnableCheckbox.Checked && TwitchUsername.Text.Length > 0 && TwitchAccessToken.Text.Length > 0 && TwitchChannel.Text.Length > 0)
            {
                _globalTwitchAPI = new();
                var result = await _globalTwitchAPI.ValidateAccessToken(TwitchAccessToken.Text);
                if (!result)
                {
                    _bBBlog.Error("Twitch access token is invalid, please re-authenticate");
                    MessageBox.Show("Twitch access token is invalid, please re-authenticate", "Twitch Auth error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TextLog.Text += "Twitch access token is invalid, please re-authenticate\r\n";
                    _twitchValidateCheckStarted = false;
                    TwitchAPIStatusTextBox.Text = "DISABLED";
                    TwitchAPIStatusTextBox.BackColor = Color.Red;
                    _bigError = true;
                }
                else
                {
                    _bBBlog.Info("Twitch access token is valid. Starting automated /validate call");
                    TextLog.Text += "Twitch access token is valid. Starting automated /validate call\r\n";
                    _twitchValidateCheckStarted = true;
                    TwitchAPIStatusTextBox.Text = "ENABLED";
                    TwitchAPIStatusTextBox.BackColor = Color.Green;

                    //ok so all is good, lets start the eventsub client
                    if (await EventSubStartWebsocketClient())
                        //if we are good, start the hourly check
                        await _globalTwitchAPI.CheckHourlyAccessToken();
                    else
                    {
                        _bBBlog.Error("Error starting EventSub client");
                        MessageBox.Show("Error starting EventSub client", "Twitch EventSub error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TextLog.Text += "Error starting EventSub client\r\n";
                        _bigError = true;
                    }
                }
                //TODO: we also need to start the EventSub Client
            }
        }

        [SupportedOSPlatform("windows6.1")]
        //fill the audio input and output list boxes
        public void GetAudioDevices()
        {
            var captureDevices = WaveInDevice.EnumerateDevices();
            foreach (var device in captureDevices)
            {
                SoundInputDevices.Items.Add(device.Name);
            }

            foreach (var device in WaveOutDevice.EnumerateDevices())
            {
                _ = TTSAudioOutputComboBox.Items.Add(device.Name);
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void STTTestButton_Click(object sender, EventArgs e)
        {

            String selectedProvider = STTProviderBox.GetItemText(STTProviderBox.SelectedItem);
            if (STTTestButton.Text == "Test")
            {
                STTTestOutput.Text = "";
                UpdateTextLog("Test Microphone on\r\n");
                _bBBlog.Info("Test Microphone on");
                STTTestButton.Text = "Recording";
                UpdateTextLog(selectedProvider + "\r\n");

                _sTTDone = false;
                _bigError = false;
                if (selectedProvider == "Native")
                {
                    UpdateTextLog("Test Native STT calling\r\n");
                    _bBBlog.Info("Test Native STT calling");
                    NativeInputStreamtoWav();
                    while (!_sTTDone)
                    {
                        await Task.Delay(500);
                    }
                }
                else if (selectedProvider == "Azure")
                {
                    UpdateTextLog("Test Azure STT calling\r\n");
                    _bBBlog.Info("Test Azure STT calling");
                    //cant be empty

                    if ((STTAPIKeyEditbox.Text.Length < 1) || (STTRegionEditbox.Text.Length < 1))
                    {
                        STTTestOutput.Text = "Error! API Key or region cannot be empty!";
                        _bBBlog.Error("Error! API Key or region cannot be empty!");
                        STTTestButton.Text = "Test";
                    }
                    else
                    {
                        AzureConvertVoicetoText();
                        while (!_sTTDone && !_bigError)
                        {
                            await Task.Delay(500);
                        }

                    }
                }
            }
            else
            {
                STTTestButton.Text = "Test";
                UpdateTextLog("Test stopped recording\r\n");
                _bBBlog.Info("Test stopped recording");
                STTTestOutput.BackColor = SystemColors.Control;
                if (selectedProvider == "Native")
                    StopWavCapture();
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void STTProviderBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedProvider = STTProviderBox.GetItemText(STTProviderBox.SelectedItem);
            if (selectedProvider == "Native")
            {
                UpdateTextLog("Native STT selected\r\n");
                _bBBlog.Info("Native STT selected");
                STTAPIKeyEditbox.Enabled = false;
                STTRegionEditbox.Enabled = false;
                STTTestOutput.Text = "Hint: For better native Speech-To-Text always train your voice at least once in Control Panel\\Ease of Access\\Speech Recognition";
            }
            else if (selectedProvider == "Azure")
            {
                UpdateTextLog("Azure STT selected\r\n");
                _bBBlog.Info("Azure STT selected");
                STTAPIKeyEditbox.Enabled = true;
                STTRegionEditbox.Enabled = true;
                STTTestOutput.Text = "Be sure to set API key and region!";
            }
        }

        [SupportedOSPlatform("windows6.1")]
        //this sets "SelectedInputDevice" to the correct input/microphone
        private void SetSelectedInputDevice()
        {
            var devices = MMDeviceEnumerator.EnumerateDevices(DataFlow.Capture, DeviceState.Active);
            foreach (var device in devices)
            {
                if (device.FriendlyName == SoundInputDevices.Text)
                {
                    SelectedInputDevice = device;
                }
            }
        }

        [SupportedOSPlatform("windows6.1")]
        /// <summary>
        /// This uses the Azure Cognitive Services Speech SDK to convert voice to text.
        /// </summary>
        private async void AzureConvertVoicetoText()
        {
            AzureSpeechAPI azureSpeechAPI = new(STTAPIKeyEditbox.Text, STTRegionEditbox.Text, STTLanguageComboBox.Text);
            //call the Azure STT function with the selected input device
            //first initialize the Azure STT class
            azureSpeechAPI.AzureSTTInit(SoundInputDevices.Text);
            _bBBlog.Info("Azure STT microphone start.");
            while ((STTTestButton.Text == "Recording" || MainRecordingStart.Text == "Recording") && !_sTTDone && !_bigError)
            {
                var recognizeResult = await azureSpeechAPI.RecognizeSpeechAsync();
                if (recognizeResult == "NOMATCH")
                {
                    STTTestOutput.Text += "NOMATCH: Speech could not be recognized.\r\n";
                }
                else if (recognizeResult == null)
                {
                    STTTestOutput.Text += "Fail! Speech could not be proccessed. Check log for more info.\r\n";
                    TextLog.Text += "Azure Speech-To-Text: Fail! Speech could not be proccessed. Check log for more info.\r\n";
                    _bigError = true;
                    _sTTDone = true;
                }
                else
                {
                    STTTestOutput.Text += recognizeResult + "\r\n";
                    TextLog.Text += "Azure Speech-To-Text: " + recognizeResult + "\r\n";
                }
            }
            _sTTDone = true;
        }

        //help with selected inputdevice to return the ID
        //that corresponds with the name
        private IWaveSource _finalSource;
        public MMDevice SelectedInputDevice
        {
            get { return _selectedDevice; }
            set
            {
                _selectedDevice = value;
            }
        }

        public MMDevice SelectedOutputDevice
        {
            get { return _selectedDevice; }
            set
            {
                _selectedDevice = value;
            }
        }

        //NATIVE
        //Saving data from specific input device into a .wav file for Speech recognition
        private MMDevice _selectedDevice;
        private WasapiCapture _soundIn;
        private IWriteable _writer;

        [SupportedOSPlatform("windows6.1")]
        readonly private string _tmpWavFile = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\tmp.wav";

        [SupportedOSPlatform("windows6.1")]
        private void NativeInputStreamtoWav()
        {

            SetSelectedInputDevice();
            _bBBlog.Info("Selected audio input device for Native: " + SelectedInputDevice);
            _soundIn = new WasapiCapture() { Device = SelectedInputDevice };
            _soundIn.Initialize();
            var soundInSource = new SoundInSource(_soundIn) { FillWithZeros = false };
            var singleBlockNotificationStream = new SingleBlockNotificationStream(soundInSource.ToSampleSource());

            //speech recognition is painful, like life
            //this has to be this setting or Native TTS function will throw an error
            _finalSource = singleBlockNotificationStream.ToMono().ChangeSampleRate(16000).ToWaveSource(16);
            _writer = new WaveWriter(_tmpWavFile, _finalSource.WaveFormat);
            soundInSource.DataAvailable += (s, e) =>
            {
                int read;
                byte[] buffer = new byte[_finalSource.WaveFormat.BytesPerSecond / 2];
                while ((read = _finalSource.Read(buffer, 0, buffer.Length)) > 0)
                    _writer.Write(buffer, 0, read);
            };
            _soundIn.Start();
            UpdateTextLog("STT microphone start. -- SPEAK NOW -- \r\n");
            _bBBlog.Info("Native STT microphone start.");
        }

        [SupportedOSPlatform("windows6.1")]
        private void StopWavCapture()
        {
            UpdateTextLog("Stopping capture to WAV file\r\n");
            _bBBlog.Info("Stopping capture to WAV file");

            if (_soundIn != null)
            {
                _soundIn.Stop();
                _soundIn.Dispose();
                _soundIn = null;
                _finalSource.Dispose();
                if (_writer is IDisposable)
                    ((IDisposable)_writer).Dispose();

            }
            _sTTDone = false;
            //give the disk a moment to catch up
            Thread.Sleep(1000);
            //now lets convert the saved .wav to Text
            NativeSTTfromWAV();
        }

        [SupportedOSPlatform("windows6.1")]
        private async void NativeSTTfromWAV()
        {
            // Create an in-process speech recognizer for the en-US locale.  
            SpeechRecognitionEngine recognizer2 = new(new System.Globalization.CultureInfo("en-US"));
            // Create and load a dictation grammar.  
            recognizer2.LoadGrammar(new DictationGrammar());
            recognizer2.SetInputToWaveFile(_tmpWavFile);
            // Attach event handlers for the results of recognition.  
            recognizer2.SpeechRecognized +=
              new EventHandler<SpeechRecognizedEventArgs>(NativeSpeechRecognized);
            recognizer2.RecognizeCompleted +=
              new EventHandler<RecognizeCompletedEventArgs>(NativeRecognizeCompletedHandler);

            UpdateTextLog("Starting asynchronous Native recognition... on " + _tmpWavFile + "\r\n");
            _bBBlog.Info("Starting asynchronous Native recognition... on " + _tmpWavFile);

            _sTTDone = false;
            recognizer2.RecognizeAsync(RecognizeMode.Multiple);
            while (!_sTTDone)
            {
                await Task.Delay(1000);
            }
            UpdateTextLog("Native STT done.\r\n");
            _bBBlog.Info("Native STT done.");
            recognizer2.Dispose();
        }

        [SupportedOSPlatform("windows6.1")]
        // Handle the SpeechHypothesized event.  
        private void NativeSpeechHypothesizedHandler(object sender, SpeechHypothesizedEventArgs e)
        {
            UpdateTextLog(" In SpeechHypothesizedHandler:+\r\n");
            _bBBlog.Info("in hypothesishandler");
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

        [SupportedOSPlatform("windows6.1")]
        private void NativeRecognizeCompletedHandler(object sender, RecognizeCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                UpdateTextLog("Native STT Error encountered, " + e.Error.GetType().Name + " : " + e.Error.Message + "\r\n");
                _bBBlog.Error("Native STT Error encountered, " + e.Error.GetType().Name + " : " + e.Error.Message);

            }
            if (e.Cancelled)
            {
                UpdateTextLog("Native STT Operation cancelled\r\n");
                _bBBlog.Info("Native STT Operation cancelled");
            }
            if (e.InputStreamEnded)
            {
                UpdateTextLog("Native STT recognize Stopped.\r\n");
                _bBBlog.Info("Mative STT recognize Stopped.");
            }

            _sTTDone = true;
        }

        [SupportedOSPlatform("windows6.1")]
        // Handle the SpeechRecognized event.  
        private void NativeSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result != null && e.Result.Text != null)
            {
                UpdateTextLog("Native recognized text: " + e.Result.Text + "\r\n");
                _bBBlog.Info("Native recognized text: " + e.Result.Text);
                STTTestOutput.AppendText(e.Result.Text + "\r\n");
            }
            else
            {
                STTTestOutput.AppendText("Native recognized text not available.");
                _bBBlog.Info("Native recognized text not available.");
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void NativeSpeechDetectedHandler(object sender, SpeechDetectedEventArgs e)
        {
            UpdateTextLog(" In NativeSpeechDetectedHandler:\r\n");
            UpdateTextLog(" - AudioPosition = " + e.AudioPosition + "\r\n");
            _bBBlog.Info(" In NativeSpeechDetectedHandler: ");
            _bBBlog.Info(" - AudioPosition = \" + e.AudioPosition");
        }

        [SupportedOSPlatform("windows6.1")]
        private async Task TalkToOpenAIGPT(String UserInput)
        {
            UpdateTextLog("Sending to GPT: " + UserInput + "\r\n");
            _bBBlog.Info("Sending to GPT: " + UserInput);
            _gPTDone = false;
            OpenAIAPI api = new(LLMAPIKeyTextBox.Text);
            var chat = api.Chat.CreateConversation();
            chat.Model = Model.ChatGPTTurbo;
            chat.RequestParameters.Temperature = 0;
            chat.RequestParameters.MaxTokens = 100;

            //mood is setting the system text description
            UpdateTextLog("SystemRole: " + LLMRoleTextBox.Text + "\r\n");
            _bBBlog.Info("SystemRole: " + LLMRoleTextBox.Text);
            chat.AppendSystemMessage(LLMRoleTextBox.Text);

            chat.AppendUserInput(UserInput);
            try
            {
                UpdateTextLog("ChatGPT response: ");
                _bBBlog.Info("ChatGPT response: ");
                await chat.StreamResponseFromChatbotAsync(res =>
                 {
                     UpdateTextLog(res);
                     LLMTestOutputbox.AppendText(res);
                 });
                _bBBlog.Info(LLMTestOutputbox.Text);
                UpdateTextLog("\r\nGPT Response done\r\n");
                _bBBlog.Info("GPT Response done");
                _gPTDone = true;
            }
            catch (System.Security.Authentication.AuthenticationException ex)
            {
                MessageBox.Show(ex.Message, "GPT API Auth error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _bBBlog.Error("GPT API Auth error: " + ex.Message);
            }

        }

        [SupportedOSPlatform("windows6.1")]
        private async void GPTTestButton_Click(object sender, EventArgs e)
        {
            LLMTestOutputbox.Text = "";
            GPTTestButton.Enabled = false;
            GPTTestButton.Text = "Wait...";
            if (LLMProviderComboBox.Text == "OpenAI ChatGPT")
            {
                UpdateTextLog("Testing using ChatGPT\r\n");
                _bBBlog.Info("Testing using ChatGPT");
                await TalkToOpenAIGPT("How are you?");
            }
            GPTTestButton.Text = "Test";
            GPTTestButton.Enabled = true;
        }

        [SupportedOSPlatform("windows6.1")]

        //output to the selected audio device
        private void OutputStream(MemoryStream stream)
        {
            int deviceID = 0;
            foreach (var device in WaveOutDevice.EnumerateDevices())
            {
                if (device.Name == TTSAudioOutputComboBox.Text)
                {
                    deviceID = device.DeviceId;
                }
            }
            //TODO fix to "SelectedOutputDevice"
            var waveOut = new WaveOut { Device = new WaveOutDevice(deviceID) };
            using var waveSource = new MediaFoundationDecoder(stream);
            waveOut.Initialize(waveSource);
            waveOut.Play();
            waveOut.WaitForStopped();
        }

        /// <summary>
        /// Holds the list of Azure Voices and their options
        /// </summary>
        List<AzureVoices> _azureRegionVoicesList = [];

        [SupportedOSPlatform("windows6.1")]
        /// <summary>
        /// This function gets the list of voices available in the Azure TTS API
        /// </summary>
        private async Task TTSGetAzureVoices()
        {
            //only bother if the two fields are not empty or not "placeholder"
            if ((TTSAPIKeyTextBox.Text.Length < 1) || (TTSRegionTextBox.Text.Length < 1) || TTSAPIKeyTextBox.Text == "placeholder" || TTSRegionTextBox.Text == "placeholder")
            {
                _bBBlog.Error("API Key or region cannot be empty!");
                MessageBox.Show("API Key or region cannot be empty!", "Azure TTS error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _bigError = true;
                return;
            }
            _bBBlog.Info("Finding TTS Azure voices available");
            AzureSpeechAPI AzureSpeech = new(TTSAPIKeyTextBox.Text, TTSRegionTextBox.Text, "en-US");
            _azureRegionVoicesList = await AzureSpeech.TTSGetAzureVoices();

            if (_azureRegionVoicesList == null)
            {
                MessageBox.Show("Problem retreiving Azure API voicelist. Is your API key or subscription information still valid?", "Azure No voices", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _bigError = true;
            }
            else
            {
                _bBBlog.Info($"Found {_azureRegionVoicesList.Count} voices");
            }
        }

        [SupportedOSPlatform("windows6.1")]
        /// <summary>
        /// This function fills the Azure voice list in the GUI TTSOutputVoice
        /// </summary>
        private void TTSFillAzureVoicesList()
        {
            _bBBlog.Info("Fill Azure voice list");

            // Locale, Gender, Localname
            TTSOutputVoice.Items.Clear();
            foreach (var azureRegionVoice in _azureRegionVoicesList)
            {
                TTSOutputVoice.Items.Add(azureRegionVoice.LocaleDisplayname + "-" + azureRegionVoice.Gender + "-" + azureRegionVoice.LocalName);
            }
            TTSOutputVoice.Sorted = true;
            //if we dont have a real value (i.e. teh first startup placeholders) we need to set it to the first item
            if (TTSOutputVoice.Text == "placeholder")
                TTSOutputVoice.Text = TTSOutputVoice.Items[0].ToString();
            TTSOutputVoiceOption1.Text = "";

            TTSAzureFillOptions(TTSOutputVoice.Text);
        }

        [SupportedOSPlatform("windows6.1")]
        /// <summary>
        /// This function fills the Azure voice options in the GUI TTSOutputVoiceOptions, depends on the selected voice
        /// </summary>
        private void TTSAzureFillOptions(string SelectedVoice)
        {
            _bBBlog.Info("Finding Azure voice options (if available)");
            TTSOutputVoiceOption1.Items.Clear();
            //the voice is the item in TTSOutputVoice 
            //now to find it in AzureRegionVoicesList
            foreach (var azureRegionVoice in _azureRegionVoicesList)
            {
                if (SelectedVoice == (azureRegionVoice.LocaleDisplayname + "-" + azureRegionVoice.Gender + "-" + azureRegionVoice.LocalName))
                {
                    _bBBlog.Info("Match found, checking for voice options");
                    foreach (var voiceOption in azureRegionVoice.StyleList)
                    {
                        if (voiceOption.Length > 0)
                            TTSOutputVoiceOption1.Items.Add(voiceOption);
                        else
                            TTSOutputVoiceOption1.Items.Add("Default");
                    }
                }
            }
            //if nothing ends up being selected, pick the top one so at least something is selected
            if (TTSOutputVoiceOption1.SelectedIndex == -1)
            {
                try
                {
                    TTSOutputVoiceOption1.Text = TTSOutputVoiceOption1.Items[0].ToString();
                }
                catch (Exception ex)
                {
                    _bBBlog.Error("Issue assigning Azure voice. Error: " + ex.Message);
                }
            }

        }

        [SupportedOSPlatform("windows6.1")]
        //Azure Text-To-Speach
        private async void TTSAzureSpeakToOutput(string TextToSpeak)
        {
            _bBBlog.Info("Azure TTS called with text, seting up");
            AzureSpeechAPI azureSpeechAPI = new(STTAPIKeyEditbox.Text, STTRegionEditbox.Text, STTLanguageComboBox.Text);

            //set the output voice, gender and locale, and the style
            await azureSpeechAPI.AzureTTSInit(TTSOutputVoice.Text, TTSOutputVoiceOption1.Text, TTSAudioOutputComboBox.Text);

            var result = await azureSpeechAPI.AzureSpeak(TextToSpeak);
            if (!result)
            {
                _bBBlog.Error("Azure TTS error. Is there a problem with your API key or subscription?");
                MessageBox.Show("Azure TTS error. Is there a problem with your API key or subscription?", "Azure TTS error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _bigError = true;
            }
        }


        [SupportedOSPlatform("windows6.1")]
        private async void TTSNativeSpeakToOutput(String TTSText)
        {
            UpdateTextLog("Saying text with Native TTS\r\n");
            _bBBlog.Info("Saying text with Native TTS");
            NativeSpeech nativeSpeech = new();
            await nativeSpeech.NativeTTSInit(TTSAudioOutputComboBox.Text);
            await nativeSpeech.NativeSpeak(TTSText);
        }


        //agnostic TTS function
        [SupportedOSPlatform("windows6.1")]
        private async Task SayText (string TextToSay)
        {
            if (TTSProviderComboBox.Text == "Native")
            {
                TTSNativeSpeakToOutput(TextToSay);
            }
            else if (TTSProviderComboBox.Text == "Azure")
            {
                //we also need to fill the list and select the first available voice
                //then the options
                if (TTSOutputVoice.Items.Count < 1 || TTSOutputVoice.Text == "placeholder")
                {
                    await TTSGetAzureVoices();
                    if (_bigError)
                    {
                        return;
                    }
                    TTSFillAzureVoicesList();
                }
                TTSAzureSpeakToOutput(TextToSay);
            }
        }


        [SupportedOSPlatform("windows6.1")]
        private async void TTSTestButton_Click(object sender, EventArgs e)
        {
            await SayText(TTSTestTextBox.Text);

        }

        [SupportedOSPlatform("windows6.1")]
        private async Task TalkToLLM(string TextToPass)
        {
            LLMTestOutputbox.Text = "";
            _gPTDone = false;
            if (LLMProviderComboBox.Text == "OpenAI ChatGPT")
            {
                UpdateTextLog("Using ChatGPT\r\n");
                _bBBlog.Info("Using ChatGPT");
                await TalkToOpenAIGPT(TextToPass);
            }
            //lets wait for GPT to be done
            while (!_gPTDone)
            {
                await Task.Delay(500);
            }
        }


        [SupportedOSPlatform("windows6.1")]
        private async void MainRecordingStart_Click(object sender, EventArgs e)
        {
            _bigError = false;
            String selectedProvider = STTProviderBox.GetItemText(STTProviderBox.SelectedItem);
            //first, lets call STT
            _sTTDone = false;
            if (MainRecordingStart.Text == "Start")
            {
                STTTestOutput.Text = "";
                MainRecordingStart.Text = "Recording";
                if (selectedProvider == "Native")
                {
                    UpdateTextLog("Main button Native STT calling\r\n");
                    _bBBlog.Info("Main button Native STT calling");
                    NativeInputStreamtoWav();
                    while (!_sTTDone)
                    {
                        await Task.Delay(500);
                    }
                }
                else if (selectedProvider == "Azure")
                {
                    UpdateTextLog("Azure STT calling\r\n");
                    _bBBlog.Info("Azure STT calling");
                    //cant be empty

                    if ((STTAPIKeyEditbox.Text.Length < 1) || (STTRegionEditbox.Text.Length < 1))
                    {
                        TextLog.Text = "Error! API Key or region cannot be empty!\r\n";
                        _bBBlog.Error("Error! API Key or region cannot be empty!");
                        MainRecordingStart.Text = "Start";
                    }
                    else
                    {
                        AzureConvertVoicetoText();
                        while (!_sTTDone)
                        {
                            await Task.Delay(500);
                        }

                    }
                }

                //if _bigError is true, stop! something is very wrong.
                if (_bigError)
                {
                    UpdateTextLog("Theres an error, stopping execution!\r\n");
                    _bBBlog.Error("Theres an error, stopping execution");
                    MainRecordingStart.Text = "Start";
                    return;
                }

                Thread.Sleep(500);
                
                //now the STT text is in STTTestOutput.Text, lets pass that to ChatGPT
                if (STTTestOutput.Text.Length > 1)
                {
                    await TalkToLLM(STTTestOutput.Text);

                    //result text is in LLMTestOutputbox.Text, lets pass that to TTS
                    if (TTSProviderComboBox.Text == "Native")
                    {
                        TTSNativeSpeakToOutput(LLMTestOutputbox.Text);
                    }
                    else if (TTSProviderComboBox.Text == "Azure")
                    {
                        TTSAzureSpeakToOutput(LLMTestOutputbox.Text);
                    }
                }
                else
                {
                    UpdateTextLog("No audio recorded");
                    _bBBlog.Info("No audio recorded");
                }
            }
            else
            {
                MainRecordingStart.Text = "Start";
                if (selectedProvider == "Native")
                    StopWavCapture();

            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void LoadSettings()
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
            STTAPIKeyEditbox.Text = Properties.Settings.Default.STTAPIKey;
            STTRegionEditbox.Text = Properties.Settings.Default.STTAPIRegion;
            LLMAPIKeyTextBox.Text = Properties.Settings.Default.LLMAPIKey;
            TTSOutputVoice.Enabled = Properties.Settings.Default.TTSAudioVoiceEnabled;
            TTSOutputVoiceOption1.Enabled = Properties.Settings.Default.TTSAudioVoiceOptionsEnabled;
            STTAPIKeyEditbox.Enabled = Properties.Settings.Default.STTAPIKeyEnabled;
            STTRegionEditbox.Enabled = Properties.Settings.Default.STTAPIRegionEnabled;
            TwitchUsername.Text = Properties.Settings.Default.TwitchUsername;
            TwitchAccessToken.Text = Properties.Settings.Default.TwitchAccessToken;
            TwitchChannel.Text = Properties.Settings.Default.TwitchChannel;
            TwitchCommandTrigger.Text = Properties.Settings.Default.TwitchCommandTrigger;
            TwitchChatCommandDelay.Text = Properties.Settings.Default.TwitchChatCommandDelay.ToString();
            TwitchNeedsFollower.Checked = Properties.Settings.Default.TwitchNeedsFollower;
            TwitchNeedsSubscriber.Checked = Properties.Settings.Default.TwitchNeedsSubscriber;
            TwitchMinBits.Text = Properties.Settings.Default.TwitchMinBits.ToString();
            TwitchSubscribed.Checked = Properties.Settings.Default.TwitchSubscribed;
            TwitchCommunitySubs.Checked = Properties.Settings.Default.TwitchCommunitySubs;
            TwitchGiftedSub.Checked = Properties.Settings.Default.TwitchGiftedSub;
            TwitchSendTextCheckBox.Checked = Properties.Settings.Default.TwitchSendTextCheckBox;
            TTSAPIKeyTextBox.Text = Properties.Settings.Default.TTSAPIKeyTextBox;
            TTSRegionTextBox.Text = Properties.Settings.Default.TTSRegionTextBox;
            STTLanguageComboBox.Text = Properties.Settings.Default.STTLanguageComboBox;
            TwitchEnableCheckbox.Checked = Properties.Settings.Default.TwitchEnable;
            TwitchCheckAuthAtStartup.Checked = Properties.Settings.Default.TwitchCheckAuthAtStartup;
            TwitchReadChatCheckBox.Checked = Properties.Settings.Default.TwitchReadChatCheckBox;
            TwitchCheerCheckbox.Checked = Properties.Settings.Default.TwitchCheerCheckbox;
            TTSAPIKeyTextBox.Enabled = Properties.Settings.Default.TTSAPIKeyTextBoxEnabled;
            TTSRegionTextBox.Enabled = Properties.Settings.Default.TTSRegionTextBoxEnabled;
            //load HotkeyList into _setHotkeys
            /* foreach (String key in Properties.Settings.Default.HotkeyList)
             {
                 Keys tmpKey = (Keys)Enum.Parse(typeof(Keys), key, true);
                 _setHotkeys.Add(tmpKey);
             }*
            */
            //we need to get azure regions and voice options if azure is selected so we dont need to fill it later
            if (TTSProviderComboBox.Text == "Azure")
            {
                //fill the list if its empty
                if (TTSOutputVoice.Items.Count < 1)
                {
                    if (TTSAPIKeyTextBox.Text.Length > 0 && TTSRegionTextBox.Text.Length > 0)
                    {
                        await TTSGetAzureVoices();
                        if (_bigError)
                        {
                            return;
                        }
                        //fill the listboxes
                        TTSFillAzureVoicesList();
                    }
                }
            }
            //this last so it overwrites possibly loaded voice options
            TTSOutputVoiceOption1.Text = Properties.Settings.Default.TTSAudioVoiceOptions;
        }

        [SupportedOSPlatform("windows6.1")]
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
            Properties.Settings.Default.TTSAudioVoiceOptions = TTSOutputVoiceOption1.Text;
            Properties.Settings.Default.STTAPIKey = STTAPIKeyEditbox.Text;
            Properties.Settings.Default.STTAPIRegion = STTRegionEditbox.Text;
            Properties.Settings.Default.LLMAPIKey = LLMAPIKeyTextBox.Text;
            Properties.Settings.Default.TTSAudioVoiceEnabled = TTSOutputVoice.Enabled;
            Properties.Settings.Default.TTSAudioVoiceOptionsEnabled = TTSOutputVoiceOption1.Enabled;
            Properties.Settings.Default.STTAPIKeyEnabled = STTAPIKeyEditbox.Enabled;
            Properties.Settings.Default.STTAPIRegionEnabled = STTRegionEditbox.Enabled;
            Properties.Settings.Default.TwitchUsername = TwitchUsername.Text;
            Properties.Settings.Default.TwitchAccessToken = TwitchAccessToken.Text;
            Properties.Settings.Default.TwitchChannel = TwitchChannel.Text;
            Properties.Settings.Default.TwitchCommandTrigger = TwitchCommandTrigger.Text;
            Properties.Settings.Default.TwitchChatCommandDelay = int.Parse(TwitchChatCommandDelay.Text);
            Properties.Settings.Default.TwitchNeedsFollower = TwitchNeedsFollower.Checked;
            Properties.Settings.Default.TwitchNeedsSubscriber = TwitchNeedsSubscriber.Checked;
            Properties.Settings.Default.TwitchMinBits = int.Parse(TwitchMinBits.Text);
            Properties.Settings.Default.TwitchSubscribed = TwitchSubscribed.Checked;
            Properties.Settings.Default.TwitchCommunitySubs = TwitchCommunitySubs.Checked;
            Properties.Settings.Default.TwitchGiftedSub = TwitchGiftedSub.Checked;
            Properties.Settings.Default.TwitchSendTextCheckBox = TwitchSendTextCheckBox.Checked;
            Properties.Settings.Default.TTSAPIKeyTextBox = TTSAPIKeyTextBox.Text;
            Properties.Settings.Default.TTSRegionTextBox = TTSRegionTextBox.Text;
            Properties.Settings.Default.STTLanguageComboBox = STTLanguageComboBox.Text;
            Properties.Settings.Default.TwitchEnable = TwitchEnableCheckbox.Checked;
            Properties.Settings.Default.TwitchCheckAuthAtStartup = TwitchCheckAuthAtStartup.Checked;
            Properties.Settings.Default.TwitchReadChatCheckBox = TwitchReadChatCheckBox.Checked;
            Properties.Settings.Default.TwitchCheerCheckbox = TwitchCheerCheckbox.Checked;
            Properties.Settings.Default.TTSAPIKeyTextBoxEnabled = TTSAPIKeyTextBox.Enabled;
            Properties.Settings.Default.TTSRegionTextBoxEnabled = TTSRegionTextBox.Enabled;
            /* //add the hotkeys in settings list, not in text
             Properties.Settings.Default.HotkeyList.Clear();
             foreach (Keys key in _setHotkeys)
             {
                 Properties.Settings.Default.HotkeyList.Add(key.ToString());
             }*/
            Properties.Settings.Default.Save();

            //remove hotkey hooks
            //  Unsubscribe();
        }

        [SupportedOSPlatform("windows6.1")]
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        [SupportedOSPlatform("windows6.1")]
        public void ShowHotkeyDialogBox()
        {
            //local, but a form, so PascalCase is allowed
            HotkeyForm HotkeyDialog = new();

            var result = HotkeyDialog.ShowDialog(this);
            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (result == DialogResult.OK && HotkeyDialog.ReturnValue1 != null)
            {
                _setHotkeys.Clear();
                this.MicrophoneHotkeyEditbox.Text = "";
                List<Keys> hotKeys = HotkeyDialog.ReturnValue1;
                //ok now we got the keys, parse them and put them in the index box
                // and the global list for hotkeys

                for (var i = 0; i < hotKeys.Count; i++)
                {
                    //add to the current hotkey list for keyup event checks
                    _setHotkeys.Add(hotKeys[i]);

                    //add to the text box
                    if (i < hotKeys.Count - 1)
                        this.MicrophoneHotkeyEditbox.Text += hotKeys[i].ToString() + " + ";
                    else
                        this.MicrophoneHotkeyEditbox.Text += hotKeys[i].ToString();
                }
            }
            UpdateTextLog("Hotkey set to " + MicrophoneHotkeyEditbox.Text + "\r\n");
            _bBBlog.Info("Hotkey set to " + MicrophoneHotkeyEditbox.Text);
            HotkeyDialog.Dispose();
            //bind the new value 
            Subscribe();
        }

        [SupportedOSPlatform("windows6.1")]
        private void MicrophoneHotkeySet_Click(object sender, EventArgs e)
        {
            Unsubscribe();
            ShowHotkeyDialogBox();
        }

        [SupportedOSPlatform("windows6.1")]
        //Keyboard hooks
        public void Unsubscribe()
        {
            m_GlobalHook.KeyDown -= GlobalHookKeyDown;
            m_GlobalHook.KeyUp -= GlobalHookKeyUp;
            //It is recommened to dispose it
            m_GlobalHook.Dispose();
        }

        [SupportedOSPlatform("windows6.1")]
        public void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.KeyDown += GlobalHookKeyDown;
            m_GlobalHook.KeyUp += GlobalHookKeyUp;
        }

        [SupportedOSPlatform("windows6.1")]
        private async void HandleHotkeyButton()
        {
            if (!_hotkeyCalled)
            {
                if (MainRecordingStart.Text == "Start")
                {
                    MainRecordingStart_Click(null, null);
                    _hotkeyCalled = true;
                    //ok lets wait 1 sec before checking shit again
                    await Task.Delay(1000);
                }
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void GlobalHookKeyUp(object sender, KeyEventArgs e)
        {
            //if microphone is on (hotkeycalled = true) and of the hotkeys are in the keyup event turn off the microphone
            //current hotkeys are in _setHotkeys
            if (_hotkeyCalled)
            {
                //if one of the keys in the _setHotkeys is detected as UP, give it a second then stop recording
                //foreach (Keys key in _setHotkeys)
                if (_setHotkeys.Contains(e.KeyCode))
                {
                    MainRecordingStart_Click(null, null);
                    await Task.Delay(1000);
                    _hotkeyCalled = false;
                }
            }

        }

        [SupportedOSPlatform("windows6.1")]
        //handle the current hotkey setting
        private void GlobalHookKeyDown(object sender, KeyEventArgs e)
        {
            var map = new Dictionary<Combination, Action>
             {
                {Combination.FromString(MicrophoneHotkeyEditbox.Text),  () => HandleHotkeyButton() }
             };

            m_GlobalHook.OnCombination(map);
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TwitchTestButton_Click(object sender, EventArgs e)
        {
            //first lets make sure people cant click too often
            TwitchTestButton.Enabled = false;
            //first we check if the Authorization key is fine, using the API
            TwitchAPIESub twAPITest = new();

            //check to see if we need to send a message on join
            if (TwitchSendTextCheckBox.Checked)
            {
                twAPITest.TwitchSendTestMessageOnJoin = TwitchTestSendText.Text;
            }

            //we need the username AND channel name to get the broadcasterid which is needed for sending a message via the API
            //we need both since the username of the bot and teh channel it joins can be different.
            var VerifyOk = await twAPITest.CheckAuthCodeAPI(TwitchAccessToken.Text, TwitchUsername.Text, TwitchChannel.Text);
            if (!VerifyOk)
            {
                _bBBlog.Error("Problem verifying Access token, something is wrong with the access token!");
                TextLog.Text += "Problem verifying Access token, invalid access token\r\n";
                MessageBox.Show("Problem verifying Access token, invalid access token", "Twitch Access Token veryfication result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TwitchTestButton.Enabled = true;
                TwitchAPIStatusTextBox.Text = "DISABLED";
                TwitchAPIStatusTextBox.BackColor = Color.Red;
                //if the token is invalid, lets disable the checkboxes
                TwitchEnableCheckbox.Checked = false;
                if (TwitchCheckAuthAtStartup.Checked)
                    TwitchCheckAuthAtStartup.Checked = false;
                return;
            }
            else
            {
                _bBBlog.Info($"Twitch Access token verified success!");
                UpdateTextLog("Twitch Access token verified success!\r\n");
                MessageBox.Show($"Twitch Access token verified success!", "Twitch Access Token verification result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TwitchAPIStatusTextBox.Text = "ENABLED";
                TwitchAPIStatusTextBox.BackColor = Color.Green;
                //if the token is valid, and twitch enabled lets start up the hourly validation timer
                if (TwitchEnableCheckbox.Checked)
                    SetTwitchValidateTokenTimer();
            }

            TwitchTestButton.Enabled = true;
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TwitchAuthorizeButton_Click(object sender, EventArgs e)
        {
            //lets not block everything, but lets try get a Twitch Auth token.
            //This is done by spawning a browser where the user has to authorize (implicit grant) 
            //the application. 
            TwitchAPIESub twitchAPI = new();
            //see https://dev.twitch.tv/docs/authentication/scopes/ and https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchatmessage
            //API events:
            //channel.send.message ("user:write:chat")
            //eventsub events:
            //channel.chat.message (user:read:chat) to read chat
            //channel.subscribe (channel:read:subscriptions) to get subscription events
            //channel.subscription.gift (channel:read:subscriptions) to get gifted sub events
            //channel.subscription.message (channel:read:subscriptions) to get sub message events
            //channel.cheer (bits:read) to get information on cheered bits
            //channel.follow (moderator:read:followers) to get who followed a channel
            //channel.channel_points_automatic_reward_redemption.add (channel:read:redemptions) to get automatic reward redemptions by viewers
            //channel.channel_points_custom_reward_redemption.add (channel:read:redemptions) to get custom reward redemptions by viewers

            var twitchAPIResult = await twitchAPI.GetTwitchAuthToken([
                //API scope to send text to chat
                "user:write:chat", 
                //EventSub scopes for subscription types to read chat, get subscription events, read when people cheer (bits) and follower events
                "user:read:chat", "channel:read:subscriptions", "bits:read", "moderator:read:followers", "channel:read:redemptions"]);

            if (!twitchAPIResult)
            {
                _bBBlog.Error("Issue with getting auth token. Check logs for more information.");
                MessageBox.Show($"Issue with getting Auth token. Check logs for more information.", "Twitch Authorization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                TwitchAccessToken.Text = twitchAPI.TwitchAccessToken;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TTSProviderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TTSProviderComboBox.Text == "Native")
            {
                TTSAPIKeyTextBox.Enabled = false;
                TTSAudioOutputComboBox.Enabled = false;
                TTSOutputVoice.Enabled = false;
                TTSOutputVoiceOption1.Enabled = false;
                TTSRegionTextBox.Enabled = false;

            }
            else if (TTSProviderComboBox.Text == "Azure")
            {
                TTSAPIKeyTextBox.Enabled = true;
                TTSAudioOutputComboBox.Enabled = true;
                TTSOutputVoice.Enabled = true;
                TTSOutputVoiceOption1.Enabled = true;
                TTSRegionTextBox.Enabled = true;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        //if we change the region box, lets make sure we till have the right voices
        private async void TTSRegionTextBox_Leave(object sender, EventArgs e)
        {
            _bBBlog.Info("Region edit box exited");
            if (TTSAPIKeyTextBox.Text.Length > 0 && TTSRegionTextBox.Text.Length > 0)
            {
                await TTSGetAzureVoices();
                if (_bigError)
                {
                    return;
                }
                //fill the listboxes
                TTSFillAzureVoicesList();
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TTSOutputVoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            //depending on what voice is selected we need to now select the voice options (if any)
            TTSAzureFillOptions(TTSOutputVoice.Text);
        }

        [SupportedOSPlatform("windows6.1")]
        private void SoundInputDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            _bBBlog.Info("Selected input device changed to " + SoundInputDevices.Text);
        }

        [SupportedOSPlatform("windows6.1")]
        private void GithubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //spawn browser for github link
            var t = new Thread(() => Process.Start("https://github.com/WhiskerWeirdo/BanterBrain-Buddy"));
            t.Start();
            Thread.Sleep(100);
        }

        [SupportedOSPlatform("windows6.1")]
        private void DiscordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //spawn browser for discord link
            var t = new Thread(() => Process.Start("https://discord.banterbrain.tv"));
            t.Start();
            Thread.Sleep(100);
        }


        [SupportedOSPlatform("windows6.1")]
        private void TwitchCheckAuthAtStartup_Click(object sender, EventArgs e)
        {
            _bBBlog.Debug("Twitch check auth at startup changed to " + TwitchCheckAuthAtStartup.Checked);
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchEnableCheckbox_Click(object sender, EventArgs e)
        {
            //if the checkbox is checked, lets enable the timer to check the token every hour
            //and start the eventsub server
            //TODO: only allow this after both API and EventSub are tested and working
            if (TwitchEnableCheckbox.Checked)
            {
                SetTwitchValidateTokenTimer();
                //allow for it to be automatic at startup
                TwitchCheckAuthAtStartup.Enabled = true;
            }
            else
            { //turning off Twitch
                _bBBlog.Info("Twitch disabled. Stopping timer and clearing token");
                if (_globalTwitchAPI != null)
                {
                    _globalTwitchAPI.StopHourlyAccessTokenCheck();
                    _globalTwitchAPI = null;
                    TwitchAPIStatusTextBox.Text = "DISABLED";
                }
            }
            _bBBlog.Debug("Twitch enable checkbox changed to " + TwitchEnableCheckbox.Checked);
        }

        [SupportedOSPlatform("windows6.1")]
        private async Task<bool> EventSubStartWebsocketClient()
        {
            TwitchAPIESub twitchEventSub = new();
            bool eventSubStart = false;
            //we should set here what eventhandlers we want to have enabled based on the twitch Settings

            if (await twitchEventSub.EventSubInit(TwitchAccessToken.Text, TwitchUsername.Text, TwitchChannel.Text))
            {
                //we need to first set the event handlers we want to use
                //do we want to check cheers?
                if (TwitchCheerCheckbox.Checked)
                    twitchEventSub.EventSubHandleCheer(int.Parse(TwitchMinBits.Text));

                //do we want to check chat messages?
                if (TwitchReadChatCheckBox.Checked)
                {
                    twitchEventSub.EventSubHandleReadchat(TwitchCommandTrigger.Text, int.Parse(TwitchChatCommandDelay.Text), TwitchNeedsFollower.Checked, TwitchNeedsSubscriber.Checked);
                    //set local eventhanlder for valid chat messages to trigger the bot
                    twitchEventSub.OnEsubChatMessage += TwitchEventSub_OnEsubChatMessage;
                }
                //now we can connect the client to the server
                eventSubStart = await twitchEventSub.EventSubStartAsync();

                if (eventSubStart)
                {
                    _bBBlog.Info("Twitch EventSub server started successfully");
                    TextLog.AppendText("Twitch EventSub server started successfully\r\n");
                    TwitchEventSubStatusTextBox.Text = "ENABLED";
                    TwitchEventSubStatusTextBox.BackColor = Color.Green;
                }
                return true;
            }
            else
            {
                _bBBlog.Error("Issue with starting Twitch EventSub server. Check logs for more information.");
                TwitchEventSubStatusTextBox.Text = "DISABLED";
                TwitchEventSubStatusTextBox.BackColor = Color.Red;
                return false;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void EventSubTest_Click(object sender, EventArgs e)
        {
            //This only works once API access-token is verified
            if (TwitchAPIStatusTextBox.Text.ToLower() != "enabled")
            {
                MessageBox.Show("You need to verify the API key first.", "Twitch EventSub error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ( await EventSubStartWebsocketClient() )
            {
                MessageBox.Show("EventSub server started successfully so all is well!", "Twitch EventSub success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else
            {
                MessageBox.Show("Issue with starting EventSub server. Check logs for more information.", "Twitch EventSub error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [SupportedOSPlatform("windows6.1")]
        /// <summary>
        /// To write to TextLog irregardless of thread
        public void UpdateTextLog (string TextToAppend)
        {
            if (!InvokeRequired)
            {
                TextLog.AppendText(TextToAppend);
            }
            else
            {
                Invoke(new Action(() =>
                {
                    TextLog.AppendText(TextToAppend);
                }));
            }     
        }

        //A simple way to invoke UI elements from another thread
        [SupportedOSPlatform("windows6.1")]
        private async Task InvokeUI(Action a)
        {
            this.BeginInvoke(new MethodInvoker(a));
        }

        [SupportedOSPlatform("windows6.1")]
        //eventhandler for valid chat messages trigger
        private async void TwitchEventSub_OnEsubChatMessage(object sender, TwitchEventhandlers.OnChatEventArgs e)
        {

            string message = e.GetChatInfo()[1].Replace(TwitchCommandTrigger.Text, "");
            string user = e.GetChatInfo()[0];
            //we got a valid chat message, lets see what we can do with it
            _bBBlog.Info("Valid Twitch Chat message received from user: " + user + " message: " + message);
            await InvokeUI(async () => {
                TextLog.AppendText("Valid Twitch Chat message received from user: " + user + " message: " + message + "\r\n");
                await SayText(user + " said " + message);
            });
            await InvokeUI(async () => {
                await TalkToLLM("respond to " + user + ", who said: " + message);
            });
            //we have to await the GPT response, due to running this from another thread await alone is not enough.
            while (!_gPTDone)
            {
                await Task.Delay(500);
            }
            //ok we waited, lets say the response, but we need a small delay to not sound unnatural      
            await InvokeUI(async () => {
                await Task.Delay(3000);
                await SayText(LLMTestOutputbox.Text);
            });

        }


        [SupportedOSPlatform("windows6.1")]
        private void TwitchReadChatCheckBox_Click(object sender, EventArgs e)
        {
            if (TwitchReadChatCheckBox.Checked)
            {
                _bBBlog.Info("This enables reading chat messages to watch for a command, in busy channels this will cause significant load on your computer");
                MessageBox.Show("Reading chat creates a high load on busy channels. Be warned!", "Twitch Channel messages enabled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                _bBBlog.Info("Twitch read chat unchecked");
            }
        }
    }
}