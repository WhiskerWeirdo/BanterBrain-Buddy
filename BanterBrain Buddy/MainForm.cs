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
using System.Threading;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Reflection;
using Newtonsoft.Json;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;


/// <summary>
/// CODING RULES:
/// •	Local variables, private instance, static fields and method parameters should be camelCase.
/// •	Methods, constants, properties, events and classes should be PascalCase.
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
        private bool _hotkeyCalled;
        // check if STT is finished yet
        private bool _sTTDone;
        //check if TTS is finished yet
        private bool _tTSSpeaking;

        [SupportedOSPlatform("windows6.1")]
        //Hotkey Storage
        readonly private List<Keys> _setHotkeys = [];

        //check if the GPT LLM is donestop audio capture
        private bool _gPTDone;

        //error checker for async events. If true, stop execution of whatever you're doing
        private bool _bigError;

        private bool _twitchStarted;
        private bool _twitchAPIVerified;

        //Global Twitch API class
        //we need this for the hourly /validate check
        private TwitchAPIESub _globalTwitchAPI;
        private bool _twitchValidateCheckStarted;
        private TwitchAPIESub _twitchEventSub;

        //this needs to be public because the settings form needs to know
#pragma warning disable CA2211 // Non-constant fields should not be visible
        public static bool isTwitchRunning;
#pragma warning restore CA2211 // Non-constant fields should not be visible

        //this will hold the STT output text
        private string _sTTOutputText;
        private string _gPTOutputText;

        private ElLabs _elevenLabsApi;
        private AzureSpeechAPI _azureSpeech;
        private OllamaLLM _ollamaLLM;
        private OpenAI _openAI;
        private readonly List<Personas> _personas = [];

        [SupportedOSPlatform("windows6.1")]
        public BBB()
        {
            _twitchValidateCheckStarted = false;

            InitializeComponent();
            SetupConfigFiles();
            LoadPersonas();

            _bBBlog.Info("Program Starting...");

            UpdateTextLog("Program Starting...\r\n");
            //TODO: verify API token first
            Startup();


        }

        [SupportedOSPlatform("windows6.1")]
        private async void Startup()
        {

            //loading the settings, this might take a bit
            _bBBlog.Info("Loading settings...");
            UpdateTextLog("Loading settings...Please wait\r\n");
            BBBTabs.Enabled = false;
            menuStrip1.Enabled = false;
            if (TwitchEnableCheckbox.Checked && Properties.Settings.Default.TwitchAccessToken.Length > 1)
                SetTwitchValidateTokenTimer(true);

            await CheckConfiguredSTTProviders();
            await CheckConfiguredLLMProviders();
            await LoadSettings();
            await SetSelectedSTTProvider();
            SetSelectedLLMProvider();
            Subscribe();
            UpdateTextLog("Settings loaded.\r\n\r\n");
            BBBTabs.Enabled = true;
            menuStrip1.Enabled = true;
        }

        [SupportedOSPlatform("windows6.1")]
        private void SetSelectedLLMProvider()
        {
            _bBBlog.Debug("Setting selected LLM provider");
            _bBBlog.Debug("LLM count: " + LLMResponseSelecter.Items.Count);
            if (Properties.Settings.Default.SelectedLLM.Length > 1)
            {
                _bBBlog.Debug("Setting LLM to saved value: " + Properties.Settings.Default.SelectedLLM);
                LLMResponseSelecter.SelectedIndex = LLMResponseSelecter.FindStringExact(Properties.Settings.Default.SelectedLLM);
                if (LLMResponseSelecter.SelectedIndex == -1)
                {
                    _bBBlog.Error("Selected LLM not found in list, setting to first in list");
                    if (LLMResponseSelecter.Items.Count > 0)
                    {
                        _bBBlog.Debug("Setting LLM to first in list");
                        LLMResponseSelecter.SelectedIndex = 0;
                    }
                    else
                    {
                        MainRecordingStart.Enabled = false;
                        TwitchStartButton.Enabled = false;
                        LLMGroupSettings.Enabled = false;
                        _bBBlog.Error("No LLM's found. You need at least one. Please check your settings.");
                        UpdateTextLog("No LLM's found. You need at least one. Please check your settings.\r\n");
                        //  MessageBox.Show("No LLM's found. You need at least one. Please check your settings.", "LLM error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                UpdateTextLog("Selected LLM: " + LLMResponseSelecter.Text + "\r\n");
            }
            else
            {
                //ok then we just load the first one we can find in the list!
                if (LLMResponseSelecter.Items.Count > 0)
                {
                    _bBBlog.Debug("Setting LLM to first in list");
                    LLMResponseSelecter.SelectedIndex = 0;
                }
                else
                {
                    MainRecordingStart.Enabled = false;
                    TwitchStartButton.Enabled = false;
                    LLMGroupSettings.Enabled = false;
                    //MessageBox.Show("No LLM's found. You need at least one. Please check your settings.", "LLM error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        [SupportedOSPlatform("windows6.1")]
        //here we check what LLM's are available and add them to the LLMResponseSelecter list 
        private async Task CheckConfiguredLLMProviders()
        {
            LLMResponseSelecter.Items.Clear();
            //check GPT
            if (Properties.Settings.Default.GPTAPIKey.Length > 1)
            {
                try
                {
                    if (_openAI == null)
                        _openAI = new();

                    if (await _openAI.OpenAICheckAPIKey())
                    {
                        LLMResponseSelecter.Items.Add("ChatGPT");
                        _bBBlog.Info("GPT API setting valid, adding to list");
                    }
                    else
                    {
                        _bBBlog.Error("GPT API setting invalid");
                    }
                }
                catch (Exception ex)
                {
                    _bBBlog.Error("Error checking GPT API key: " + ex.Message);
                }
            }
            //check Ollama  
            if (Properties.Settings.Default.OllamaURI.Length > 1 && Properties.Settings.Default.UseOllamaLLMCheckBox)
            {
                try
                {
                    _ollamaLLM ??= new(Properties.Settings.Default.OllamaURI);
                    if (await _ollamaLLM.OllamaVerify())
                    {
                        LLMResponseSelecter.Items.Add("Ollama");
                        _bBBlog.Info("Ollama setting valid, adding to list");
                        UpdateTextLog("Ollama API setting valid.\r\n");
                    }
                    else
                    {
                        _bBBlog.Error("Ollama API setting invalid or not running");
                    }

                }
                catch (Exception ex)
                {
                    _bBBlog.Error("Error checking Ollama API key: " + ex.Message);
                }
            }
        }

        //this is to make sure config files are writable, in the correct %APPDATA%\BanterBrain folder and can be read and write
        [SupportedOSPlatform("windows6.1")]
        private static void SetupConfigFiles()
        {
            //check if the folder exists, if not, create it

            string appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BanterBrain";
            if (!Directory.Exists(appdataFolder))
            {
                _bBBlog.Info("Creating BanterBrain folder in %APPDATA% because it does not exist");
                Directory.CreateDirectory(appdataFolder);
            }

            //copy from install folder to %APPDATA%\BanterBrain
            string sourcefolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //check if the settings file exists, if not, copy it
            string tmpFile = appdataFolder + "\\settings.json";
            if (!File.Exists(tmpFile))
            {
                _bBBlog.Debug("Copying settings.json file to appdata");
                File.Copy(sourcefolder + "\\settings.json", tmpFile);
            }
            //check if the personas file exists, if not, create it
            tmpFile = appdataFolder + "\\personas.json";
            if (!File.Exists(tmpFile) && File.Exists(sourcefolder + "\\personas.json"))
            {
                _bBBlog.Debug("Copying personas.json file appdata");
                File.Copy(sourcefolder + "\\personas.json", tmpFile);
            }
            else
            {
                _bBBlog.Debug("Personas file not found in install folder. It will be created later.");
            }
        }

        //we need to do this so we fill the default saved value only after the API voices are checked
        [SupportedOSPlatform("windows6.1")]
        private async Task SetSelectedSTTProvider()
        {
            //TODO: check if the selected provider is still valid
            await Task.Delay(1000);
            try
            {
                if (Properties.Settings.Default.STTSelectedProvider.Length < 1)
                {
                    _bBBlog.Debug("Setting STT provider to default");
                    STTSelectedComboBox.SelectedIndex = 0;
                    //Oh, we in first install we should reload personas then too just to make sure the mainscreen persona dropdown is filled
                    LoadPersonas();
                    if (BroadcasterSelectedPersonaComboBox.SelectedIndex == -1)
                        BroadcasterSelectedPersonaComboBox.SelectedIndex = 0;
                }
                else
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
            // Personas tmpPersona = new();
            foreach (var persona in _personas)
            {
                if (persona.Name == personaName)
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

            var tmpFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BanterBrain\\personas.json";
            if (!File.Exists(tmpFile))
            {
                _bBBlog.Debug("Personas file not found, creating it");
                //there might be a native voice installed, if so we should add it to the list
                _ = new                //there might be a native voice installed, if so we should add it to the list
                NativeSpeech();
                var nativeRegionVoicesList = await NativeSpeech.TTSNativeGetVoices();
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
                var newPersonas = new List<Personas>
                {
                    new() { Name = "Default", RoleText = "You are a cheeky streamer assistant with a silly personality.", VoiceProvider = "Native", VoiceName = tmpNativeVoice, VoiceOptions = [] }
                };
                string tmpString = JsonConvert.SerializeObject(newPersonas);
                using var sw = new StreamWriter(tmpFile, true);
                sw.Write(tmpString);
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
                using var sr = new StreamReader(tmpFile);
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
            //alright now lets load up the selected persona if there is one

        }

        /// <summary>
        /// We check the available STT providers and add them to the list for the broadcater selection list
        /// </summary>
        [SupportedOSPlatform("windows6.1")]
        private async Task CheckConfiguredSTTProviders()
        {

            //is there already one from loadsettings?
            var tmpProvider = STTSelectedComboBox.Text;
            if (tmpProvider.Length > 0)
            {
                _bBBlog.Debug("STT provider already set: " + tmpProvider);

            }
            else
            {
                //if there's at least one native voice installed, enable the native STT
                _ = new            //if there's at least one native voice installed, enable the native STT
                NativeSpeech();
                var result = await NativeSpeech.TTSNativeGetVoices();
                _bBBlog.DebugFormat("Found {0} native voices", result.Count);
                if (result.Count > 0)
                {
                    STTSelectedComboBox.Items.Add("Native");
                }
            }
            //if the Azure API key is set, we verify if the key can be used to synthesize voice
            if (Properties.Settings.Default.AzureAPIKeyTextBox.Length > 1)
            {
                //holds the result of the API ShowSettingsForm
                bool APIResult = false;
                _bBBlog.Info("Checking Azure API key");
                UpdateTextLog("Checking Azure API key\r\n");
                _azureSpeech ??= new(Properties.Settings.Default.AzureAPIKeyTextBox, Properties.Settings.Default.AzureRegionTextBox, Properties.Settings.Default.AzureLanguageComboBox);

                try
                {
                    APIResult = await _azureSpeech.AzureVerifyAPI();
                }
                catch (Exception ex)
                {
                    _bBBlog.Error("Error checking Azure API: " + ex.Message);
                    UpdateTextLog("Error checking Azure API configuration. See logfile for details\r\n");
                }
                if (!APIResult)
                {
                    _bBBlog.Error("No valid Azure API key found. Value cleared, please fix in settings");
                    UpdateTextLog("No valid Azure API key found. Value cleared, please fix in settings\r\n");
                    Properties.Settings.Default.AzureAPIKeyTextBox = "";
                    Properties.Settings.Default.Save();
                    MessageBox.Show("No valid Azure API key found. Value cleared, please fix in settings", "Azure TTS error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    STTSelectedComboBox.Items.Add("Azure");
                    _bBBlog.Info("Azure API setting valid");
                    UpdateTextLog("Azure API setting valid.\r\n");
                }
            }
            //ShowSettingsForm if teh API key for ElevenLabs is set and valid
            if (Properties.Settings.Default.ElevenLabsAPIkey.Length > 1)
            {
                //call ShowSettingsForm api key for elevenlabs
                _elevenLabsApi ??= new(Properties.Settings.Default.ElevenLabsAPIkey);
                if (await _elevenLabsApi.ElevenLabsAPIKeyTest())
                {
                    UpdateTextLog("ElevenLabs API key is valid\r\n");
                    _bBBlog.Info("ElevenLabs API key is valid");
                }
                else
                {
                    _bBBlog.Error("ElevenLabs API key is invalid.Value cleared, please fix in settings.");
                    UpdateTextLog("ElevenLabs API key is invalid. Value cleared, please fix in settings.\r\n");
                    Properties.Settings.Default.ElevenLabsAPIkey = "";
                    Properties.Settings.Default.Save();
                    MessageBox.Show("ElevenLabs API key is invalid. Value cleared, please fix in settings.", "ElevenLabs TTS error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (Properties.Settings.Default.GPTAPIKey.Length > 1)
            {
                // we need to check if the OpenAI API key is valid
                if (_openAI == null)
                    _openAI = new();
                if (await _openAI.OpenAICheckAPIKey())
                {
                    STTSelectedComboBox.Items.Add("OpenAI Whisper");
                    _bBBlog.Info("OpenAI API setting valid, adding Whisper to list");
                }
                else
                {
                    _bBBlog.Error("OpenAI API setting invalid! Value cleared, please fix in settings.");
                    UpdateTextLog("OpenAI API setting invalid!Value cleared, please fix in settings. \r\n");
                    Properties.Settings.Default.GPTAPIKey = "";
                    Properties.Settings.Default.Save();
                    MessageBox.Show("OpenAI API setting invalid! Value cleared, please fix in settings.", "OpenAI Whisper error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                _globalTwitchAPI ??= new();

                var result = await _globalTwitchAPI.ValidateAccessToken(Properties.Settings.Default.TwitchAccessToken);
                if (!result)
                {
                    _bBBlog.Error("Twitch access token is invalid, please re-authenticate");
                    MessageBox.Show("Twitch access token is invalid, please re-authenticate", "Twitch Auth error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateTextLog("Twitch access token is invalid, please re-authenticate\r\n");
                    _twitchValidateCheckStarted = false;
                    TwitchAPIStatusTextBox.Text = "DISABLED";
                    TwitchAPIStatusTextBox.BackColor = Color.Red;
                    _bigError = true;
                }
                else
                {
                    _bBBlog.Info("Twitch access token is valid. Starting automated /validate call");
                    UpdateTextLog("Twitch access token is valid. Starting automated /validate call\r\n");
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
                        UpdateTextLog("Error starting EventSub client\r\n");
                        _bigError = true;
                    }
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
            _azureSpeech ??= new(Properties.Settings.Default.AzureAPIKeyTextBox, Properties.Settings.Default.AzureRegionTextBox, Properties.Settings.Default.AzureLanguageComboBox);
            //call the Azure STT function with the selected input device
            //first initialize the Azure STT class
            _azureSpeech.AzureSTTInit(Properties.Settings.Default.VoiceInput);
            _bBBlog.Info("Azure STT microphone start. Language: " + Properties.Settings.Default.AzureLanguageComboBox);
            while (MainRecordingStart.Text == "Recording" && !_sTTDone && !_bigError)
            {
                var recognizeResult = await _azureSpeech.RecognizeSpeechAsync();
                if (recognizeResult == "NOMATCH")
                {
                    UpdateTextLog("Azure Speech-To-Text: NOMATCH: Speech could not be recognized.\r\n");
                    //_sTTOutputText += "NOMATCH: Speech could not be recognized.\r\n";
                    _bigError = true;
                    _sTTDone = true;
                }
                else if (recognizeResult == null)
                {
                    _sTTOutputText += "Fail! Speech could not be proccessed. Check log for more info.\r\n";
                    UpdateTextLog("Azure Speech-To-Text: Fail! Speech could not be proccessed. Check log for more info.\r\n");
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



        [SupportedOSPlatform("windows6.1")]
        private async Task InputStreamtoWav()
        {
            _bBBlog.Info("STT microphone start.");
            _bBBlog.Debug("Selected audio input device for Audio to Wav: " + Properties.Settings.Default.VoiceInput);

            string tmpWavFile = System.IO.Path.GetTempPath() + $"{Guid.NewGuid()}.wav";

            var recordingDevice = 0;
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                var tmpEnum = NAudio.Wave.WaveIn.GetCapabilities(i).ProductName;
                _bBBlog.Debug("Checking recording device: " + tmpEnum + "at id: " + i);
                if (tmpEnum.StartsWith(Properties.Settings.Default.VoiceInput))
                {
                    recordingDevice = i;
                    break;
                }
            }
            _bBBlog.Debug("Recording device: " + recordingDevice);

            bool writeDone = false;
            var waveIn = new WaveInEvent
            {
                DeviceNumber = recordingDevice,
                WaveFormat = new NAudio.Wave.WaveFormat(16000, 16, 1)
            };
            var writer = new WaveFileWriter(tmpWavFile, waveIn.WaveFormat);
            waveIn.StartRecording();
            waveIn.DataAvailable += (s, a) =>
            {
                writer.Write(a.Buffer, 0, a.BytesRecorded);

            };
            waveIn.RecordingStopped += (s, a) =>
            {
                _bBBlog.Debug("Recording stopped. Disposing writer");
                writer?.Dispose();
                writer = null;
                writeDone = true;
            };
            UpdateTextLog("STT microphone start. -- SPEAK NOW -- \r\n");
            _bBBlog.Info("STT microphone start.");
            while (MainRecordingStart.Text == "Recording")
            {
                await Task.Delay(500);
            }
            _bBBlog.Info("STT microphone stop.");
            waveIn.StopRecording();
            //we have to wait till the file is done writing  to disk
            while (!writeDone)
            {
                await Task.Delay(500);
            }
            //now we have the .wav file, lets convert it to text
            //but lets make sure its actually a decent size?
            _bBBlog.Debug("converting to Text from WAV");
            _sTTDone = false;

            //now lets convert the saved .wav to Text
            if (STTSelectedComboBox.Text == "Native")
                NativeSTTfromWAV(tmpWavFile);
            if (STTSelectedComboBox.Text == "OpenAI Whisper")
                WhisperSTTfromWAV(tmpWavFile);
        }


        [SupportedOSPlatform("windows6.1")]
        private async void WhisperSTTfromWAV(string tmpWavFile)
        {
            _sTTOutputText = "";
            if (_openAI == null)
                _openAI = new();
            _bBBlog.Info("Starting OpenAI STT from WAV");
            _sTTOutputText = await _openAI.OpenAISTT(tmpWavFile);
            if (_sTTOutputText == null)
            {
                _bBBlog.Error("OpenAI STT failed");
                _bigError = true;
            }
            else
            {
                _bBBlog.Info($"OpenAI STT done: {_sTTOutputText}");
            }
            _sTTDone = true;
            _bBBlog.Debug($"Deleting temp wav file: {tmpWavFile}");
            File.Delete(tmpWavFile);
        }
        
        [SupportedOSPlatform("windows6.1")]
        private async void NativeSTTfromWAV(string tmpWavFile)
        {
            _sTTDone = false;
            _sTTOutputText = "";
            NativeSpeech nativeSpeech = new();
            _bBBlog.Info("Starting Native STT from WAV");
            _sTTOutputText = await nativeSpeech.NativeSpeechRecognizeStart(tmpWavFile);
            if (_sTTOutputText == null)
            {
                _bBBlog.Error("Native STT failed");
                _bigError = true;
            }
            else
            {
                _bBBlog.Info($"Native STT done: {_sTTOutputText}");
            }
            _sTTDone = true;
            //now delete wav file
            _bBBlog.Debug($"Deleting temp wav file: {tmpWavFile}");
            File.Delete(tmpWavFile);
        }

        //play a wav file to the bots selected audio channel
        [SupportedOSPlatform("windows6.1")]
        private static async Task PlayWaveFile(string tmpWavFile)
        {
            _bBBlog.Debug($"Playing wav file: {tmpWavFile}");
            NativeSpeech nativeSpeech = new();
            //get current working directory
            await nativeSpeech.NativePlayWaveFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\sounds\\" + tmpWavFile);
        }

        [SupportedOSPlatform("windows6.1")]
        private async Task TalkToOllama(String UserInput, string tmpPersonaRoletext)
        {
            _gPTOutputText = "";
            UpdateTextLog("Sending to Ollama: " + UserInput + "\r\n");
            _ollamaLLM ??= new(Properties.Settings.Default.OllamaURI);

            var result = await _ollamaLLM.OllamaGetResponse(UserInput, tmpPersonaRoletext);
            if (result == null)
            {
                _bBBlog.Error("Ollama API error. Is there a problem with your API key or subscription?");
                MessageBox.Show("Ollama API error. Is there a problem with your API key or subscription?", "Ollama API error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _bigError = true;
            }
            else
            {
                UpdateTextLog("Ollama Response: " + result + "\r\n");
                _gPTOutputText = result;
                _gPTDone = true;
            }

        }

        [SupportedOSPlatform("windows6.1")]
        private async Task TalkToOpenAIGPT(String UserInput, string tmpPersonaRoletext)
        {
            _gPTOutputText = "";
            UpdateTextLog("Sending to GPT: " + UserInput + "\r\n");
            _bBBlog.Info("Sending to GPT: " + UserInput);
            _gPTDone = false;

            if (_openAI == null)
                _openAI = new();
            var result = await _openAI.GetOpenAIIGPTResponse(UserInput, tmpPersonaRoletext);
            if (result == null)
            {
                _bBBlog.Error("GPT API error. Is there a problem with your API key or subscription?");
                MessageBox.Show("GPT API error. Is there a problem with your API key or subscription?", "GPT API error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _bigError = true;
            }
            else
            {
                UpdateTextLog("GPT Response: " + result + "\r\n");
                _gPTOutputText = result;
                _gPTDone = true;
            }
        }

        /// <summary>
        /// Holds the list of Azure Voices and their options
        /// </summary>


        [SupportedOSPlatform("windows6.1")]
        //Azure Text-To-Speach
        private async Task TTSAzureSpeakToOutput(string TextToSpeak, Personas tmpPersona)
        {
            if (Properties.Settings.Default.AzureAPIKeyTextBox.Length < 1)
            {
                _bBBlog.Error("Azure TTS error. No API key found but this persona uses it. Please check your settings");
                UpdateTextLog("Azure TTS error. No API key found but this persona uses it. Please check your settings\r\n");
                MainRecordingStart.Enabled = true;
                return;
            }

            UpdateTextLog("Saying text with Azure TTS\r\n");
            _bBBlog.Info("Saying text with Azure TTS");
            _azureSpeech ??= new(Properties.Settings.Default.AzureAPIKeyTextBox, Properties.Settings.Default.AzureRegionTextBox, Properties.Settings.Default.AzureLanguageComboBox);

            //set the output voice, gender and locale, and the style
            //this now depends on the selected persona
            await _azureSpeech.AzureTTSInit(tmpPersona.VoiceName, tmpPersona.VoiceOptions[0], Properties.Settings.Default.TTSAudioOutput);
            if (!await _azureSpeech.AzureVerifyAPI())
            {
                _bBBlog.Error("Azure TTS error. Is there a problem with your API key or subscription?");
                UpdateTextLog("Azure TTS error. Is there a problem with your API key or subscription?\r\n");
                _bigError = true;
                MainRecordingStart.Enabled = true;
                return;
            }

            var result = await _azureSpeech.AzureSpeak(TextToSpeak);
            if (!result)
            {
                _bBBlog.Error("Azure TTS error. Is there a problem with your API key or subscription?");
                MessageBox.Show("Azure TTS error. Is there a problem with your API key or subscription?", "Azure TTS error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _bigError = true;
            }
            _bBBlog.Info("Azure TTS done");
            UpdateTextLog("Azure TTS done\r\n");
            MainRecordingStart.Enabled = true;
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
            UpdateTextLog("Native TTS done\r\n");
            _bBBlog.Info("Native TTS done");
            MainRecordingStart.Enabled = true;
        }

        [SupportedOSPlatform("windows6.1")]
        //this is the generic caller for TTS function that makes sure the TTS is done before continuing
        //we make this public so we can call it to ShowSettingsForm
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
            _bBBlog.Info("Saying text: " + TextToSay + " with " + tmpPersona.Name);
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
            else if (tmpPersona.VoiceProvider == "OpenAI Whisper")
            {
                await TTSOpenAISpeakToOutput(TextToSay, tmpPersona);
            }
            else if (tmpPersona.VoiceProvider == "ElevenLabs")
            {
                await TTSElevenLabsSpeakToOutput(TextToSay, tmpPersona);
            }

            await Task.Delay(DelayWhenDone);
            _tTSSpeaking = false;
        }

        [SupportedOSPlatform("windows6.1")]
        private async Task TTSElevenLabsSpeakToOutput(string TextToSay, Personas tmpPersona)
        {
            UpdateTextLog("Saying text with ElevenLabs TTS\r\n");
            _bBBlog.Info("Saying text with ElevenLabs TTS");

            if (Properties.Settings.Default.ElevenLabsAPIkey.Length < 1)
            {
                _bBBlog.Error("ElevenLabs TTS error. No API key found but this persona uses it. Please check your settings");
                UpdateTextLog("Elevenlans TTS error. No API key found but this persona uses it. Please check your settings\r\n");
                MainRecordingStart.Enabled = true;
                return;
            }

            if (_elevenLabsApi == null)
            {
                _elevenLabsApi = new(Properties.Settings.Default.ElevenLabsAPIkey);
                _ = await _elevenLabsApi.TTSGetElevenLabsVoices();
            }
            else
            {
                //we check if length is > 1 and if the key is different
                //if so we need to update the key
                if (Properties.Settings.Default.ElevenLabsAPIkey != _elevenLabsApi.ElevelLabsAPIKey)
                {
                    _elevenLabsApi.ElevelLabsAPIKey = Properties.Settings.Default.ElevenLabsAPIkey;
                }
            }

            //ShowSettingsForm the current key
            if (!await _elevenLabsApi.ElevenLabsAPIKeyTest())
            {
                _bBBlog.Error("ElevenLabs TTS error. Is there a problem with your API key or subscription?");
                MessageBox.Show("ElevenLabs TTS error. Is there a problem with your API key or subscription?", "ElevenLabs TTS error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainRecordingStart.Enabled = true;
                return;
            }
            var result = await _elevenLabsApi.ElevenLabsTTS(TextToSay, Properties.Settings.Default.TTSAudioOutput, tmpPersona.VoiceName, int.Parse(tmpPersona.VoiceOptions[0]), int.Parse(tmpPersona.VoiceOptions[1]), int.Parse(tmpPersona.VoiceOptions[2]));

            if (!result)
            {
                _bBBlog.Error("ElevenLabs TTS error. Is there a problem with your API key or subscription?");
                MessageBox.Show("ElevenLabs TTS error. Is there a problem with your API key or subscription?", "ElevenLabs TTS error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _bBBlog.Info("ElevenLabs TTS done");
            UpdateTextLog("ElevenLabs TTS done\r\n");
            MainRecordingStart.Enabled = true;

        }

        [SupportedOSPlatform("windows6.1")]
        private async Task TTSOpenAISpeakToOutput(string TextToSpeak, Personas tmpPersona)
        {
            if (Properties.Settings.Default.GPTAPIKey.Length < 1)
            {
                _bBBlog.Error("OpenAI TTS error. No API key found but this persona uses it. Please check your settings");
                UpdateTextLog("OpenAI TTS error. No API key found but this persona uses it. Please check your settings\r\n");
                MainRecordingStart.Enabled = true;
                return;
            }
            UpdateTextLog("Saying text with OpenAI TTS\r\n");
            _bBBlog.Info("Saying text with OpenAI TTS");
            if (_openAI == null)
                _openAI = new();
            //ShowSettingsForm the available key
            if (!await _openAI.OpenAICheckAPIKey())
            {
                _bBBlog.Error("OpenAI TTS error. Is there a problem with your API key or subscription?");
                MessageBox.Show("OpenAI TTS error. Is there a problem with your API key or subscription?", "OpenAI TTS error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainRecordingStart.Enabled = true;
                return;
            }

            await _openAI.OpenAITTS(TextToSpeak, Properties.Settings.Default.TTSAudioOutput, tmpPersona.VoiceName);
            _bBBlog.Info("OpenAI TTS done");
            UpdateTextLog("OpenAI TTS done\r\n");
            MainRecordingStart.Enabled = true;
        }

        //talk to the various LLM's
        [SupportedOSPlatform("windows6.1")]
        private async Task TalkToLLM(string TextToPass, string tmpPersonaRoleText)
        {
            _gPTOutputText = "";
            _gPTDone = false;
            if (Properties.Settings.Default.SelectedLLM == "ChatGPT")
            {
                UpdateTextLog("Using ChatGPT\r\n");
                _bBBlog.Info("Using ChatGPT");
                await TalkToOpenAIGPT(TextToPass, tmpPersonaRoleText);
            }
            else if (Properties.Settings.Default.SelectedLLM == "Ollama")
            {
                UpdateTextLog("Using Ollama\r\n");
                _bBBlog.Info("Using Ollama");
                await TalkToOllama(TextToPass, tmpPersonaRoleText);
            }
            else
            {
                _bBBlog.Error("No LLM selected");
                LLMGroupSettings.Enabled = false;
                MessageBox.Show("No LLM selected. This is bad!", "LLM error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (STTSelectedComboBox.Text.Length < 1)
            {
                UpdateTextLog("Error! No STT provider selected!\r\n");
                _bBBlog.Error("Error! No STT provider selected!");
                MainRecordingStart.Text = "Start";
                return;
            }
            _sTTDone = false;
            if (MainRecordingStart.Text == "Start")
            {
                _sTTOutputText = "";
                MainRecordingStart.Text = "Recording";
                if (selectedProvider == "Native")
                {
                    UpdateTextLog("Main button Native STT calling\r\n");
                    _bBBlog.Info("Main button Native STT calling");
                    await InputStreamtoWav();
                    while (!_sTTDone)
                    {
                        await Task.Delay(500);
                    }

                }
                else if (selectedProvider == "OpenAI Whisper")
                {

                    UpdateTextLog("OpenAI Whisper STT calling\r\n");
                    _bBBlog.Info("OpenAI Whisper STT calling");
                    await InputStreamtoWav();
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
                        UpdateTextLog("Error! API Key or region cannot be empty!\r\n");
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

                MainRecordingStart.Enabled = false;
                Thread.Sleep(500);

                //now the STT text is in _sTTOutputText, lets pass that to ChatGPT

                if (_sTTOutputText.Length > 1)
                {
                    // in this case we need to add the streamer name if entered
                    if (Properties.Settings.Default.TwitchUsername.Length > 0)
                    {
                        _bBBlog.Debug("Adding streamer name to STT text");
                        _sTTOutputText = Properties.Settings.Default.TwitchUsername + " says: " + _sTTOutputText;
                    }

                    await TalkToLLM(_sTTOutputText, GetSelectedPersona(BroadcasterSelectedPersonaComboBox.Text).RoleText);

                    //this depends on the selected persona now
                    //var tmpPersona = GetSelectedPersona(BroadcasterSelectedPersonaComboBox.Text);
                    await SayText(_gPTOutputText, 0, GetSelectedPersona(BroadcasterSelectedPersonaComboBox.Text));

                }
                else
                {
                    UpdateTextLog("No audio recorded");
                    _bBBlog.Info("No audio recorded");
                    MainRecordingStart.Enabled = true;
                }
            }
            else
            {

                MainRecordingStart.Text = "Start";

            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async Task LoadSettings()
        {
            TwitchCommandTrigger.Text = Properties.Settings.Default.TwitchCommandTrigger;
            TwitchChatCommandDelay.Text = Properties.Settings.Default.TwitchChatCommandDelay.ToString();
            TwitchNeedsSubscriber.Checked = Properties.Settings.Default.TwitchNeedsSubscriber;
            TwitchMinBits.Text = Properties.Settings.Default.TwitchMinBits.ToString();
            TwitchSubscribed.Checked = Properties.Settings.Default.TwitchSubscribed;
            TwitchGiftedSub.Checked = Properties.Settings.Default.TwitchGiftedSub;
            TwitchAutoStart.Checked = Properties.Settings.Default.TwitchAutoStart;
            TwitchDelayFinishToChatcCheckBox.Checked = Properties.Settings.Default.DelayFinishToChatcCheckBox;
            if (TwitchDelayFinishToChatcCheckBox.Checked)
            {
                TwitchDelayMessageTextBox.Enabled = true;
            }
            else
            {
                TwitchDelayMessageTextBox.Enabled = false;
            }
            TwitchDelayMessageTextBox.Text = Properties.Settings.Default.TwitchDelayMessageTextBox;
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
                TwitchSoundsSettings.Enabled = true;
                TwitchResponseSettings.Enabled = true;
            }
            else
            {
                TwitchStartButton.Enabled = false;
                TwitchTriggerSettings.Enabled = false;
                TwitchCheerSettings.Enabled = false;
                TwitchSubscriberSettings.Enabled = false;
                TwitchChannelPointsSettings.Enabled = false;
                TwitchAutoStart.Enabled = false;
                TwitchSoundsSettings.Enabled = false;
                TwitchResponseSettings.Enabled = false;
            }

            TwitchReadChatCheckBox.Checked = Properties.Settings.Default.TwitchReadChatCheckBox;
            TwitchCheerCheckBox.Checked = Properties.Settings.Default.TwitchCheerCheckbox;
            TwitchCustomRewardName.Text = Properties.Settings.Default.TwitchCustomRewardName;
            TwitchChannelPointCheckBox.Checked = Properties.Settings.Default.TwitchChannelPointCheckBox;
            STTSelectedComboBox.SelectedIndex = STTSelectedComboBox.FindStringExact(Properties.Settings.Default.STTSelectedProvider);
            if (STTSelectedComboBox.SelectedIndex == -1)
            {
                STTSelectedComboBox.SelectedIndex = 0;
            }

            BroadcasterSelectedPersonaComboBox.SelectedIndex = BroadcasterSelectedPersonaComboBox.FindStringExact(Properties.Settings.Default.MainSelectedPersona);
            TwitchCheeringPersonaComboBox.SelectedIndex = TwitchCheeringPersonaComboBox.FindStringExact(Properties.Settings.Default.TwitchCheeringPersona);
            TwitchChannelPointPersonaComboBox.SelectedIndex = TwitchChannelPointPersonaComboBox.FindStringExact(Properties.Settings.Default.TwitchChannelPointPersona);
            TwitchSubscriptionPersonaComboBox.SelectedIndex = TwitchSubscriptionPersonaComboBox.FindStringExact(Properties.Settings.Default.TwitchSubscriptionPersona);
            TwitchResponseToChatCheckBox.Checked = Properties.Settings.Default.TwitchResponseToChatCheckBox;
            TwitchResponseToChatDelayTextBox.Text = Properties.Settings.Default.TwitchResponseToChatDelayTextBox;
            TwitchChatPersonaComboBox.SelectedIndex = TwitchChatPersonaComboBox.FindStringExact(Properties.Settings.Default.TwitchChatPersona);

            //load settings but also have safe defaults
            TwitchSubscriptionTTSResponseOnlyRadioButton.Checked = Properties.Settings.Default.TwitchSubscriptionTTSResponseOnlyRadioButton;
            if (!TwitchSubscriptionTTSResponseOnlyRadioButton.Checked)
            {
                TwitchSubscriptionTTSEverythingRadioButton.Checked = true;
            }

            TwitchCheeringTTSResponseOnlyRadioButton.Checked = Properties.Settings.Default.TwitchCheeringTTSResponseOnlyRadioButton;
            if (!TwitchCheeringTTSResponseOnlyRadioButton.Checked)
            {
                TwitchCheeringTTSEverythingRadioButton.Checked = true;
            }

            TwitchChannelPointTTSResponseOnlyRadioButton.Checked = Properties.Settings.Default.TwitchChannelPointTTSResponseOnlyRadioButton;
            if (!TwitchChannelPointTTSResponseOnlyRadioButton.Checked)
            {
                TwitchChannelPointTTSEverythingRadioButton.Checked = true;
            }

            TwitchChatTTSResponseOnlyRadioButton.Checked = Properties.Settings.Default.TwitchChatTTSResponseOnlyRadioButton;
            if (!TwitchChatTTSResponseOnlyRadioButton.Checked)
            {
                TwitchChatTTSEverythingRadioButton.Checked = true;
            }

            TwitchChatSoundCheckBox.Checked = Properties.Settings.Default.TwitchChatSoundCheckBox;
            if (TwitchChatSoundCheckBox.Checked)
            {
                TwitchChatSoundTextBox.Enabled = true;
            }
            else
            {
                TwitchChatSoundTextBox.Enabled = false;
            }
            TwitchChatSoundTextBox.Items.Clear();
            AddFilesToDropdown(TwitchChatSoundTextBox);
            TwitchChatSoundTextBox.SelectedIndex = TwitchChatSoundTextBox.FindStringExact(Properties.Settings.Default.TwitchChatSound);

            TwitchChannelSoundCheckBox.Checked = Properties.Settings.Default.TwitchChannelSoundCheckBox;
            if (TwitchChannelSoundCheckBox.Checked)
            {
                TwitchChannelSoundTextBox.Enabled = true;
            }
            else
            {
                TwitchChannelSoundTextBox.Enabled = false;
            }
            TwitchChannelSoundTextBox.Items.Clear();
            AddFilesToDropdown(TwitchChannelSoundTextBox);
            TwitchChannelSoundTextBox.SelectedIndex = TwitchChannelSoundTextBox.FindStringExact(Properties.Settings.Default.TwitchChannelSound);

            TwitchCheeringSoundCheckBox.Checked = Properties.Settings.Default.TwitchCheeringSoundCheckBox;
            if (TwitchCheeringSoundCheckBox.Checked)
            {
                TwitchCheeringSoundTextBox.Enabled = true;
            }
            else
            {
                TwitchCheeringSoundTextBox.Enabled = false;
            }
            TwitchCheeringSoundTextBox.Items.Clear();
            AddFilesToDropdown(TwitchCheeringSoundTextBox);
            TwitchCheeringSoundTextBox.SelectedIndex = TwitchCheeringSoundTextBox.FindStringExact(Properties.Settings.Default.TwitchCheeringSound);

            TwitchSubscriptionSoundCheckBox.Checked = Properties.Settings.Default.TwitchSubscriptionSoundCheckBox;
            if (TwitchSubscriptionSoundCheckBox.Checked)
            {
                TwitchSubscriptionSoundTextBox.Enabled = true;
            }
            else
            {
                TwitchSubscriptionSoundTextBox.Enabled = false;
            }
            TwitchSubscriptionSoundTextBox.Items.Clear();
            AddFilesToDropdown(TwitchSubscriptionSoundTextBox);
            TwitchSubscriptionSoundTextBox.SelectedIndex = TwitchSubscriptionSoundTextBox.FindStringExact(Properties.Settings.Default.TwitchSubscriptionSoundTextBox);

            //check if theres a speaker selected if not select the default onne
            if (Properties.Settings.Default.TTSAudioOutput.Length < 1)
            {
                _bBBlog.Info("No audio output selected, setting to default");
                var enumerator = new MMDeviceEnumerator();
                enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
                Properties.Settings.Default.TTSAudioOutput = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console).FriendlyName;
                Properties.Settings.Default.Save();
            }

            //check if thers a default microphone selected, if not, select the default one
            if (Properties.Settings.Default.VoiceInput.Length < 1)
            {
                _bBBlog.Info("No microphone selected, setting to default");
                var enumerator = new MMDeviceEnumerator();
                enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
                Properties.Settings.Default.VoiceInput = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console).FriendlyName;
                Properties.Settings.Default.Save();
            }

            //check twitch key
            _bBBlog.Info("Checking Twitch API key if Twitch not active");
            //if Twitch is already running, we dont need to check the key
            if (Properties.Settings.Default.TwitchAccessToken.Length > 1 && TwitchAPIStatusTextBox.Text == "DISABLED" && TwitchEventSubStatusTextBox.Text == "DISABLED")
            {
                _bBBlog.Info("Twitch API key is set");
                UpdateTextLog("Twitch API key is set.\r\n");
                //lets check if the key is valid
                TwitchAPIESub twitchAPI = new();
                if (await twitchAPI.ValidateAccessToken(Properties.Settings.Default.TwitchAccessToken))
                {
                    _bBBlog.Info("Twitch API key is valid");
                    UpdateTextLog("Twitch API key is valid.\r\n");
                    _twitchAPIVerified = true;
                    TwitchStartButton.Enabled = true;
                }
                else
                {
                    _bBBlog.Error("Twitch API key is invalid");
                    UpdateTextLog("Twitch API key is invalid.\r\n");
                    TwitchStartButton.Enabled = false;
                }
            }
            else
            {
                _bBBlog.Error("Twitch API key is not set");
                UpdateTextLog("Twitch API key is not set.\r\n");
            }

            //load HotkeyList into _setHotkeys
            if (Properties.Settings.Default.PTTHotkey.Length < 1)
            {
                _bBBlog.Info("No hotkey set");
            }
            else
            {
                _bBBlog.Info($"Loading hotkeys: {Properties.Settings.Default.PTTHotkey}");
                UpdateTextLog("PPT hotkey: " + Properties.Settings.Default.PTTHotkey + "\r\n");
                _bBBlog.Info("Hotkey set");
                var hotKeys = Properties.Settings.Default.PTTHotkey.Split('+').ToList();
                _setHotkeys.Clear();
                foreach (var key in hotKeys)
                {
                    _setHotkeys.Add((Keys)Enum.Parse(typeof(Keys), key));
                }
                Subscribe();
            }

            //lets check if the selected OpenAI API key is valid   
            if (Properties.Settings.Default.SelectedLLM == "ChatGPT")
            {
                OpenAIAPI api = new(Properties.Settings.Default.GPTAPIKey);
                if (await api.Auth.ValidateAPIKey())
                {
                    _bBBlog.Info("GPT API key is valid");
                    UpdateTextLog("OpenAI key is valid.\r\n");
                }
                else
                {
                    _bBBlog.Error("GPT API is selected but key is invalid invalid. \r\n");
                    UpdateTextLog("OpenAI ChatGPT is selected as LLM but key is invalid. \r\n");
                    MessageBox.Show("OpenAI ChatGPT is selected as LLM but key is invalid. \r\n", "OpenAI ChatGPT error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else if (Properties.Settings.Default.SelectedLLM == "None" || Properties.Settings.Default.SelectedLLM == "")
            {
                UpdateTextLog("No LLM selected. You should set one in the settings first\r\n");
                LLMGroupSettings.Enabled = false;
            }

            if (TwitchAutoStart.Checked && TwitchAPIStatusTextBox.Text == "DISABLED" && TwitchEventSubStatusTextBox.Text == "DISABLED")
            {
                _bBBlog.Info("Auto starting Twitch");
                UpdateTextLog("Auto starting Twitch\r\n");
                TwitchStartButton_Click(null, null);
            }

        }

        [SupportedOSPlatform("windows6.1")]
        private void SaveALLSettings()
        {
            if (TwitchCommandTrigger.Text.Length > 0)
                Properties.Settings.Default.TwitchCommandTrigger = TwitchCommandTrigger.Text;
            if (TwitchChatCommandDelay.Text.Length > 0)
                Properties.Settings.Default.TwitchChatCommandDelay = int.Parse(TwitchChatCommandDelay.Text);
            Properties.Settings.Default.TwitchNeedsSubscriber = TwitchNeedsSubscriber.Checked;
            if (TwitchMinBits.Text.Length > 0)
                Properties.Settings.Default.TwitchMinBits = int.Parse(TwitchMinBits.Text);
            Properties.Settings.Default.TwitchSubscribed = TwitchSubscribed.Checked;
            Properties.Settings.Default.TwitchGiftedSub = TwitchGiftedSub.Checked;
            Properties.Settings.Default.TwitchEnable = TwitchEnableCheckbox.Checked;
            Properties.Settings.Default.TwitchReadChatCheckBox = TwitchReadChatCheckBox.Checked;
            Properties.Settings.Default.TwitchCheerCheckbox = TwitchCheerCheckBox.Checked;
            if (TwitchCustomRewardName.Text.Length > 0)
                Properties.Settings.Default.TwitchCustomRewardName = TwitchCustomRewardName.Text;
            Properties.Settings.Default.TwitchChannelPointCheckBox = TwitchChannelPointCheckBox.Checked;
            if (STTSelectedComboBox.Text.Length > 0)
                Properties.Settings.Default.STTSelectedProvider = STTSelectedComboBox.Text;
            if (BroadcasterSelectedPersonaComboBox.Text.Length > 0)
                Properties.Settings.Default.MainSelectedPersona = BroadcasterSelectedPersonaComboBox.Text;
            if (TwitchChannelPointPersonaComboBox.Text.Length > 0)
                Properties.Settings.Default.TwitchChannelPointPersona = TwitchChannelPointPersonaComboBox.Text;
            if (TwitchCheeringPersonaComboBox.Text.Length > 0)
                Properties.Settings.Default.TwitchCheeringPersona = TwitchCheeringPersonaComboBox.Text;
            if (TwitchSubscriptionPersonaComboBox.Text.Length > 0)
                Properties.Settings.Default.TwitchSubscriptionPersona = TwitchSubscriptionPersonaComboBox.Text;
            if (TwitchChatPersonaComboBox.Text.Length > 0)
                Properties.Settings.Default.TwitchChatPersona = TwitchChatPersonaComboBox.Text;
            Properties.Settings.Default.TwitchAutoStart = TwitchAutoStart.Checked;
            if (LLMResponseSelecter.Text.Length > 0)
                Properties.Settings.Default.SelectedLLM = LLMResponseSelecter.Text;
            Properties.Settings.Default.TwitchChatSoundCheckBox = TwitchChatSoundCheckBox.Checked;
            if (TwitchChatSoundTextBox.Text.Length > 0)
                Properties.Settings.Default.TwitchChatSound = TwitchChatSoundTextBox.Text;
            Properties.Settings.Default.TwitchChannelSoundCheckBox = TwitchChannelSoundCheckBox.Checked;
            if (TwitchChannelSoundTextBox.Text.Length > 0)
                Properties.Settings.Default.TwitchChannelSound = TwitchChannelSoundTextBox.Text;
            if (TwitchCheeringSoundTextBox.Text.Length > 0)
                Properties.Settings.Default.TwitchCheeringSound = TwitchCheeringSoundTextBox.Text;
            Properties.Settings.Default.TwitchCheeringSoundCheckBox = TwitchCheeringSoundCheckBox.Checked;
            if (TwitchSubscriptionSoundTextBox.Text.Length > 0)
                Properties.Settings.Default.TwitchSubscriptionSoundTextBox = TwitchSubscriptionSoundTextBox.Text;
            Properties.Settings.Default.TwitchSubscriptionSoundCheckBox = TwitchSubscriptionSoundCheckBox.Checked;
            Properties.Settings.Default.TwitchResponseToChatCheckBox = TwitchResponseToChatCheckBox.Checked;
            if (TwitchResponseToChatDelayTextBox.Text.Length > 0)
                Properties.Settings.Default.TwitchResponseToChatDelayTextBox = TwitchResponseToChatDelayTextBox.Text;
            if (TwitchDelayMessageTextBox.Text.Length > 0)
                Properties.Settings.Default.TwitchDelayMessageTextBox = TwitchDelayMessageTextBox.Text;
            Properties.Settings.Default.DelayFinishToChatcCheckBox = TwitchDelayFinishToChatcCheckBox.Checked;
            Properties.Settings.Default.TwitchSubscriptionTTSResponseOnlyRadioButton = TwitchSubscriptionTTSResponseOnlyRadioButton.Checked;
            Properties.Settings.Default.TwitchCheeringTTSResponseOnlyRadioButton = TwitchCheeringTTSResponseOnlyRadioButton.Checked;
            Properties.Settings.Default.TwitchChannelPointTTSResponseOnlyRadioButton = TwitchChannelPointTTSResponseOnlyRadioButton.Checked;
            Properties.Settings.Default.TwitchChatTTSResponseOnlyRadioButton = TwitchChatTTSResponseOnlyRadioButton.Checked;
            Properties.Settings.Default.Save();
        }

        [SupportedOSPlatform("windows6.1")]
        private async void BBB_FormClosing(object sender, FormClosingEventArgs e)
        {
            //turn off potentionally running Twitch stuff
            if (_globalTwitchAPI != null)
                await _globalTwitchAPI.EventSubStopAsync();

            if (_twitchEventSub != null)
                await _twitchEventSub.EventSubStopAsync();
            //only save if theres something to be saved!

            SaveALLSettings();


            //remove hotkey hooks
            Unsubscribe();
        }

        [SupportedOSPlatform("windows6.1")]
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }


        [SupportedOSPlatform("windows6.1")]
        //Keyboard hooks
        public void Unsubscribe()
        {
            if (_setHotkeys.Count < 1)
            {
                _bBBlog.Info("No hotkeys set, not unsubscribing");
                return;
            }
            else
            {
                _bBBlog.Info("Unsubscribing from hotkeys");
            }
            m_GlobalHook.KeyDown -= GlobalHookKeyDown;
            m_GlobalHook.KeyUp -= GlobalHookKeyUp;
            //It is recommened to dispose it
            m_GlobalHook.Dispose();
            m_GlobalHook = null;
        }

        [SupportedOSPlatform("windows6.1")]
        public void Subscribe()
        {
            if (_setHotkeys.Count < 1)
            {
                _bBBlog.Info("No hotkeys set, not subscribing");
                return;
            }
            _bBBlog.Info("Subscribing to hotkeys");
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.KeyDown += GlobalHookKeyDown;
            m_GlobalHook.KeyUp += GlobalHookKeyUp;

            var map = new Dictionary<Combination, Action>
             {
                {Combination.FromString(Properties.Settings.Default.PTTHotkey), async () => await HandleHotkeyButton() }
             };

            m_GlobalHook.OnCombination(map);

        }

        [SupportedOSPlatform("windows6.1")]
        private async Task HandleHotkeyButton()
        {
            if (!_hotkeyCalled)
            {
                if (BBB.ActiveForm != null)
                    if (BBB.ActiveForm.Name == "SettingsForm")
                    {
                        _bBBlog.Debug("activeform: " + BBB.ActiveForm.Name + " has focus so ignoring hotkey");
                        return;
                    }

                //&& BBB.ActiveForm.Name != "SettingsForm"
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

                if (_setHotkeys.Contains(e.KeyCode) && MainRecordingStart.Text == "Recording")
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
            var keyCombo = "";
            for (int i = 0; i < _setHotkeys.Count; i++)
            {
                if (e.KeyCode == _setHotkeys[i])
                {
                    keyCombo += _setHotkeys[i].ToString() + " + ";
                }
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void GithubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //spawn browser for github link
            var t = new Thread(() => Process.Start(new ProcessStartInfo("https://github.com/WhiskerWeirdo/BanterBrain-Buddy/wiki") { UseShellExecute = true }));
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
        private void TwitchEnableCheckbox_Click(object sender, EventArgs e)
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

            if (await _twitchEventSub.EventSubInit(Properties.Settings.Default.TwitchAccessToken, Properties.Settings.Default.TwitchUsername, Properties.Settings.Default.TwitchChannel))
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
                    _bBBlog.Info($"Twitch subscriptions enabled, calling EventSubHandleSubscription: {_twitchEventSub}");
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
                    UpdateTextLog("Twitch EventSub client started successfully\r\n");
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
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            TextToAppend = timestamp + " " + TextToAppend;
            if (!InvokeRequired && TextLog != null)
            {
                try
                {
                    TextLog.AppendText(TextToAppend);
                }
                catch (Exception ex)
                {
                    _bBBlog.Error("Error writing to TextLog: " + ex.Message);
                }
            }
            else if (TextLog != null)
            {
                Invoke(new Action(() =>
                {
                    TextLog.AppendText(TextToAppend);
                }));
            }
            else if (TextLog == null)
            {
                _bBBlog.Error("TextLog is null, cannot write to it");
            }
        }

        //A simple way to invoke UI elements from another thread
        [SupportedOSPlatform("windows6.1")]
        private async Task InvokeUI(Action a)
        {
            await Task.Run(() => this.BeginInvoke(new System.Windows.Forms.MethodInvoker(a)));
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TwitchEventSub_OnESubCheerMessage(object sender, TwitchEventhandlers.OnCheerEventsArgs e)
        {
            await InvokeUI(async () =>
            {
                if (TwitchCheeringSoundCheckBox.Checked)
                {
                    if (TwitchCheeringSoundTextBox.Text.Length > 1)
                    {
                        _bBBlog.Info("Playing alert sound for cheer message");
                        await PlayWaveFile(TwitchCheeringSoundTextBox.Text);
                    }
                }
            });
            string user = e.GetCheerInfo()[0];
            string message = e.GetCheerInfo()[1];
            //we got a valid cheer message, lets see what we can do with it
            _bBBlog.Info("Valid Twitch Cheer message received");
            // bool _TalkDone = false;

            if (TwitchCheeringTTSEverythingRadioButton.Checked)
            {
                await InvokeUI(async () =>
                {
                    UpdateTextLog($"Valid Twitch Cheer message received from {user}\r\n");
                    // await SayText($"Thank you for the bits, {user}!");
                    await SayText($"{user} cheered with message {message}", 2000, GetSelectedPersona(Properties.Settings.Default.TwitchCheeringPersona));
                    // _TalkDone = true;
                });
            }

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
                _bBBlog.Info("LLM response received, saying it");
                await SayText(_gPTOutputText, 0, GetSelectedPersona(Properties.Settings.Default.TwitchCheeringPersona));
                _bBBlog.Info("LLM response said");
                //do we need to post the response in chat?
                if (TwitchResponseToChatCheckBox.Checked)
                {
                    //first lets wait the delay set in the textbox
                    await Task.Delay(int.Parse(TwitchResponseToChatDelayTextBox.Text) * 1000);
                    //then post the response
                    await _twitchEventSub.SendMessage(_gPTOutputText);
                }
            });
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TwitchEventSub_OnESubChannelPointRedemption(object sender, TwitchEventhandlers.OnChannelPointCustomRedemptionEventArgs e)
        {
            await InvokeUI(async () =>
            {
                if (TwitchChannelSoundCheckBox.Checked)
                {
                    if (TwitchChannelSoundTextBox.Text.Length > 1)
                    {
                        _bBBlog.Info("Playing alert sound for channel point redemption message");
                        await PlayWaveFile(TwitchChannelSoundTextBox.Text);
                    }
                }
            });

            string user = e.GetChannelPointCustomRedemptionInfo()[0];
            string message = e.GetChannelPointCustomRedemptionInfo()[1];
            _gPTDone = false;
            _bBBlog.Info($"Valid Twitch Channel Point Redemption message received: {user} redeemed with message: {message}");

            if (TwitchChannelPointTTSEverythingRadioButton.Checked)
            {
                await InvokeUI(async () =>
                {
                    _bBBlog.Info("Lets say a short \"thank you\" for the channel point redemption, and pass the text to the LLM");
                    await SayText($"{user} redeemed with message {message}", 3000, GetSelectedPersona(Properties.Settings.Default.TwitchChannelPointPersona));
                });
            }

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
                _bBBlog.Info("LLM response said");
                //do we need to post the response in chat?
                if (TwitchResponseToChatCheckBox.Checked)
                {
                    //first lets wait the delay set in the textbox
                    await Task.Delay(int.Parse(TwitchResponseToChatDelayTextBox.Text) * 1000);
                    //then post the response
                    await _twitchEventSub.SendMessage(_gPTOutputText);
                }
            });
        }

        [SupportedOSPlatform("windows6.1")]
        //TwitchEventSub_OnESubGiftedSub
        private async void TwitchEventSub_OnESubGiftedSub(object sender, TwitchEventhandlers.OnSubscriptionGiftEventArgs e)
        {
            await InvokeUI(async () =>
            {
                if (TwitchSubscriptionSoundCheckBox.Checked)
                {
                    if (TwitchSubscriptionSoundTextBox.Text.Length > 1)
                    {
                        _bBBlog.Info("Playing alert sound for subscription message");
                        await PlayWaveFile(TwitchSubscriptionSoundTextBox.Text);
                    }
                }
            });

            string user = e.GetSubscriptionGiftInfo()[0];
            string amount = e.GetSubscriptionGiftInfo()[1];
            string tier = e.GetSubscriptionGiftInfo()[2];
            _bBBlog.Info($"Valid Twitch Gifted Subscription message received: {user} gifted {amount} subs tier {tier}");

            if (TwitchSubscriptionTTSEverythingRadioButton.Checked)
            {
                await InvokeUI(async () =>
                {
                    _bBBlog.Info("Lets say a short \"thank you\" for the gifted sub(s)");
                    if (int.Parse(amount) > 1)
                        await SayText($"Thanks {user} for gifting {amount} tier {tier} subs!", 0, GetSelectedPersona(Properties.Settings.Default.TwitchSubscriptionPersona));
                    else
                        await SayText($"Thanks {user} for gifting {amount} tier {tier} sub!", 0, GetSelectedPersona(Properties.Settings.Default.TwitchSubscriptionPersona));
                });
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TwitchEventSub_OnESubSubscribe(object sender, TwitchEventhandlers.OnSubscribeEventArgs e)
        {
            await InvokeUI(async () =>
            {
                if (TwitchSubscriptionSoundCheckBox.Checked)
                {
                    if (TwitchSubscriptionSoundTextBox.Text.Length > 1)
                    {
                        _bBBlog.Info("Playing alert sound for subscription message");
                        await PlayWaveFile(TwitchSubscriptionSoundTextBox.Text);
                    }
                }
            });

            string user = e.GetSubscribeInfo()[0];
            string broadcaster = e.GetSubscribeInfo()[1];
            _bBBlog.Info($"Valid Twitch Subscription message received: {user} subscribed to {broadcaster}");
            if (TwitchSubscriptionTTSEverythingRadioButton.Checked)
            {
                await InvokeUI(async () =>
                {
                    _bBBlog.Info("Lets say a short \"thank you\" for the subscriber");
                    await SayText($"Thanks {user} for subscribing!", 0, GetSelectedPersona(Properties.Settings.Default.TwitchSubscriptionPersona));
                });
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TwitchEventSub_OnESubReSubscribe(object sender, TwitchEventhandlers.OnReSubscribeEventArgs e)
        {
            await InvokeUI(async () =>
            {
                if (TwitchSubscriptionSoundCheckBox.Checked)
                {
                    if (TwitchSubscriptionSoundTextBox.Text.Length > 1)
                    {
                        _bBBlog.Info("Playing alert sound for subscription message");
                        await PlayWaveFile(TwitchSubscriptionSoundTextBox.Text);
                    }
                }
            });

            string user = e.GetSubscribeInfo()[0];
            string message = e.GetSubscribeInfo()[1];
            string months = e.GetSubscribeInfo()[2];
            _gPTDone = false;
            _bBBlog.Info($"Valid Twitch Re-Subscription message received: {user} subscribed for {months} months with message: {message}");

            if (TwitchSubscriptionTTSEverythingRadioButton.Checked)
            {
                await InvokeUI(async () =>
                {
                    if (message.Length <= 1)
                        await SayText($"{user} has resubscribed for a total of months {months}!", 0, GetSelectedPersona(Properties.Settings.Default.TwitchSubscriptionPersona));
                    else
                    {
                        await SayText($"{user} has resubscribed for a total of {months} months saying {message}.", 0, GetSelectedPersona(Properties.Settings.Default.TwitchSubscriptionPersona));
                    }
                });
            }

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
                    await SayText(_gPTOutputText, 0, GetSelectedPersona(Properties.Settings.Default.TwitchSubscriptionPersona));
                    _bBBlog.Info("LLM response said");
                    //do we need to post the response in chat?
                    if (TwitchResponseToChatCheckBox.Checked)
                    {
                        //first lets wait the delay set in the textbox
                        await Task.Delay(int.Parse(TwitchResponseToChatDelayTextBox.Text) * 1000);
                        //then post the response
                        await _twitchEventSub.SendMessage(_gPTOutputText);
                    }
                });
            }
        }


        [SupportedOSPlatform("windows6.1")]
        //eventhandler for valid chat messages trigger
        private async void TwitchEventSub_OnESubChatMessage(object sender, TwitchEventhandlers.OnChatEventArgs e)
        {
            await InvokeUI(async () =>
            {
                if (TwitchChatSoundCheckBox.Checked)
                {
                    if (TwitchChatSoundTextBox.Text.Length > 1)
                    {

                        _bBBlog.Info("Playing alert sound for chat message");
                        await PlayWaveFile(TwitchChatSoundTextBox.Text);
                    }
                }
            });

            string message = e.GetChatInfo()[1].Replace(TwitchCommandTrigger.Text, "");
            string user = e.GetChatInfo()[0];
            //we got a valid chat message, lets see what we can do with it
            _bBBlog.Info("Valid Twitch Chat message received from user: " + user + " message: " + message + " using: " + Properties.Settings.Default.TwitchChatPersona + "\r\n");
            _gPTDone = false;
            //we use InvokeUI to make sure we can write to the textlog from another thread that is not the Ui thread.
            //we only have to say this part if we have to say everything!
            if (TwitchChatTTSEverythingRadioButton.Checked)
            {
                await InvokeUI(async () =>
                {
                    UpdateTextLog("Valid Twitch Chat message received from user: " + user + " message: " + message + ". Using trigger persona: " + Properties.Settings.Default.TwitchChatPersona + "\r\n");
                    await SayText($"{user} said {message}", 3000, GetSelectedPersona(Properties.Settings.Default.TwitchChatPersona));
                });
            }
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
                _bBBlog.Info("LLM response said");
                //do we need to post the response in chat?
                if (TwitchResponseToChatCheckBox.Checked)
                {
                    //first lets wait the delay set in the textbox
                    await Task.Delay(int.Parse(TwitchResponseToChatDelayTextBox.Text) * 1000);
                    //then post the response
                    await _twitchEventSub.SendMessage(_gPTOutputText);
                }

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
                if (TwitchCommandTrigger.Text.Length < 1)
                {
                    MessageBox.Show("You need to set a command trigger to watch for!", "Twitch error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _bBBlog.Error("You need to set a command trigger to watch for!");
                    TwitchReadChatCheckBox.Checked = false;
                    return;
                }
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
                if (TwitchMinBits.Text.Length < 1)
                {
                    MessageBox.Show("You need to set a minimum amount of bits to trigger the bot", "Twitch error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _bBBlog.Error("You need to set a minimum amount of bits to trigger the bot");
                    TwitchCheerCheckBox.Checked = false;
                    return;
                }
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
            TwitchEnableDisableFields();
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
            TwitchEnableDisableFields();
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
            TwitchEnableDisableFields();
        }

        [SupportedOSPlatform("windows6.1")]
        private void BBB_Load(object sender, EventArgs e)
        {
            _bBBlog.Info("BanterBrain Buddy started");
            MessageBox.Show("This is a beta version of BanterBrain Buddy. Please report any bugs to the discord!", "BanterBrain Buddy Beta", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (TwitchCustomRewardName.Text.Length < 1)
                { //no custom reward name set, so we cant do anything
                    MessageBox.Show("You need to set a custom reward name in the settings to enable channel point redemptions", "Twitch error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _bBBlog.Error("You need to set a custom reward name in the settings to enable channel point redemptions");
                    TwitchChannelPointCheckBox.Checked = false;
                    return;
                }
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
            TwitchEnableDisableFields();
        }

        [SupportedOSPlatform("windows6.1")]
        private void SeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _bBBlog.Debug("BanterBrain Buddy leaving main form, saving settings (just in case)");
            SaveALLSettings();
            Unsubscribe();
            if (_twitchEventSub != null)
            {
                //twitch is running so....lets make sure the settings form knows this
                isTwitchRunning = true;
            }
            else
                isTwitchRunning = false;

            SettingsForm ShowSettingsForm = new();
            ShowSettingsForm.FormClosing += BBB_Test_FormClosing;
            ShowSettingsForm.ShowDialog();
        }

        //handle the settings form closing and be sure to reload the new settings
        [SupportedOSPlatform("windows6.1")]
        private async void BBB_Test_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateTextLog("Settings closed. We loaded settings!\r\n");
            _bBBlog.Info("Settings form closed. We should load the new settings!");
            //we should clear the global TTS/STT and reload the possible new API settings
            //we cant do this for twitch though. 
            if (_azureSpeech != null)
                _azureSpeech.AzureAPIKey = Properties.Settings.Default.AzureAPIKeyTextBox;
            if (_elevenLabsApi != null)
                _elevenLabsApi.ElevelLabsAPIKey = Properties.Settings.Default.ElevenLabsAPIkey;
            if (_openAI != null)
                _openAI.OpenAIAPIKey = Properties.Settings.Default.GPTAPIKey;

            LoadPersonas();
            await CheckConfiguredSTTProviders();
            await CheckConfiguredLLMProviders();
            await LoadSettings();
            await SetSelectedSTTProvider();
            SetSelectedLLMProvider();
        }

        private void BBB_VisibleChanged(object sender, EventArgs e)
        {
            _bBBlog.Info("BanterBrain Buddy visible changed. to test if closing form2 is done");
        }

        [SupportedOSPlatform("windows6.1")]
        private void BroadcasterSelectedPersonaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Properties.Settings.Default.MainSelectedPersona != BroadcasterSelectedPersonaComboBox.SelectedItem.ToString())
            {
                _bBBlog.Info("Broadcaster selected persona changed to: " + BroadcasterSelectedPersonaComboBox.SelectedItem.ToString());
                Properties.Settings.Default.MainSelectedPersona = BroadcasterSelectedPersonaComboBox.SelectedItem.ToString();
                Properties.Settings.Default.Save();
            }
        }




        [SupportedOSPlatform("windows6.1")]
        private async void TwitchStartButton_Click(object sender, EventArgs e)
        {
            //lets at least save the settings before we start
            SaveALLSettings();
            if (!_twitchAPIVerified)
            {
                MessageBox.Show("Twitch API key is invalid. Please check the settings.", "Twitch API error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _bBBlog.Error("Twitch API key is invalid. Please check the settings.");
                return;
            }
            //we also need at least one subsription event enabled else its useless
            if (!TwitchReadChatCheckBox.Checked && !TwitchCheerCheckBox.Checked && !TwitchSubscribed.Checked && !TwitchGiftedSub.Checked && !TwitchChannelPointCheckBox.Checked)
            {
                MessageBox.Show("You need to enable at least one event to listen to. Please check the settings.", "Twitch error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                UpdateTextLog("Twitch disabled. Stopping timer and clearing token. Stopping Websocket client.\r\n");
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
                    _twitchEventSub = null;
                }
            }
            TwitchEnableDisableFields();
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
                    TwitchChatTTSEverythingRadioButton.Enabled = false;
                    TwitchChatTTSResponseOnlyRadioButton.Enabled = false;
                }
                else
                {
                    TwitchNeedsSubscriber.Enabled = true;
                    TwitchCommandTrigger.Enabled = true;
                    TwitchChatCommandDelay.Enabled = true;
                    TwitchChatPersonaComboBox.Enabled = true;
                    TwitchChatTTSEverythingRadioButton.Enabled = true;
                    TwitchChatTTSResponseOnlyRadioButton.Enabled = true;
                }

                if (TwitchCheerCheckBox.Checked)
                {
                    _bBBlog.Info("Twitch cheers enabled, disabling settings");
                    TwitchMinBits.Enabled = false;
                    TwitchCheeringPersonaComboBox.Enabled = false;
                    TwitchCheeringTTSEverythingRadioButton.Enabled = false;
                    TwitchCheeringTTSResponseOnlyRadioButton.Enabled = false;
                }
                else
                {
                    TwitchMinBits.Enabled = true;
                    TwitchCheeringPersonaComboBox.Enabled = true;
                    TwitchCheeringTTSEverythingRadioButton.Enabled = true;
                    TwitchCheeringTTSResponseOnlyRadioButton.Enabled = true;
                }

                if (TwitchSubscribed.Checked || TwitchGiftedSub.Checked)
                {
                    _bBBlog.Info("Twitch subscriptions enabled, disabling settings");
                    TwitchSubscriptionPersonaComboBox.Enabled = false;
                    TwitchSubscriptionTTSEverythingRadioButton.Enabled = false;
                    TwitchSubscriptionTTSResponseOnlyRadioButton.Enabled = false;
                }
                else
                {
                    TwitchSubscriptionPersonaComboBox.Enabled = true;
                    TwitchSubscriptionTTSEverythingRadioButton.Enabled = true;
                    TwitchSubscriptionTTSResponseOnlyRadioButton.Enabled = true;
                }

                if (TwitchChannelPointCheckBox.Checked)
                {
                    _bBBlog.Info("Twitch channel points enabled, disabling settings");
                    TwitchChannelPointPersonaComboBox.Enabled = false;
                    TwitchCustomRewardName.Enabled = false;
                    TwitchChannelPointTTSEverythingRadioButton.Enabled = false;
                    TwitchChannelPointTTSResponseOnlyRadioButton.Enabled = false;
                }
                else
                {
                    TwitchChannelPointPersonaComboBox.Enabled = true;
                    TwitchCustomRewardName.Enabled = true;
                    TwitchChannelPointTTSEverythingRadioButton.Enabled = true;
                    TwitchChannelPointTTSResponseOnlyRadioButton.Enabled = true;
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
                TwitchChannelPointTTSEverythingRadioButton.Enabled = true;
                TwitchChannelPointTTSResponseOnlyRadioButton.Enabled = true;
                TwitchSubscriptionTTSEverythingRadioButton.Enabled = true;
                TwitchSubscriptionTTSResponseOnlyRadioButton.Enabled = true;
                TwitchCheeringTTSEverythingRadioButton.Enabled = true;
                TwitchCheeringTTSResponseOnlyRadioButton.Enabled = true;
                TwitchChatTTSEverythingRadioButton.Enabled = true;
                TwitchChatTTSResponseOnlyRadioButton.Enabled = true;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchEnableCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            //first we check if theres actually info in the API settings or else lets not even bother
            if (Properties.Settings.Default.TwitchAccessToken.Length < 1 && Properties.Settings.Default.TwitchChannel.Length < 1 && Properties.Settings.Default.TwitchUsername.Length < 1)
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
                TwitchSoundsSettings.Enabled = true;
                TwitchResponseSettings.Enabled = true;
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
                TwitchSoundsSettings.Enabled = false;
                TwitchResponseSettings.Enabled = false;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchChatPersonaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BBBTabs.SelectedTab.Name.Equals("StreamingSettingsTab"))
            {
                _bBBlog.Info("Twitch chat persona changed to: " + TwitchChatPersonaComboBox.Text);
                Properties.Settings.Default.TwitchChatPersona = TwitchChatPersonaComboBox.Text;
                Properties.Settings.Default.Save();
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchSubscriptionPersonaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BBBTabs.SelectedTab.Name.Equals("StreamingSettingsTab"))
            {
                _bBBlog.Info("Twitch chat persona changed to: " + TwitchSubscriptionPersonaComboBox.Text);
                Properties.Settings.Default.TwitchChatPersona = TwitchSubscriptionPersonaComboBox.Text;
                Properties.Settings.Default.Save();
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchCheeringPersonaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BBBTabs.SelectedTab.Name.Equals("StreamingSettingsTab"))
            {
                _bBBlog.Info("Twitch chat persona changed to: " + TwitchCheeringPersonaComboBox.Text);
                Properties.Settings.Default.TwitchChatPersona = TwitchCheeringPersonaComboBox.Text;
                Properties.Settings.Default.Save();
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchChannelPointPersonaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BBBTabs.SelectedTab.Name.Equals("StreamingSettingsTab"))
            {
                _bBBlog.Info("Twitch chat persona changed to: " + TwitchChannelPointPersonaComboBox.Text);
                Properties.Settings.Default.TwitchChatPersona = TwitchChannelPointPersonaComboBox.Text;
                Properties.Settings.Default.Save();
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void LLMResponseSelecter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.SelectedLLM != LLMResponseSelecter.Text)
            {
                _bBBlog.Info("Selected LLM changed to: " + LLMResponseSelecter.Text);
                Properties.Settings.Default.SelectedLLM = LLMResponseSelecter.Text;
                Properties.Settings.Default.Save();
            }

        }

        [SupportedOSPlatform("windows6.1")]
        private static void AddFilesToDropdown(ComboBox box)
        {
            string soundDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/sounds";
            string[] files = Directory.GetFiles(soundDir, "*.wav");
            foreach (string file in files)
            {
                box.Items.Add(Path.GetFileName(file));
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchChatSoundTextBox_Click(object sender, EventArgs e)
        {
            TwitchChatSoundTextBox.Items.Clear();
            AddFilesToDropdown(TwitchChatSoundTextBox);

        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchChatSoundCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (TwitchChatSoundCheckBox.Checked)
            {
                TwitchChatSoundTextBox.Enabled = true;
            }
            else
            {
                TwitchChatSoundTextBox.Enabled = false;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchChannelSoundCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (TwitchChannelSoundCheckBox.Checked)
            {
                TwitchChannelSoundTextBox.Enabled = true;
            }
            else
            {
                TwitchChannelSoundTextBox.Enabled = false;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchCheeringSoundCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (TwitchCheeringSoundCheckBox.Checked)
            {
                TwitchCheeringSoundTextBox.Enabled = true;
            }
            else
            {
                TwitchCheeringSoundTextBox.Enabled = false;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchSubscriptionSoundCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (TwitchSubscriptionSoundCheckBox.Checked)
            {
                TwitchSubscriptionSoundTextBox.Enabled = true;
            }
            else
            {
                TwitchSubscriptionSoundTextBox.Enabled = false;
            }
        }

        private void TwitchChatSoundSelectButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\sounds");
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchResponseToChatCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (TwitchResponseToChatCheckBox.Checked)
            {
                TwitchResponseToChatDelayTextBox.Enabled = true;
                Properties.Settings.Default.TwitchResponseToChatCheckBox = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                TwitchResponseToChatDelayTextBox.Enabled = false;
                Properties.Settings.Default.TwitchResponseToChatCheckBox = false;
                Properties.Settings.Default.Save();
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchChatCommandDelay_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the input is not a digit or control (like backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;  // Ignore the input
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchResponseToChatDelayTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the input is not a digit or control (like backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;  // Ignore the input
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchMinBits_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the input is not a digit or control (like backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;  // Ignore the input
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchResponseToChatDelayTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox currenttb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(currenttb.Text))
            {
                MessageBox.Show("This field cannot be empty");
                e.Cancel = true;  // Cancel the event and keep the focus on the TextBox
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchMinBits_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox currenttb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(currenttb.Text))
            {
                MessageBox.Show("This field cannot be empty");
                e.Cancel = true;  // Cancel the event and keep the focus on the TextBox
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchChatCommandDelay_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox currenttb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(currenttb.Text))
            {
                MessageBox.Show("This field cannot be empty");
                e.Cancel = true;  // Cancel the event and keep the focus on the TextBox
            }
            else
            {
                Properties.Settings.Default.TwitchChatCommandDelay = int.Parse(currenttb.Text);
                Properties.Settings.Default.Save();
            }

        }

        private void LogfileDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _bBBlog.Debug("Opening log directory: " + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\logs");
            Process.Start("explorer.exe", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\logs");
        }

        [SupportedOSPlatform("windows6.1")]
        private void STTSelectedComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.STTSelectedProvider != STTSelectedComboBox.Text)
            {
                _bBBlog.Info("STT provider changed to: " + STTSelectedComboBox.Text);
                Properties.Settings.Default.STTSelectedProvider = STTSelectedComboBox.Text;
                Properties.Settings.Default.Save();
            }
        }


        [SupportedOSPlatform("windows6.1")]
        private void BBBTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            _bBBlog.Debug("Tab changed to: " + BBBTabs.SelectedTab.Name);
            _bBBlog.Debug("Leaving Main Window settings, saving");
            SaveALLSettings();
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchDelayMessageTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox currenttb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(currenttb.Text))
            {
                MessageBox.Show("This field cannot be empty");
                e.Cancel = true;  // Cancel the event and keep the focus on the TextBox
            } else
            {
                Properties.Settings.Default.TwitchDelayMessageTextBox = currenttb.Text;
                Properties.Settings.Default.Save();
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchDelayFinishToChatcCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (TwitchDelayFinishToChatcCheckBox.Checked)
            {
                TwitchDelayMessageTextBox.Enabled = true;
                Properties.Settings.Default.DelayFinishToChatcCheckBox = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                TwitchDelayMessageTextBox.Enabled = false;
                Properties.Settings.Default.DelayFinishToChatcCheckBox = false;
                Properties.Settings.Default.Save();
            }
        }
    }
}
