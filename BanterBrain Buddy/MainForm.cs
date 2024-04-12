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

using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Reflection;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;
using SpeechSynthesizer = System.Speech.Synthesis.SpeechSynthesizer;
using System.Globalization;

using Microsoft.Extensions.Logging;
using TwitchLib.Communication.Interfaces;
using TwitchLib.Client.Events;
using TwitchLib.Api;
using System.Runtime.Versioning;

namespace BanterBrain_Buddy
{
    public partial class BBB : Form
    {
        //set logger
        private static readonly log4net.ILog BBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //PTT hotkey hook
        private IKeyboardMouseEvents m_GlobalHook;

        //used for PTT checking
        private bool HotkeyCalled = false;
        // check if SST is finished yet
        private bool STTDone = false;

        [SupportedOSPlatform("windows6.1")]
        //Hotkey Storage
        readonly private List<Keys> SetHotkeys = [];

        //check if the GPT LLM is donestop audio capture
        private bool GPTDone = false;
        //error checker
        private bool BigError = false;

        //Global Twitch API class
        //we need this for the hourly /validate check
        private TwitchAPI GlobalTwitchAPI;
        private bool TwitchValidateCheckStarted;

        [SupportedOSPlatform("windows6.1")]
        public BBB()
        {
            TwitchValidateCheckStarted = false;

            InitializeComponent();
            LoadSettings();
            GetAudioDevices();

            BBBlog.Info("Program Starting...");
            BBBlog.Info("PPT hotkey: " + MicrophoneHotkeyEditbox.Text);
            TextLog.AppendText("Program Starting...");
            TextLog.AppendText("PPT hotkey: " + MicrophoneHotkeyEditbox.Text + "\r\n");
            if (TwitchEnableCheckbox.Checked && TwitchCheckAuthAtStartup.Checked)
                SetTwitchValidateTokenTimer();
            else
                TwitchCheckAuthAtStartup.Enabled = false;
        }

        [SupportedOSPlatform("windows6.1")]
        /// <summary>
        public async void SetTwitchValidateTokenTimer()
        {
            if (!TwitchValidateCheckStarted && TwitchEnableCheckbox.Checked && TwitchUsername.Text.Length > 0 && TwitchAccessToken.Text.Length > 0 && TwitchChannel.Text.Length > 0)
            {
                GlobalTwitchAPI = new();
                var result = await GlobalTwitchAPI.ValidateAccessToken(TwitchAccessToken.Text);
                if (!result)
                {
                    BBBlog.Error("Twitch access token is invalid, please re-authenticate");
                    MessageBox.Show("Twitch access token is invalid, please re-authenticate", "Twitch Auth error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TextLog.Text += "Twitch access token is invalid, please re-authenticate\r\n";
                    TwitchValidateCheckStarted = false;
                    TwitchStatusTextBox.Text = "DISABLED";
                    BigError = true;
                }
                else
                {
                    BBBlog.Info("Twitch access token is valid. Starting automated /validate call");
                    TextLog.Text += "Twitch access token is valid. Starting automated /validate call\r\n";
                    TwitchValidateCheckStarted = true;
                    TwitchStatusTextBox.Text = "ENABLED";
                    //if we are good, start the hourly check
                    await GlobalTwitchAPI.CheckHourlyAccessToken();
                }

            }
        }

        [SupportedOSPlatform("windows6.1")]
        //fill the audio input and output list boxes
        public void GetAudioDevices()
        {
            var CaptureDevices = WaveInDevice.EnumerateDevices();
            foreach (var device in CaptureDevices)
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

            String SelectedProvider = STTProviderBox.GetItemText(STTProviderBox.SelectedItem);
            if (STTTestButton.Text == "Test")
            {
                STTTestOutput.Text = "";
                TextLog.AppendText("Test Microphone on\r\n");
                BBBlog.Info("Test Microphone on");
                STTTestButton.Text = "Recording";
                TextLog.AppendText(SelectedProvider + "\r\n");

                STTDone = false;
                BigError = false;
                if (SelectedProvider == "Native")
                {
                    TextLog.AppendText("Test Native STT calling\r\n");
                    BBBlog.Info("Test Native STT calling");
                    NativeInputStreamtoWav();
                    while (!STTDone)
                    {
                        await Task.Delay(500);
                    }
                }
                else if (SelectedProvider == "Azure")
                {
                    TextLog.AppendText("Test Azure STT calling\r\n");
                    BBBlog.Info("Test Azure STT calling");
                    //cant be empty

                    if ((STTAPIKeyEditbox.Text.Length < 1) || (STTRegionEditbox.Text.Length < 1))
                    {
                        STTTestOutput.Text = "Error! API Key or region cannot be empty!";
                        BBBlog.Error("Error! API Key or region cannot be empty!");
                        STTTestButton.Text = "Test";
                    }
                    else
                    {
                        AzureConvertVoicetoText();
                        while (!STTDone && !BigError)
                        {
                            await Task.Delay(500);
                        }

                    }
                }
            }
            else
            {
                STTTestButton.Text = "Test";
                TextLog.AppendText("Test stopped recording\r\n");
                BBBlog.Info("Test stopped recording");
                STTTestOutput.BackColor = SystemColors.Control;
                if (SelectedProvider == "Native")
                    StopWavCapture();
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void STTProviderBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String SelectedProvider = STTProviderBox.GetItemText(STTProviderBox.SelectedItem);
            if (SelectedProvider == "Native")
            {
                TextLog.AppendText("Native STT selected\r\n");
                BBBlog.Info("Native STT selected");
                STTAPIKeyEditbox.Enabled = false;
                STTRegionEditbox.Enabled = false;
                STTTestOutput.Text = "Hint: For better native Speech-To-Text always train your voice at least once in Control Panel\\Ease of Access\\Speech Recognition";
            }
            else if (SelectedProvider == "Azure")
            {
                TextLog.AppendText("Azure STT selected\r\n");
                BBBlog.Info("Azure STT selected");
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
            BBBlog.Info("Azure STT microphone start.");
            while ((STTTestButton.Text == "Recording" || MainRecordingStart.Text == "Recording") && !STTDone && !BigError)
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
                    BigError = true;
                    STTDone = true;
                }
                else
                {
                    STTTestOutput.Text += recognizeResult + "\r\n";
                    TextLog.Text += "Azure Speech-To-Text: " + recognizeResult + "\r\n";
                }
            }
            STTDone = true;
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
        readonly private string tmpWavFile = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\tmp.wav";

        [SupportedOSPlatform("windows6.1")]
        private void NativeInputStreamtoWav()
        {

            SetSelectedInputDevice();
            BBBlog.Info("Selected audio input device for Native: " + SelectedInputDevice);
            _soundIn = new WasapiCapture() { Device = SelectedInputDevice };
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
            BBBlog.Info("Native STT microphone start.");
        }

        [SupportedOSPlatform("windows6.1")]
        private void StopWavCapture()
        {
            TextLog.AppendText("Stopping capture to WAV file\r\n");
            BBBlog.Info("Stopping capture to WAV file");

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

        [SupportedOSPlatform("windows6.1")]
        private async void NativeSTTfromWAV()
        {
            // Create an in-process speech recognizer for the en-US locale.  
            SpeechRecognitionEngine recognizer2 = new(new System.Globalization.CultureInfo("en-US"));
            // Create and load a dictation grammar.  
            recognizer2.LoadGrammar(new DictationGrammar());
            recognizer2.SetInputToWaveFile(tmpWavFile);
            // Attach event handlers for the results of recognition.  
            recognizer2.SpeechRecognized +=
              new EventHandler<SpeechRecognizedEventArgs>(NativeSpeechRecognized);
            recognizer2.RecognizeCompleted +=
              new EventHandler<RecognizeCompletedEventArgs>(NativeRecognizeCompletedHandler);

            TextLog.AppendText("Starting asynchronous Native recognition... on " + tmpWavFile + "\r\n");
            BBBlog.Info("Starting asynchronous Native recognition... on " + tmpWavFile);

            STTDone = false;
            recognizer2.RecognizeAsync(RecognizeMode.Multiple);
            while (!STTDone)
            {
                await Task.Delay(1000);
            }
            TextLog.AppendText("Native STT done.\r\n");
            BBBlog.Info("Native STT done.");
            recognizer2.Dispose();
        }

        [SupportedOSPlatform("windows6.1")]
        // Handle the SpeechHypothesized event.  
        private void NativeSpeechHypothesizedHandler(object sender, SpeechHypothesizedEventArgs e)
        {
            TextLog.AppendText(" In SpeechHypothesizedHandler:+\r\n");
            BBBlog.Info("in hypothesishandler");
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
                TextLog.AppendText("Native STT Error encountered, " + e.Error.GetType().Name + " : " + e.Error.Message + "\r\n");
                BBBlog.Error("Native STT Error encountered, " + e.Error.GetType().Name + " : " + e.Error.Message);

            }
            if (e.Cancelled)
            {
                TextLog.AppendText("Native STT Operation cancelled\r\n");
                BBBlog.Info("Native STT Operation cancelled");
            }
            if (e.InputStreamEnded)
            {
                TextLog.AppendText("Native STT recognize Stopped.\r\n");
                BBBlog.Info("Mative STT recognize Stopped.");
            }

            STTDone = true;
        }

        [SupportedOSPlatform("windows6.1")]
        // Handle the SpeechRecognized event.  
        private void NativeSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result != null && e.Result.Text != null)
            {
                TextLog.AppendText("Native recognized text: " + e.Result.Text + "\r\n");
                BBBlog.Info("Native recognized text: " + e.Result.Text);
                STTTestOutput.AppendText(e.Result.Text + "\r\n");
            }
            else
            {
                STTTestOutput.AppendText("Native recognized text not available.");
                BBBlog.Info("Native recognized text not available.");
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void NativeSpeechDetectedHandler(object sender, SpeechDetectedEventArgs e)
        {
            TextLog.AppendText(" In NativeSpeechDetectedHandler:\r\n");
            TextLog.AppendText(" - AudioPosition = " + e.AudioPosition + "\r\n");
            BBBlog.Info(" In NativeSpeechDetectedHandler: ");
            BBBlog.Info(" - AudioPosition = \" + e.AudioPosition");
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TalkToOpenAIGPT(String UserInput)
        {
            TextLog.AppendText("Sending to GPT: " + UserInput + "\r\n");
            BBBlog.Info("Sending to GPT: " + UserInput);
            GPTDone = false;
            OpenAIAPI api = new(LLMAPIKeyTextBox.Text);
            var chat = api.Chat.CreateConversation();
            chat.Model = Model.ChatGPTTurbo;
            chat.RequestParameters.Temperature = 0;
            chat.RequestParameters.MaxTokens = 100;

            //mood is setting the system text description
            TextLog.AppendText("SystemRole: " + LLMRoleTextBox.Text + "\r\n");
            BBBlog.Info("SystemRole: " + LLMRoleTextBox.Text);
            chat.AppendSystemMessage(LLMRoleTextBox.Text);

            chat.AppendUserInput(UserInput);
            try
            {
                TextLog.AppendText("ChatGPT response: ");
                BBBlog.Info("ChatGPT response: ");
                await chat.StreamResponseFromChatbotAsync(res =>
                 {
                     TextLog.AppendText(res);
                     LLMTestOutputbox.AppendText(res);
                 });
                BBBlog.Info(LLMTestOutputbox.Text);
                TextLog.AppendText("\r\nGPT Response done\r\n");
                BBBlog.Info("GPT Response done");
                GPTDone = true;
            }
            catch (System.Security.Authentication.AuthenticationException ex)
            {
                MessageBox.Show(ex.Message, "GPT API Auth error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BBBlog.Error("GPT API Auth error: " + ex.Message);
            }

        }

        [SupportedOSPlatform("windows6.1")]
        private void GPTTestButton_Click(object sender, EventArgs e)
        {
            LLMTestOutputbox.Text = "";
            GPTTestButton.Enabled = false;
            GPTTestButton.Text = "Wait...";
            if (LLMProviderComboBox.Text == "OpenAI ChatGPT")
            {
                TextLog.AppendText("Testing using ChatGPT\r\n");
                BBBlog.Info("Testing using ChatGPT");
                TalkToOpenAIGPT("How are you?");
            }
            GPTTestButton.Text = "Test";
            GPTTestButton.Enabled = true;
        }

        [SupportedOSPlatform("windows6.1")]

        //output to the selected audio device
        private void OutputStream(MemoryStream stream)
        {
            //var Devices = WaveOutDevice.EnumerateDevices();
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
        List<AzureVoices> AzureRegionVoicesList = [];

        [SupportedOSPlatform("windows6.1")]
        /// <summary>
        /// This function gets the list of voices available in the Azure TTS API
        /// </summary>
        private async Task TTSGetAzureVoices()
        {
            //only bother if the two fields are not empty or not "placeholder"
            if ((TTSAPIKeyTextBox.Text.Length < 1) || (TTSRegionTextBox.Text.Length < 1) || TTSAPIKeyTextBox.Text == "placeholder" || TTSRegionTextBox.Text == "placeholder")
            {
                BBBlog.Error("API Key or region cannot be empty!");
                MessageBox.Show("API Key or region cannot be empty!", "Azure TTS error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BigError = true;
                return;
            }
            BBBlog.Info("Finding TTS Azure voices available");
            AzureSpeechAPI AzureSpeech = new(TTSAPIKeyTextBox.Text, TTSRegionTextBox.Text, "en-US");
            AzureRegionVoicesList = await AzureSpeech.TTSGetAzureVoices();

            if (AzureRegionVoicesList == null)
            {
                MessageBox.Show("Problem retreiving Azure API voicelist. Is your API key or subscription information still valid?", "Azure No voices", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BigError = true;
            }
            else
            {
                BBBlog.Info($"Found {AzureRegionVoicesList.Count} voices");
            }
        }

        [SupportedOSPlatform("windows6.1")]
        /// <summary>
        /// This function fills the Azure voice list in the GUI TTSOutputVoice
        /// </summary>
        private void TTSFillAzureVoicesList()
        {
            BBBlog.Info("Fill Azure voice list");

            // Locale, Gender, Localname
            TTSOutputVoice.Items.Clear();
            foreach (var AzureRegionVoice in AzureRegionVoicesList)
            {
                TTSOutputVoice.Items.Add(AzureRegionVoice.LocaleDisplayname + "-" + AzureRegionVoice.Gender + "-" + AzureRegionVoice.LocalName);
            }
            TTSOutputVoice.Sorted = true;
            //if we dont have a real value (i.e. teh first startup placeholders) we need to set it to the first item
            if (TTSOutputVoice.Text == "placeholder")
                TTSOutputVoice.Text = TTSOutputVoice.Items[0].ToString();
            TTSOutputVoiceOptions.Text = "";

            TTSAzureFillOptions(TTSOutputVoice.Text);
        }

        [SupportedOSPlatform("windows6.1")]
        /// <summary>
        /// This function fills the Azure voice options in the GUI TTSOutputVoiceOptions, depends on the selected voice
        /// </summary>
        private void TTSAzureFillOptions(string SelectedVoice)
        {
            BBBlog.Info("Finding Azure voice options (if available)");
            TTSOutputVoiceOptions.Items.Clear();
            //the voice is the item in TTSOutputVoice 
            //now to find it in AzureRegionVoicesList
            foreach (var AzureRegionVoice in AzureRegionVoicesList)
            {
                if (SelectedVoice == (AzureRegionVoice.LocaleDisplayname + "-" + AzureRegionVoice.Gender + "-" + AzureRegionVoice.LocalName))
                {
                    BBBlog.Info("Match found, checking for voice options");
                    foreach (var voiceOption in AzureRegionVoice.StyleList)
                    {
                        if (voiceOption.Length > 0)
                            TTSOutputVoiceOptions.Items.Add(voiceOption);
                        else
                            TTSOutputVoiceOptions.Items.Add("Default");
                    }
                }
            }
            //if nothing ends up being selected, pick the top one so at least something is selected
            if (TTSOutputVoiceOptions.SelectedIndex == -1)
            {
                try
                {
                    TTSOutputVoiceOptions.Text = TTSOutputVoiceOptions.Items[0].ToString();
                }
                catch (Exception ex)
                {
                    BBBlog.Error("Issue assigning Azure voice. Error: " + ex.Message);
                }
            }

        }

        [SupportedOSPlatform("windows6.1")]
        //Azure Text-To-Speach
        private async void TTSAzureSpeakToOutput(string TextToSpeak)
        {
            BBBlog.Info("Azure TTS called with text, seting up");
            AzureSpeechAPI azureSpeechAPI = new(STTAPIKeyEditbox.Text, STTRegionEditbox.Text, STTLanguageComboBox.Text);

            //set the output voice, gender and locale, and the style
            await azureSpeechAPI.AzureTTSInit(TTSOutputVoice.Text, TTSOutputVoiceOptions.Text, TTSAudioOutputComboBox.Text);

            var result = await azureSpeechAPI.AzureSpeak(TextToSpeak);
            if (!result)
            {
                BBBlog.Error("Azure TTS error. Is there a problem with your API key or subscription?");
                MessageBox.Show("Azure TTS error. Is there a problem with your API key or subscription?", "Azure TTS error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BigError = true;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TTSNativeSpeakToOutput(String TTSText)
        {
            TextLog.AppendText("Saying text with Native TTS\r\n");
            BBBlog.Info("Saying text with Native TTS");
            //using the full name to make sure we dont confuse native with Azure
            SpeechSynthesizer synthesizer = new();
            var stream = new MemoryStream();
            synthesizer.SetOutputToWaveStream(stream);
            synthesizer.Speak(TTSText);

            int deviceID = 0;
            foreach (var device in WaveOutDevice.EnumerateDevices())
            {
                if (device.Name == TTSAudioOutputComboBox.Text)
                {
                    deviceID = device.DeviceId;
                }
            }
            var waveOut = new WaveOut { Device = new WaveOutDevice(deviceID) };
            using var waveSource = new MediaFoundationDecoder(stream);
            waveOut.Initialize(waveSource);
            waveOut.Play();
            while (waveOut.PlaybackState != PlaybackState.Stopped)
            {
                await Task.Delay(500);
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TTSTestButton_Click(object sender, EventArgs e)
        {
            if (TTSProviderComboBox.Text == "Native")
            {
                TTSNativeSpeakToOutput(TTSTestTextBox.Text);
            }
            else if (TTSProviderComboBox.Text == "Azure")
            {
                //we also need to fill the list and select the first available voice
                //then the options
                if (TTSOutputVoice.Items.Count < 1 || TTSOutputVoice.Text == "placeholder")
                {
                    await TTSGetAzureVoices();
                    if (BigError)
                    {
                        return;
                    }
                    TTSFillAzureVoicesList();
                }
                TTSAzureSpeakToOutput(TTSTestTextBox.Text);
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void MainRecordingStart_Click(object sender, EventArgs e)
        {
            BigError = false;
            String SelectedProvider = STTProviderBox.GetItemText(STTProviderBox.SelectedItem);
            //first, lets call STT
            STTDone = false;
            if (MainRecordingStart.Text == "Start")
            {
                STTTestOutput.Text = "";
                MainRecordingStart.Text = "Recording";
                if (SelectedProvider == "Native")
                {
                    TextLog.AppendText("Main button Native STT calling\r\n");
                    BBBlog.Info("Main button Native STT calling");
                    NativeInputStreamtoWav();
                    while (!STTDone)
                    {
                        await Task.Delay(500);
                    }
                }
                else if (SelectedProvider == "Azure")
                {
                    TextLog.AppendText("Azure STT calling\r\n");
                    BBBlog.Info("Azure STT calling");
                    //cant be empty

                    if ((STTAPIKeyEditbox.Text.Length < 1) || (STTRegionEditbox.Text.Length < 1))
                    {
                        TextLog.Text = "Error! API Key or region cannot be empty!\r\n";
                        BBBlog.Error("Error! API Key or region cannot be empty!");
                        MainRecordingStart.Text = "Start";
                    }
                    else
                    {
                        AzureConvertVoicetoText();
                        while (!STTDone)
                        {
                            await Task.Delay(500);
                        }

                    }
                }

                //if BigError is true, stop! something is very wrong.
                if (BigError)
                {
                    TextLog.AppendText("Theres an error, stopping execution!\r\n");
                    BBBlog.Error("Theres an error, stopping execution");
                    MainRecordingStart.Text = "Start";
                    return;
                }

                Thread.Sleep(500);
                //now the STT text is in STTTestOutput.Text, lets pass that to ChatGPT
                if (STTTestOutput.Text.Length > 1)
                {
                    LLMTestOutputbox.Text = "";
                    if (LLMProviderComboBox.Text == "OpenAI ChatGPT")
                    {
                        TextLog.AppendText("Using ChatGPT\r\n");
                        BBBlog.Info("Using ChatGPT");
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
                    else if (TTSProviderComboBox.Text == "Azure")
                    {
                        TTSAzureSpeakToOutput(LLMTestOutputbox.Text);
                    }
                }
                else
                {
                    TextLog.AppendText("No audio recorded");
                    BBBlog.Info("No audio recorded");
                }
            }
            else
            {
                MainRecordingStart.Text = "Start";
                if (SelectedProvider == "Native")
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
            TTSOutputVoiceOptions.Enabled = Properties.Settings.Default.TTSAudioVoiceOptionsEnabled;
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
            //load HotkeyList into SetHotKeys
            /* foreach (String key in Properties.Settings.Default.HotkeyList)
             {
                 Keys tmpKey = (Keys)Enum.Parse(typeof(Keys), key, true);
                 SetHotkeys.Add(tmpKey);
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
                        if (BigError)
                        {
                            return;
                        }
                        //fill the listboxes
                        TTSFillAzureVoicesList();
                    }
                }
            }
            //this last so it overwrites possibly loaded voice options
            TTSOutputVoiceOptions.Text = Properties.Settings.Default.TTSAudioVoiceOptions;
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

            /* //add the hotkeys in settings list, not in text
             Properties.Settings.Default.HotkeyList.Clear();
             foreach (Keys key in SetHotkeys)
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

            HotkeyForm HotkeyDialog = new();
            var result = HotkeyDialog.ShowDialog(this);
            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (result == DialogResult.OK && HotkeyDialog.ReturnValue1 != null)
            {
                SetHotkeys.Clear();
                this.MicrophoneHotkeyEditbox.Text = "";
                List<Keys> HotKeys = HotkeyDialog.ReturnValue1;
                //ok now we got the keys, parse them and put them in the index box
                // and the global list for hotkeys

                for (var i = 0; i < HotKeys.Count; i++)
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
            BBBlog.Info("Hotkey set to " + MicrophoneHotkeyEditbox.Text);
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
            if (!HotkeyCalled)
            {
                if (MainRecordingStart.Text == "Start")
                {
                    MainRecordingStart_Click(null, null);
                    HotkeyCalled = true;
                    //ok lets wait 1 sec before checking shit again
                    await Task.Delay(1000);
                }
            }
        }

        [SupportedOSPlatform("windows6.1")]
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
                    MainRecordingStart_Click(null, null);
                    await Task.Delay(1000);
                    HotkeyCalled = false;
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
            TwitchAPI TwAPITest = new();

            //check to see if we need to send a message on join
            if (TwitchSendTextCheckBox.Checked)
            {
                TwAPITest.TwitchSendTestMessageOnJoin = TwitchTestSendText.Text;
            }

            //we need the username AND channel name to get the broadcasterid which is needed for sending a message via the API
            //we need both since the username of the bot and teh channel it joins can be different.
            var VerifyOk = await TwAPITest.CheckAuthCodeAPI(TwitchAccessToken.Text, TwitchUsername.Text, TwitchChannel.Text);
            if (!VerifyOk)
            {
                BBBlog.Error("Problem verifying Access token, something is wrong with the access token!");
                TextLog.Text += "Problem verifying Access token, invalid access token\r\n";
                MessageBox.Show("Problem verifying Access token, invalid access token", "Twitch Access Token veryfication result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TwitchTestButton.Enabled = true;

                //if the token is invalid, lets disable the checkboxes
                TwitchEnableCheckbox.Checked = false;
                if (TwitchCheckAuthAtStartup.Checked)
                    TwitchCheckAuthAtStartup.Checked = false;
                return;
            }
            else
            {
                BBBlog.Info($"Twitch Access token verified success!");
                MessageBox.Show($"Twitch Access token verified success!", "Twitch Access Token verification result", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            TwitchAPI twitchAPI = new();
            var TwitchAPIResult = await twitchAPI.GetTwitchAuthToken(["chat:read", "whispers:read", "whispers:edit", "chat:edit", "user:write:chat"]);

            if (!TwitchAPIResult)
            {
                BBBlog.Error("Issue with getting auth token. Check logs for more information.");
                MessageBox.Show($"Issue with getting Auth token. Check logs for more information.", "Twitch Authorization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                TwitchAccessToken.Text = twitchAPI.TwitchAuthToken;
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
                TTSOutputVoiceOptions.Enabled = false;
                TTSRegionTextBox.Enabled = false;

            }
            else if (TTSProviderComboBox.Text == "Azure")
            {
                TTSAPIKeyTextBox.Enabled = true;
                TTSAudioOutputComboBox.Enabled = true;
                TTSOutputVoice.Enabled = true;
                TTSOutputVoiceOptions.Enabled = true;
                TTSRegionTextBox.Enabled = true;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        //if we change the region box, lets make sure we till have the right voices
        private async void TTSRegionTextBox_Leave(object sender, EventArgs e)
        {
            BBBlog.Info("Region edit box exited");
            if (TTSAPIKeyTextBox.Text.Length > 0 && TTSRegionTextBox.Text.Length > 0)
            {
                await TTSGetAzureVoices();
                if (BigError)
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
            BBBlog.Info("Selected input device changed to " + SoundInputDevices.Text);
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
            BBBlog.Debug("Twitch check auth at startup changed to " + TwitchCheckAuthAtStartup.Checked);
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchEnableCheckbox_Click(object sender, EventArgs e)
        {
            //if the checkbox is checked, lets enable the timer to check the token every hour
            if (TwitchEnableCheckbox.Checked)
            {
                SetTwitchValidateTokenTimer();
                TwitchCheckAuthAtStartup.Enabled = true;
            }
            else
            { //turning off Twitch
                BBBlog.Info("Twitch disabled. Stopping timer and clearing token");
                if (GlobalTwitchAPI != null)
                {
                    GlobalTwitchAPI.StopHourlyAccessTokenCheck();
                    GlobalTwitchAPI = null;
                    TwitchStatusTextBox.Text = "DISABLED";
                }
            }
            BBBlog.Debug("Twitch enable checkbox changed to " + TwitchEnableCheckbox.Checked);
        }
    }
}
