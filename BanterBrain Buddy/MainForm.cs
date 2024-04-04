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
using CSCore.CoreAudioAPI;
using CSCore.Streams;
using CSCore.Codecs.WAV;
using System.Threading;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Reflection;
using TwitchLib.Communication.Interfaces;
using TwitchLib.Api.Helix.Models.Search;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;
using SpeechSynthesizer = System.Speech.Synthesis.SpeechSynthesizer;
using static System.Net.Mime.MediaTypeNames;
using OpenAI_API.Moderation;
using System.ComponentModel;
using System.Globalization;

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

            BBBlog.Info("Program Starting...");
            BBBlog.Info("PPT hotkey: " + MicrophoneHotkeyEditbox.Text);
            TextLog.AppendText("Program Starting...");
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
            String SelectedProvider = STTProviderBox.GetItemText(STTProviderBox.SelectedItem);
            if (STTTestButton.Text == "Test")
            {
                STTTestOutput.Text = "";
                TextLog.AppendText("Test Microphone on\r\n");
                BBBlog.Info("Test Microphone on");
                STTTestButton.Text = "Recording";
                TextLog.AppendText(SelectedProvider + "\r\n");

                STTDone = false;
                if (SelectedProvider == "Native")
                {
                    TextLog.AppendText("Test Native STT calling\r\n");
                    BBBlog.Info("Test Native STT calling");
                    NativeInputStreamtoWav();
                    while (!STTDone)
                    {
                        await Task.Delay(500);
                    }
                } else if (SelectedProvider == "Azure")
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
                        AzureInputStream();
                        while (!STTDone)
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

        private void STTProviderBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String SelectedProvider = STTProviderBox.GetItemText(STTProviderBox.SelectedItem);
            if (SelectedProvider == "Native")
            {
                TextLog.AppendText("Native STT\r\n");
                BBBlog.Info("Native STT");
                STTAPIKeyEditbox.Enabled = false;
                STTRegionEditbox.Enabled = false;
                STTTestOutput.Text = "Hint: For better native Speech-To-Text always train your voice at least once in Control Panel\\Ease of Access\\Speech Recognition";
            }
            else if (SelectedProvider == "Azure")
            {
                TextLog.AppendText("Azure\r\n");
                BBBlog.Info("Azure STT");
                STTAPIKeyEditbox.Enabled = true;
                STTRegionEditbox.Enabled = true;
                STTTestOutput.Text = "Be sure to set API key and region!";
            }
        }

        //this sets "SelectedDevice" to the correct input/microphone
        private void SetSelectedInputDevice()
        {
            var devices = MMDeviceEnumerator.EnumerateDevices(DataFlow.Capture, DeviceState.Active);
            foreach (var device in devices)
            {
                if (device.FriendlyName == SoundInputDevices.Text)
                {
                    SelectedDevice = device;
                }
            }
        }

        //Azure
        //Maybe should make it a Task instead of a void 
        private async void AzureInputStream()
        {
            //ok lets just start
            var AzureSpeechConfig = SpeechConfig.FromSubscription(STTAPIKeyEditbox.Text, STTRegionEditbox.Text);
            AzureSpeechConfig.SpeechRecognitionLanguage = "en-US"; //default language

            SetSelectedInputDevice();

            BBBlog.Info("selected audio input device for azure: " + SelectedDevice);
            var AzureAudioConfig = AudioConfig.FromMicrophoneInput(SelectedDevice.DeviceID);  
            var AzureSpeechRecognizer = new Microsoft.CognitiveServices.Speech.SpeechRecognizer(AzureSpeechConfig, AzureAudioConfig);
            STTTestOutput.Text = "";
            BBBlog.Info("Azure STT microphone start.");

            while (STTTestButton.Text == "Recording" || MainRecordingStart.Text =="Recording")
            {
                var speechRecognitionResult = await AzureSpeechRecognizer.RecognizeOnceAsync();
                AzureOutputSpeechRecognitionResult(speechRecognitionResult);
            }
            STTDone = true;
        }

        private void AzureOutputSpeechRecognitionResult(SpeechRecognitionResult speechRecognitionResult)
        {     
            switch (speechRecognitionResult.Reason)
            {
                case ResultReason.RecognizedSpeech:
                    BBBlog.Info($"RECOGNIZED: Text={speechRecognitionResult.Text}");
                    STTTestOutput.Text += speechRecognitionResult.Text + "\r\n";
                    TextLog.Text += speechRecognitionResult.Text + "\r\n";
                    break;
                case ResultReason.NoMatch:
                    BBBlog.Info("NOMATCH: Speech could not be recognized.");
                    STTTestOutput.Text += "NOMATCH: Speech could not be recognized.\r\n";
                    break;
                case ResultReason.Canceled:
                    var cancellation = CancellationDetails.FromResult(speechRecognitionResult);
                    BBBlog.Info($"CANCELED: Reason={cancellation.Reason}");
                    STTTestOutput.Text += $"CANCELED: Reason={cancellation.Reason}\r\n";

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        STTTestOutput.Text = $"CANCELED: ErrorCode={cancellation.ErrorCode}\r\n";
                        STTTestOutput.Text += $"CANCELED: ErrorDetails={cancellation.ErrorDetails}\r\n";
                        STTTestOutput.Text += $"CANCELED: Did you set the speech resource key and region values?\r\n";
                        BBBlog.Info($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        BBBlog.Info($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        BBBlog.Info($"CANCELED: Did you set the speech resource key and region values?");
                    }
                    break;
            }
            STTTestOutput.Text += "\r\n";
        }

        //help with selected inputdevice to return the ID
        //that corresponds with the name
        private IWaveSource _finalSource;
        public MMDevice SelectedDevice
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
        private string tmpWavFile = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\tmp.wav";
        private void NativeInputStreamtoWav()
        {

            SetSelectedInputDevice();
            BBBlog.Info("Selected audio input device for Native: " + SelectedDevice);
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
            BBBlog.Info("Native STT microphone start.");
        }

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

        private async void NativeSTTfromWAV()
        {
            // Create an in-process speech recognizer for the en-US locale.  
            SpeechRecognitionEngine recognizer2 = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            // Create and load a dictation grammar.  
            recognizer2.LoadGrammar(new DictationGrammar());
            recognizer2.SetInputToWaveFile(tmpWavFile);
            // Attach event handlers for the results of recognition.  
            recognizer2.SpeechRecognized +=
              new EventHandler<SpeechRecognizedEventArgs>(NativeSpeechRecognized);
            recognizer2.RecognizeCompleted +=
              new EventHandler<RecognizeCompletedEventArgs>(NativeRecognizeCompletedHandler);

            TextLog.AppendText("Starting asynchronous Native recognition... on " +tmpWavFile +"\r\n");
            BBBlog.Info("Starting asynchronous Native recognition... on " +tmpWavFile);

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

        private void NativeRecognizeCompletedHandler(object sender, RecognizeCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                TextLog.AppendText("Native STT Error encountered, "+ e.Error.GetType().Name+" : " +e.Error.Message+"\r\n");
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

        private void NativeSpeechDetectedHandler(object sender, SpeechDetectedEventArgs e)
        {
            TextLog.AppendText(" In NativeSpeechDetectedHandler:\r\n");
            TextLog.AppendText(" - AudioPosition = " + e.AudioPosition + "\r\n");
            BBBlog.Info(" In NativeSpeechDetectedHandler: ");
            BBBlog.Info(" - AudioPosition = \" + e.AudioPosition");
        }

        private async void TalkToOpenAIGPT(String UserInput)
        {
            TextLog.AppendText("Sending to GPT: " +UserInput+ "\r\n");
            BBBlog.Info("Sending to GPT: " + UserInput);
            GPTDone = false;
            OpenAIAPI api = new OpenAIAPI(LLMAPIKeyTextBox.Text);
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
                BBBlog.Error("GPT API Auth error: " +ex.Message);
            }

        }

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

        //holder of the list of Azure Voices and their options
        List<AzureVoices> AzureRegionVoicesList = new List<AzureVoices>();
        private async Task TTSGetAzureVoices()
        {           
            BBBlog.Info("Finding TTS Azure voices available");
            SpeechConfig speechConfig = SpeechConfig.FromSubscription(TTSAPIKeyTextBox.Text, TTSRegionTextBox.Text);
            //lets get the voices available
            var speechSynthesizer = new Microsoft.CognitiveServices.Speech.SpeechSynthesizer(speechConfig, null as AudioConfig);
            SynthesisVoicesResult result = await speechSynthesizer.GetVoicesAsync();
            
            if (result.Reason == ResultReason.VoicesListRetrieved)
            {
                //remove the old listed items
                AzureRegionVoicesList.Clear();
                BBBlog.Info($"Found {result.Voices.Count} voices");
                foreach (var voice in result.Voices)
                {
                    var tmpVoice = new AzureVoices();
                    tmpVoice.Locale = voice.Locale;
                    tmpVoice.LocalName = voice.LocalName;
                    //we dont use this directly, but this is what we need to refer to for selecting it.
                    tmpVoice.Name = voice.Name;
                    tmpVoice.Gender = voice.Gender.ToString();
                    tmpVoice.StyleList = new List<string>(voice.StyleList);
                    RegionInfo ri = new RegionInfo(voice.Locale);
                    tmpVoice.LocaleDisplayname = ri.ThreeLetterISORegionName;
                    AzureRegionVoicesList.Add(tmpVoice);
                }
            }
        }

        private void TTSFillAzureVoicesList()
        {
            BBBlog.Info("Fill Azure voice list");
            //list setup TTSOutputVoice
            // Locale, Gender, Localname
            TTSOutputVoice.Items.Clear();
            foreach (var AzureRegionVoice in AzureRegionVoicesList)
            {
                TTSOutputVoice.Items.Add(AzureRegionVoice.LocaleDisplayname + "-" + AzureRegionVoice.Gender + "-"+ AzureRegionVoice.LocalName );
            }
            TTSOutputVoice.Sorted = true;
            TTSOutputVoiceOptions.Text = "";
            TTSAzureFillOptions(TTSOutputVoice.Text);
        }

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
            if (TTSOutputVoiceOptions.SelectedIndex == -1)
            {
                TTSOutputVoiceOptions.Text = TTSOutputVoiceOptions.Items[0].ToString();
            }

        }

        //Azure Text-To-Speach
        private async void TTSAzureSpeakToOutput(string TextToSpeak)
        {
            //get all options but only if API key and Region are filled
            if (TTSAPIKeyTextBox.Text.Length > 0 && TTSRegionTextBox.Text.Length > 0)
            {
                await TTSGetAzureVoices();
                //fill the listboxes
                TTSFillAzureVoicesList();
            }
        }



        private async void TTSNativeSpeakToOutput(String TTSText)
        {
            TextLog.AppendText("Saying text with Native TTS\r\n");
            BBBlog.Info("Saying text with Native TTS");
            //using the full name to make sure we dont confuse native with Azure
            SpeechSynthesizer synthesizer = new System.Speech.Synthesis.SpeechSynthesizer();
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
            else if (TTSProviderComboBox.Text == "Azure")
            {
                TTSAzureSpeakToOutput(TTSTestTextBox.Text);
            }
        }

        private async void MainRecordingStart_Click(object sender, EventArgs e)
        {
            String SelectedProvider = STTProviderBox.GetItemText(STTProviderBox.SelectedItem);
            //first, lets call STT
            if (MainRecordingStart.Text == "Start")
            {
                STTTestOutput.Text = "";
                MainRecordingStart.Text = "Recording";
                

                if (SelectedProvider == "Native")
                {
                    TextLog.AppendText("ProframFlow Native STT calling\r\n");
                    BBBlog.Info("ProframFlow Native STT calling");
                    NativeInputStreamtoWav();
                    while (!STTDone)
                    {
                        await Task.Delay(500);
                    }
                } else if (SelectedProvider == "Azure")
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
                        AzureInputStream();
                        while (!STTDone)
                        {
                            await Task.Delay(500);
                        }

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
            //load HotkeyList into SetHotKeys
            foreach (String key in Properties.Settings.Default.HotkeyList)
            {
                Keys tmpKey = (Keys)Enum.Parse(typeof(Keys), key, true);
                SetHotkeys.Add(tmpKey);
             }

            //we need to get azure regions and voice options if azure is selected so we dont need to fill it later
            if (TTSProviderComboBox.Text == "Azure")
            {
                //fill the list if its empty
                if (TTSOutputVoice.Items.Count < 1)
                {
                    if (TTSAPIKeyTextBox.Text.Length > 0 && TTSRegionTextBox.Text.Length > 0)
                    {
                        TTSGetAzureVoices();
                        //fill the listboxes
                        TTSFillAzureVoicesList();
                    }
                }
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
            Properties.Settings.Default.TwitchChatCommandDelay  = int.Parse(TwitchChatCommandDelay.Text);
            Properties.Settings.Default.TwitchNeedsFollower = TwitchNeedsFollower.Checked;
            Properties.Settings.Default.TwitchNeedsSubscriber = TwitchNeedsSubscriber.Checked;
            Properties.Settings.Default.TwitchMinBits = int.Parse(TwitchMinBits.Text);
            Properties.Settings.Default.TwitchSubscribed =  TwitchSubscribed.Checked;
            Properties.Settings.Default.TwitchCommunitySubs = TwitchCommunitySubs.Checked;
            Properties.Settings.Default.TwitchGiftedSub = TwitchGiftedSub.Checked;
            Properties.Settings.Default.TwitchSendTextCheckBox = TwitchSendTextCheckBox.Checked;
            Properties.Settings.Default.TTSAPIKeyTextBox = TTSAPIKeyTextBox.Text;
            Properties.Settings.Default.TTSRegionTextBox = TTSRegionTextBox.Text;
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
            BBBlog.Info("Hotkey set to "+ MicrophoneHotkeyEditbox.Text);
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
                if (MainRecordingStart.Text == "Start")
                {
                    MainRecordingStart_Click(null,null);
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
                    MainRecordingStart_Click(null, null);
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

      
        //yes this is lame as shit, i know classes, objects do it correctly etc.
        //but it works for now, so eh.
        private void TwitchTestButton_Click(object sender, EventArgs e)
        {
            TwitchClient client;
            //lets make a bot and see if we can connect and send a message to a channel
            ConnectionCredentials credentials = new ConnectionCredentials(TwitchUsername.Text, TwitchAccessToken.Text);

            WebSocketClient customClient = new WebSocketClient();
            client = new TwitchClient(customClient);

            client.Initialize(credentials, TwitchChannel.Text);

            //commands start with $
            client.AddChatCommandIdentifier(char.Parse(TwitchCommandTrigger.Text));

            //Logwriter
            client.OnLog += (o, a) => {
                BBBlog.Info($"Twitch: {a.BotUsername} - {a.Data}");
            };

            //Connection evets
            client.OnConnected += (o, a) =>
            {
                BBBlog.Info($"Twitch OnConnected => Connected to {a.AutoJoinChannel}");
            };
            client.OnJoinedChannel += (o, a) =>
            {
                if (TwitchSendTextCheckBox.Checked)
                    client.SendMessage(a.Channel, TwitchTestSendText.Text);
                else
                    BBBlog.Info("Twitch I would have send: " + a.Channel + " => " + TwitchTestSendText.Text);
            };

            /*
                    error handling
            */
            //connection and login
            client.OnConnectionError += (o, a) => {
                BBBlog.Error("Twitch OnConnectionError =>" + a.Error);
                client.Disconnect();
            };
            client.OnIncorrectLogin += (o, a) =>
            {
                BBBlog.Error("Twitch OnIncorrectLogin => " + a.Exception);
                client.Disconnect();

            };
            client.OnDisconnected += (o, a) =>
            {
                BBBlog.Error("Twitch onDisconnected => I got disconnected");
            };
            //Channel joining issues
            client.OnFailureToReceiveJoinConfirmation += (o, a) =>
            {
                BBBlog.Error("Twitch OnFailureToReceiveJoinConfirmation => " + a.Exception);
            };

            //channel permissions & events
            client.OnNoPermissionError += (o, a) =>
            {
                BBBlog.Error("Twitch OnNoPermissionError => " + a.ToString());
            };
            client.OnRateLimit += (o, a) =>
            {
                BBBlog.Error("Twitch OnRateLimit => " + a.Message);
            };
            client.OnBanned += (o, a) =>
            {
                BBBlog.Error("Twitch onBanned => " + a.Message);
            };
            client.OnSlowMode += (o, a) =>
            {
                BBBlog.Error("Twitch OnSlowMode => " + a.Message);
            };
            client.OnSubsOnly += (o, a) =>
            {
                BBBlog.Error("Twitch OnSubsOnly => " + a.Message);
            };
            // respond to: $tts ""
            client.OnChatCommandReceived += (o, a) =>
            {
                if (a.Command.CommandText == "tts")
                {
                    client.SendMessage(TwitchChannel.Text, "you send command tts with param: " + a.Command.ArgumentsAsString);
                }
            };
            //account verification not met
            client.OnRequiresVerifiedEmail += (o, a) =>
            {
                BBBlog.Error("Twitch OnRequiresVerifiedEmail => " + a.Message);
            };
            client.OnRequiresVerifiedPhoneNumber += (o, a) =>
            {
                BBBlog.Error("Twitch OnRequiresVerifiedPhoneNumber => " + a.Message);
            };

            //general error
            client.OnError += (o, a) => {
                BBBlog.Error("Twitch OnError => " + a.Exception.Message);
            };


            client.OnMessageReceived += (o, a) =>
            {
                BBBlog.Info("Twitch  onMessageReceived => " + a.ChatMessage);
            };
            client.Connect();
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
            string TwitchAuthRedirect = null;
            string TwitchAuthClientId = null;
            using (StreamReader r = new StreamReader(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\settings.json"))
            {
                string json = r.ReadToEnd();
                Console.WriteLine($"{json}");
                dynamic data = JObject.Parse(json);
                Console.WriteLine(data);
                SecretsJson = data.TwitchAPISecret;
                TwitchAuthRedirect = data.TwitchAuthRedirect;
                TwitchAuthClientId= data.TwitchAuthClientId;
            }

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
            //also save this in our form
            TwitchAccessToken.Text = resp.AccessToken;

            // get the auth'd user
            var user = (await api.Helix.Users.GetUsersAsync()).Users[0];

            // print out all the data we've got
          //  Console.WriteLine($"Authorization success!\n\nUser: {user.DisplayName} (id: {user.Id})\nAccess token: {resp.AccessToken}\nRefresh token: {resp.RefreshToken}\nExpires in: {resp.ExpiresIn}\nScopes: {string.Join(", ", resp.Scopes)}");

            // refresh token
            var refresh = await api.Auth.RefreshAuthTokenAsync(resp.RefreshToken, SecretsJson);
            api.Settings.AccessToken = refresh.AccessToken;

            // confirm new token works
            user = (await api.Helix.Users.GetUsersAsync()).Users[0];

            // print out all the data we've got
            BBBlog.Debug($"Authorization success!\n\nUser: {user.DisplayName} (id: {user.Id})\nAccess token: {refresh.AccessToken}\nRefresh token: {refresh.RefreshToken}\nExpires in: {refresh.ExpiresIn}\nScopes: {string.Join(", ", refresh.Scopes)}");
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

        private void TTSProviderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TTSProviderComboBox.Text == "Native")
            {
                TTSAPIKeyTextBox.Enabled = false;
                TTSAudioOutputComboBox.Enabled = false;
                TTSOutputVoice.Enabled = false;
                TTSOutputVoiceOptions.Enabled = false;
                TTSRegionTextBox.Enabled = false;

            } else if (TTSProviderComboBox.Text == "Azure")
            {
                TTSAPIKeyTextBox.Enabled = true;
                TTSAudioOutputComboBox.Enabled = true;
                TTSOutputVoice.Enabled = true;
                TTSOutputVoiceOptions.Enabled = true;
                TTSRegionTextBox.Enabled = true;
            }
        }

        //if we change the region box, lets make sure we till have the right voices
        private async void TTSRegionTextBox_Leave(object sender, EventArgs e)
        {
            BBBlog.Info("Region edit box exited");
            if (TTSAPIKeyTextBox.Text.Length > 0 && TTSRegionTextBox.Text.Length > 0)
            {
                await TTSGetAzureVoices();
                //fill the listboxes
                TTSFillAzureVoicesList();
            }
        }

        private void TTSOutputVoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            //depending on what voice is selected we need to now select the voice options (if any)
            TTSAzureFillOptions(TTSOutputVoice.Text);
        }

    }
    }
