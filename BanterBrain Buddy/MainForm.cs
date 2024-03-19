using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BanterBrain_Buddy
{
    public partial class BBB : Form
    {
        public BBB()
        {
            InitializeComponent();

            //Get Sound devices
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            //default speaker
            var defaultOutputDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);

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
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void STTTestButton_Click(object sender, EventArgs e)
        {
            if (STTTestButton.Text == "Test")
            {
                toolStripStatusLabel1.Text = "Microphone on";
                statusStrip1.Refresh();
                STTTestButton.Text = "Recording";
                String SelectedProvider = STTProviderBox.GetItemText(STTProviderBox.SelectedItem);
                Console.WriteLine(SelectedProvider);

                if (SelectedProvider == "Native")
                {
                    Console.WriteLine("Native STT calling");
                    STTTestOutput.BackColor = Color.Orange;
                    STTNative();
                }
            }
            else
            {
                toolStripStatusLabel1.Text = "Microphone off";
                statusStrip1.Refresh();
                STTTestButton.Text = "Test";
                Console.WriteLine("Stopped recording");
                STTTestOutput.BackColor = SystemColors.Control;
            }
        }

        private void SSTProviderBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String SelectedProvider = STTProviderBox.GetItemText(STTProviderBox.SelectedItem);
            if (SelectedProvider == "Native")
            {
                Console.WriteLine("Native");
                STTAPIKeyEditbox.Enabled = false;
                STTRegionEditbox.Enabled = false;
                //grey out region and api key
            }
            else if (SelectedProvider == "Azure")
            {
                Console.WriteLine("Azure");
            }
            else if (SelectedProvider == "Google")
            {
                Console.WriteLine("Google");
            }
        }

        private async void STTNative()
        {
            Console.WriteLine("STTNative called");
            // Create an in-process speech recognizer for the en-US locale.  
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            // Create and load a dictation grammar.  
            recognizer.LoadGrammarAsync(new DictationGrammar());

            // Add a handler for the speech recognized event.  
            recognizer.SpeechRecognized +=
              new EventHandler<SpeechRecognizedEventArgs>(SpeechRecognized);

            recognizer.SpeechDetected +=
                new EventHandler<SpeechDetectedEventArgs>(SpeechDetectedHandler);

            recognizer.SpeechHypothesized +=
                new EventHandler<SpeechHypothesizedEventArgs>(SpeechHypothesizedHandler);

            recognizer.RecognizeCompleted +=
                new EventHandler<RecognizeCompletedEventArgs>(RecognizeCompletedHandler);

            // Configure input to the speech recognizer.  
            //TODO: use the selected audio device, not default
            recognizer.SetInputToDefaultAudioDevice();


            // Modify the initial silence time-out value.  
            // Start synchronous speech recognition.
            Console.WriteLine("async start");

            recognizer.RecognizeAsync(RecognizeMode.Multiple);
            //recognize for 10 seconsd
            while (STTTestButton.Text == "Recording")
            {
                await Task.Delay(500);
            }
            recognizer.RecognizeAsyncStop();
            Console.WriteLine("async stop");
        }

        // Handle the SpeechHypothesized event.  
        private static void SpeechHypothesizedHandler(
          object sender, SpeechHypothesizedEventArgs e)
        {
            Console.WriteLine(" In SpeechHypothesizedHandler:");

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

        private  static void RecognizeCompletedHandler(object sender, RecognizeCompletedEventArgs e)
        {
            Console.WriteLine("Recognize Completed");
        }

        private static void SpeechDetectedHandler(object sender, SpeechDetectedEventArgs e)
        {
            Console.WriteLine(" In SpeechDetectedHa ndler:");
            Console.WriteLine(" - AudioPosition = {0}", e.AudioPosition);
        }

        // Handle the SpeechRecognized event.  
        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Recognized text: " + e.Result.Text);
            STTTestOutput.AppendText(e.Result.Text);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }

}
