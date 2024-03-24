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
        //check if the GPT LLM is done
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

        private void STTTestButton_Click(object sender, EventArgs e)
        {
            if (STTTestButton.Text == "Test")
            {
                STTTestOutput.Text = "";
                TextLog.AppendText("Microphone on\r\n");
                STTTestButton.Text = "Recording";
                String SelectedProvider = STTProviderBox.GetItemText(STTProviderBox.SelectedItem);
                TextLog.AppendText(SelectedProvider);

                if (SelectedProvider == "Native")
                {
                    TextLog.AppendText("Native STT calling\r\n");
                    STTTestOutput.BackColor = Color.Orange;
                    STTNative(STTTestButton);
                }
            }
            else
            {
                STTTestButton.Text = "Test";
                TextLog.AppendText("Stopped recording\r\n");
                STTTestOutput.BackColor = SystemColors.Control;
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
                //grey out region and api key
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

        private void TestSTTNative()
        {
            Console.WriteLine("STTNative:" + SoundInputDevices.Text + ":" + SoundInputDevices.Items.Count);
            //get id from selected device
            foreach (var device in WaveInDevice.EnumerateDevices())
            {
                if (SoundInputDevices.Text == device.Name)
                {
                    InputStream();
                }
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

        private MMDevice _selectedDevice;
        private WasapiCapture _soundIn;
        private IWriteable _writer;
        private string tmpWavFile = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\tmp.wav";
        private void InputStream()
        {
            Console.WriteLine("in inputstream");
            var devices = MMDeviceEnumerator.EnumerateDevices(DataFlow.Capture, DeviceState.Active);
            foreach (var device in devices)
            {
                if (device.FriendlyName == SoundInputDevices.Text)
                {
                    SelectedDevice = device;
                }
            }
            _soundIn = new WasapiCapture();
            Console.WriteLine("><>" + SelectedDevice.FriendlyName);
            _soundIn.Device = SelectedDevice;
            _soundIn.Initialize();
            var soundInSource = new SoundInSource(_soundIn);
            var singleBlockNotificationStream = new SingleBlockNotificationStream(soundInSource.ToSampleSource());
            _finalSource = singleBlockNotificationStream.ToWaveSource();

            _writer = new WaveWriter(tmpWavFile, _finalSource.WaveFormat);
            byte[] buffer = new byte[_finalSource.WaveFormat.BytesPerSecond / 2];
            soundInSource.DataAvailable += (s, e) =>
            {
                int read;
                while ((read = _finalSource.Read(buffer, 0, buffer.Length)) > 0)
                    _writer.Write(buffer, 0, read);
            };
            _soundIn.Start();
            TextLog.AppendText("STT microphone start. -- SPEAK NOW -- \r\n");
        }

        private void StopCapture()
        {
            TextLog.AppendText("stop capture to file");
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
            System.Threading.Thread.Sleep(3000);
            //ok now that we have an MP3, lets push it to TTS
            NativeSTT();
            //now lets delete it
        }

        private void NativeSTT()
        {

            TextLog.AppendText(STTDone.ToString()+" STT Native from file called: "+ tmpWavFile + "\r\n");
            // Create an in-process speech recognizer for the en-US locale.  
            SpeechRecognitionEngine recognizer2 = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            // Create and load a dictation grammar.  
            recognizer2.LoadGrammarAsync(new DictationGrammar());

            //TODO: use the selected audio device, not default
            STTDone = false;
            
            using (FileStream stream = new FileStream(tmpWavFile, FileMode.Open))
            {
                recognizer2.SetInputToAudioStream(stream, new SpeechAudioFormatInfo(9000, AudioBitsPerSample.Sixteen, AudioChannel.Stereo));
                RecognitionResult result = recognizer2.Recognize();
                TextLog.AppendText("Recognized text: " + result.Text + "\r\n");
                STTTestOutput.AppendText(result.Text + "\r\n");
                stream.Close();
            }

            TextLog.AppendText("STT done.\r\n");
        }

        private async void STTNative(System.Windows.Forms.Button ButtonPressed)
        {

            STTDone = false;
           // return;
            TextLog.AppendText("STT Native called\r\n");
            // Create an in-process speech recognizer for the en-US locale.  
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            // Create and load a dictation grammar.  
            recognizer.LoadGrammarAsync(new DictationGrammar());

            // Add a handler for the speech recognized event.  
            recognizer.SpeechRecognized +=
              new EventHandler<SpeechRecognizedEventArgs>(SpeechRecognized);

            recognizer.RecognizeCompleted +=
                new EventHandler<RecognizeCompletedEventArgs>(RecognizeCompletedHandler);

            //TODO: use the selected audio device, not default
            recognizer.SetInputToDefaultAudioDevice();
          
            TextLog.AppendText("STT microphone start. -- SPEAK NOW -- \r\n");

            recognizer.RecognizeAsync(RecognizeMode.Multiple);
            while (ButtonPressed.Text == "Recording")
            {
                await Task.Delay(500);
            }
            recognizer.RecognizeAsyncStop();
            TextLog.AppendText("STT microphone stop.\r\n");
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
            TextLog.AppendText("STT recognize Stopped.\r\n");
            STTDone = true;
        }

        // Handle the SpeechRecognized event.  
        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //   Console.WriteLine("recognizing text\r\n");
            TextLog.AppendText("Recognized text: " + e.Result.Text + "\r\n");
            STTTestOutput.AppendText(e.Result.Text + "\r\n");

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
                STTTestOutput.AppendText("Using ChatGPT\r\n");
                TalkToOpenAIGPT("How are you?");
            }
            GPTTestButton.Text = "Test";
            GPTTestButton.Enabled = true;
        }

        //output to the selected audio device
        private void OutputStream(MemoryStream stream)
        {
            //find the id of the correct output device
            var Devices = WaveOutDevice.EnumerateDevices();
            int deviceID = 0;
            foreach (var device in WaveOutDevice.EnumerateDevices())
            {
                Console.WriteLine("{0}: {1}", device.DeviceId, device.Name);
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
 
        private void TTSNative(String TTSText)
        {
            TextLog.AppendText("Saying text with Native TTS\r\n");
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            var stream = new MemoryStream();
            synthesizer.SetOutputToWaveStream(stream);
            synthesizer.Speak(TTSText);
            OutputStream(stream);
        }

        private void TTSTestButton_Click(object sender, EventArgs e)
        {
            if (TTSProviderComboBox.Text == "Native")
            {
                TTSNative(TTSTestTextBox.Text);
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
                    STTNative(ProgramFlowTest);
                    while (!STTDone)
                    {
                        await Task.Delay(500);
                    }
                }
                Console.WriteLine("dadad");
                //now the STT text is in STTTestOutput.Text, lets pass that to ChatGPT
                if (STTTestOutput.Text.Length > 1)
                {
                    LLMTestOutputbox.Text = "";
                    if (LLMProviderComboBox.Text == "OpenAI ChatGPT")
                    {
                        STTTestOutput.AppendText("Using ChatGPT\r\n");
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
                        TTSNative(LLMTestOutputbox.Text);
                    }
                }
                else
                    TextLog.AppendText("No audio recorded");
            }
            else
            {
                ProgramFlowTest.Text = "Start";
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
            Console.WriteLine("keyboard hook OFF");
        }
        public void Subscribe()
        {
            Console.WriteLine("keyboard hook ON");
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
                //if one of the keys in teh sethotkeys is detected as UP, give it a second then stop recording
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

        private void button1_Click(object sender, EventArgs e)
        {
            NativeSTT();
            /*
            if (button1.Text == "Start")
            {
                button1.Text = "Recording";
                TestSTTNative();
            }
            else
            {
                button1.Text = "Start";
                StopCapture();
            } */
        }
    }
}
