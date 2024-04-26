﻿using CSCore.SoundIn;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BanterBrain_Buddy
{
    public partial class SettingsForm : Form
    {
        //set logger
        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private List<Personas> _personas = new List<Personas>();
        private List<AzureVoices> _azureRegionVoicesList = [];
        private List<NativeVoices> _nativeRegionVoicesList = [];

        private TwitchAPIESub _twitchTestEventSub;
        private bool _twitchStartedTest = false;
        private bool _twitchAPIVerified = false;

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
            //_bBBlog.Debug("Selected node: " + selectedNode);
            switch (selectedNode)
            {
                case "VoiceSettings":
                    PanelVisible(MicrophonePanel.Name);
                    break;
                case "Microphone":
                    PanelVisible(MicrophonePanel.Name);
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

                case "TwitchTriggers":
                    PanelVisible(TwitchPanel.Name);
                    break;
                case "Personas":
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
            TwitchUsername.Text = Properties.Settings.Default.TwitchUsername;
            TwitchAccessToken.Text = Properties.Settings.Default.TwitchAccessToken;
            TwitchChannel.Text = Properties.Settings.Default.TwitchChannel;
            TwitchSendTextCheckBox.Checked = Properties.Settings.Default.TwitchSendTextCheckBox;
            TwitchCheckAuthAtStartup.Checked = Properties.Settings.Default.TwitchCheckAuthAtStartup;
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
            if (Properties.Settings.Default.SelectedLLM == "GPT")
            {
                UseGPTLLMCheckBox.Checked = true;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _bBBlog.Info("Settings form closing, saving settings");
            Properties.Settings.Default.TwitchUsername = TwitchUsername.Text;
            Properties.Settings.Default.TwitchAccessToken = TwitchAccessToken.Text;
            Properties.Settings.Default.TwitchChannel = TwitchChannel.Text;
            Properties.Settings.Default.TwitchSendTextCheckBox = TwitchSendTextCheckBox.Checked;
            Properties.Settings.Default.TwitchCheckAuthAtStartup = TwitchCheckAuthAtStartup.Checked;
            Properties.Settings.Default.VoiceInput = SoundInputDevices.Text;
            Properties.Settings.Default.PTTHotkey = MicrophoneHotkeyEditbox.Text;
            Properties.Settings.Default.TTSAudioOutput = TTSAudioOutputComboBox.Text;
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

            //we should also close the EventSub client if it is running
            if (_twitchStartedTest)
            {
                _bBBlog.Info("Closing EventSub client");
                await _twitchTestEventSub.EventSubStopAsync();
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
            _twitchAPIVerified = false;
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
                //      TwitchAPIStatusTextBox.Text = "DISABLED";
                //     TwitchAPIStatusTextBox.BackColor = Color.Red;
                //if the token is invalid, lets disable the checkboxes
                //      TwitchEnableCheckbox.Checked = false;
                if (TwitchCheckAuthAtStartup.Checked)
                    TwitchCheckAuthAtStartup.Checked = false;
                return;
            }
            else
            {
                _bBBlog.Info($"Twitch Access token verified success!");
                _twitchAPIVerified = true;
                //       UpdateTextLog("Twitch Access token verified success!\r\n");
                MessageBox.Show($"Twitch Access token verified success!", "Twitch Access Token verification result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //      TwitchAPIStatusTextBox.Text = "ENABLED";
                //      TwitchAPIStatusTextBox.BackColor = Color.Green;
                //if the token is valid, and twitch enabled lets start up the hourly validation timer
                //   if (TwitchEnableCheckbox.Checked)
                //      SetTwitchValidateTokenTimer(false);
            }

            TwitchAPITestButton.Enabled = true;
        }

        [SupportedOSPlatform("windows6.1")]
        private async void EventSubTest_Click(object sender, EventArgs e)
        {
            //This only works once API access-token is verified
            if (_twitchAPIVerified)
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
            _twitchTestEventSub = new();
            bool eventSubStart = false;
            //we should set here what eventhandlers we want to have enabled based on the twitch Settings

            if (await _twitchTestEventSub.EventSubInit(TwitchAccessToken.Text, TwitchUsername.Text, TwitchChannel.Text))
            {
                //we need to first set the event handlers we want to use

                //if we are in MOCK mode, just enable all handlers?
                if (TwitchMockEventSub.Checked)
                {
                    _bBBlog.Info("Twitch TEST read chat enabled, calling eventsubhandlereadchat. Command set to $BBB for test");
                    _twitchTestEventSub.EventSubHandleReadchat("$BBB", 300, false, true);
                    //set local eventhanlder for valid chat messages to trigger the bot
                    _twitchTestEventSub.OnESubChatMessage += TwitchEventSub_OnESubChatMessage;
                }

                //do we want to check cheer messages?
                if (TwitchMockEventSub.Checked)
                {
                    _bBBlog.Info("Twitch TEST cheers enabled, calling EventSubHandleCheer with the min amount of bits needed to trigger. Set to 10 for test");
                    _twitchTestEventSub.EventSubHandleCheer(10);
                    _twitchTestEventSub.OnESubCheerMessage += TwitchEventSub_OnESubCheerMessage;
                }

                //do we want to check for subscription events?
                if (TwitchMockEventSub.Checked)
                {
                    //new subs
                    _bBBlog.Info($"Twitch TEST subscriptions enabled, calling EventSubHandleSubscription: {_twitchTestEventSub.ToString()}");
                    _twitchTestEventSub.EventSubHandleSubscription();
                    _twitchTestEventSub.OnESubSubscribe += TwitchEventSub_OnESubSubscribe;
                    _twitchTestEventSub.OnESubReSubscribe += TwitchEventSub_OnESubReSubscribe;
                    //todo set eventhandler being thrown when a new sub is detected or resub
                }
                //TODO: gifted subs TwitchGiftedSub.Checked
                if (TwitchMockEventSub.Checked)
                {
                    _bBBlog.Info("Twitch TEST gifted subs enabled, calling EventSubHandleGiftedSubs");
                    _twitchTestEventSub.EventSubHandleSubscriptionGift();
                    _twitchTestEventSub.OnESubSubscriptionGift += TwitchEventSub_OnESubGiftedSub;
                }

                //do we want to check for channel point redemptions?
                if (TwitchMockEventSub.Checked)
                {
                    _bBBlog.Info("Twitch TEST channel points enabled, calling EventSubHandleChannelPoints");
                    _twitchTestEventSub.EventSubHandleChannelPointRedemption("rewardtest");
                    _twitchTestEventSub.OnESubChannelPointRedemption += TwitchEventSub_OnESubChannelPointRedemption;
                }

                if (!TwitchMockEventSub.Checked)
                {
                    //if we are not in mock mode, we can start the client
                    eventSubStart = await _twitchTestEventSub.EventSubStartAsync();
                }
                else
                { //we are in mock mode, so we just say we started
                    _bBBlog.Info("Twitch EventSub client  starting successfully in mock mode");
                    eventSubStart = await _twitchTestEventSub.EventSubStartAsyncMock();

                    _twitchStartedTest = true;
                }

                if (eventSubStart)
                {
                    _bBBlog.Info("Twitch EventSub client  started successfully");

                    _twitchStartedTest = true;
                }
                else
                {
                    _bBBlog.Error("Issue with starting Twitch EventSub server. Check logs for more information.");
                    _twitchStartedTest = false;
                    return false;
                }
                return true;
            }
            else
            {
                _bBBlog.Error("Issue with starting Twitch EventSub server. Check logs for more information.");
                _twitchStartedTest = false;
                return false;
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TwitchEventSub_OnESubChannelPointRedemption(object sender, TwitchEventhandlers.OnChannelPointCustomRedemptionEventArgs e)
        {
            string user = e.GetChannelPointCustomRedemptionInfo()[0];
            string message = e.GetChannelPointCustomRedemptionInfo()[1];
            _bBBlog.Info($"TEST: Valid Twitch Channel Point Redemption message received: {user} redeemed with message: {message}");
        }

        [SupportedOSPlatform("windows6.1")]
        //TwitchEventSub_OnESubGiftedSub
        private async void TwitchEventSub_OnESubGiftedSub(object sender, TwitchEventhandlers.OnSubscriptionGiftEventArgs e)
        {
            string user = e.GetSubscriptionGiftInfo()[0];
            string amount = e.GetSubscriptionGiftInfo()[1];
            string tier = e.GetSubscriptionGiftInfo()[2];
            _bBBlog.Info($"TEST: Valid Twitch Gifted Subscription message received: {user} gifted {amount} subs tier {tier}");
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TwitchEventSub_OnESubSubscribe(object sender, TwitchEventhandlers.OnSubscribeEventArgs e)
        {
            string user = e.GetSubscribeInfo()[0];
            string broadcaster = e.GetSubscribeInfo()[1];
            _bBBlog.Info($"TEST: Valid Twitch Subscription message received: {user} subscribed to {broadcaster}");
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TwitchEventSub_OnESubReSubscribe(object sender, TwitchEventhandlers.OnReSubscribeEventArgs e)
        {
            string user = e.GetSubscribeInfo()[0];
            string message = e.GetSubscribeInfo()[1];
            string months = e.GetSubscribeInfo()[2];
            _bBBlog.Info($"TEST: Valid Twitch Re-Subscription message received: {user} subscribed for {months} months with message: {message}");
        }

        [SupportedOSPlatform("windows6.1")]
        //eventhandler for valid chat messages trigger
        private async void TwitchEventSub_OnESubChatMessage(object sender, TwitchEventhandlers.OnChatEventArgs e)
        {

            string message = e.GetChatInfo()[1].Replace("$BBB", "");
            string user = e.GetChatInfo()[0];
            //we got a valid chat message, lets see what we can do with it
            _bBBlog.Info("TEST: Valid Twitch Chat message received from user: " + user + " message: " + message);
        }

        [SupportedOSPlatform("windows6.1")]
        private async void TwitchEventSub_OnESubCheerMessage(object sender, TwitchEventhandlers.OnCheerEventsArgs e)
        {
            string user = e.GetCheerInfo()[0];
            string message = e.GetCheerInfo()[1];
            //we got a valid cheer message, lets see what we can do with it
            _bBBlog.Info("TEST: Valid Twitch Cheer message received");
        }


        }
}
