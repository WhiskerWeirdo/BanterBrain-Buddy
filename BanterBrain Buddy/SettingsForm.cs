using CSCore.SoundIn;
using CSCore.SoundOut;
using Newtonsoft.Json;
using OpenAI_API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BanterBrain_Buddy
{
    public partial class SettingsForm : Form
    {
        //set logger
        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private List<Personas> _personas = new List<Personas>();
        private List<AzureVoices> _azureRegionVoicesList = [];
        private List<NativeVoices> _nativeRegionVoicesList = [];

        [SupportedOSPlatform("windows6.1")]
        public SettingsForm()
        {
            InitializeComponent();
            GetAudioDevices();
            LoadSettings();
            MenuTreeView.ExpandAll();

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

                //TODO: make sure the selected device is the same as the one in the settings
                //if not, set it to the first one
            }
        }



        [SupportedOSPlatform("windows6.1")]
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            showPanels(e.Node.Name);
        }

        //show panels based on selected node and hide the others
        [SupportedOSPlatform("windows6.1")]
        private void PanelVisible(string panelName)
        {
            foreach (var panel in splitContainer1.Panel2.Controls.OfType<Panel>())
            {
                //_bBBlog.Debug("Panel name: " + panel.Name);
                if (panel.Name == panelName)
                {
                    panel.Show();
                }
                else
                {
                    panel.Hide();
                }
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void showPanels(string selectedNode)
        {
            _bBBlog.Debug("Selected node: " + selectedNode);
            switch (selectedNode)
            {
                case "VoiceSettings":
                    _bBBlog.Debug("VoiceSettings selected");
                    PanelVisible(MicrophonePanel.Name);
                    break;
                case "Microphone":
                    _bBBlog.Debug("Microphone selected");
                    PanelVisible(MicrophonePanel.Name);
                    break;
                case "Azure":
                    _bBBlog.Debug("Azure selected");
                    PanelVisible(AzurePanel.Name);
                    break;
                case "Speaker":
                    _bBBlog.Debug("Speaker selected");
                    PanelVisible(SpeakerPanel.Name);
                    break;
                case "OpenAIChatGPT":
                    _bBBlog.Debug("OpenAIChatGPT selected");
                    PanelVisible(OpenAIChatGPTPanel.Name);
                    break;
                case "Twitch":
                    _bBBlog.Debug("Twitch selected");
                    PanelVisible(TwitchConnectionPanel.Name);
                    break;
                case "TwitchConnection":
                    _bBBlog.Debug("TwitchConnection selected");
                    PanelVisible(TwitchConnectionPanel.Name);
                    break;
                case "TwitchTriggers":
                    _bBBlog.Debug("TwitchTriggers selected");
                    PanelVisible(TwitchTriggersPanel.Name);
                    break;
                case "Personas":
                    _bBBlog.Debug("Personas selected");
                    PanelVisible(PersonasPanel.Name);
                    break;
                default:
                    break;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void LoadSettings()
        {
            _bBBlog.Info("Loading settings");
            SoundInputDevices.SelectedIndex = SoundInputDevices.FindStringExact(Properties.Settings.Default.VoiceInput);
            MicrophoneHotkeyEditbox.Text = Properties.Settings.Default.PTTHotkey;
            TTSAudioOutputComboBox.SelectedIndex = TTSAudioOutputComboBox.FindStringExact(Properties.Settings.Default.TTSAudioOutput);
            AzureAPIKeyTextBox.Text = Properties.Settings.Default.AzureAPIKeyTextBox;
            AzureRegionTextBox.Text = Properties.Settings.Default.AzureRegionTextBox;
            AzureLanguageComboBox.Text = Properties.Settings.Default.AzureLanguageComboBox;
            GPTModelComboBox.SelectedIndex = GPTModelComboBox.FindStringExact(Properties.Settings.Default.GPTModel);
            GPTAPIKeyTextBox.Text = Properties.Settings.Default.GPTAPIKey;
            GPTMaxTokensTextBox.Text = Properties.Settings.Default.GPTMaxTokens.ToString();
            GPTTemperatureTextBox.Text = Properties.Settings.Default.GPTTemperature.ToString();
            if (Properties.Settings.Default.SelectedLLM== "GPT")
            {
                UseGPTLLMCheckBox.Checked = true;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _bBBlog.Info("Settings form closing, saving settings");
            Properties.Settings.Default.VoiceInput = SoundInputDevices.Text;
            _bBBlog.Info("Voice input set to " + SoundInputDevices.Text);
            Properties.Settings.Default.PTTHotkey = MicrophoneHotkeyEditbox.Text;
            Properties.Settings.Default.TTSAudioOutput = TTSAudioOutputComboBox.Text;
            _bBBlog.Info("TTS Audio Output set to: " + TTSAudioOutputComboBox.Text);
            Properties.Settings.Default.AzureAPIKeyTextBox = AzureAPIKeyTextBox.Text;
            Properties.Settings.Default.AzureRegionTextBox = AzureRegionTextBox.Text;
            Properties.Settings.Default.AzureLanguageComboBox = AzureLanguageComboBox.Text;
            Properties.Settings.Default.GPTModel = GPTModelComboBox.Text;
            Properties.Settings.Default.GPTAPIKey = GPTAPIKeyTextBox.Text;
            Properties.Settings.Default.GPTMaxTokens = int.Parse(GPTMaxTokensTextBox.Text);
            Properties.Settings.Default.GPTTemperature = int.Parse(GPTTemperatureTextBox.Text);
            if (UseGPTLLMCheckBox.Checked)
            {
                Properties.Settings.Default.SelectedLLM = "GPT";
            }
            else
            {
                Properties.Settings.Default.SelectedLLM = "None";
            }
            Properties.Settings.Default.Save();
        }

        [SupportedOSPlatform("windows6.1")]
        private void SoundInputDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            _bBBlog.Info("Selected input device changed to " + SoundInputDevices.Text);
        }

        [SupportedOSPlatform("windows6.1")]
        private async void PersonasPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (PersonasPanel.Visible)
            {
                _bBBlog.Debug("Personas panel visible. we need to load the persona's");
                await LoadPersonas();
                while (PersonaComboBox.Items.Count == 0)
                {
                    await Task.Delay(1000);
                }
                //TODO: set the originally selected persona for now just load the first one
                PersonaComboBox.SelectedIndex = 0;
                //we also need to fill the combo box with the voices
                await FillVoiceBoxes();
            }
            else
            {
                _bBBlog.Debug("Personas panel hidden. we should save the persona's");

            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async Task FillVoiceBoxes()
        {
            if (TTSProviderComboBox.Text == "Native")
            {
                TTSOutputVoice.Text = "";
                TTSOutputVoiceOption1.Enabled = false;
                //clear and fill the option box with voices
                await TTSGetNativeVoices();
                TTSFillNativeVoicesList();
            }
            else if (TTSProviderComboBox.Text == "Azure")
            {
                TTSOutputVoice.Text = "";

                TTSOutputVoice.Enabled = true;
                TTSOutputVoiceOption1.Enabled = true;

                if (AzureAPIKeyTextBox.Text.Length > 0 && AzureRegionTextBox.Text.Length > 0)
                {
                    await TTSGetAzureVoices();
                    //fill the listboxes
                    TTSFillAzureVoicesList();
                }
                else
                {
                    MessageBox.Show("Please enter your Azure API key and region", "Azure API error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async Task LoadPersonas()
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
                PersonaComboBox.Items.Clear();
                using (var sr = new StreamReader(tmpFile))
                {
                    var tmpString = sr.ReadToEnd();
                    //var tmpPersonas = JsonConvert.DeserializeObject<List<Personas>>(tmpString);
                    var tmpPersonas = JsonConvert.DeserializeObject<List<Personas>>(tmpString);
                    foreach (var persona in tmpPersonas)
                    {
                        _bBBlog.Debug("Loading persona into available list: " + persona.Name);
                        _personas.Add(persona);
                        PersonaComboBox.Items.Add(persona.Name);
                    }
                }
            }

        }

        [SupportedOSPlatform("windows6.1")]
        private void PersonaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //alright index is changed! We need to load the persona into the form
            var selectedPersona = _personas[PersonaComboBox.SelectedIndex];
            PersonaRoleTextBox.Text = selectedPersona.RoleText;
            TTSProviderComboBox.Text = selectedPersona.VoiceProvider;
            TTSOutputVoice.Text = selectedPersona.VoiceName;
            if (selectedPersona.VoiceOptions.Count > 0)
            {
                _bBBlog.Debug("Voice options found, loading them into the combo box");
                foreach (var voiceOption in selectedPersona.VoiceOptions)
                {
                    _bBBlog.Debug("Adding voice option: " + voiceOption);
                }
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void NewPersona_Click(object sender, EventArgs e)
        {
            PersonaRoleTextBox.Text = "";
            TTSProviderComboBox.Text = "";
            TTSOutputVoice.Text = "";
            PersonaComboBox.Text = "";
            TTSOutputVoiceOption1.Text = "";
        }

        [SupportedOSPlatform("windows6.1")]
        private void SavePersona_Click(object sender, EventArgs e)
        {
            _bBBlog.Debug("Save persona button clicked");
            if (PersonaComboBox.Text == "")
            {
                MessageBox.Show("Please enter a name for the persona", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (PersonaRoleTextBox.Text == "")
            {
                MessageBox.Show("Please enter a role for the persona", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (TTSProviderComboBox.Text == "")
            {
                MessageBox.Show("Please select a TTS provider for the persona", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (TTSOutputVoice.Text == "")
            {
                MessageBox.Show("Please select a voice for the persona", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //ok now we save the persona
            //var newPersonas = new List<Personas>();
            var tmpVoiceOptions = new List<string>();
            if (TTSOutputVoiceOption1.Text != "")
            {
                tmpVoiceOptions.Add(TTSOutputVoiceOption1.Text);
            }
            _personas.Add(new Personas { Name = PersonaComboBox.Text, RoleText = PersonaRoleTextBox.Text, VoiceProvider = TTSProviderComboBox.Text, VoiceName = TTSOutputVoice.Text, VoiceOptions = tmpVoiceOptions });

            //and write the file
            var tmpFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\personas.json";
            _bBBlog.Debug("Writing personas to file: " + _personas.Count);
            using (var sw = new StreamWriter(tmpFile, false))
            {
                sw.Write(JsonConvert.SerializeObject(_personas));
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async Task TTSGetNativeVoices()
        {
            NativeSpeech nativeSpeech = new();
            _nativeRegionVoicesList = await nativeSpeech.TTSNativeGetVoices();
            if (_azureRegionVoicesList == null)
            {
                MessageBox.Show("Problem retreiving Native voicelist. Do you have any native voices installed?", "Native No voices", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _bBBlog.Info($"Found {_nativeRegionVoicesList.Count} voices");
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TTSFillNativeVoicesList()
        {
            _bBBlog.Info("Fill Native voice list");
            TTSOutputVoice.Items.Clear();
            foreach (var nativeVoice in _nativeRegionVoicesList)
            {
                TTSOutputVoice.Items.Add(nativeVoice.Name + "-" + nativeVoice.Gender + "-" + nativeVoice.Culture);
            }
            TTSOutputVoice.Sorted = true;
            TTSOutputVoice.Text = TTSOutputVoice.Items[0].ToString();
            TTSOutputVoiceOption1.Text = "";
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TTSProviderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _bBBlog.Info("TTS Provider changed to " + TTSProviderComboBox.Text);
            await FillVoiceBoxes();
        }

        [SupportedOSPlatform("windows6.1")]
        /// <summary>
        /// This function gets the list of voices available in the Azure TTS API
        /// </summary>
        private async Task<bool> TTSGetAzureVoices()
        {
            //only bother if the two fields are not empty or not "placeholder"
            if ((AzureAPIKeyTextBox.Text.Length < 1) || (AzureRegionTextBox.Text.Length < 1))
            {
                _bBBlog.Error("API Key or region cannot be empty!");
                MessageBox.Show("API Key or region cannot be empty!", "Azure TTS error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            _bBBlog.Info("Finding TTS Azure voices available");
            AzureSpeechAPI AzureSpeech = new(AzureAPIKeyTextBox.Text, AzureRegionTextBox.Text, AzureLanguageComboBox.Text);
            _azureRegionVoicesList = await AzureSpeech.TTSGetAzureVoices();

            if (_azureRegionVoicesList == null)
            {
                MessageBox.Show("Problem retreiving Azure API voicelist. Is your API key or subscription information still valid?", "Azure API Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                _bBBlog.Info($"Found {_azureRegionVoicesList.Count} voices");
                return true;
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
            //select the first item, to have at least something
            TTSOutputVoice.Text = TTSOutputVoice.Items[0].ToString();
            TTSOutputVoiceOption1.Text = "";

            TTSAzureFillOptions(TTSOutputVoice.Text);
        }

        [SupportedOSPlatform("windows6.1")]
        //Azure Text-To-Speach
        private async Task TTSAzureSpeakToOutput(string TextToSpeak)
        {
            _bBBlog.Info("Azure TTS called with text, seting up");
            AzureSpeechAPI azureSpeechAPI = new(AzureAPIKeyTextBox.Text, AzureRegionTextBox.Text, AzureLanguageComboBox.Text);

            //set the output voice, gender and locale, and the style
            await azureSpeechAPI.AzureTTSInit(TTSOutputVoice.Text, TTSOutputVoiceOption1.Text, Properties.Settings.Default.TTSAudioOutput);

            var result = await azureSpeechAPI.AzureSpeak(TextToSpeak);
            if (!result)
            {
                _bBBlog.Error("Azure TTS error. Is there a problem with your API key or subscription?");
                MessageBox.Show("Azure TTS error. Is there a problem with your API key or subscription?", "Azure TTS error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    _bBBlog.Info($"Match found, checking for voice options. Voice has a total of {azureRegionVoice.StyleList.Count}");

                    foreach (var voiceOption in azureRegionVoice.StyleList)
                    {
                        if (voiceOption.Length > 0)
                            TTSOutputVoiceOption1.Items.Add(voiceOption);
                        else
                            TTSOutputVoiceOption1.Items.Add("Default");
                    }
                }
            }
            TTSOutputVoiceOption1.Text = TTSOutputVoiceOption1.Items[0].ToString();
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TestAzureAPISettings_Click(object sender, EventArgs e)
        {
            //test if the Azure API settings are correct
            if (await TTSGetAzureVoices())
            {
                MessageBox.Show("Azure API settings are correct", "Azure API Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TTSOutputVoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TTSProviderComboBox.Text == "Azure")
            {
                //depending on what voice is selected we need to now select the voice options (if any)
                TTSAzureFillOptions(TTSOutputVoice.Text);
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TestVoiceButton_Click(object sender, EventArgs e)
        {

            await SayTextTest("Hi, this is a test123");
        }

        [SupportedOSPlatform("windows6.1")]
        //this is the generic caller for TTS function that makes sure the TTS is done before continuing
        //we make this public so we can call it to test
        public async Task SayTextTest(string TextToSay)
        {
            if (TTSProviderComboBox.Text == "Native")
            {
                await TTSNativeSpeakToOutput(TextToSay);
            }
            else if (TTSProviderComboBox.Text == "Azure")
            {
                //we also need to fill the list and select the first available voice
                //then the options
                if (TTSOutputVoice.Items.Count < 1 || TTSOutputVoice.Text == "placeholder")
                {
                    await TTSGetAzureVoices();
                    TTSFillAzureVoicesList();
                }
                await TTSAzureSpeakToOutput(TextToSay);
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async Task TTSNativeSpeakToOutput(String TTSText)
        {
            _bBBlog.Info("Saying text with Native TTS");
            NativeSpeech nativeSpeech = new();
            await nativeSpeech.NativeTTSInit(TTSOutputVoice.Text, TTSAudioOutputComboBox.Text);
            await nativeSpeech.NativeSpeak(TTSText);
        }

        [SupportedOSPlatform("windows6.1")]
        private async void GPTTestButton_Click(object sender, EventArgs e)
        {
            OpenAIAPI api = new(GPTAPIKeyTextBox.Text);
            if (await api.Auth.ValidateAPIKey())
            {
                _bBBlog.Info("API key is valid");
                MessageBox.Show("API key is valid", "OpenAI API Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _bBBlog.Error("API key is invalid");
                MessageBox.Show("API key is invalid", "OpenAI API Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
