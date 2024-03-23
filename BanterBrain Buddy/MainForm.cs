using Gma.System.MouseKeyHook;
using NAudio.CoreAudioApi;
using OpenAI_API;
using OpenAI_API.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BanterBrain_Buddy
{
    public partial class BBB : Form
    {
        //PTT hotkey hook
        private IKeyboardMouseEvents m_GlobalHook;

        public BBB()
        {

            InitializeComponent();
            LoadSettings();
            Subscribe();

            //Get Sound devices
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            //default speaker
            var defaultOutputDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            TTSAudioOutputComboBox.Text = defaultOutputDevice.FriendlyName;

            //default mic
            var defaultInputDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Multimedia);

            //Show the list of possible inputs, with * is the currently default/active one
            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
            foreach (var device in devices)
                if (device.FriendlyName == defaultInputDevice.FriendlyName)
                {
                    SoundInputDevices.Items.Add(device.FriendlyName + "*");
                }
                else
                    SoundInputDevices.Items.Add(device.FriendlyName);
            TextLog.AppendText("Program Starting...\r\n");
            TextLog.AppendText("PPT hotkey: " + MicrophoneHotkeyEditbox.Text + "\r\n");
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
        
        // check if SST is finished yet
        bool STTDone = false;
        private async void STTNative(System.Windows.Forms.Button ButtonPressed)
        {
            STTDone = false;

            TextLog.AppendText("STTNative called\r\n");
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

        private void SpeechDetectedHandler(object sender, SpeechDetectedEventArgs e)
        {
            TextLog.AppendText(" In SpeechDetectedHandler:\r\n");
            TextLog.AppendText(" - AudioPosition = " + e.AudioPosition + "\r\n");
        }

        // Handle the SpeechRecognized event.  
        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            TextLog.AppendText("Recognized text: " + e.Result.Text + "\r\n");
            STTTestOutput.AppendText(e.Result.Text + "\r\n");
        }

        bool GPTDone = false;
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

        private void TTSNative(String TTSText)
        {
            TextLog.AppendText("Saying text with Native TTS\r\n");
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.SpeakAsync(TTSText);
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
                    TextLog.AppendText("Native STT calling\r\n");
                    STTNative(ProgramFlowTest);
                    while (!STTDone)
                    {
                        await Task.Delay(500);
                    }
                }

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
 
        //Get new hotkeys
        List<Keys> SetHotkeys = new List<Keys>();
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

        //here we handle the hotkey being pressed.
        //This is a bit jank because as long as the hotkey is pressed
        //the event map from GlobalHookKeyDown will keep triggering this
        //so we gotta make sure we call it only once by setting a global bool
        bool HotkeyCalled = false;
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
    }
}
