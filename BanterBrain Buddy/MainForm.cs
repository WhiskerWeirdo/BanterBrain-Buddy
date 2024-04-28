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
using System.Reflection;
using Newtonsoft.Json;

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
        // check if STT is finished yet
        private bool _sTTDone = false;
        //check if TTS is finished yet
        private bool _tTSSpeaking = false;

        [SupportedOSPlatform("windows6.1")]
        //Hotkey Storage
        readonly private List<Keys> _setHotkeys = [];

        //check if the GPT LLM is donestop audio capture
        private bool _gPTDone = false;

        //error checker for async events. If true, stop execution of whatever you're doing
        private bool _bigError = false;

        private bool _twitchStarted = false;
        private bool _twitchAPIVerified = false;

        //Global Twitch API class
        //we need this for the hourly /validate check
        private TwitchAPIESub _globalTwitchAPI;
        private bool _twitchValidateCheckStarted;
        private TwitchAPIESub _twitchEventSub;

        //this will hold the STT output text
        private string _sTTOutputText;
        private string _gPTOutputText;

        private List<Personas> _personas = new List<Personas>();

        [SupportedOSPlatform("windows6.1")]
        public BBB()
        {
            _twitchValidateCheckStarted = false;

            InitializeComponent();
            LoadPersonas();

            _bBBlog.Info("Program Starting...");
            // _bBBlog.Info("PPT hotkey: " + MicrophoneHotkeyEditbox.Text);

            UpdateTextLog("Program Starting...\r\n");
            //UpdateTextLog("PPT hotkey: " + MicrophoneHotkeyEditbox.Text + "\r\n");
            //TODO: verify API token first

            if (TwitchEnableCheckbox.Checked && Properties.Settings.Default.TwitchAccessToken.Length > 1)
                SetTwitchValidateTokenTimer(true);
            CheckConfiguredSTTProviders();
            LoadSettings();
            SetSelectedSTTProvider();
        }

        //we need to do this so we fill the default saved value only after the API voices are checked
        [SupportedOSPlatform("windows6.1")]
        private async void SetSelectedSTTProvider()
        {
            //TODO: check if the selected provider is still valid
            await Task.Delay(1000);
            try
            {
                STTSelectedComboBox.SelectedIndex = STTSelectedComboBox.FindStringExact(Properties.Settings.Default.STTSelectedProvider);
            }
            catch (Exception ex)
            {
                _bBBlog.Error("Error setting STT provider: " + ex.Message);
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private Personas GetSelectedPersona(string personaName)
        {
            Personas tmpPersona = new();
            foreach (var persona in _personas)
            {
                if (persona.Name == BroadcasterSelectedPersonaComboBox.Text)
                {
                    _bBBlog.Debug("Setting selected persona: " + persona.Name);
                    //_broadcasterSelectedPersona = persona;
                    return persona;
                }
            }
            return null;
        }

        //this loads the personas from the personas.json file
        //if one doesn't exist, it creates a default one
        [SupportedOSPlatform("windows6.1")]
        private async void LoadPersonas()
        {

            var tmpFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\personas.json";
            if (!File.Exists(tmpFile))
            {
                _bBBlog.Debug("Personas file not found, creating it");
                //there might be a native voice installed, if so we should add it to the list
                NativeSpeech nativeSpeech = new();
                var nativeRegionVoicesList = await nativeSpeech.TTSNativeGetVoices();
                string tmpNativeVoice = null;
                if (nativeRegionVoicesList.Count > 0)
                {
                    _bBBlog.Debug("Native voices found, adding them to the list");

                    foreach (var voice in nativeRegionVoicesList)
                    {
                        _bBBlog.Debug("Adding voice: " + voice);
                        tmpNativeVoice = voice.Name + "-" + voice.Gender + "-" + voice.Culture;
                        break;
                    }
                }
                else
                {
                    _bBBlog.Debug("No native voices found");
                    tmpNativeVoice = "None";
                }
                var newPersonas = new List<Personas>();
                newPersonas.Add(new Personas { Name = "Default", RoleText = "You are a cheeky streamer assistant with a silly personality.", VoiceProvider = "Native", VoiceName = tmpNativeVoice, VoiceOptions = new List<string>() });
                string tmpString = JsonConvert.SerializeObject(newPersonas);
                using (var sw = new StreamWriter(tmpFile, true))
                {
                    sw.Write(tmpString);
                }
            }
            else
            {
                _bBBlog.Debug("Personas file found, loading it.");
                _personas.Clear();
                BroadcasterSelectedPersonaComboBox.Items.Clear();
                TwitchChatPersonaComboBox.Items.Clear();
                TwitchChannelPointPersonaComboBox.Items.Clear();
                TwitchCheeringPersonaComboBox.Items.Clear();
                TwitchSubscriptionPersonaComboBox.Items.Clear();
                using (var sr = new StreamReader(tmpFile))
                {
                    var tmpString = sr.ReadToEnd();
                    //var tmpPersonas = JsonConvert.DeserializeObject<List<Personas>>(tmpString);
                    var tmpPersonas = JsonConvert.DeserializeObject<List<Personas>>(tmpString);
                    foreach (var persona in tmpPersonas)
                    {
                        _bBBlog.Debug("Loading persona into available list: " + persona.Name);
                        _personas.Add(persona);
                        BroadcasterSelectedPersonaComboBox.Items.Add(persona.Name);
                        TwitchChatPersonaComboBox.Items.Add(persona.Name);
                        TwitchChannelPointPersonaComboBox.Items.Add(persona.Name);
                        TwitchCheeringPersonaComboBox.Items.Add(persona.Name);
                        TwitchSubscriptionPersonaComboBox.Items.Add(persona.Name);

                    }
                }
            }
            //alright now lets load up the selected persona if there is one

        }

        /// <summary>
        /// We check the available STT providers and add them to the list for the broadcater selection list
        /// </summary>
        [SupportedOSPlatform("windows6.1")]
        private async void CheckConfiguredSTTProviders()
        {

            //is there already one from loadsettings?
            var tmpProvider = STTSelectedComboBox.Text;
            if (tmpProvider.Length > 0)
            {
                _bBBlog.Debug("STT provider already set: " + tmpProvider);
                return;
            }
            //if there's at least one native voice installed, enable the native STT
            NativeSpeech nativeSpeech = new();
            var result = await nativeSpeech.TTSNativeGetVoices();
            _bBBlog.DebugFormat("Found {0} native voices", result.Count);
            if (result.Count > 0)
            {
                STTSelectedComboBox.Items.Add("Native");
            }

            if (Properties.Settings.Default.AzureAPIKeyTextBox.Length > 1)
            {
                List<AzureVoices> azureRegionVoicesList = [];
                _bBBlog.Info("Finding TTS Azure voices available");
                AzureSpeechAPI AzureSpeech = new(Properties.Settings.Default.AzureAPIKeyTextBox, Properties.Settings.Default.AzureRegionTextBox, Properties.Settings.Default.AzureLanguageComboBox);
                azureRegionVoicesList = await AzureSpeech.TTSGetAzureVoices();
                if (azureRegionVoicesList.Count > 0)
                {
                    STTSelectedComboBox.Items.Add("Azure");
                    _bBBlog.Info("Azure voices found, adding to list");
                    TextLog.AppendText("Azure API setting valid.\r\n");
                }
                else
                {
                    _bBBlog.Error("No Azure voices found, despite what seems to be an API key");
                }
            }
        }

        /// <summary>
        /// Twitch requires you to validate your access token every hour. This starts this timer when Twitch is enabled.
        /// </summary>
        [SupportedOSPlatform("windows6.1")]
        /// <summary>
        public async void SetTwitchValidateTokenTimer(bool StartEventSubClient)
        {
            if (!_twitchValidateCheckStarted && TwitchEnableCheckbox.Checked && Properties.Settings.Default.TwitchUsername.Length > 0 && Properties.Settings.Default.TwitchAccessToken.Length > 0 && Properties.Settings.Default.TwitchChannel.Length > 0)
            {
                //only make a new instance if it's null
                if (_globalTwitchAPI == null)
                {
                    _globalTwitchAPI = new();
                }

                var result = await _globalTwitchAPI.ValidateAccessToken(Properties.Settings.Default.TwitchAccessToken);
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

                    //only start if StartEventSubClient = true
                    //ok so all is good, lets start the eventsub client
                    if (StartEventSubClient)
                    {
                        _bBBlog.Info("Starting EventSub client");
                        await EventSubStartWebsocketClient();
                        //if we are good, start the hourly check
                        await _globalTwitchAPI.CheckHourlyAccessToken();
                    }
                    else if (!StartEventSubClient)
                    {
                        _bBBlog.Info("We are in Test. EventSub client not started");
                        await _globalTwitchAPI.CheckHourlyAccessToken();
                    }
                    else
                    {
                        _bBBlog.Error("Error starting EventSub client");
                        MessageBox.Show("Error starting EventSub client", "Twitch EventSub error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TextLog.Text += "Error starting EventSub client\r\n";
                        _bigError = true;
                    }
                }
            }
        }

        /*
        [SupportedOSPlatform("windows6.1")]
        private async void STTTestButton_Click(object sender, EventArgs e)
        {

            String selectedProvider = STTProviderBox.GetItemText(STTProviderBox.SelectedItem);
            if (STTTestButton.Text == "Test")
            {
                _sTTOutputText = "";
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
                        _sTTOutputText = "Error! API Key or region cannot be empty!";
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
        */


        [SupportedOSPlatform("windows6.1")]
        //this sets "SelectedInputDevice" to the correct input/microphone
        private void SetSelectedInputDevice()
        {
            var devices = MMDeviceEnumerator.EnumerateDevices(DataFlow.Capture, DeviceState.Active);
            foreach (var device in devices)
            {
                if (device.FriendlyName == Properties.Settings.Default.VoiceInput)
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
            _sTTOutputText = "";
            AzureSpeechAPI azureSpeechAPI = new(Properties.Settings.Default.AzureAPIKeyTextBox, Properties.Settings.Default.AzureRegionTextBox, Properties.Settings.Default.AzureLanguageComboBox);
            //call the Azure STT function with the selected input device
            //first initialize the Azure STT class
            azureSpeechAPI.AzureSTTInit(Properties.Settings.Default.VoiceInput);
            _bBBlog.Info("Azure STT microphone start.");
            while (MainRecordingStart.Text == "Recording" && !_sTTDone && !_bigError)
            {
                var recognizeResult = await azureSpeechAPI.RecognizeSpeechAsync();
                if (recognizeResult == "NOMATCH")
                {
                    _sTTOutputText += "NOMATCH: Speech could not be recognized.\r\n";
                }
                else if (recognizeResult == null)
                {
                    _sTTOutputText += "Fail! Speech could not be proccessed. Check log for more info.\r\n";
                    TextLog.Text += "Azure Speech-To-Text: Fail! Speech could not be proccessed. Check log for more info.\r\n";
                    _bigError = true;
                    _sTTDone = true;
                }
                else
                {
                    _sTTOutputText += recognizeResult + "\r\n";
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
            _sTTOutputText = "";
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
                _sTTOutputText += e.Result.Text + "\r\n";
                //STTTestOutput.AppendText(e.Result.Text + "\r\n");
            }
            else
            {
                //STTTestOutput.AppendText("Native recognized text not available.");
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
        private async Task TalkToOpenAIGPT(String UserInput, string tmpPersonaRoletext)
        {
            _gPTOutputText = "";
            UpdateTextLog("Sending to GPT: " + UserInput + "\r\n");
            _bBBlog.Info("Sending to GPT: " + UserInput);
            _gPTDone = false;
            OpenAIAPI api = new(Properties.Settings.Default.GPTAPIKey);
            //api.Auth.ValidateAPIKey
            var chat = api.Chat.CreateConversation();
            chat.Model = Model.ChatGPTTurbo;
            chat.RequestParameters.Temperature = Properties.Settings.Default.GPTTemperature;
            chat.RequestParameters.MaxTokens = Properties.Settings.Default.GPTMaxTokens;

            //mood is setting the system text description
            //this is the persona role
            // UpdateTextLog("SystemRole: " + LLMRoleTextBox.Text + "\r\n");
            _bBBlog.Info("SystemRole: " + tmpPersonaRoletext);
            chat.AppendSystemMessage(tmpPersonaRoletext);

            chat.AppendUserInput(UserInput);
            try
            {
                UpdateTextLog("ChatGPT response: ");
                _bBBlog.Info("ChatGPT response: ");
                await chat.StreamResponseFromChatbotAsync(res =>
                 {
                     UpdateTextLog(res);
                     _gPTOutputText += res;
                 });
                _bBBlog.Info(_gPTOutputText);
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

        //output to the selected audio device
        private void OutputStream(MemoryStream stream)
        {
            int deviceID = 0;
            foreach (var device in WaveOutDevice.EnumerateDevices())
            {
                if (device.Name == Properties.Settings.Default.TTSAudioOutput)
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


        [SupportedOSPlatform("windows6.1")]
        //Azure Text-To-Speach
        private async Task TTSAzureSpeakToOutput(string TextToSpeak, Personas tmpPersona)
        {
            _bBBlog.Info("Azure TTS called with text, seting up");
            AzureSpeechAPI azureSpeechAPI = new(Properties.Settings.Default.AzureAPIKeyTextBox, Properties.Settings.Default.AzureRegionTextBox, Properties.Settings.Default.AzureLanguageComboBox);
            //STTAPIKeyEditbox.Text, STTRegionEditbox.Text, STTLanguageComboBox.Text);

            //set the output voice, gender and locale, and the style
            //this now depends on the selected persona
            await azureSpeechAPI.AzureTTSInit(tmpPersona.VoiceName, tmpPersona.VoiceOptions[0], Properties.Settings.Default.TTSAudioOutput);

            var result = await azureSpeechAPI.AzureSpeak(TextToSpeak);
            if (!result)
            {
                _bBBlog.Error("Azure TTS error. Is there a problem with your API key or subscription?");
                MessageBox.Show("Azure TTS error. Is there a problem with your API key or subscription?", "Azure TTS error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _bigError = true;
            }
        }


        [SupportedOSPlatform("windows6.1")]
        private async Task TTSNativeSpeakToOutput(String TTSText, Personas tmpPersona)
        {
            UpdateTextLog("Saying text with Native TTS\r\n");
            _bBBlog.Info("Saying text with Native TTS");
            NativeSpeech nativeSpeech = new();
            //this now depends on the selected persona
            await nativeSpeech.NativeTTSInit(tmpPersona.VoiceName, Properties.Settings.Default.TTSAudioOutput);
            await nativeSpeech.NativeSpeak(TTSText);
        }

        [SupportedOSPlatform("windows6.1")]
        //this is the generic caller for TTS function that makes sure the TTS is done before continuing
        //we make this public so we can call it to test
        private async Task SayText(string TextToSay, int DelayWhenDone, Personas tmpPersona)
        {
            while (_tTSSpeaking)
            {
                await Task.Delay(500);
            }
            await DoSayText(TextToSay, DelayWhenDone, tmpPersona);
        }
        //agnostic TTS function
        [SupportedOSPlatform("windows6.1")]
        private async Task DoSayText(string TextToSay, int DelayWhenDone, Personas tmpPersona)
        {
            //this depends on the selected TTS provider from the persona
            _tTSSpeaking = true;
            if (tmpPersona.VoiceProvider == "Native")
            {
                await TTSNativeSpeakToOutput(TextToSay, tmpPersona);
            }
            else if (tmpPersona.VoiceProvider == "Azure")
            {
                await TTSAzureSpeakToOutput(TextToSay, tmpPersona);
            }
            await Task.Delay(DelayWhenDone);
            _tTSSpeaking = false;
        }


        //talk to the various LLM's
        [SupportedOSPlatform("windows6.1")]
        private async Task TalkToLLM(string TextToPass, string tmpPersonaRoleText)
        {
            _gPTOutputText = "";
            _gPTDone = false;
            if (Properties.Settings.Default.SelectedLLM == "GPT")
            {
                UpdateTextLog("Using ChatGPT\r\n");
                _bBBlog.Info("Using ChatGPT");
                await TalkToOpenAIGPT(TextToPass, tmpPersonaRoleText);
            }
            //lets wait for GPT to be done
            while (!_gPTDone)
            {
                await Task.Delay(500);
            }
        }

        //main function for recording text, passing it to STT, then to GPT, then to TTS
        [SupportedOSPlatform("windows6.1")]
        private async void MainRecordingStart_Click(object sender, EventArgs e)
        {
            _bigError = false;

            //selected STT provider
            String selectedProvider = STTSelectedComboBox.Text;
            //first, lets call STT
            _sTTDone = false;
            if (MainRecordingStart.Text == "Start")
            {
                _sTTOutputText = "";
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

                    if ((Properties.Settings.Default.AzureAPIKeyTextBox.Length < 1) || (Properties.Settings.Default.AzureRegionTextBox.Length < 1))
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

                //now the STT text is in _sTTOutputText, lets pass that to ChatGPT
                if (_sTTOutputText.Length > 1)
                {
                    await TalkToLLM(_sTTOutputText, GetSelectedPersona(BroadcasterSelectedPersonaComboBox.Text).RoleText);

                    //this depends on the selected persona now
                    //var tmpPersona = GetSelectedPersona(BroadcasterSelectedPersonaComboBox.Text);
                    await SayText(_gPTOutputText, 0, GetSelectedPersona(BroadcasterSelectedPersonaComboBox.Text));

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
            TwitchCommandTrigger.Text = Properties.Settings.Default.TwitchCommandTrigger;
            TwitchChatCommandDelay.Text = Properties.Settings.Default.TwitchChatCommandDelay.ToString();
            TwitchNeedsSubscriber.Checked = Properties.Settings.Default.TwitchNeedsSubscriber;
            TwitchMinBits.Text = Properties.Settings.Default.TwitchMinBits.ToString();
            TwitchSubscribed.Checked = Properties.Settings.Default.TwitchSubscribed;
            TwitchGiftedSub.Checked = Properties.Settings.Default.TwitchGiftedSub;
            TwitchAutoStart.Checked = Properties.Settings.Default.TwitchAutoStart;
            //todo: change to button
            TwitchEnableCheckbox.Checked = Properties.Settings.Default.TwitchEnable;
            //setting the Twitch items enabled/disabled based on the checkbox
            if (Properties.Settings.Default.TwitchEnable)
            {
                TwitchStartButton.Enabled = true;
                TwitchTriggerSettings.Enabled = true;
                TwitchCheerSettings.Enabled = true;
                TwitchSubscriberSettings.Enabled = true;
                TwitchChannelPointsSettings.Enabled = true;
                TwitchAutoStart.Enabled = true;
            }
            else
            {
                TwitchStartButton.Enabled = false;
                TwitchTriggerSettings.Enabled = false;
                TwitchCheerSettings.Enabled = false;
                TwitchSubscriberSettings.Enabled = false;
                TwitchChannelPointsSettings.Enabled = false;
                TwitchAutoStart.Enabled = false;
            }

            TwitchReadChatCheckBox.Checked = Properties.Settings.Default.TwitchReadChatCheckBox;
            TwitchCheerCheckBox.Checked = Properties.Settings.Default.TwitchCheerCheckbox;
            TwitchCustomRewardName.Text = Properties.Settings.Default.TwitchCustomRewardName;
            TwitchChannelPointCheckBox.Checked = Properties.Settings.Default.TwitchChannelPointCheckBox;
            STTSelectedComboBox.SelectedIndex = STTSelectedComboBox.FindStringExact(Properties.Settings.Default.STTSelectedProvider);
            BroadcasterSelectedPersonaComboBox.SelectedIndex = BroadcasterSelectedPersonaComboBox.FindStringExact(Properties.Settings.Default.MainSelectedPersona);
            TwitchCheeringPersonaComboBox.SelectedIndex = TwitchCheeringPersonaComboBox.FindStringExact(Properties.Settings.Default.TwitchCheeringPersona);
            TwitchChannelPointPersonaComboBox.SelectedIndex = TwitchChannelPointPersonaComboBox.FindStringExact(Properties.Settings.Default.TwitchChannelPointPersona);
            TwitchSubscriptionPersonaComboBox.SelectedIndex = TwitchSubscriptionPersonaComboBox.FindStringExact(Properties.Settings.Default.TwitchSubscriptionPersona);
            TwitchChatPersonaComboBox.SelectedIndex = TwitchChatPersonaComboBox.FindStringExact(Properties.Settings.Default.TwitchChatPersona);


            //load HotkeyList into _setHotkeys
            /* foreach (String key in Properties.Settings.Default.HotkeyList)
             {
                 Keys tmpKey = (Keys)Enum.Parse(typeof(Keys), key, true);
                 _setHotkeys.Add(tmpKey);
             }*/

            //lets check if the selected API key for GPT is valid   
            if (Properties.Settings.Default.SelectedLLM == "GPT")
            {
                OpenAIAPI api = new(Properties.Settings.Default.GPTAPIKey);
                if (await api.Auth.ValidateAPIKey())
                {
                    _bBBlog.Info("GPT API key is valid");
                    TextLog.AppendText("OpenAI ChatGPT is selected asl LLM and key is valid.\r\n");
                    _twitchAPIVerified = true;
                    TwitchStartButton.Enabled = true;
                }
                else
                {
                    _bBBlog.Error("GPT API is selected but key is invalid invalid. \r\n");
                    TextLog.AppendText("OpenAI ChatGPT is selected as LLM but key is invalid. \r\n");
                    _twitchAPIVerified = false;
                    TwitchStartButton.Enabled = false;
                }

            }
            else if (Properties.Settings.Default.SelectedLLM == "None" || Properties.Settings.Default.SelectedLLM == "")
            {
                TextLog.AppendText("No LLM selected. You should set one in the settings first\r\n");
            }

            if (TwitchAutoStart.Checked)
            {
                TwitchStartButton_Click(null, null);
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void BBB_FormClosing(object sender, FormClosingEventArgs e)
        {
            //turn off potentionally running Twitch stuff
            if (_globalTwitchAPI != null)
                await _globalTwitchAPI.EventSubStopAsync();

            if (_twitchEventSub != null)
                await _twitchEventSub.EventSubStopAsync();

            Properties.Settings.Default.TwitchCommandTrigger = TwitchCommandTrigger.Text;
            Properties.Settings.Default.TwitchChatCommandDelay = int.Parse(TwitchChatCommandDelay.Text);
            Properties.Settings.Default.TwitchNeedsSubscriber = TwitchNeedsSubscriber.Checked;
            Properties.Settings.Default.TwitchMinBits = int.Parse(TwitchMinBits.Text);
            Properties.Settings.Default.TwitchSubscribed = TwitchSubscribed.Checked;
            Properties.Settings.Default.TwitchGiftedSub = TwitchGiftedSub.Checked;
            Properties.Settings.Default.TwitchEnable = TwitchEnableCheckbox.Checked;
            Properties.Settings.Default.TwitchReadChatCheckBox = TwitchReadChatCheckBox.Checked;
            Properties.Settings.Default.TwitchCheerCheckbox = TwitchCheerCheckBox.Checked;
            Properties.Settings.Default.TwitchCustomRewardName = TwitchCustomRewardName.Text;
            Properties.Settings.Default.TwitchChannelPointCheckBox = TwitchChannelPointCheckBox.Checked;
            Properties.Settings.Default.STTSelectedProvider = STTSelectedComboBox.Text;
            Properties.Settings.Default.MainSelectedPersona = BroadcasterSelectedPersonaComboBox.Text;
            Properties.Settings.Default.TwitchChannelPointPersona = TwitchChannelPointPersonaComboBox.Text;
            Properties.Settings.Default.TwitchCheeringPersona = TwitchCheeringPersonaComboBox.Text;
            Properties.Settings.Default.TwitchSubscriptionPersona = TwitchSubscriptionPersonaComboBox.Text;
            Properties.Settings.Default.TwitchChatPersona = TwitchChatPersonaComboBox.Text;
            Properties.Settings.Default.TwitchAutoStart = TwitchAutoStart.Checked;
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
                //this.MicrophoneHotkeyEditbox.Text = "";
                List<Keys> hotKeys = HotkeyDialog.ReturnValue1;
                //ok now we got the keys, parse them and put them in the index box
                // and the global list for hotkeys

                for (var i = 0; i < hotKeys.Count; i++)
                {
                    //add to the current hotkey list for keyup event checks
                    _setHotkeys.Add(hotKeys[i]);

                    //add to the text box
                    /*
                    if (i < hotKeys.Count - 1)
                        this.MicrophoneHotkeyEditbox.Text += hotKeys[i].ToString() + " + ";
                    else
                        this.MicrophoneHotkeyEditbox.Text += hotKeys[i].ToString();
                    */
                }
            }
            //  UpdateTextLog("Hotkey set to " + MicrophoneHotkeyEditbox.Text + "\r\n");
            //   _bBBlog.Info("Hotkey set to " + MicrophoneHotkeyEditbox.Text);
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
            /*
            var map = new Dictionary<Combination, Action>
             {
                {Combination.FromString(MicrophoneHotkeyEditbox.Text),  () => HandleHotkeyButton() }
             };

            m_GlobalHook.OnCombination(map);
            */
        }

        [SupportedOSPlatform("windows6.1")]
        private void GithubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //spawn browser for github link
            var t = new Thread(() => Process.Start(new ProcessStartInfo("https://github.com/WhiskerWeirdo/BanterBrain-Buddy") { UseShellExecute = true }));
            t.Start();
            Thread.Sleep(100);
        }

        [SupportedOSPlatform("windows6.1")]
        private void DiscordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //spawn browser for discord link
            var t = new Thread(() => Process.Start(new ProcessStartInfo("https://discord.banterbrain.tv") { UseShellExecute = true }));
            t.Start();
            Thread.Sleep(100);
        }


        [SupportedOSPlatform("windows6.1")]
        private async void TwitchEnableCheckbox_Click(object sender, EventArgs e)
        {
            //this turns off and on all twitch options


            _bBBlog.Debug("Twitch enable checkbox changed to " + TwitchEnableCheckbox.Checked);
        }

        //here we star the main websocket client for Twitch EventSub
        [SupportedOSPlatform("windows6.1")]
        private async Task<bool> EventSubStartWebsocketClient()
        {
            _twitchEventSub = new();
            bool eventSubStart = false;
            //we should set here what eventhandlers we want to have enabled based on the twitch Settings

            if (await _twitchEventSub.EventSubInit(Properties.Settings.Default.TwitchAccessToken, Properties.Settings.Default.TwitchUsername, Properties.Settings.Default.TwitchUsername))
            {
                //we need to first set the event handlers we want to use

                //do we want to check chat messages?
                if (TwitchReadChatCheckBox.Checked)
                {
                    _bBBlog.Info("Twitch read chat enabled, calling eventsubhandlereadchat");
                    _twitchEventSub.EventSubHandleReadchat(TwitchCommandTrigger.Text, int.Parse(TwitchChatCommandDelay.Text), false, TwitchNeedsSubscriber.Checked);
                    //set local eventhanlder for valid chat messages to trigger the bot
                    _twitchEventSub.OnESubChatMessage += TwitchEventSub_OnESubChatMessage;
                }

                //do we want to check cheer messages?
                if (TwitchCheerCheckBox.Checked)
                {
                    _bBBlog.Info("Twitch cheers enabled, calling EventSubHandleCheer with the min amount of bits needed to trigger");
                    _twitchEventSub.EventSubHandleCheer(int.Parse(TwitchMinBits.Text));
                    _twitchEventSub.OnESubCheerMessage += TwitchEventSub_OnESubCheerMessage;
                }

                //do we want to check for subscription events?
                if (TwitchSubscribed.Checked)
                {
                    //new subs
                    _bBBlog.Info($"Twitch subscriptions enabled, calling EventSubHandleSubscription: {_twitchEventSub.ToString()}");
                    _twitchEventSub.EventSubHandleSubscription();
                    _twitchEventSub.OnESubSubscribe += TwitchEventSub_OnESubSubscribe;
                    _twitchEventSub.OnESubReSubscribe += TwitchEventSub_OnESubReSubscribe;
                    //todo set eventhandler being thrown when a new sub is detected or resub
                }
                //TODO: gifted subs TwitchGiftedSub.Checked
                if (TwitchGiftedSub.Checked)
                {
                    _bBBlog.Info("Twitch gifted subs enabled, calling EventSubHandleGiftedSubs");
                    _twitchEventSub.EventSubHandleSubscriptionGift();
                    _twitchEventSub.OnESubSubscriptionGift += TwitchEventSub_OnESubGiftedSub;
                }

                //do we want to check for channel point redemptions?
                if (TwitchChannelPointCheckBox.Checked)
                {
                    _bBBlog.Info("Twitch channel points enabled, calling EventSubHandleChannelPoints");
                    _twitchEventSub.EventSubHandleChannelPointRedemption(TwitchCustomRewardName.Text);
                    _twitchEventSub.OnESubChannelPointRedemption += TwitchEventSub_OnESubChannelPointRedemption;
                }

                eventSubStart = await _twitchEventSub.EventSubStartAsync();

                if (eventSubStart)
                {
                    _bBBlog.Info("Twitch EventSub client  started successfully");
                    TextLog.AppendText("Twitch EventSub client started successfully\r\n");
                    TwitchEventSubStatusTextBox.Text = "ENABLED";
                    TwitchEventSubStatusTextBox.BackColor = Color.Green;
                    _twitchStarted = true;
                }
                else
                {
                    _bBBlog.Error("Issue with starting Twitch EventSub server. Check logs for more information.");
                    TwitchEventSubStatusTextBox.Text = "DISABLED";
                    TwitchEventSubStatusTextBox.BackColor = Color.Red;
                    _twitchStarted = false;
                    return false;
                }
                return true;
            }
            else
            {
                _bBBlog.Error("Issue with starting Twitch EventSub server. Check logs for more information.");
                TwitchEventSubStatusTextBox.Text = "DISABLED";
                TwitchEventSubStatusTextBox.BackColor = Color.Red;
                _twitchStarted = false;
                return false;
            }
        }


        [SupportedOSPlatform("windows6.1")]
        /// <summary>
        /// To write to TextLog irregardless of thread
        public void UpdateTextLog(string TextToAppend)
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
            this.BeginInvoke(new System.Windows.Forms.MethodInvoker(a));
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TwitchEventSub_OnESubCheerMessage(object sender, TwitchEventhandlers.OnCheerEventsArgs e)
        {

            string user = e.GetCheerInfo()[0];
            string message = e.GetCheerInfo()[1];
            //we got a valid cheer message, lets see what we can do with it
            _bBBlog.Info("Valid Twitch Cheer message received");
            bool _TalkDone = false;
            await InvokeUI(async () =>
            {
                TextLog.AppendText($"Valid Twitch Cheer message received from {user}\r\n");
                // await SayText($"Thank you for the bits, {user}!");
                await SayText($"{user} cheered with message {message}", 2000, GetSelectedPersona(Properties.Settings.Default.TwitchCheeringPersona));
                _TalkDone = true;
            });
            await InvokeUI(async () =>
            {
                if (message.Length >= 1)
                    await TalkToLLM($"Respond to the message of {user} who said: {message}", GetSelectedPersona(Properties.Settings.Default.TwitchCheeringPersona).RoleText);
                //no message is no text
            });
            //we have to await the GPT response, due to running this from another thread await alone is not enough.
            while (!_gPTDone)
            {
                await Task.Delay(500);
            }
            //ok we waited, lets say the response, but we need a small delay to not sound unnatural      
            await InvokeUI(async () =>
            {
                _bBBlog.Info("GPT response received, saying it");
                while (!_TalkDone)
                {
                    await Task.Delay(1000);
                }
                await SayText(_gPTOutputText, 0, GetSelectedPersona(Properties.Settings.Default.TwitchCheeringPersona));
            });
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TwitchEventSub_OnESubChannelPointRedemption(object sender, TwitchEventhandlers.OnChannelPointCustomRedemptionEventArgs e)
        {
            string user = e.GetChannelPointCustomRedemptionInfo()[0];
            string message = e.GetChannelPointCustomRedemptionInfo()[1];
            _gPTDone = false;
            _bBBlog.Info($"Valid Twitch Channel Point Redemption message received: {user} redeemed with message: {message}");
            await InvokeUI(async () =>
            {
                _bBBlog.Info("Lets say a short \"thank you\" for the channel point redemption, and pass the text to the LLM");
                await SayText($"{user} redeemed with message {message}", 3000, GetSelectedPersona(Properties.Settings.Default.TwitchChannelPointPersona));
            });
            await InvokeUI(async () =>
            {
                await TalkToLLM($"Respond to the message of {user} saying: {message}", GetSelectedPersona(Properties.Settings.Default.TwitchChannelPointPersona).RoleText);
            });
            //we have to await the GPT response, due to running this from another thread await alone is not enough.
            while (!_gPTDone)
            {
                await Task.Delay(500);
            }
            //ok we waited, lets say the response, but we need a small delay to not sound unnatural      
            await InvokeUI(async () =>
            {
                await SayText(_gPTOutputText, 0, GetSelectedPersona(Properties.Settings.Default.TwitchChannelPointPersona));
            });
        }

        [SupportedOSPlatform("windows6.1")]
        //TwitchEventSub_OnESubGiftedSub
        private async void TwitchEventSub_OnESubGiftedSub(object sender, TwitchEventhandlers.OnSubscriptionGiftEventArgs e)
        {
            string user = e.GetSubscriptionGiftInfo()[0];
            string amount = e.GetSubscriptionGiftInfo()[1];
            string tier = e.GetSubscriptionGiftInfo()[2];
            _bBBlog.Info($"Valid Twitch Gifted Subscription message received: {user} gifted {amount} subs tier {tier}");
            await InvokeUI(async () =>
            {
                _bBBlog.Info("Lets say a short \"thank you\" for the gifted sub(s)");
                if (int.Parse(amount) > 1)
                    await SayText($"Thanks {user} for gifting {amount} tier {tier} subs!", 0, GetSelectedPersona(Properties.Settings.Default.TwitchSubscriptionPersona));
                else
                    await SayText($"Thanks {user} for gifting {amount} tier {tier} sub!", 0, GetSelectedPersona(Properties.Settings.Default.TwitchSubscriptionPersona));
            });

        }

        [SupportedOSPlatform("windows6.1")]
        private async void TwitchEventSub_OnESubSubscribe(object sender, TwitchEventhandlers.OnSubscribeEventArgs e)
        {
            string user = e.GetSubscribeInfo()[0];
            string broadcaster = e.GetSubscribeInfo()[1];
            _bBBlog.Info($"Valid Twitch Subscription message received: {user} subscribed to {broadcaster}");
            await InvokeUI(async () =>
            {
                _bBBlog.Info("Lets say a short \"thank you\" for the subscriber");
                await SayText($"Thanks {user} for subscribing!", 0, GetSelectedPersona(Properties.Settings.Default.TwitchSubscriptionPersona));
            });
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TwitchEventSub_OnESubReSubscribe(object sender, TwitchEventhandlers.OnReSubscribeEventArgs e)
        {
            string user = e.GetSubscribeInfo()[0];
            string message = e.GetSubscribeInfo()[1];
            string months = e.GetSubscribeInfo()[2];
            _gPTDone = false;
            _bBBlog.Info($"Valid Twitch Re-Subscription message received: {user} subscribed for {months} months with message: {message}");
            await InvokeUI(async () =>
            {
                if (message.Length >= 1)
                    await SayText($"{user} has resubscribed for a total of months {months}!", 0, GetSelectedPersona(Properties.Settings.Default.TwitchSubscriptionPersona));
                else
                    await SayText($"{user} has resubscribed for a total of {months} months saying {message}.", 0, GetSelectedPersona(Properties.Settings.Default.TwitchSubscriptionPersona));

            });
            if (message.Length >= 1)
            {
                _gPTDone = false;
                await InvokeUI(async () =>
                {
                    await TalkToLLM($"Respond to the message of {user} saying: {message}", GetSelectedPersona(Properties.Settings.Default.TwitchSubscriptionPersona).RoleText);
                });
                //we have to await the GPT response, due to running this from another thread await alone is not enough.
                while (!_gPTDone)
                {
                    await Task.Delay(500);
                }
                //ok we waited, lets say the response, but we need a small delay to not sound unnatural      
                await InvokeUI(async () =>
                {
                    await Task.Delay(3000);
                    await SayText(_gPTOutputText, 0, GetSelectedPersona(Properties.Settings.Default.TwitchSubscriptionPersona));
                });
            }
        }


        [SupportedOSPlatform("windows6.1")]
        //eventhandler for valid chat messages trigger
        private async void TwitchEventSub_OnESubChatMessage(object sender, TwitchEventhandlers.OnChatEventArgs e)
        {

            string message = e.GetChatInfo()[1].Replace(TwitchCommandTrigger.Text, "");
            string user = e.GetChatInfo()[0];
            //we got a valid chat message, lets see what we can do with it
            _bBBlog.Info("Valid Twitch Chat message received from user: " + user + " message: " + message);
            _gPTDone = false;
            //we use InvokeUI to make sure we can write to the textlog from another thread that is not the Ui thread.
            await InvokeUI(async () =>
            {
                TextLog.AppendText("Valid Twitch Chat message received from user: " + user + " message: " + message + "\r\n");
                await SayText($"{user} said {message}", 3000, GetSelectedPersona(Properties.Settings.Default.TwitchChatPersona));
            });
            await InvokeUI(async () =>
            {
                await TalkToLLM($"respond to {user} who said: {message}", GetSelectedPersona(Properties.Settings.Default.TwitchChatPersona).RoleText);
            });
            //we have to await the GPT response, due to running this from another thread await alone is not enough.
            while (!_gPTDone)
            {
                await Task.Delay(500);
            }
            //ok we waited, lets say the response, but we need a small delay to not sound unnatural      
            await InvokeUI(async () =>
            {
                await SayText(_gPTOutputText, 0, GetSelectedPersona(Properties.Settings.Default.TwitchChatPersona));
            });

        }


        [SupportedOSPlatform("windows6.1")]
        private async void TwitchReadChatCheckBox_Click(object sender, EventArgs e)
        {
            //if twitch isnt enabled, we cant do anything internal so ignore anything Twitch related
            if (!_twitchStarted)
            {
                _bBBlog.Debug("Twitch not enabled, ignoring read chat checkbox");
                return;
            }
            _bBBlog.Debug($"eventsub websocket: {_twitchEventSub._eventSubWebsocketClient.SessionId}");
            if (TwitchReadChatCheckBox.Checked)
            {
                _bBBlog.Info("This enables reading chat messages to watch for a command, in busy channels this will cause significant load on your computer");
                MessageBox.Show("Reading chat creates a high load on busy channels. Be warned!", "Twitch Channel messages enabled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //just double check its not already enabled
                if (!_globalTwitchAPI.EventSubReadChatMessages)
                {
                    _bBBlog.Info("Twitch read chat enabled.");
                    TwitchEnableDisableFields();
                    _twitchEventSub.EventSubHandleReadchat(TwitchCommandTrigger.Text, int.Parse(TwitchChatCommandDelay.Text), false, TwitchNeedsSubscriber.Checked);
                    //set local eventhanlder for valid chat messages to trigger the bot
                    _twitchEventSub.OnESubChatMessage += TwitchEventSub_OnESubChatMessage;
                }
            }
            else
            {
                _bBBlog.Info("Twitch read chat unchecked. Disabling event handler.");
                TwitchEnableDisableFields();
                _twitchEventSub.OnESubChatMessage -= TwitchEventSub_OnESubChatMessage;
                await _twitchEventSub.EventSubStopReadChat();
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TwitchCheerCheckbox_Click(object sender, EventArgs e)
        {
            //if twitch isnt enabled, we cant do anything internal so ignore anything Twitch related
            if (!_twitchStarted)
            {
                _bBBlog.Debug("Twitch not enabled, ignoring cheer checkbox");
                return;
            }
            _bBBlog.Debug($"eventsub websocket: {_twitchEventSub._eventSubWebsocketClient.SessionId}");
            if (TwitchCheerCheckBox.Checked)
            {
                _bBBlog.Info("This enables reading cheers and messages when cheered over a certain amount");
                //just double check its not already enabled
                if (!_globalTwitchAPI.EventSubCheckCheer)
                {
                    _bBBlog.Info("Twitch cheering enabled.");
                    _twitchEventSub.EventSubHandleCheer(int.Parse(TwitchMinBits.Text));
                    //set local eventhanlder for valid chat messages to trigger the bot
                    _twitchEventSub.OnESubCheerMessage += TwitchEventSub_OnESubCheerMessage;
                }
            }
            else
            {
                _bBBlog.Info("Twitch cheering unchecked. Disabling event handler.");
                _twitchEventSub.OnESubCheerMessage -= TwitchEventSub_OnESubCheerMessage;
                await _twitchEventSub.EventSubStopCheer();

            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TwitchSubscribed_Click(object sender, EventArgs e)
        {
            //if twitch isnt enabled, we cant do anything internal so ignore anything Twitch related
            if (!_twitchStarted)
            {
                _bBBlog.Debug("Twitch not enabled, ignoring subscribed checkbox");
                return;
            }
            _bBBlog.Debug($"eventsub websocket: {_twitchEventSub._eventSubWebsocketClient.SessionId}");
            if (TwitchSubscribed.Checked)
            {
                _bBBlog.Info("This enables reading subscription events");

                //just double check its not already enabled
                if (!_globalTwitchAPI.EventSubCheckSubscriptions)
                {
                    _bBBlog.Info("Twitch subscriptions enabled.");
                    _twitchEventSub.EventSubHandleSubscription();
                    //set local eventhanlder for valid chat messages to trigger the bot
                    _twitchEventSub.OnESubSubscribe += TwitchEventSub_OnESubSubscribe;
                }
            }
            else
            {
                _bBBlog.Info("Twitch subscriptions unchecked. Disabling event handler.");
                _twitchEventSub.OnESubSubscribe -= TwitchEventSub_OnESubSubscribe;
                await _twitchEventSub.EventSubStopSubscription();
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TwitchGiftedSub_Click(object sender, EventArgs e)
        {
            //if twitch isnt enabled, we cant do anything internal so ignore anything Twitch related
            if (!_twitchStarted)
            {
                _bBBlog.Debug("Twitch not enabled, ignoring gifted subs checkbox");
                return;
            }
            _bBBlog.Debug($"eventsub websocket: {_twitchEventSub._eventSubWebsocketClient.SessionId}");
            if (TwitchGiftedSub.Checked)
            {
                _bBBlog.Info("This enables reading gifted subscription events");

                //just double check its not already enabled
                if (!_globalTwitchAPI.EventSubCheckSubscriptionGift)
                {
                    _bBBlog.Info("Twitch gifted subscriptions enabled.");
                    _twitchEventSub.EventSubHandleSubscriptionGift();
                    //set local eventhanlder for valid chat messages to trigger the bot
                    _twitchEventSub.OnESubSubscriptionGift += TwitchEventSub_OnESubGiftedSub;
                }
            }
            else
            {
                _bBBlog.Info("Twitch gifted subscriptions unchecked. Disabling event handler.");
                _twitchEventSub.OnESubSubscriptionGift -= TwitchEventSub_OnESubGiftedSub;
                await _twitchEventSub.EventSubStopSubscriptionGift();
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void BBB_Load(object sender, EventArgs e)
        {
            _bBBlog.Info("BanterBrain Buddy started");
            MessageBox.Show("This is a alpha version of BanterBrain Buddy. Don\'t expect anything to work reliably", "BanterBrain Buddy Alpha", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        [SupportedOSPlatform("windows6.1")]
        private async void ExitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            if (_globalTwitchAPI != null)
            {
                _globalTwitchAPI.StopHourlyAccessTokenCheck();

                _globalTwitchAPI = null;
            }
            if (_twitchEventSub != null)
            {
                await _twitchEventSub.EventSubStopAsync();
                _twitchEventSub = null;
            }
            this.Close();
        }

        [SupportedOSPlatform("windows6.1")]
        private void button1_Click(object sender, EventArgs e)
        {

        }

        [SupportedOSPlatform("windows6.1")]
        private async void TwitchChannelPointCheckBox_Click(object sender, EventArgs e)
        {
            //if twitch isnt enabled, we cant do anything internal so ignore anything Twitch related
            if (!_twitchStarted)
            {
                _bBBlog.Debug("Twitch not enabled, ignoring channelpoint redemption checkbox");
                return;
            }
            if (TwitchChannelPointCheckBox.Checked)
            {
                _bBBlog.Info("This enables reading channel point redemption events");
                //just double check its not already enabled
                if (!_globalTwitchAPI.EventSubCheckChannelPointRedemption)
                {
                    _bBBlog.Info("Twitch channel points enabled, calling EventSubHandleChannelPoints");
                    _twitchEventSub.EventSubHandleChannelPointRedemption(TwitchCustomRewardName.Text);
                    _twitchEventSub.OnESubChannelPointRedemption += TwitchEventSub_OnESubChannelPointRedemption;
                }
            }
            else
            {
                _bBBlog.Info("Twitch channel points unchecked. Disabling event handler.");
                _twitchEventSub.OnESubChannelPointRedemption -= TwitchEventSub_OnESubChannelPointRedemption;
                await _twitchEventSub.EventSubStopChannelPointRedemption();
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void seToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm test = new();
            test.FormClosing += BBB_Test_FormClosing;
            test.ShowDialog();
        }

        //handle the settings form closing and be sure to reload the new settings
        [SupportedOSPlatform("windows6.1")]
        private void BBB_Test_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateTextLog("Settings closed. We loaded settings!\r\n");
            _bBBlog.Info("Settings form closed. We should load the new settings!");
            LoadPersonas();
            LoadSettings();
        }

        private void BBB_VisibleChanged(object sender, EventArgs e)
        {
            _bBBlog.Info("BanterBrain Buddy visible changed. to test if closing form2 is done");
        }

        [SupportedOSPlatform("windows6.1")]
        private void BroadcasterSelectedPersonaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TwitchStartButton_Click(object sender, EventArgs e)
        {
            if (!_twitchAPIVerified)
            {
                MessageBox.Show("Twitch API key is invalid. Please check the settings.", "Twitch API error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _bBBlog.Error("Twitch API key is invalid. Please check the settings.");
                return;
            }
            //we also need at least one subsription event enabled else its useless
            if (!TwitchReadChatCheckBox.Checked && !TwitchCheerCheckBox.Checked && !TwitchSubscribed.Checked && !TwitchGiftedSub.Checked && !TwitchChannelPointCheckBox.Checked)
            {
                MessageBox.Show("You need to enable at least one event to listen to. Please check the settings.", "Twitch API error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _bBBlog.Error("You need to enable at least one event to listen to. Please check the settings.");
                return;
            }


            //if the checkbox is checked, lets enable the timer to check the token every hour
            //and start the eventsub server
            //TODO: only allow this after both API and EventSub are tested and working
            if (TwitchStartButton.Text == "Start")
            {
                _bBBlog.Info("Twitch enabled. Starting API and EventSub");
                SetTwitchValidateTokenTimer(true);
                //we need to wait till the eventsub is started before we can enable the fields
                while (!_twitchValidateCheckStarted)
                {
                    await Task.Delay(500);
                }
                TwitchStartButton.Text = "Stop";
                TwitchEnableDisableFields();
            }
            else
            { //turning off Twitch
                _bBBlog.Info("Twitch disabled. Stopping timer and clearing token. Stopping Websocket client.");
                TextLog.AppendText("Twitch disabled. Stopping timer and clearing token. Stopping Websocket client.\r\n");
                TwitchStartButton.Text = "Start";

                if (_globalTwitchAPI != null)
                {
                    _globalTwitchAPI.StopHourlyAccessTokenCheck();
                    if (_globalTwitchAPI != null)
                        await _globalTwitchAPI.EventSubStopAsync();

                    if (_twitchEventSub != null)
                        await _twitchEventSub.EventSubStopAsync();
                    _twitchValidateCheckStarted = false;
                    TwitchAPIStatusTextBox.Text = "DISABLED";
                    TwitchAPIStatusTextBox.BackColor = Color.Red;
                    TwitchEventSubStatusTextBox.Text = "DISABLED";
                    TwitchEventSubStatusTextBox.BackColor = Color.Red;
                    TwitchEnableDisableFields();
                }
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchEnableDisableFields()
        {
            _bBBlog.Info($"Twitch enable/disable fields. Twitch started: {_twitchValidateCheckStarted}");
            //we need to disable the ability to change settings of eventlisteners that are active
            if (_twitchValidateCheckStarted)
            {
                if (TwitchReadChatCheckBox.Checked)
                {
                    _bBBlog.Info("Twitch read chat enabled, disabling settings");
                    TwitchNeedsSubscriber.Enabled = false;
                    TwitchCommandTrigger.Enabled = false;
                    TwitchChatCommandDelay.Enabled = false;
                    TwitchChatPersonaComboBox.Enabled = false;
                } else
                {
                    TwitchNeedsSubscriber.Enabled = true;
                    TwitchCommandTrigger.Enabled = true;
                    TwitchChatCommandDelay.Enabled = true;
                    TwitchChatPersonaComboBox.Enabled = true;
                }

                if (TwitchCheerCheckBox.Checked)
                {
                    _bBBlog.Info("Twitch cheers enabled, disabling settings");
                    TwitchMinBits.Enabled = false;
                    TwitchCheeringPersonaComboBox.Enabled = false;

                } else
                {
                    TwitchMinBits.Enabled = true;
                    TwitchCheeringPersonaComboBox.Enabled = true;
                }

                if (TwitchSubscribed.Checked || TwitchGiftedSub.Checked)
                {
                    _bBBlog.Info("Twitch subscriptions enabled, disabling settings");
                    TwitchSubscriptionPersonaComboBox.Enabled = false;
                }
                else
                {
                    TwitchSubscriptionPersonaComboBox.Enabled = true;
                }

                if (TwitchChannelPointCheckBox.Checked)
                {
                    _bBBlog.Info("Twitch channel points enabled, disabling settings");
                    TwitchChannelPointPersonaComboBox.Enabled = false;
                    TwitchCustomRewardName.Enabled = false;
                } else
                {
                    TwitchChannelPointPersonaComboBox.Enabled = true;
                    TwitchCustomRewardName.Enabled = true;
                }
            }
            else
            {
                TwitchNeedsSubscriber.Enabled = true;
                TwitchCommandTrigger.Enabled = true;
                TwitchChatCommandDelay.Enabled = true;
                TwitchChatPersonaComboBox.Enabled = true;
                TwitchMinBits.Enabled = true;
                TwitchCheeringPersonaComboBox.Enabled = true;
                TwitchSubscriptionPersonaComboBox.Enabled = true;
                TwitchChannelPointPersonaComboBox.Enabled = true;
                TwitchCustomRewardName.Enabled = true;

            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchEnableCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            //first we check if theres actually info in the API settings or else lets not even bother
            if (Properties.Settings.Default.TwitchAccessToken.Length < 1 && Properties.Settings.Default.TwitchChannel.Length < 1 && Properties.Settings.Default.TwitchUsername.Length <1 )
            {
                MessageBox.Show("Twitch API settings are not filled in. Please check the settings.", "Twitch API error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _bBBlog.Error("Twitch API settings are not filled in. Please check the settings.");
                return;
            }
            if (TwitchEnableCheckbox.Checked)
            {
                _bBBlog.Info("Twitch enabled. Enabling all settings");
                TwitchStartButton.Enabled = true;
                TwitchTriggerSettings.Enabled = true;
                TwitchCheerSettings.Enabled = true;
                TwitchSubscriberSettings.Enabled = true;
                TwitchChannelPointsSettings.Enabled = true;
                TwitchAutoStart.Enabled = true;
            }
            else
            {
                _bBBlog.Info("Twitch disabled. Stopping API and EventSub");
                //do same as stop also disable stuff
                TwitchStartButton.Enabled = false;
                TwitchTriggerSettings.Enabled = false;
                TwitchCheerSettings.Enabled = false;
                TwitchSubscriberSettings.Enabled = false;
                TwitchChannelPointsSettings.Enabled = false;
                TwitchAutoStart.Enabled = false;
            }
        }
    }
}