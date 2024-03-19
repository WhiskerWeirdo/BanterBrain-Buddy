using NAudio.CoreAudioApi;
using OpenAI_API;
using OpenAI_API.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BanterBrain_Buddy
{
    public partial class BBB : Form
    {
        public BBB()
        {
            InitializeComponent();
            //TODO: load settings into forms
            //TODO: grey out items that are not required for specific settings

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
                    SoundInputDevices.Text = device.FriendlyName;
                }
                else
                    SoundInputDevices.Items.Add(device.FriendlyName);
            TextLog.AppendText("Program Starting...\r\n");

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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
                toolStripStatusLabel1.Text = "Microphone off";
                statusStrip1.Refresh();
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
                //grey out region and api key
            }
            else if (SelectedProvider == "Azure")
            {
                TextLog.AppendText("Azure\r\n");
            }
            else if (SelectedProvider == "Google")
            {
                TextLog.AppendText("Google\r\n");
            }
        }
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

           // recognizer.SpeechDetected +=
              //  new EventHandler<SpeechDetectedEventArgs>(SpeechDetectedHandler);

           // recognizer.SpeechHypothesized +=
              //  new EventHandler<SpeechHypothesizedEventArgs>(SpeechHypothesizedHandler);

            recognizer.RecognizeCompleted +=
                new EventHandler<RecognizeCompletedEventArgs>(RecognizeCompletedHandler);

            // Configure input to the speech recognizer.  
            //TODO: use the selected audio device, not default
            recognizer.SetInputToDefaultAudioDevice();

            // Modify the initial silence time-out value.  
            // Start synchronous speech recognition.
            TextLog.AppendText("STT microphone start. -- SPEAK NOW -- \r\n");

            recognizer.RecognizeAsync(RecognizeMode.Multiple);
            while (ButtonPressed.Text == "Recording")
           // while (STTTestButton.Text == "Recording")
            {
                await Task.Delay(500);
            }
            recognizer.RecognizeAsyncStop();
            TextLog.AppendText("STT microphone stop.\r\n");
        }

        // Handle the SpeechHypothesized event.  
        private void SpeechHypothesizedHandler(
          object sender, SpeechHypothesizedEventArgs e)
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
            TextLog.AppendText("Recognized text: " + e.Result.Text+ "\r\n");
            STTTestOutput.AppendText(e.Result.Text+ "\r\n");
        }

        bool GPTDone = false;
        private async void TalkToOpenAIGPT(String UserInput)
        {
            TextLog.AppendText("Sending to GPT\r\n");
            GPTDone = false;
            OpenAIAPI api = new OpenAIAPI(GPTAPIKeyTextBox.Text);
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
                MessageBox.Show( ex.Message,"GPT API Auth error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
      
        }

        private void GPTTestButton_Click(object sender, EventArgs e)
        {
            LLMTestOutputbox.Text = "";
            GPTTestButton.Enabled = false;
            GPTTestButton.Text = "Wait...";
            if (GPTProviderComboBox.Text == "OpenAI ChatGPT")
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
                LLMTestOutputbox.Text = "";
                if (GPTProviderComboBox.Text == "OpenAI ChatGPT")
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
            {
                ProgramFlowTest.Text = "Start";
            }



        }

    }

}
