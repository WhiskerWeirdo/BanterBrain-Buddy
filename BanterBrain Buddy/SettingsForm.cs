using Newtonsoft.Json;
using OpenAI_API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BanterBrain_Buddy
{
    public partial class SettingsForm : Form
    {
        //set logger
        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly List<Personas> personas = [];
        private List<AzureVoices> tTSAzureVoicesList = [];
        private List<NativeVoices> nativeRegionVoicesList = [];

        private TwitchAPIESub twitchTestEventSub;
        private bool twitchStartedTest;
        private bool twitchAPIVerified;
        private bool personaEdited;
        private ElLabs elevenLabsApi;

        [SupportedOSPlatform("windows6.1")]
        public SettingsForm()
        {
            InitializeComponent();
            DisablePersonaEventHandlers();
            GetAudioDevices();
            GetInstalledNativeSpeecRecognitionCultures();
            LoadSettings();
            MenuTreeView.ExpandAll();

            //we need to disable the Twitch settings if its running
            if (BBB.isTwitchRunning)
            {
                _bBBlog.Info("Twitch is running, disabling Twitch settings");
                TwitchPanel.Enabled = false;
            }
            else
            {
                _bBBlog.Info("Twitch is not running, enabling Twitch settings");
                TwitchPanel.Enabled = true;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void GetInstalledNativeSpeecRecognitionCultures()
        {
            foreach (RecognizerInfo config in SpeechRecognitionEngine.InstalledRecognizers())
            {
                NativeSpeechRecognitionLanguageComboBox.Items.Add(config.Culture);
            }
        }

        [SupportedOSPlatform("windows6.1")]
        //fill the audio input and output list boxes
        public void GetAudioDevices()
        {
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                SoundInputDevices.Items.Add(NAudio.Wave.WaveIn.GetCapabilities(i).ProductName);
            }

            for (int i = 0; i < NAudio.Wave.WaveOut.DeviceCount; i++)
            {
                TTSAudioOutputComboBox.Items.Add(NAudio.Wave.WaveOut.GetCapabilities(i).ProductName);
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void DisablePersonaEventHandlers()
        {
            _bBBlog.Debug("Disabling Persona screen eventhandlers");
            PersonaComboBox.SelectedValueChanged -= PersonaComboBox_SelectedValueChanged;
            PersonaRoleTextBox.TextChanged -= PersonaRoleTextBox_TextChanged;
            TTSProviderComboBox.SelectedValueChanged -= TTSProviderComboBox_SelectedValueChanged;
            TTSOutputVoice.SelectedValueChanged -= TTSOutputVoice_SelectedValueChanged;
            TTSOutputVoiceOption1.SelectedIndexChanged -= TTSOutputVoiceOption1_SelectedIndexChanged;
            TTSOutputVoiceOption1.TextChanged -= TTSOutputVoiceOption1_TextChanged;
            TTSOutputVoiceOption2.TextChanged -= TTSOutputVoiceOption2_TextChanged;
            TTSOutputVoiceOption3.TextChanged -= TTSOutputVoiceOption3_TextChanged;
        }

        [SupportedOSPlatform("windows6.1")]
        private void EnablePersonaEventHandlers(
            )
        {
            _bBBlog.Debug("Enabling Persona screen eventhandlers");
            PersonaComboBox.SelectedValueChanged += PersonaComboBox_SelectedValueChanged;
            PersonaRoleTextBox.TextChanged += PersonaRoleTextBox_TextChanged;
            TTSProviderComboBox.SelectedValueChanged += TTSProviderComboBox_SelectedValueChanged;
            TTSOutputVoice.SelectedValueChanged += TTSOutputVoice_SelectedValueChanged;
            TTSOutputVoiceOption1.SelectedIndexChanged += TTSOutputVoiceOption1_SelectedIndexChanged;
            TTSOutputVoiceOption1.TextChanged += TTSOutputVoiceOption1_TextChanged;
            TTSOutputVoiceOption2.TextChanged += TTSOutputVoiceOption2_TextChanged;
            TTSOutputVoiceOption3.TextChanged += TTSOutputVoiceOption3_TextChanged;

        }

        [SupportedOSPlatform("windows6.1")]
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowPanels(e.Node.Name);
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
        private void ShowPanels(string selectedNode)
        {

            switch (selectedNode)
            {
                case "VoiceSettings":
                    PanelVisible(MicrophonePanel.Name);
                    break;
                case "Microphone":
                    PanelVisible(MicrophonePanel.Name);
                    break;
                case "APISettings":
                    PanelVisible(AzurePanel.Name);
                    break;
                case "Azure":
                    PanelVisible(AzurePanel.Name);
                    break;
                case "Speaker":
                    PanelVisible(SpeakerPanel.Name);
                    break;
                case "OpenAIChatGPT":
                    PanelVisible(OpenAIChatGPTPanel.Name);
                    break;
                case "Twitch":
                    PanelVisible(TwitchPanel.Name);
                    break;
                case "ElevenLabs":
                    PanelVisible(ElevenLabsPanel.Name);
                    break;
                case "OllamaLLM":
                    PanelVisible(OllamaPanel.Name);
                    break;
                case "StreamingSettings":
                    PanelVisible(TwitchPanel.Name);
                    break;
                case "TwitchTriggers":
                    PanelVisible(TwitchPanel.Name);
                    break;
                case "NativeSpeech":
                    PanelVisible(NativeSpeechPanel.Name);
                    break;
                case "Personas":
                    PanelVisible(PersonasPanel.Name);
                    break;
                default:
                    break;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void LoadSettings()
        {
            _bBBlog.Info("Loading settings");
            TwitchUsername.Text = Properties.Settings.Default.TwitchUsername;
            TwitchAccessToken.Text = Properties.Settings.Default.TwitchAccessToken;
            TwitchChannel.Text = Properties.Settings.Default.TwitchChannel;
            TwitchSendTextCheckBox.Checked = Properties.Settings.Default.TwitchSendTextCheckBox;
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
            ElevenlabsAPIKeyTextBox.Text = Properties.Settings.Default.ElevenLabsAPIkey;
            OllamaModelsComboBox.SelectedIndex = OllamaModelsComboBox.FindStringExact(Properties.Settings.Default.OllamaSelectedModel);
            OllamaResponseLengthComboBox.SelectedIndex = OllamaResponseLengthComboBox.FindStringExact(Properties.Settings.Default.OllamaResponseLengthComboBox);
            OllamaURITextBox.Text = Properties.Settings.Default.OllamaURI;
            UseOllamaLLMCheckBox.Checked = Properties.Settings.Default.UseOllamaLLMCheckBox;
            TwitchAuthServerConfig.Text = Properties.Settings.Default.TwitchAuthServerConfig;
            //if empty set default
            if (Properties.Settings.Default.TwitchAuthServerConfig.Length < 1)
            {
                TwitchAuthServerConfig.Text = "http://localhost:8080";
            }
            if (Properties.Settings.Default.WhisperSpeechRecognitionComboBox.Length > 1)
            {
                WhisperSpeechRecognitionComboBox.SelectedIndex = WhisperSpeechRecognitionComboBox.FindStringExact(Properties.Settings.Default.WhisperSpeechRecognitionComboBox);
            }
            else
            {
                //ok nothing is set, so lets just select the first one by default
                _bBBlog.Info("No whisper speech recognition language set, selecting English");
                WhisperSpeechRecognitionComboBox.SelectedIndex = WhisperSpeechRecognitionComboBox.FindStringExact("English");
            }

            if (Properties.Settings.Default.NativeSpeechRecognitionLanguageComboBox.Length > 1)
            {
                NativeSpeechRecognitionLanguageComboBox.SelectedIndex = NativeSpeechRecognitionLanguageComboBox.FindStringExact(Properties.Settings.Default.NativeSpeechRecognitionLanguageComboBox);
            }
            else
            {
                //ok nothing is set, so lets just select the first one by default
                _bBBlog.Info("No native speech recognition language set, selecting the first one");
                NativeSpeechRecognitionLanguageComboBox.SelectedIndex = 0;
            }

            if (!UseOllamaLLMCheckBox.Checked)
            {
                OllamaURITextBox.Enabled = false;
                OllamaModelsComboBox.Enabled = false;
                OllamaResponseLengthComboBox.Enabled = false;
                OllamaTestButton.Enabled = false;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (personaEdited)
            {
                _bBBlog.Debug("Personas panel hidden. we should save the persona's if anything changed");
                //ask if we need to save the persona
                var result = MessageBox.Show("Information changed. Do you want to save the persona?", "Save Persona", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SavePersona_Click(sender, e);
                }
                personaEdited = false;
            }
            _bBBlog.Info("Settings form closing, saving settings");
            //only save if theres actual data to be saved
            if (TwitchUsername.Text.Length > 0)
                Properties.Settings.Default.TwitchUsername = TwitchUsername.Text;
            if (TwitchAccessToken.Text.Length > 0)
                Properties.Settings.Default.TwitchAccessToken = TwitchAccessToken.Text;
            if (TwitchChannel.Text.Length > 0)
                Properties.Settings.Default.TwitchChannel = TwitchChannel.Text;
            Properties.Settings.Default.TwitchSendTextCheckBox = TwitchSendTextCheckBox.Checked;
            if (SoundInputDevices.Text.Length > 0)
                Properties.Settings.Default.VoiceInput = SoundInputDevices.Text;
            if (MicrophoneHotkeyEditbox.Text.Length > 0)
                Properties.Settings.Default.PTTHotkey = MicrophoneHotkeyEditbox.Text;
            if (TTSAudioOutputComboBox.Text.Length > 0)
                Properties.Settings.Default.TTSAudioOutput = TTSAudioOutputComboBox.Text;
            if (AzureAPIKeyTextBox.Text.Length > 0)
                Properties.Settings.Default.AzureAPIKeyTextBox = AzureAPIKeyTextBox.Text;
            if (AzureRegionTextBox.Text.Length > 0)
                Properties.Settings.Default.AzureRegionTextBox = AzureRegionTextBox.Text;
            if (AzureLanguageComboBox.Text.Length > 0)
                Properties.Settings.Default.AzureLanguageComboBox = AzureLanguageComboBox.Text;
            if (GPTModelComboBox.Text.Length > 0)
                Properties.Settings.Default.GPTModel = GPTModelComboBox.Text;
            if (GPTAPIKeyTextBox.Text.Length > 0)
                Properties.Settings.Default.GPTAPIKey = GPTAPIKeyTextBox.Text;
            if (GPTMaxTokensTextBox.Text.Length > 0)
                Properties.Settings.Default.GPTMaxTokens = int.Parse(GPTMaxTokensTextBox.Text);
            if (GPTTemperatureTextBox.Text.Length > 0)
                Properties.Settings.Default.GPTTemperature = float.Parse(GPTTemperatureTextBox.Text);
            if (ElevenlabsAPIKeyTextBox.Text.Length > 0)
                Properties.Settings.Default.ElevenLabsAPIkey = ElevenlabsAPIKeyTextBox.Text;
            if (OllamaModelsComboBox.Text.Length > 0)
                Properties.Settings.Default.OllamaSelectedModel = OllamaModelsComboBox.Text;
            if (OllamaResponseLengthComboBox.Text.Length > 0)
                Properties.Settings.Default.OllamaResponseLengthComboBox = OllamaResponseLengthComboBox.Text;
            if (OllamaURITextBox.Text.Length > 0)
                Properties.Settings.Default.OllamaURI = OllamaURITextBox.Text;
            if (NativeSpeechRecognitionLanguageComboBox.Text.Length > 1)
                Properties.Settings.Default.NativeSpeechRecognitionLanguageComboBox = NativeSpeechRecognitionLanguageComboBox.Text;
            if (WhisperSpeechRecognitionComboBox.Text.Length > 1)
                Properties.Settings.Default.WhisperSpeechRecognitionComboBox = WhisperSpeechRecognitionComboBox.Text;
            if (TwitchAuthServerConfig.Text.Length > 1)
                Properties.Settings.Default.TwitchAuthServerConfig = TwitchAuthServerConfig.Text;
            Properties.Settings.Default.UseOllamaLLMCheckBox = UseOllamaLLMCheckBox.Checked;

            Properties.Settings.Default.Save();

            //we should also close the EventSub client if it is running
            if (twitchStartedTest)
            {
                _bBBlog.Info("Closing EventSub client");
                await twitchTestEventSub.EventSubStopAsync();
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void SoundInputDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            _bBBlog.Info("Selected input device changed to " + SoundInputDevices.Text);
        }

        [SupportedOSPlatform("windows6.1")]
        private async void PersonasPanel_VisibleChanged(object sender, EventArgs e)
        {
            //we dont need the eventhandlers when its becoming visible
            if (PersonasPanel.Visible)
            {
                //TTSProvider combobox needs to be filled with the available providers
                TTSProviderComboBox.Items.Clear();
                TTSProviderComboBox.Items.Add("Native");
                if (AzureAPIKeyTextBox.Text.Length > 0 && AzureRegionTextBox.Text.Length > 0)
                    TTSProviderComboBox.Items.Add("Azure");
                if (GPTAPIKeyTextBox.Text.Length > 0)
                    TTSProviderComboBox.Items.Add("OpenAI Whisper");
                if (ElevenlabsAPIKeyTextBox.Text.Length > 0)
                    TTSProviderComboBox.Items.Add("ElevenLabs");
                TTSProviderComboBox.SelectedIndex = 0;

                _bBBlog.Debug("Personas panel visible. we need to load the persona's and enable the eventhandlers");
                await LoadPersonas();
                while (PersonaComboBox.Items.Count == 0)
                {
                    await Task.Delay(1000);
                }


                //TODO: set the originally selected persona for now just load the first one
                PersonaComboBox.SelectedIndex = 0;
                var selectedPersona = personas[PersonaComboBox.SelectedIndex];
                PersonaRoleTextBox.Text = selectedPersona.RoleText;
                TTSProviderComboBox.SelectedIndex = TTSProviderComboBox.FindStringExact(selectedPersona.VoiceProvider);
                //we need to fill the combo box with the voices available. We do that here cos provider needs to be loaded first.
                await FillVoiceBoxes();
                _bBBlog.Debug($"Voice boxes filled, now to select the voice. Personavoice: {selectedPersona.VoiceName} ");
                TTSOutputVoice.SelectedIndex = TTSOutputVoice.FindStringExact(selectedPersona.VoiceName);
                personaEdited = false;

                //prevent deleting of Default persona
                if (PersonaComboBox.Text == "Default")
                {
                    DeletePersona.Enabled = false;
                }
                else
                {
                    DeletePersona.Enabled = true;
                }
                EnablePersonaEventHandlers();
            }
            else
            {
                if (personaEdited)
                {
                    _bBBlog.Debug("Personas panel hidden. we should save the persona's if anything changed");
                    //ask if we need to save the persona
                    var result = MessageBox.Show("Information changed. Do you want to save the persona?", "Save Persona", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        SavePersona_Click(sender, e);
                    }
                }
                DisablePersonaEventHandlers();
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async Task FillVoiceBoxes()
        {
            _bBBlog.Info("Fill voice boxes: " + TTSProviderComboBox.Text);
            if (TTSProviderComboBox.Text == "Native")
            {
                TTSOutputVoice.Text = "";
                TTSOutputVoiceOption1.Visible = false;
                TTSOption1Label.Visible = false;
                TTSOutputVoiceOption2.Text = "";
                TTSOutputVoiceOption2.Visible = false;
                TTSOption2Label.Visible = false;
                TTSOutputVoiceOption3.Text = "";
                TTSOutputVoiceOption3.Visible = false;
                TTSOption3Label.Visible = false;
                //clear and fill the option box with voices
                await TTSGetNativeVoices();
                TTSFillNativeVoicesList();
            }
            else if (TTSProviderComboBox.Text == "ElevenLabs")
            {
                TTSOutputVoice.Text = "";
                TTSOutputVoiceOption1.Visible = true;
                TTSOption1Label.Visible = true;
                TTSOutputVoiceOption1.DropDownStyle = ComboBoxStyle.Simple;
                TTSOption1Label.Text = "Similarity  (0-100)";
                TTSOutputVoiceOption2.Visible = true;
                TTSOutputVoiceOption2.DropDownStyle = ComboBoxStyle.Simple;
                TTSOption2Label.Visible = true;
                TTSOption2Label.Text = "Stability (0-100)";
                TTSOutputVoiceOption3.Visible = true;
                TTSOption3Label.Visible = true;
                TTSOutputVoiceOption3.DropDownStyle = ComboBoxStyle.Simple;
                TTSOption3Label.Text = "Style (0-100)";
                await TTSGetElevenLabsVoices();
            }
            else if (TTSProviderComboBox.Text == "Azure")
            {
                TTSOutputVoice.Text = "";
                TTSOutputVoiceOption1.Visible = true;
                TTSOption1Label.Visible = true;
                TTSOption1Label.Text = "Style";
                TTSOutputVoiceOption1.DropDownStyle = ComboBoxStyle.DropDownList;
                TTSOutputVoiceOption2.Visible = false;
                TTSOutputVoiceOption2.Text = "";
                TTSOption2Label.Visible = false;
                TTSOutputVoiceOption3.Visible = false;
                TTSOption3Label.Visible = false;
                TTSOutputVoiceOption3.Text = "";

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
            else if (TTSProviderComboBox.Text == "OpenAI Whisper")
            {
                TTSOutputVoice.Text = "";
                TTSOutputVoiceOption1.Visible = false;
                TTSOption1Label.Visible = false;
                TTSOutputVoiceOption2.Visible = false;
                TTSOption2Label.Visible = false;
                TTSOutputVoiceOption3.Visible = false;
                TTSOption3Label.Visible = false;
                //these voices are static
                //alloy, echo, fable, onyx, nova, and shimmer
                TTSOutputVoice.Items.Clear();
                TTSOutputVoice.Items.Add("alloy");
                TTSOutputVoice.Items.Add("echo");
                TTSOutputVoice.Items.Add("fable");
                TTSOutputVoice.Items.Add("onyx");
                TTSOutputVoice.Items.Add("nova");
                TTSOutputVoice.Items.Add("shimmer");
                TTSOutputVoice.Sorted = true;
                TTSOutputVoice.Text = TTSOutputVoice.Items[0].ToString();
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async Task LoadPersonas()
        {
            PersonaComboBox.Items.Clear();
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
                personas.Clear();
                PersonaComboBox.Items.Clear();
                using var sr = new StreamReader(tmpFile);
                var tmpString = sr.ReadToEnd();
                //var tmpPersonas = JsonConvert.DeserializeObject<List<Personas>>(tmpString);
                var tmpPersonas = JsonConvert.DeserializeObject<List<Personas>>(tmpString);
                foreach (var persona in tmpPersonas)
                {
                    _bBBlog.Debug("Loading persona into available list: " + persona.Name);
                    personas.Add(persona);
                    PersonaComboBox.Items.Add(persona.Name);
                }
            }

        }


        [SupportedOSPlatform("windows6.1")]
        private void NewPersona_Click(object sender, EventArgs e)
        {
            //if the persona is changed we first need to check if the persona was edited if so we need to ask to save it
            if (personaEdited && PersonasPanel.Visible)
            {
                var result = MessageBox.Show("New Persona. Do you want to save the persona?", "Save Persona", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SavePersona_Click(sender, e);

                }
                personaEdited = false;
            }
            PersonaRoleTextBox.Text = "";
            TTSProviderComboBox.Text = "";
            TTSOutputVoice.Text = "";
            PersonaComboBox.Text = "";
            TTSOutputVoiceOption1.Text = "";
        }

        [SupportedOSPlatform("windows6.1")]
        private void SavePersona_Click(object sender, EventArgs e)
        {
            personaEdited = false;
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

            //if the persona.name is already in the file, we need to update it not add
            //else we do add it
            bool personaExists = false;
            foreach (var persona in personas)
            {
                if (persona.Name == PersonaComboBox.Text)
                {
                    personaExists = true;
                    _bBBlog.Debug("Persona already exists, updating it");
                    persona.RoleText = PersonaRoleTextBox.Text;
                    persona.VoiceProvider = TTSProviderComboBox.Text;
                    persona.VoiceName = TTSOutputVoice.Text;
                    persona.VoiceOptions.Clear();
                    if (TTSOutputVoiceOption1.Text != "")
                    {
                        persona.VoiceOptions.Add(TTSOutputVoiceOption1.Text);
                    }
                    if (TTSOutputVoiceOption2.Text != "")
                    {
                        persona.VoiceOptions.Add(TTSOutputVoiceOption2.Text);
                    }
                    if (TTSOutputVoiceOption3.Text != "")
                    {
                        persona.VoiceOptions.Add(TTSOutputVoiceOption3.Text);
                    }
                }
            }
            if (!personaExists)
            {
                _bBBlog.Debug("Persona does not exist, adding it");

                var tmpVoiceOptions = new List<string>();
                if (TTSOutputVoiceOption1.Text != "")
                {
                    tmpVoiceOptions.Add(TTSOutputVoiceOption1.Text);
                }
                personas.Add(new Personas { Name = PersonaComboBox.Text, RoleText = PersonaRoleTextBox.Text, VoiceProvider = TTSProviderComboBox.Text, VoiceName = TTSOutputVoice.Text, VoiceOptions = tmpVoiceOptions });

            }
            //and write the file
            var tmpFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BanterBrain\\personas.json";
            _bBBlog.Debug("Writing personas to file: " + personas.Count);
            using (var sw = new StreamWriter(tmpFile, false))
            {
                sw.Write(JsonConvert.SerializeObject(personas));
            }
            personaEdited = false;
            SavePersona.Enabled = false;
            return;
        }

        [SupportedOSPlatform("windows6.1")]
        private async Task TTSGetNativeVoices()
        {
            _ = new NativeSpeech();
            nativeRegionVoicesList = await NativeSpeech.TTSNativeGetVoices();
            if (tTSAzureVoicesList == null)
            {
                MessageBox.Show("Problem retreiving Native voicelist. Do you have any native voices installed?", "Native No voices", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _bBBlog.Info($"Found {nativeRegionVoicesList.Count} voices");
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async Task TTSGetElevenLabsVoices()
        {
            elevenLabsApi ??= new(ElevenlabsAPIKeyTextBox.Text);

            var _elevenLabsVoicesList = await elevenLabsApi.TTSGetElevenLabsVoices();
            if (_elevenLabsVoicesList == null)
            {
                MessageBox.Show("Problem retreiving ElevenLabs voicelist. Is your API key still valid?", "ElevenLabs No voices", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                _bBBlog.Info($"Found {_elevenLabsVoicesList.Count} voices");
            }
            TTSOutputVoice.Items.Clear();
            foreach (var elevenLabsVoice in _elevenLabsVoicesList)
            {
                TTSOutputVoice.Items.Add(elevenLabsVoice);
            }
            TTSOutputVoice.Sorted = true;
            TTSOutputVoice.Text = TTSOutputVoice.Items[0].ToString();
            if (TTSOutputVoiceOption2.Text.Length < 1)
            {
                TTSOutputVoiceOption1.Text = "100";
                TTSOutputVoiceOption2.Text = "100";
                TTSOutputVoiceOption3.Text = "0";
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TTSFillNativeVoicesList()
        {
            _bBBlog.Info("Fill Native voice list");
            //no need to clear we only add if we find new things
            TTSOutputVoice.Items.Clear();
            foreach (var nativeVoice in nativeRegionVoicesList)
            {
                string OutputVoice = nativeVoice.Name + "-" + nativeVoice.Gender + "-" + nativeVoice.Culture;
                TTSOutputVoice.Items.Add(OutputVoice);
            }
            TTSOutputVoice.Sorted = true;
            TTSOutputVoice.Text = TTSOutputVoice.Items[0].ToString();
            TTSOutputVoiceOption1.Text = "";
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

            AzureSpeechAPI AzureSpeech = new(AzureAPIKeyTextBox.Text, AzureRegionTextBox.Text, AzureLanguageComboBox.Text);
            tTSAzureVoicesList = await AzureSpeech.TTSGetAzureVoices();
            if (tTSAzureVoicesList == null)
            {
                MessageBox.Show("Problem retreiving Azure API voicelist. Is your API key or subscription information still valid?", "Azure API Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                _bBBlog.Info($"Found {tTSAzureVoicesList.Count} voices");
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
            foreach (var azureRegionVoice in tTSAzureVoicesList)
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
            foreach (var azureRegionVoice in tTSAzureVoicesList)
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
        private async void TestVoiceButton_Click(object sender, EventArgs e)
        {
            TestVoiceButton.Enabled = false;
            await SayTextTest("Hi, this is a test123");
            TestVoiceButton.Enabled = true;
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
            else if (TTSProviderComboBox.Text == "OpenAI Whisper")
            {
                await TTSOpenAIWhisperSpeakToOutput(TextToSay);
            }
            else if (TTSProviderComboBox.Text == "ElevenLabs")
            {
                await TTSElevenLabsSpeakToOutput(TextToSay);
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async Task TTSOpenAIWhisperSpeakToOutput(string TextToSay)
        {
            OpenAI openAI = new();
            var result = await openAI.OpenAITTS(TextToSay, TTSAudioOutputComboBox.Text, TTSOutputVoice.Text);
            if (!result)
            {
                _bBBlog.Error("OpenAI Whisper TTS error. Is there a problem with your API key or subscription?");
                MessageBox.Show("OpenAI Whisper TTS error. Is there a problem with your API key or subscription?", "OpenAI Whisper TTS error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async Task TTSElevenLabsSpeakToOutput(string TextToSay)
        {
            elevenLabsApi ??= new(ElevenlabsAPIKeyTextBox.Text);

            var result = await elevenLabsApi.ElevenLabsTTS(TextToSay, TTSAudioOutputComboBox.Text, TTSOutputVoice.Text, int.Parse(TTSOutputVoiceOption1.Text), int.Parse(TTSOutputVoiceOption2.Text), int.Parse(TTSOutputVoiceOption3.Text));
            if (!result)
            {
                _bBBlog.Error("ElevenLabs TTS error. Is there a problem with your API key or subscription?");
                MessageBox.Show("ElevenLabs TTS error. Is there a problem with your API key or subscription?", "ElevenLabs TTS error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                _bBBlog.Info("ChatGPT API key is valid");
                MessageBox.Show("ChatGPT API key is valid", "OpenAI API Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _bBBlog.Error("ChatGPT API key is invalid");
                MessageBox.Show("ChatGPT API key is invalid", "OpenAI API Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        private async void TwitchTestButton_Click(object sender, EventArgs e)
        {
            //first lets make sure people cant click too often
            twitchAPIVerified = false;
            TwitchAPITestButton.Enabled = false;
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
                //  TextLog.Text += "Problem verifying Access token, invalid access token\r\n";
                MessageBox.Show("Problem verifying Access token, invalid access token", "Twitch Access Token veryfication result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TwitchAPITestButton.Enabled = true;
                twitchAPIVerified = false;
                TwitchEventSubTestButton.Enabled = false;
                return;
            }
            else
            {
                _bBBlog.Info($"Twitch Access token verified success!");
                twitchAPIVerified = true;
                TwitchEventSubTestButton.Enabled = true;
                MessageBox.Show($"Twitch Access token verified success!", "Twitch Access Token verification result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            TwitchAPITestButton.Enabled = true;
        }

        [SupportedOSPlatform("windows6.1")]
        private async void EventSubTest_Click(object sender, EventArgs e)
        {
            //This only works once API access-token is verified
            if (!twitchAPIVerified)
            {
                MessageBox.Show("You need to verify the API key first.", "Twitch EventSub error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (await EventSubStartWebsocketClientTest())
            {
                MessageBox.Show("EventSub server started successfully so all is well!", "Twitch EventSub success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Issue with starting EventSub server. Check logs for more information.", "Twitch EventSub error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //here we star the main websocket client for Twitch EventSub
        [SupportedOSPlatform("windows6.1")]
        private async Task<bool> EventSubStartWebsocketClientTest()
        {
            twitchTestEventSub = new();
            bool eventSubStart = false;
            //we should set here what eventhandlers we want to have enabled based on the twitch Settings

            if (await twitchTestEventSub.EventSubInit(TwitchAccessToken.Text, TwitchUsername.Text, TwitchChannel.Text))
            {
                //we need to first set the event handlers we want to use

                //if we are in MOCK mode, just enable all handlers?
                if (TwitchMockEventSub.Checked)
                {
                    _bBBlog.Info("Twitch TEST read chat enabled, calling eventsubhandlereadchat. Command set to $BBB for test");
                    twitchTestEventSub.EventSubHandleReadchat("$BBB", 300, false, true);
                    //set local eventhanlder for valid chat messages to trigger the bot
                    twitchTestEventSub.OnESubChatMessage += TwitchEventSub_OnESubChatMessage;
                }

                //do we want to check cheer messages?
                if (TwitchMockEventSub.Checked)
                {
                    _bBBlog.Info("Twitch TEST cheers enabled, calling EventSubHandleCheer with the min amount of bits needed to trigger. Set to 10 for test");
                    twitchTestEventSub.EventSubHandleCheer(10);
                    twitchTestEventSub.OnESubCheerMessage += TwitchEventSub_OnESubCheerMessage;
                }

                //do we want to check for subscription events?
                if (TwitchMockEventSub.Checked)
                {
                    //new subs
                    _bBBlog.Info($"Twitch TEST subscriptions enabled, calling EventSubHandleSubscription: {twitchTestEventSub}");
                    twitchTestEventSub.EventSubHandleSubscription();
                    twitchTestEventSub.OnESubSubscribe += TwitchEventSub_OnESubSubscribe;
                    twitchTestEventSub.OnESubReSubscribe += TwitchEventSub_OnESubReSubscribe;
                    //todo set eventhandler being thrown when a new sub is detected or resub
                }
                //TODO: gifted subs TwitchGiftedSub.Checked
                if (TwitchMockEventSub.Checked)
                {
                    _bBBlog.Info("Twitch TEST gifted subs enabled, calling EventSubHandleGiftedSubs");
                    twitchTestEventSub.EventSubHandleSubscriptionGift();
                    twitchTestEventSub.OnESubSubscriptionGift += TwitchEventSub_OnESubGiftedSub;
                }

                //do we want to check for channel point redemptions?
                if (TwitchMockEventSub.Checked)
                {
                    _bBBlog.Info("Twitch TEST channel points enabled, calling EventSubHandleChannelPoints");
                    twitchTestEventSub.EventSubHandleChannelPointRedemption("rewardtest");
                    twitchTestEventSub.OnESubChannelPointRedemption += TwitchEventSub_OnESubChannelPointRedemption;
                }

                if (!TwitchMockEventSub.Checked)
                {
                    //if we are not in mock mode, we can start the client
                    eventSubStart = await twitchTestEventSub.EventSubStartAsync();
                }
                else
                { //we are in mock mode, so we just say we started
                    _bBBlog.Info("Twitch EventSub client  starting successfully in mock mode");
                    eventSubStart = await twitchTestEventSub.EventSubStartAsyncMock();

                    twitchStartedTest = true;
                }

                if (eventSubStart)
                {
                    _bBBlog.Info("Twitch EventSub client  started successfully");

                    twitchStartedTest = true;
                }
                else
                {
                    _bBBlog.Error("Issue with starting Twitch EventSub server. Check logs for more information.");
                    twitchStartedTest = false;
                    return false;
                }
                return true;
            }
            else
            {
                _bBBlog.Error("Issue with starting Twitch EventSub server. Check logs for more information.");
                twitchStartedTest = false;
                return false;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchEventSub_OnESubChannelPointRedemption(object sender, TwitchEventhandlers.OnChannelPointCustomRedemptionEventArgs e)
        {
            string user = e.GetChannelPointCustomRedemptionInfo()[0];
            string message = e.GetChannelPointCustomRedemptionInfo()[1];
            _bBBlog.Info($"TEST: Valid Twitch Channel Point Redemption message received: {user} redeemed with message: {message}");
        }

        [SupportedOSPlatform("windows6.1")]
        //TwitchEventSub_OnESubGiftedSub
        private void TwitchEventSub_OnESubGiftedSub(object sender, TwitchEventhandlers.OnSubscriptionGiftEventArgs e)
        {
            string user = e.GetSubscriptionGiftInfo()[0];
            string amount = e.GetSubscriptionGiftInfo()[1];
            string tier = e.GetSubscriptionGiftInfo()[2];
            _bBBlog.Info($"TEST: Valid Twitch Gifted Subscription message received: {user} gifted {amount} subs tier {tier}");
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchEventSub_OnESubSubscribe(object sender, TwitchEventhandlers.OnSubscribeEventArgs e)
        {
            string user = e.GetSubscribeInfo()[0];
            string broadcaster = e.GetSubscribeInfo()[1];
            _bBBlog.Info($"TEST: Valid Twitch Subscription message received: {user} subscribed to {broadcaster}");
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchEventSub_OnESubReSubscribe(object sender, TwitchEventhandlers.OnReSubscribeEventArgs e)
        {
            string user = e.GetSubscribeInfo()[0];
            string message = e.GetSubscribeInfo()[1];
            string months = e.GetSubscribeInfo()[2];
            _bBBlog.Info($"TEST: Valid Twitch Re-Subscription message received: {user} subscribed for {months} months with message: {message}");
        }

        [SupportedOSPlatform("windows6.1")]
        //eventhandler for valid chat messages trigger
        private void TwitchEventSub_OnESubChatMessage(object sender, TwitchEventhandlers.OnChatEventArgs e)
        {

            string message = e.GetChatInfo()[1].Replace("$BBB", "");
            string user = e.GetChatInfo()[0];
            //we got a valid chat message, lets see what we can do with it
            _bBBlog.Info("TEST: Valid Twitch Chat message received from user: " + user + " message: " + message);
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchEventSub_OnESubCheerMessage(object sender, TwitchEventhandlers.OnCheerEventsArgs e)
        {
            //string user = e.GetCheerInfo()[0];
            // string message = e.GetCheerInfo()[1];
            //we got a valid cheer message, lets see what we can do with it
            _bBBlog.Debug("TEST: Valid Twitch Cheer message received");
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (twitchAPIVerified)
            {
                TwitchEventSubTestButton.Enabled = true;
            }
            else
            {
                TwitchEventSubTestButton.Enabled = false;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void PersonaRoleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (PersonasPanel.Visible)
            {
                _bBBlog.Debug("Persona role text changed");
                personaEdited = true;
                SavePersona.Enabled = true;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TTSOutputVoiceOption1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PersonasPanel.Visible)
            {
                _bBBlog.Debug("Persona voice option changed");
                personaEdited = true;
                SavePersona.Enabled = true;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void PersonaComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            PersonasPanel.Enabled = false;
            _bBBlog.Debug($"Persona selected value changed to {PersonaComboBox.Text} index {PersonaComboBox.SelectedIndex}");
            _bBBlog.Debug($"Disable eventhandlers for now while loading the new persona data");
            DisablePersonaEventHandlers();
            //if the persona is changed we first need to check if the persona was edited if so we need to ask to save it
            if (personaEdited && PersonasPanel.Visible)
            {
                var result = MessageBox.Show("Persona Changed. Do you want to save the persona?", "Save Persona", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SavePersona_Click(sender, e);

                }
                personaEdited = false;
                SavePersona.Enabled = false;
            }

            //alright index is changed! We need to load the persona into the form
            var selectedPersona = personas[PersonaComboBox.SelectedIndex];
            _bBBlog.Debug($"Loading persona into form. Personaname: {selectedPersona.VoiceName} VoiceProvider: {selectedPersona.VoiceProvider} TTSOutputvoice: {selectedPersona.VoiceName}  Amount of options: {selectedPersona.VoiceOptions.Count}");
            PersonaRoleTextBox.Text = selectedPersona.RoleText;
            //now lets load the one thats part of the selected persona
            TTSProviderComboBox.SelectedIndex = TTSProviderComboBox.FindStringExact(selectedPersona.VoiceProvider);
            //the provider can be different from the one before so we need to load teh voices
            await FillVoiceBoxes();
            _bBBlog.Debug($"Voice boxes filled, now to select the voice. Personavoice: {selectedPersona.VoiceName} ");
            TTSOutputVoice.SelectedIndex = TTSOutputVoice.FindStringExact(selectedPersona.VoiceName);
            //now to fill the options field
            if (TTSProviderComboBox.Text == "Azure")
                TTSAzureFillOptions(TTSOutputVoice.Text);
            if (selectedPersona.VoiceOptions.Count > 0)
            {
                _bBBlog.Debug("Voice options found, loading them into the combo box");
                if (TTSProviderComboBox.Text == "Azure")
                    TTSOutputVoiceOption1.SelectedIndex = TTSOutputVoiceOption1.FindStringExact(selectedPersona.VoiceOptions[0]);
                if (TTSProviderComboBox.Text == "ElevenLabs")
                {
                    _bBBlog.Debug("Persona Elevenlabs voice options found, loading them into the combo box");
                    _bBBlog.Debug($"Option1: {selectedPersona.VoiceOptions[0]} option2: {selectedPersona.VoiceOptions[1]} option3: {selectedPersona.VoiceOptions[2]}");
                    TTSOutputVoiceOption1.Text = selectedPersona.VoiceOptions[0];
                    TTSOutputVoiceOption2.Text = selectedPersona.VoiceOptions[1];
                    TTSOutputVoiceOption3.Text = selectedPersona.VoiceOptions[2];
                }
                /*
        foreach (var voiceOption in selectedPersona.VoiceOptions)
        {
            _bBBlog.Debug("Adding voice option: " + voiceOption);
            TTSOutputVoiceOption1.SelectedIndex = TTSOutputVoiceOption1.FindStringExact(voiceOption);
        }*/
            }
            if (PersonaComboBox.Text == "Default")
            {
                DeletePersona.Enabled = false;
            }
            else
            {
                DeletePersona.Enabled = true;
            }
            //enable them again.
            EnablePersonaEventHandlers();
            PersonasPanel.Enabled = true;
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TTSProviderComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            PersonasPanel.Enabled = false;
            _bBBlog.Debug("TTSProvider selected value changed");
            if (PersonasPanel.Visible)
            {
                SavePersona.Enabled = true;
                personaEdited = true;
                _bBBlog.Info("TTS Provider changed to " + TTSProviderComboBox.Text);
                TTSOutputVoiceOption1.Text = "";
                TTSOutputVoiceOption2.Text = "";
                TTSOutputVoiceOption3.Text = "";
                await FillVoiceBoxes();
            }
            PersonasPanel.Enabled = true;
        }

        [SupportedOSPlatform("windows6.1")]
        private void TTSOutputVoice_SelectedValueChanged(object sender, EventArgs e)
        {
            if (PersonasPanel.Visible)
            {
                SavePersona.Enabled = true;
                personaEdited = true;
                if (TTSProviderComboBox.Text == "Azure")
                {
                    _bBBlog.Info($"Azure voice changed, filling options on {TTSOutputVoice.Text}");
                    //depending on what voice is selected we need to now select the voice options (if any)
                    TTSAzureFillOptions(TTSOutputVoice.Text);
                }
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void MenuTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (personaEdited)
            {
                _bBBlog.Debug("Personas panel hidden. we should save the persona's if anything changed");
                //ask if we need to save the persona
                var result = MessageBox.Show("Information changed. Do you want to save the persona?", "Save Persona", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SavePersona_Click(sender, e);
                }
                personaEdited = false;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        public void ShowHotkeyDialogBox()
        {
            HotkeyForm HotkeyDialog = new();

            var result = HotkeyDialog.ShowDialog(this);
            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (result == DialogResult.OK && HotkeyDialog.ReturnValue1 != null)
            {
                // _setHotkeys.Clear();
                this.MicrophoneHotkeyEditbox.Text = "";
                List<Keys> hotKeys = HotkeyDialog.ReturnValue1;
                //ok now we got the keys, parse them and put them in the index box
                // and the global list for hotkeys

                for (var i = 0; i < hotKeys.Count; i++)
                {

                    if (i < hotKeys.Count - 1)
                        this.MicrophoneHotkeyEditbox.Text += hotKeys[i].ToString() + " + ";
                    else
                        this.MicrophoneHotkeyEditbox.Text += hotKeys[i].ToString();

                }
            }
            _bBBlog.Info("Hotkey set to " + MicrophoneHotkeyEditbox.Text);
            HotkeyDialog.Dispose();
        }

        [SupportedOSPlatform("windows6.1")]
        private void MicrophoneHotkeySet_Click(object sender, EventArgs e)
        {
            ShowHotkeyDialogBox();
        }

        [SupportedOSPlatform("windows6.1")]
        private void DeletePersona_Click(object sender, EventArgs e)
        {
            if (PersonasPanel.Visible)
            {
                if (PersonaComboBox.Text == "Default")
                {
                    MessageBox.Show("You cannot delete the default persona", "Delete Persona", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var result = MessageBox.Show("Delete Persona. Do you want to delete the persona?", "Delete Persona", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    personas.Remove(personas[PersonaComboBox.SelectedIndex]);
                    PersonaComboBox.Items.Remove(PersonaComboBox.SelectedItem);
                    PersonaComboBox.SelectedIndex = 0;
                    SavePersona_Click(sender, e);
                }
                personaEdited = false;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void ElevenLabsTestButton_Click(object sender, EventArgs e)
        {
            //call test api key for elevenlabs
            elevenLabsApi ??= new(ElevenlabsAPIKeyTextBox.Text);

            if (await elevenLabsApi.ElevenLabsAPIKeyTest())
            {
                _bBBlog.Info("ElevenLabs API key is valid");
                MessageBox.Show("ElevenLabs API key is valid", "ElevenLabs API Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _bBBlog.Error("ElevenLabs API key is invalid");
                MessageBox.Show("ElevenLabs API key is invalid", "ElevenLabs API Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TTSOutputVoiceOption1_TextChanged(object sender, EventArgs e)
        {
            if (PersonasPanel.Visible && TTSOutputVoiceOption1.DropDownStyle == ComboBoxStyle.Simple)
            {
                SavePersona.Enabled = true;
                personaEdited = true;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TTSOutputVoiceOption2_TextChanged(object sender, EventArgs e)
        {
            if (PersonasPanel.Visible && TTSOutputVoiceOption2.DropDownStyle == ComboBoxStyle.Simple)
            {
                SavePersona.Enabled = true;
                personaEdited = true;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TTSOutputVoiceOption3_TextChanged(object sender, EventArgs e)
        {
            if (PersonasPanel.Visible && TTSOutputVoiceOption3.DropDownStyle == ComboBoxStyle.Simple)
            {
                SavePersona.Enabled = true;
                personaEdited = true;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void OllamaTestButton_Click(object sender, EventArgs e)
        {
            if (OllamaURITextBox.Text.Length > 1)
            {
                OllamaTestButton.Enabled = false;
                OllamaLLM ollama = new(OllamaURITextBox.Text);
                //var installedModels = ollama.OllamaLLMGetModels();

                var result = await ollama.OllamaGetResponse("this is a test!", OllamaModelsComboBox.Text);
                _bBBlog.Debug("Ollama test result: " + result);
                OllamaTestButton.Enabled = true;
                MessageBox.Show("Ollama works!", "Ollama Test", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Ollama URI cannot be empty", "Ollama Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void OllamaPanel_VisibleChanged(object sender, EventArgs e)
        {
            //dont bother filling if we dont use the Ollama LLM
            if (!UseOllamaLLMCheckBox.Checked)
                return;

            OllamaPanel.Enabled = false;
            _bBBlog.Info("Ollama panel visible.");
            _bBBlog.Info("Ollama now we load the Uri");
            //if theres nothing we use the default for Ollama
            if (OllamaPanel.Visible && Properties.Settings.Default.OllamaURI.Length < 1)
            {
                _bBBlog.Info("Ollama URI is empty, setting to default");
                OllamaURITextBox.Text = "http://localhost:11434";
                Properties.Settings.Default.OllamaURI = OllamaURITextBox.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                OllamaURITextBox.Text = Properties.Settings.Default.OllamaURI;
            }

            if (OllamaPanel.Visible && Properties.Settings.Default.OllamaURI.Length > 1)
            {

                _bBBlog.Info("Getting available Ollama models, but first lets see if it runs");
                OllamaLLM ollama = new(OllamaURITextBox.Text);
                var installedModels = await ollama.OllamaLLMGetModels();
                OllamaModelsComboBox.Items.Clear();
                for (int i = 0; i < installedModels.Count; i++)
                {
                    OllamaModelsComboBox.Items.Add(installedModels[i]);
                }
                _bBBlog.Info($"Found {installedModels.Count} models");
                // lets set a default if there's none
                if (Properties.Settings.Default.OllamaSelectedModel.Length < 1)
                {
                    _bBBlog.Info("Ollama no model selected, setting to default");
                    if (OllamaModelsComboBox.Items.Count > 0)
                        OllamaModelsComboBox.SelectedIndex = 0;
                    else
                        _bBBlog.Info("Ollama no models found");
                    //TODO show throw an error here
                } //else we pre-select the one thats in the settings
                else
                {
                    _bBBlog.Info("Setting selected model to: " + Properties.Settings.Default.OllamaSelectedModel);
                    OllamaModelsComboBox.SelectedIndex = OllamaModelsComboBox.FindStringExact(Properties.Settings.Default.OllamaSelectedModel);
                }

            }
            else
            {
                _bBBlog.Info("Ollama Uri is empty, so we cant preload models");
            }
            OllamaPanel.Enabled = true;
        }

        [SupportedOSPlatform("windows6.1")]
        private void UseOllamaLLMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            /*    if (UseOllamaLLMCheckBox.Checked)
                {
                    _bBBlog.Info("Ollama LLM enabled, loading settings if not yet loaded");
                    if (OllamaModelsComboBox.Items.Count < 1)
                    {
                        OllamaPanel_VisibleChanged(sender, e);
                    }
                }
            */
        }

        [SupportedOSPlatform("windows6.1")]
        private void GPTMaxTokensTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the input is not a digit or control (like backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;  // Ignore the input
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void GPTTemperatureTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            // Check if the input is not a digit, not a control (like backspace), and not the decimal separator
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar.ToString() != decimalSeparator)
            {
                e.Handled = true;  // Ignore the input
            }
            // Check if it's the decimal separator, and if so, check if there's already one in the text box
            else if (e.KeyChar.ToString() == decimalSeparator && (sender as System.Windows.Forms.TextBox).Text.IndexOf(decimalSeparator) > -1)
            {
                e.Handled = true;  // Ignore the input
            }
            else
            {
                // Check if the resulting value would be higher than 2
                string newText = (sender as System.Windows.Forms.TextBox).Text + e.KeyChar;
                if (float.TryParse(newText, NumberStyles.Float, CultureInfo.CurrentCulture, out float result) && result > 2)
                {
                    e.Handled = true;  // Ignore the input
                }
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void GPTTemperatureTextBox_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.TextBox currenttb = (System.Windows.Forms.TextBox)sender;
            if (string.IsNullOrWhiteSpace(currenttb.Text))
            {
                MessageBox.Show("This field cannot be empty");
                e.Cancel = true;  // Cancel the event and keep the focus on the TextBox
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void GPTMaxTokensTextBox_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.TextBox currenttb = (System.Windows.Forms.TextBox)sender;
            if (string.IsNullOrWhiteSpace(currenttb.Text))
            {
                MessageBox.Show("This field cannot be empty");
                e.Cancel = true;  // Cancel the event and keep the focus on the TextBox
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void PersonaComboBox_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.ComboBox currenttb = (System.Windows.Forms.ComboBox)sender;
            if (string.IsNullOrWhiteSpace(currenttb.Text))
            {
                MessageBox.Show("This field cannot be empty");
                e.Cancel = true;  // Cancel the event and keep the focus on the TextBox
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void PersonaRoleTextBox_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.TextBox currenttb = (System.Windows.Forms.TextBox)sender;
            if (string.IsNullOrWhiteSpace(currenttb.Text))
            {
                MessageBox.Show("This field cannot be empty");
                e.Cancel = true;  // Cancel the event and keep the focus on the TextBox
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TTSOutputVoiceOption1_Validating(object sender, CancelEventArgs e)
        {
            if (TTSOutputVoiceOption1.Enabled)
            {
                System.Windows.Forms.ComboBox currenttb = (System.Windows.Forms.ComboBox)sender;
                if (string.IsNullOrWhiteSpace(currenttb.Text))
                {
                    MessageBox.Show("This field cannot be empty");
                    e.Cancel = true;  // Cancel the event and keep the focus on the TextBox
                }
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TTSOutputVoiceOption2_Validating(object sender, CancelEventArgs e)
        {
            if (TTSOutputVoiceOption2.Enabled)
            {
                System.Windows.Forms.ComboBox currenttb = (System.Windows.Forms.ComboBox)sender;
                if (string.IsNullOrWhiteSpace(currenttb.Text))
                {
                    MessageBox.Show("This field cannot be empty");
                    e.Cancel = true;  // Cancel the event and keep the focus on the TextBox
                }
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TTSOutputVoiceOption3_Validating(object sender, CancelEventArgs e)
        {
            if (TTSOutputVoiceOption3.Enabled)
            {
                System.Windows.Forms.ComboBox currenttb = (System.Windows.Forms.ComboBox)sender;
                if (string.IsNullOrWhiteSpace(currenttb.Text))
                {
                    MessageBox.Show("This field cannot be empty");
                    e.Cancel = true;  // Cancel the event and keep the focus on the TextBox
                }
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void UseOllamaLLMCheckBox_Click(object sender, EventArgs e)
        {
            if (!UseOllamaLLMCheckBox.Checked)
            {
                OllamaURITextBox.Enabled = false;
                OllamaModelsComboBox.Enabled = false;
                OllamaResponseLengthComboBox.Enabled = false;
                OllamaTestButton.Enabled = false;

            }
            else
            {
                OllamaURITextBox.Enabled = true;
                OllamaModelsComboBox.Enabled = true;
                OllamaResponseLengthComboBox.Enabled = true;
                OllamaTestButton.Enabled = true;
                OllamaPanel_VisibleChanged(sender, e);
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private void TwitchAuthServerConfig_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.TextBox currenttb = (System.Windows.Forms.TextBox)sender;
            if (string.IsNullOrWhiteSpace(currenttb.Text))
            {
                MessageBox.Show("This field cannot be empty");
                e.Cancel = true;  // Cancel the event and keep the focus on the TextBox
            }
        }
    }
}
