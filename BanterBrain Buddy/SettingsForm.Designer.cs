namespace BanterBrain_Buddy
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Azure");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("OpenAI");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("ElevenLabs");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Ollama LLM");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("API Settings", new System.Windows.Forms.TreeNode[] { treeNode1, treeNode2, treeNode3, treeNode4 });
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Native speech");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Personas");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Microphone");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Speaker");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Sound & Voice settings", new System.Windows.Forms.TreeNode[] { treeNode8, treeNode9 });
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Twitch ");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Streaming settings", new System.Windows.Forms.TreeNode[] { treeNode11 });
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            MenuTreeView = new System.Windows.Forms.TreeView();
            TwitchPanel = new System.Windows.Forms.Panel();
            groupBox3 = new System.Windows.Forms.GroupBox();
            AuthorizeBotTwitch = new System.Windows.Forms.Button();
            TwitchBotName = new System.Windows.Forms.TextBox();
            TwitchBotAuthKey = new System.Windows.Forms.TextBox();
            TwitchBotNameLabel = new System.Windows.Forms.Label();
            TwitchBotAccessTokenLabel = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            TwitchAuthorizeButton = new System.Windows.Forms.Button();
            TwitchBroadcasterChannel = new System.Windows.Forms.TextBox();
            TwitchBroadcasterAccessToken = new System.Windows.Forms.TextBox();
            TwitchChannelNameLabel = new System.Windows.Forms.Label();
            TwitchAccesstokenLabel = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            TwitchAuthServerConfig = new System.Windows.Forms.ComboBox();
            label24 = new System.Windows.Forms.Label();
            EventSubGroupbox = new System.Windows.Forms.GroupBox();
            TwitchMockEventSub = new System.Windows.Forms.CheckBox();
            TwitchEventSubTestButton = new System.Windows.Forms.Button();
            TwitchAPITestGroupBox = new System.Windows.Forms.GroupBox();
            TwitchTestSendText = new System.Windows.Forms.TextBox();
            TwitchSendTextCheckBox = new System.Windows.Forms.CheckBox();
            TwitchAPITestButton = new System.Windows.Forms.Button();
            label9 = new System.Windows.Forms.Label();
            ElevenLabsPanel = new System.Windows.Forms.Panel();
            ElevenLabsTestButton = new System.Windows.Forms.Button();
            ElevenlabsAPIKeyTextBox = new System.Windows.Forms.TextBox();
            label11 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            OpenAIChatGPTPanel = new System.Windows.Forms.Panel();
            WhisperSpeechRecognitionComboBox = new System.Windows.Forms.ComboBox();
            label23 = new System.Windows.Forms.Label();
            LLMMaxTokensHelpText = new System.Windows.Forms.Label();
            LLMTempHelpText = new System.Windows.Forms.Label();
            LLMMaxTokenLabel = new System.Windows.Forms.Label();
            LLMTempLabel = new System.Windows.Forms.Label();
            GPTMaxTokensTextBox = new System.Windows.Forms.TextBox();
            GPTTemperatureTextBox = new System.Windows.Forms.TextBox();
            GPTModelComboBox = new System.Windows.Forms.ComboBox();
            label4 = new System.Windows.Forms.Label();
            GPTTestButton = new System.Windows.Forms.Button();
            GPTAPIKeyTextBox = new System.Windows.Forms.TextBox();
            GPTAPIKeyLabel = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            PersonasPanel = new System.Windows.Forms.Panel();
            label31 = new System.Windows.Forms.Label();
            label32 = new System.Windows.Forms.Label();
            TTSPitchLevel = new System.Windows.Forms.Label();
            PitchTrackBar = new System.Windows.Forms.TrackBar();
            TTSSpeedLevel = new System.Windows.Forms.Label();
            TTSVoiceLevel = new System.Windows.Forms.Label();
            label28 = new System.Windows.Forms.Label();
            RateTrackBar = new System.Windows.Forms.TrackBar();
            label5 = new System.Windows.Forms.Label();
            VolumeTrackBar = new System.Windows.Forms.TrackBar();
            TTSOutputVoiceOption3 = new System.Windows.Forms.ComboBox();
            TTSOption3Label = new System.Windows.Forms.Label();
            TTSOutputVoiceOption2 = new System.Windows.Forms.ComboBox();
            TTSOption2Label = new System.Windows.Forms.Label();
            TestVoiceButton = new System.Windows.Forms.Button();
            DeletePersona = new System.Windows.Forms.Button();
            SavePersona = new System.Windows.Forms.Button();
            NewPersona = new System.Windows.Forms.Button();
            TTSOutputVoiceOption1 = new System.Windows.Forms.ComboBox();
            TTSOption1Label = new System.Windows.Forms.Label();
            TTSOutputVoice = new System.Windows.Forms.ComboBox();
            label16 = new System.Windows.Forms.Label();
            TTSProviderComboBox = new System.Windows.Forms.ComboBox();
            label14 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            PersonaComboBox = new System.Windows.Forms.ComboBox();
            label12 = new System.Windows.Forms.Label();
            PersonaRoleTextBox = new System.Windows.Forms.TextBox();
            label10 = new System.Windows.Forms.Label();
            SpeakerPanel = new System.Windows.Forms.Panel();
            OutputVolumeLabel = new System.Windows.Forms.Label();
            label30 = new System.Windows.Forms.Label();
            label29 = new System.Windows.Forms.Label();
            SpeakerDeviceVolumeTrackBar = new System.Windows.Forms.TrackBar();
            TTSAudioOutputComboBox = new System.Windows.Forms.ComboBox();
            TTSAudioOutputLabel = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            AzurePanel = new System.Windows.Forms.Panel();
            label2 = new System.Windows.Forms.Label();
            AzureLanguageComboBox = new System.Windows.Forms.ComboBox();
            TestAzureAPISettings = new System.Windows.Forms.Button();
            label17 = new System.Windows.Forms.Label();
            AzureRegionTextBox = new System.Windows.Forms.TextBox();
            label18 = new System.Windows.Forms.Label();
            AzureAPIKeyTextBox = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            OllamaPanel = new System.Windows.Forms.Panel();
            UseOllamaLLMCheckBox = new System.Windows.Forms.CheckBox();
            OllamaResponseLengthComboBox = new System.Windows.Forms.ComboBox();
            label20 = new System.Windows.Forms.Label();
            OllamaTestButton = new System.Windows.Forms.Button();
            OllamaModelsTextLabel = new System.Windows.Forms.Label();
            OllamaModelsComboBox = new System.Windows.Forms.ComboBox();
            OllamaURITextBox = new System.Windows.Forms.TextBox();
            label19 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            OBSPanel = new System.Windows.Forms.Panel();
            WebsourceServerEnable = new System.Windows.Forms.CheckBox();
            label27 = new System.Windows.Forms.Label();
            TwitchChatSoundSelectButton = new System.Windows.Forms.Button();
            label26 = new System.Windows.Forms.Label();
            label25 = new System.Windows.Forms.Label();
            WebsourceServer = new System.Windows.Forms.TextBox();
            NativeSpeechPanel = new System.Windows.Forms.Panel();
            label22 = new System.Windows.Forms.Label();
            NativeSpeechRecognitionLanguageComboBox = new System.Windows.Forms.ComboBox();
            label21 = new System.Windows.Forms.Label();
            MicrophonePanel = new System.Windows.Forms.Panel();
            PTTKeyLabel = new System.Windows.Forms.Label();
            MicrophoneHotkeyEditbox = new System.Windows.Forms.TextBox();
            VoiceInputLabel = new System.Windows.Forms.Label();
            SoundInputDevices = new System.Windows.Forms.ComboBox();
            MicrophoneHotkeySet = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            BBBToolTip = new System.Windows.Forms.ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            TwitchPanel.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            EventSubGroupbox.SuspendLayout();
            TwitchAPITestGroupBox.SuspendLayout();
            ElevenLabsPanel.SuspendLayout();
            OpenAIChatGPTPanel.SuspendLayout();
            PersonasPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PitchTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RateTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)VolumeTrackBar).BeginInit();
            SpeakerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SpeakerDeviceVolumeTrackBar).BeginInit();
            AzurePanel.SuspendLayout();
            OllamaPanel.SuspendLayout();
            OBSPanel.SuspendLayout();
            NativeSpeechPanel.SuspendLayout();
            MicrophonePanel.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(MenuTreeView);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(TwitchPanel);
            splitContainer1.Panel2.Controls.Add(ElevenLabsPanel);
            splitContainer1.Panel2.Controls.Add(OpenAIChatGPTPanel);
            splitContainer1.Panel2.Controls.Add(PersonasPanel);
            splitContainer1.Panel2.Controls.Add(SpeakerPanel);
            splitContainer1.Panel2.Controls.Add(AzurePanel);
            splitContainer1.Panel2.Controls.Add(OllamaPanel);
            splitContainer1.Panel2.Controls.Add(OBSPanel);
            splitContainer1.Panel2.Controls.Add(NativeSpeechPanel);
            splitContainer1.Panel2.Controls.Add(MicrophonePanel);
            splitContainer1.Size = new System.Drawing.Size(1143, 965);
            splitContainer1.SplitterDistance = 290;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 0;
            // 
            // MenuTreeView
            // 
            MenuTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            MenuTreeView.Location = new System.Drawing.Point(0, 0);
            MenuTreeView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            MenuTreeView.Name = "MenuTreeView";
            treeNode1.Name = "Azure";
            treeNode1.Text = "Azure";
            treeNode2.Name = "OpenAIChatGPT";
            treeNode2.Text = "OpenAI";
            treeNode3.Name = "ElevenLabs";
            treeNode3.Text = "ElevenLabs";
            treeNode4.Name = "OllamaLLM";
            treeNode4.Text = "Ollama LLM";
            treeNode5.Name = "APISettings";
            treeNode5.Text = "API Settings";
            treeNode6.Name = "NativeSpeech";
            treeNode6.Text = "Native speech";
            treeNode7.Name = "Personas";
            treeNode7.Text = "Personas";
            treeNode8.Name = "Microphone";
            treeNode8.Text = "Microphone";
            treeNode9.Name = "Speaker";
            treeNode9.Text = "Speaker";
            treeNode10.Name = "VoiceSettings";
            treeNode10.Text = "Sound & Voice settings";
            treeNode11.Name = "Twitch";
            treeNode11.Text = "Twitch ";
            treeNode12.Name = "StreamingSettings";
            treeNode12.Text = "Streaming settings";
            MenuTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { treeNode5, treeNode6, treeNode7, treeNode10, treeNode12 });
            MenuTreeView.PathSeparator = "";
            MenuTreeView.Size = new System.Drawing.Size(290, 965);
            MenuTreeView.TabIndex = 0;
            MenuTreeView.BeforeSelect += MenuTreeView_BeforeSelect;
            MenuTreeView.AfterSelect += TreeView1_AfterSelect;
            // 
            // TwitchPanel
            // 
            TwitchPanel.Controls.Add(groupBox3);
            TwitchPanel.Controls.Add(groupBox2);
            TwitchPanel.Controls.Add(groupBox1);
            TwitchPanel.Controls.Add(EventSubGroupbox);
            TwitchPanel.Controls.Add(TwitchAPITestGroupBox);
            TwitchPanel.Controls.Add(label9);
            TwitchPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            TwitchPanel.Location = new System.Drawing.Point(0, 0);
            TwitchPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            TwitchPanel.Name = "TwitchPanel";
            TwitchPanel.Size = new System.Drawing.Size(847, 965);
            TwitchPanel.TabIndex = 5;
            TwitchPanel.VisibleChanged += TwitchPanel_VisibleChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(AuthorizeBotTwitch);
            groupBox3.Controls.Add(TwitchBotName);
            groupBox3.Controls.Add(TwitchBotAuthKey);
            groupBox3.Controls.Add(TwitchBotNameLabel);
            groupBox3.Controls.Add(TwitchBotAccessTokenLabel);
            groupBox3.Location = new System.Drawing.Point(393, 65);
            groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            groupBox3.Size = new System.Drawing.Size(437, 200);
            groupBox3.TabIndex = 37;
            groupBox3.TabStop = false;
            groupBox3.Text = "Bot Account";
            BBBToolTip.SetToolTip(groupBox3, "Used for sending text to the channel");
            // 
            // AuthorizeBotTwitch
            // 
            AuthorizeBotTwitch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            AuthorizeBotTwitch.Location = new System.Drawing.Point(11, 132);
            AuthorizeBotTwitch.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            AuthorizeBotTwitch.Name = "AuthorizeBotTwitch";
            AuthorizeBotTwitch.Size = new System.Drawing.Size(174, 45);
            AuthorizeBotTwitch.TabIndex = 44;
            AuthorizeBotTwitch.Text = "Authorize to Twitch";
            AuthorizeBotTwitch.UseVisualStyleBackColor = true;
            AuthorizeBotTwitch.Click += AuthorizeBotTwitch_Click;
            // 
            // TwitchBotName
            // 
            TwitchBotName.Location = new System.Drawing.Point(147, 20);
            TwitchBotName.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TwitchBotName.Name = "TwitchBotName";
            TwitchBotName.Size = new System.Drawing.Size(217, 31);
            TwitchBotName.TabIndex = 43;
            // 
            // TwitchBotAuthKey
            // 
            TwitchBotAuthKey.Location = new System.Drawing.Point(147, 75);
            TwitchBotAuthKey.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TwitchBotAuthKey.Name = "TwitchBotAuthKey";
            TwitchBotAuthKey.PasswordChar = '*';
            TwitchBotAuthKey.Size = new System.Drawing.Size(217, 31);
            TwitchBotAuthKey.TabIndex = 42;
            // 
            // TwitchBotNameLabel
            // 
            TwitchBotNameLabel.AutoSize = true;
            TwitchBotNameLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchBotNameLabel.Location = new System.Drawing.Point(10, 23);
            TwitchBotNameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            TwitchBotNameLabel.Name = "TwitchBotNameLabel";
            TwitchBotNameLabel.Size = new System.Drawing.Size(129, 25);
            TwitchBotNameLabel.TabIndex = 41;
            TwitchBotNameLabel.Text = "Account Name";
            // 
            // TwitchBotAccessTokenLabel
            // 
            TwitchBotAccessTokenLabel.AutoSize = true;
            TwitchBotAccessTokenLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchBotAccessTokenLabel.Location = new System.Drawing.Point(10, 82);
            TwitchBotAccessTokenLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            TwitchBotAccessTokenLabel.Name = "TwitchBotAccessTokenLabel";
            TwitchBotAccessTokenLabel.Size = new System.Drawing.Size(116, 25);
            TwitchBotAccessTokenLabel.TabIndex = 40;
            TwitchBotAccessTokenLabel.Text = "Access Token";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(TwitchAuthorizeButton);
            groupBox2.Controls.Add(TwitchBroadcasterChannel);
            groupBox2.Controls.Add(TwitchBroadcasterAccessToken);
            groupBox2.Controls.Add(TwitchChannelNameLabel);
            groupBox2.Controls.Add(TwitchAccesstokenLabel);
            groupBox2.Location = new System.Drawing.Point(21, 55);
            groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            groupBox2.Size = new System.Drawing.Size(363, 208);
            groupBox2.TabIndex = 36;
            groupBox2.TabStop = false;
            groupBox2.Text = "Broadcaster Account";
            // 
            // TwitchAuthorizeButton
            // 
            TwitchAuthorizeButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchAuthorizeButton.Location = new System.Drawing.Point(11, 140);
            TwitchAuthorizeButton.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TwitchAuthorizeButton.Name = "TwitchAuthorizeButton";
            TwitchAuthorizeButton.Size = new System.Drawing.Size(174, 45);
            TwitchAuthorizeButton.TabIndex = 39;
            TwitchAuthorizeButton.Text = "Authorize to Twitch";
            TwitchAuthorizeButton.UseVisualStyleBackColor = true;
            TwitchAuthorizeButton.Click += TwitchAuthorizeButton_Click;
            // 
            // TwitchBroadcasterChannel
            // 
            TwitchBroadcasterChannel.Location = new System.Drawing.Point(149, 37);
            TwitchBroadcasterChannel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TwitchBroadcasterChannel.Name = "TwitchBroadcasterChannel";
            TwitchBroadcasterChannel.Size = new System.Drawing.Size(200, 31);
            TwitchBroadcasterChannel.TabIndex = 38;
            // 
            // TwitchBroadcasterAccessToken
            // 
            TwitchBroadcasterAccessToken.Location = new System.Drawing.Point(149, 87);
            TwitchBroadcasterAccessToken.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TwitchBroadcasterAccessToken.Name = "TwitchBroadcasterAccessToken";
            TwitchBroadcasterAccessToken.PasswordChar = '*';
            TwitchBroadcasterAccessToken.Size = new System.Drawing.Size(200, 31);
            TwitchBroadcasterAccessToken.TabIndex = 37;
            // 
            // TwitchChannelNameLabel
            // 
            TwitchChannelNameLabel.AutoSize = true;
            TwitchChannelNameLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchChannelNameLabel.Location = new System.Drawing.Point(11, 36);
            TwitchChannelNameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            TwitchChannelNameLabel.Name = "TwitchChannelNameLabel";
            TwitchChannelNameLabel.Size = new System.Drawing.Size(129, 25);
            TwitchChannelNameLabel.TabIndex = 36;
            TwitchChannelNameLabel.Text = "Account Name";
            // 
            // TwitchAccesstokenLabel
            // 
            TwitchAccesstokenLabel.AutoSize = true;
            TwitchAccesstokenLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchAccesstokenLabel.Location = new System.Drawing.Point(11, 93);
            TwitchAccesstokenLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            TwitchAccesstokenLabel.Name = "TwitchAccesstokenLabel";
            TwitchAccesstokenLabel.Size = new System.Drawing.Size(116, 25);
            TwitchAccesstokenLabel.TabIndex = 35;
            TwitchAccesstokenLabel.Text = "Access Token";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(TwitchAuthServerConfig);
            groupBox1.Controls.Add(label24);
            groupBox1.Location = new System.Drawing.Point(21, 300);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            groupBox1.Size = new System.Drawing.Size(491, 115);
            groupBox1.TabIndex = 35;
            groupBox1.TabStop = false;
            groupBox1.Text = "Webserver Config";
            // 
            // TwitchAuthServerConfig
            // 
            TwitchAuthServerConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TwitchAuthServerConfig.FormattingEnabled = true;
            TwitchAuthServerConfig.Items.AddRange(new object[] { "http://localhost:8080", "http://localhost:9080", "http://localhost:8888", "http://localhost:9088", "http://localhost:2384" });
            TwitchAuthServerConfig.Location = new System.Drawing.Point(214, 37);
            TwitchAuthServerConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            TwitchAuthServerConfig.Name = "TwitchAuthServerConfig";
            TwitchAuthServerConfig.Size = new System.Drawing.Size(251, 33);
            TwitchAuthServerConfig.TabIndex = 3;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new System.Drawing.Point(16, 42);
            label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label24.Name = "label24";
            label24.Size = new System.Drawing.Size(192, 25);
            label24.TabIndex = 2;
            label24.Text = "Twitch Auth Webserver";
            // 
            // EventSubGroupbox
            // 
            EventSubGroupbox.Controls.Add(TwitchMockEventSub);
            EventSubGroupbox.Controls.Add(TwitchEventSubTestButton);
            EventSubGroupbox.Location = new System.Drawing.Point(19, 638);
            EventSubGroupbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            EventSubGroupbox.Name = "EventSubGroupbox";
            EventSubGroupbox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            EventSubGroupbox.Size = new System.Drawing.Size(491, 93);
            EventSubGroupbox.TabIndex = 32;
            EventSubGroupbox.TabStop = false;
            EventSubGroupbox.Text = "EventSub Test";
            // 
            // TwitchMockEventSub
            // 
            TwitchMockEventSub.AutoSize = true;
            TwitchMockEventSub.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchMockEventSub.Location = new System.Drawing.Point(304, 37);
            TwitchMockEventSub.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            TwitchMockEventSub.Name = "TwitchMockEventSub";
            TwitchMockEventSub.Size = new System.Drawing.Size(168, 29);
            TwitchMockEventSub.TabIndex = 2;
            TwitchMockEventSub.Text = "MOCK EventSub";
            TwitchMockEventSub.UseVisualStyleBackColor = true;
            // 
            // TwitchEventSubTestButton
            // 
            TwitchEventSubTestButton.Enabled = false;
            TwitchEventSubTestButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchEventSubTestButton.Location = new System.Drawing.Point(9, 37);
            TwitchEventSubTestButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            TwitchEventSubTestButton.Name = "TwitchEventSubTestButton";
            TwitchEventSubTestButton.Size = new System.Drawing.Size(124, 38);
            TwitchEventSubTestButton.TabIndex = 1;
            TwitchEventSubTestButton.Text = "Test";
            TwitchEventSubTestButton.UseVisualStyleBackColor = true;
            TwitchEventSubTestButton.Click += EventSubTest_Click;
            // 
            // TwitchAPITestGroupBox
            // 
            TwitchAPITestGroupBox.Controls.Add(TwitchTestSendText);
            TwitchAPITestGroupBox.Controls.Add(TwitchSendTextCheckBox);
            TwitchAPITestGroupBox.Controls.Add(TwitchAPITestButton);
            TwitchAPITestGroupBox.Location = new System.Drawing.Point(21, 450);
            TwitchAPITestGroupBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TwitchAPITestGroupBox.Name = "TwitchAPITestGroupBox";
            TwitchAPITestGroupBox.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TwitchAPITestGroupBox.Size = new System.Drawing.Size(491, 153);
            TwitchAPITestGroupBox.TabIndex = 31;
            TwitchAPITestGroupBox.TabStop = false;
            TwitchAPITestGroupBox.Text = "API Test";
            // 
            // TwitchTestSendText
            // 
            TwitchTestSendText.Location = new System.Drawing.Point(26, 97);
            TwitchTestSendText.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TwitchTestSendText.Name = "TwitchTestSendText";
            TwitchTestSendText.Size = new System.Drawing.Size(437, 31);
            TwitchTestSendText.TabIndex = 20;
            TwitchTestSendText.Text = "Hello! I am BanterBrain Buddy https://banterbrain.tv";
            // 
            // TwitchSendTextCheckBox
            // 
            TwitchSendTextCheckBox.AutoSize = true;
            TwitchSendTextCheckBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchSendTextCheckBox.Location = new System.Drawing.Point(10, 33);
            TwitchSendTextCheckBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TwitchSendTextCheckBox.Name = "TwitchSendTextCheckBox";
            TwitchSendTextCheckBox.Size = new System.Drawing.Size(278, 29);
            TwitchSendTextCheckBox.TabIndex = 19;
            TwitchSendTextCheckBox.Text = "Send Message on join channel";
            TwitchSendTextCheckBox.UseVisualStyleBackColor = true;
            // 
            // TwitchAPITestButton
            // 
            TwitchAPITestButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchAPITestButton.Location = new System.Drawing.Point(340, 25);
            TwitchAPITestButton.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TwitchAPITestButton.Name = "TwitchAPITestButton";
            TwitchAPITestButton.Size = new System.Drawing.Size(126, 45);
            TwitchAPITestButton.TabIndex = 18;
            TwitchAPITestButton.Text = "Test Credentials";
            TwitchAPITestButton.UseVisualStyleBackColor = true;
            TwitchAPITestButton.Click += TwitchTestButton_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(304, 15);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(130, 25);
            label9.TabIndex = 0;
            label9.Text = "Twitch Settings";
            // 
            // ElevenLabsPanel
            // 
            ElevenLabsPanel.Controls.Add(ElevenLabsTestButton);
            ElevenLabsPanel.Controls.Add(ElevenlabsAPIKeyTextBox);
            ElevenLabsPanel.Controls.Add(label11);
            ElevenLabsPanel.Controls.Add(label8);
            ElevenLabsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            ElevenLabsPanel.Location = new System.Drawing.Point(0, 0);
            ElevenLabsPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            ElevenLabsPanel.Name = "ElevenLabsPanel";
            ElevenLabsPanel.Size = new System.Drawing.Size(847, 965);
            ElevenLabsPanel.TabIndex = 40;
            ElevenLabsPanel.Validating += ElevenLabsPanel_Validating;
            // 
            // ElevenLabsTestButton
            // 
            ElevenLabsTestButton.Location = new System.Drawing.Point(620, 65);
            ElevenLabsTestButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            ElevenLabsTestButton.Name = "ElevenLabsTestButton";
            ElevenLabsTestButton.Size = new System.Drawing.Size(107, 38);
            ElevenLabsTestButton.TabIndex = 3;
            ElevenLabsTestButton.Text = "Test";
            ElevenLabsTestButton.UseVisualStyleBackColor = true;
            ElevenLabsTestButton.Click += ElevenLabsTestButton_Click;
            // 
            // ElevenlabsAPIKeyTextBox
            // 
            ElevenlabsAPIKeyTextBox.Location = new System.Drawing.Point(260, 65);
            ElevenlabsAPIKeyTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            ElevenlabsAPIKeyTextBox.Name = "ElevenlabsAPIKeyTextBox";
            ElevenlabsAPIKeyTextBox.PasswordChar = '*';
            ElevenlabsAPIKeyTextBox.Size = new System.Drawing.Size(320, 31);
            ElevenlabsAPIKeyTextBox.TabIndex = 2;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(59, 75);
            label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(72, 25);
            label11.TabIndex = 1;
            label11.Text = "API Key";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(256, 17);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(98, 25);
            label8.TabIndex = 0;
            label8.Text = "ElevenLabs";
            // 
            // OpenAIChatGPTPanel
            // 
            OpenAIChatGPTPanel.Controls.Add(WhisperSpeechRecognitionComboBox);
            OpenAIChatGPTPanel.Controls.Add(label23);
            OpenAIChatGPTPanel.Controls.Add(LLMMaxTokensHelpText);
            OpenAIChatGPTPanel.Controls.Add(LLMTempHelpText);
            OpenAIChatGPTPanel.Controls.Add(LLMMaxTokenLabel);
            OpenAIChatGPTPanel.Controls.Add(LLMTempLabel);
            OpenAIChatGPTPanel.Controls.Add(GPTMaxTokensTextBox);
            OpenAIChatGPTPanel.Controls.Add(GPTTemperatureTextBox);
            OpenAIChatGPTPanel.Controls.Add(GPTModelComboBox);
            OpenAIChatGPTPanel.Controls.Add(label4);
            OpenAIChatGPTPanel.Controls.Add(GPTTestButton);
            OpenAIChatGPTPanel.Controls.Add(GPTAPIKeyTextBox);
            OpenAIChatGPTPanel.Controls.Add(GPTAPIKeyLabel);
            OpenAIChatGPTPanel.Controls.Add(label7);
            OpenAIChatGPTPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            OpenAIChatGPTPanel.Location = new System.Drawing.Point(0, 0);
            OpenAIChatGPTPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            OpenAIChatGPTPanel.Name = "OpenAIChatGPTPanel";
            OpenAIChatGPTPanel.Size = new System.Drawing.Size(847, 965);
            OpenAIChatGPTPanel.TabIndex = 3;
            OpenAIChatGPTPanel.Validating += OpenAIChatGPTPanel_Validating;
            // 
            // WhisperSpeechRecognitionComboBox
            // 
            WhisperSpeechRecognitionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            WhisperSpeechRecognitionComboBox.FormattingEnabled = true;
            WhisperSpeechRecognitionComboBox.Items.AddRange(new object[] { "Dutch", "Danish", "English", "French", "German", "Italian", "Japanese", "Norwegian", "Polish", "Spanish", "Swedish" });
            WhisperSpeechRecognitionComboBox.Location = new System.Drawing.Point(320, 300);
            WhisperSpeechRecognitionComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            WhisperSpeechRecognitionComboBox.Name = "WhisperSpeechRecognitionComboBox";
            WhisperSpeechRecognitionComboBox.Size = new System.Drawing.Size(260, 33);
            WhisperSpeechRecognitionComboBox.TabIndex = 41;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new System.Drawing.Point(53, 305);
            label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label23.Name = "label23";
            label23.Size = new System.Drawing.Size(242, 25);
            label23.TabIndex = 40;
            label23.Text = "Speech recognition language";
            // 
            // LLMMaxTokensHelpText
            // 
            LLMMaxTokensHelpText.AutoSize = true;
            LLMMaxTokensHelpText.BackColor = System.Drawing.Color.Gold;
            LLMMaxTokensHelpText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            LLMMaxTokensHelpText.Location = new System.Drawing.Point(553, 193);
            LLMMaxTokensHelpText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LLMMaxTokensHelpText.Name = "LLMMaxTokensHelpText";
            LLMMaxTokensHelpText.Size = new System.Drawing.Size(30, 25);
            LLMMaxTokensHelpText.TabIndex = 38;
            LLMMaxTokensHelpText.Text = "[?]";
            BBBToolTip.SetToolTip(LLMMaxTokensHelpText, "Default: 100. Max: 4096. Tokens are aproximately the amount of words. More tokens means longer words, but might also cost more");
            // 
            // LLMTempHelpText
            // 
            LLMTempHelpText.AutoSize = true;
            LLMTempHelpText.BackColor = System.Drawing.Color.Gold;
            LLMTempHelpText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            LLMTempHelpText.Location = new System.Drawing.Point(374, 197);
            LLMTempHelpText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LLMTempHelpText.Name = "LLMTempHelpText";
            LLMTempHelpText.Size = new System.Drawing.Size(30, 25);
            LLMTempHelpText.TabIndex = 37;
            LLMTempHelpText.Text = "[?]";
            BBBToolTip.SetToolTip(LLMTempHelpText, "Default: 0. Suggested: 0,9. Max: 2. The higher the temperature the more likely the answer uses more diverse words, but also is more likely to make mistakes");
            // 
            // LLMMaxTokenLabel
            // 
            LLMMaxTokenLabel.AutoSize = true;
            LLMMaxTokenLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            LLMMaxTokenLabel.Location = new System.Drawing.Point(439, 193);
            LLMMaxTokenLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LLMMaxTokenLabel.Name = "LLMMaxTokenLabel";
            LLMMaxTokenLabel.Size = new System.Drawing.Size(103, 25);
            LLMMaxTokenLabel.TabIndex = 36;
            LLMMaxTokenLabel.Text = "Max tokens";
            // 
            // LLMTempLabel
            // 
            LLMTempLabel.AutoSize = true;
            LLMTempLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            LLMTempLabel.Location = new System.Drawing.Point(261, 197);
            LLMTempLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LLMTempLabel.Name = "LLMTempLabel";
            LLMTempLabel.Size = new System.Drawing.Size(110, 25);
            LLMTempLabel.TabIndex = 35;
            LLMTempLabel.Text = "Temperature";
            // 
            // GPTMaxTokensTextBox
            // 
            GPTMaxTokensTextBox.Location = new System.Drawing.Point(439, 227);
            GPTMaxTokensTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            GPTMaxTokensTextBox.Name = "GPTMaxTokensTextBox";
            GPTMaxTokensTextBox.Size = new System.Drawing.Size(141, 31);
            GPTMaxTokensTextBox.TabIndex = 34;
            GPTMaxTokensTextBox.Text = "100";
            GPTMaxTokensTextBox.KeyPress += GPTMaxTokensTextBox_KeyPress;
            GPTMaxTokensTextBox.Validating += GPTMaxTokensTextBox_Validating;
            // 
            // GPTTemperatureTextBox
            // 
            GPTTemperatureTextBox.Location = new System.Drawing.Point(261, 227);
            GPTTemperatureTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            GPTTemperatureTextBox.Name = "GPTTemperatureTextBox";
            GPTTemperatureTextBox.Size = new System.Drawing.Size(141, 31);
            GPTTemperatureTextBox.TabIndex = 33;
            GPTTemperatureTextBox.Text = "0";
            BBBToolTip.SetToolTip(GPTTemperatureTextBox, "Default: 0. Maximum 2");
            GPTTemperatureTextBox.KeyPress += GPTTemperatureTextBox_KeyPress;
            GPTTemperatureTextBox.Validating += GPTTemperatureTextBox_Validating;
            // 
            // GPTModelComboBox
            // 
            GPTModelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            GPTModelComboBox.FormattingEnabled = true;
            GPTModelComboBox.Items.AddRange(new object[] { "gpt-3.5-turbo", "gpt-4-omni" });
            GPTModelComboBox.Location = new System.Drawing.Point(261, 137);
            GPTModelComboBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            GPTModelComboBox.Name = "GPTModelComboBox";
            GPTModelComboBox.Size = new System.Drawing.Size(318, 33);
            GPTModelComboBox.TabIndex = 32;
            GPTModelComboBox.UseWaitCursor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label4.Location = new System.Drawing.Point(53, 137);
            label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(99, 25);
            label4.TabIndex = 31;
            label4.Text = "GPT Model";
            // 
            // GPTTestButton
            // 
            GPTTestButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            GPTTestButton.Location = new System.Drawing.Point(624, 78);
            GPTTestButton.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            GPTTestButton.Name = "GPTTestButton";
            GPTTestButton.Size = new System.Drawing.Size(126, 45);
            GPTTestButton.TabIndex = 29;
            GPTTestButton.Text = "Test";
            GPTTestButton.UseVisualStyleBackColor = true;
            GPTTestButton.Click += GPTTestButton_Click;
            // 
            // GPTAPIKeyTextBox
            // 
            GPTAPIKeyTextBox.Location = new System.Drawing.Point(261, 78);
            GPTAPIKeyTextBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            GPTAPIKeyTextBox.Name = "GPTAPIKeyTextBox";
            GPTAPIKeyTextBox.PasswordChar = '*';
            GPTAPIKeyTextBox.Size = new System.Drawing.Size(318, 31);
            GPTAPIKeyTextBox.TabIndex = 28;
            // 
            // GPTAPIKeyLabel
            // 
            GPTAPIKeyLabel.AutoSize = true;
            GPTAPIKeyLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            GPTAPIKeyLabel.Location = new System.Drawing.Point(53, 85);
            GPTAPIKeyLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            GPTAPIKeyLabel.Name = "GPTAPIKeyLabel";
            GPTAPIKeyLabel.Size = new System.Drawing.Size(72, 25);
            GPTAPIKeyLabel.TabIndex = 27;
            GPTAPIKeyLabel.Text = "API Key";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(304, 15);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(73, 25);
            label7.TabIndex = 0;
            label7.Text = "OpenAI";
            // 
            // PersonasPanel
            // 
            PersonasPanel.Controls.Add(label31);
            PersonasPanel.Controls.Add(label32);
            PersonasPanel.Controls.Add(TTSPitchLevel);
            PersonasPanel.Controls.Add(PitchTrackBar);
            PersonasPanel.Controls.Add(TTSSpeedLevel);
            PersonasPanel.Controls.Add(TTSVoiceLevel);
            PersonasPanel.Controls.Add(label28);
            PersonasPanel.Controls.Add(RateTrackBar);
            PersonasPanel.Controls.Add(label5);
            PersonasPanel.Controls.Add(VolumeTrackBar);
            PersonasPanel.Controls.Add(TTSOutputVoiceOption3);
            PersonasPanel.Controls.Add(TTSOption3Label);
            PersonasPanel.Controls.Add(TTSOutputVoiceOption2);
            PersonasPanel.Controls.Add(TTSOption2Label);
            PersonasPanel.Controls.Add(TestVoiceButton);
            PersonasPanel.Controls.Add(DeletePersona);
            PersonasPanel.Controls.Add(SavePersona);
            PersonasPanel.Controls.Add(NewPersona);
            PersonasPanel.Controls.Add(TTSOutputVoiceOption1);
            PersonasPanel.Controls.Add(TTSOption1Label);
            PersonasPanel.Controls.Add(TTSOutputVoice);
            PersonasPanel.Controls.Add(label16);
            PersonasPanel.Controls.Add(TTSProviderComboBox);
            PersonasPanel.Controls.Add(label14);
            PersonasPanel.Controls.Add(label13);
            PersonasPanel.Controls.Add(PersonaComboBox);
            PersonasPanel.Controls.Add(label12);
            PersonasPanel.Controls.Add(PersonaRoleTextBox);
            PersonasPanel.Controls.Add(label10);
            PersonasPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            PersonasPanel.Location = new System.Drawing.Point(0, 0);
            PersonasPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            PersonasPanel.Name = "PersonasPanel";
            PersonasPanel.Size = new System.Drawing.Size(847, 965);
            PersonasPanel.TabIndex = 6;
            PersonasPanel.Visible = false;
            PersonasPanel.VisibleChanged += PersonasPanel_VisibleChanged;
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Location = new System.Drawing.Point(231, 768);
            label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label31.Name = "label31";
            label31.Size = new System.Drawing.Size(475, 25);
            label31.TabIndex = 46;
            label31.Text = "Note: changing the slider values can create sound artifacts ";
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Location = new System.Drawing.Point(574, 638);
            label32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label32.Name = "label32";
            label32.Size = new System.Drawing.Size(148, 25);
            label32.TabIndex = 45;
            label32.Text = "TTS Pitch Change";
            // 
            // TTSPitchLevel
            // 
            TTSPitchLevel.AutoSize = true;
            TTSPitchLevel.Location = new System.Drawing.Point(643, 720);
            TTSPitchLevel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            TTSPitchLevel.Name = "TTSPitchLevel";
            TTSPitchLevel.Size = new System.Drawing.Size(22, 25);
            TTSPitchLevel.TabIndex = 44;
            TTSPitchLevel.Text = "0";
            // 
            // PitchTrackBar
            // 
            PitchTrackBar.LargeChange = 10;
            PitchTrackBar.Location = new System.Drawing.Point(574, 668);
            PitchTrackBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            PitchTrackBar.Maximum = 100;
            PitchTrackBar.Minimum = -100;
            PitchTrackBar.Name = "PitchTrackBar";
            PitchTrackBar.Size = new System.Drawing.Size(159, 69);
            PitchTrackBar.SmallChange = 10;
            PitchTrackBar.TabIndex = 43;
            PitchTrackBar.TickFrequency = 10;
            PitchTrackBar.ValueChanged += PitchTrackBar_ValueChanged;
            // 
            // TTSSpeedLevel
            // 
            TTSSpeedLevel.AutoSize = true;
            TTSSpeedLevel.Location = new System.Drawing.Point(456, 720);
            TTSSpeedLevel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            TTSSpeedLevel.Name = "TTSSpeedLevel";
            TTSSpeedLevel.Size = new System.Drawing.Size(22, 25);
            TTSSpeedLevel.TabIndex = 42;
            TTSSpeedLevel.Text = "0";
            // 
            // TTSVoiceLevel
            // 
            TTSVoiceLevel.AutoSize = true;
            TTSVoiceLevel.Location = new System.Drawing.Point(271, 720);
            TTSVoiceLevel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            TTSVoiceLevel.Name = "TTSVoiceLevel";
            TTSVoiceLevel.Size = new System.Drawing.Size(22, 25);
            TTSVoiceLevel.TabIndex = 41;
            TTSVoiceLevel.Text = "0";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Location = new System.Drawing.Point(397, 640);
            label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label28.Name = "label28";
            label28.Size = new System.Drawing.Size(145, 25);
            label28.TabIndex = 40;
            label28.Text = "TTS Rate Change";
            // 
            // RateTrackBar
            // 
            RateTrackBar.Location = new System.Drawing.Point(387, 670);
            RateTrackBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            RateTrackBar.Maximum = 100;
            RateTrackBar.Minimum = -100;
            RateTrackBar.Name = "RateTrackBar";
            RateTrackBar.Size = new System.Drawing.Size(159, 69);
            RateTrackBar.TabIndex = 39;
            RateTrackBar.TickFrequency = 10;
            RateTrackBar.ValueChanged += RateTrackBar_ValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(206, 640);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(170, 25);
            label5.TabIndex = 38;
            label5.Text = "TTS Volume Change";
            // 
            // VolumeTrackBar
            // 
            VolumeTrackBar.Location = new System.Drawing.Point(207, 670);
            VolumeTrackBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            VolumeTrackBar.Maximum = 100;
            VolumeTrackBar.Minimum = -100;
            VolumeTrackBar.Name = "VolumeTrackBar";
            VolumeTrackBar.Size = new System.Drawing.Size(156, 69);
            VolumeTrackBar.TabIndex = 37;
            VolumeTrackBar.TickFrequency = 10;
            VolumeTrackBar.ValueChanged += VolumeTrackBar_ValueChanged;
            // 
            // TTSOutputVoiceOption3
            // 
            TTSOutputVoiceOption3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TTSOutputVoiceOption3.FormattingEnabled = true;
            TTSOutputVoiceOption3.Location = new System.Drawing.Point(261, 590);
            TTSOutputVoiceOption3.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TTSOutputVoiceOption3.Name = "TTSOutputVoiceOption3";
            TTSOutputVoiceOption3.Size = new System.Drawing.Size(400, 33);
            TTSOutputVoiceOption3.TabIndex = 36;
            TTSOutputVoiceOption3.TextChanged += TTSOutputVoiceOption3_TextChanged;
            TTSOutputVoiceOption3.Validating += TTSOutputVoiceOption3_Validating;
            // 
            // TTSOption3Label
            // 
            TTSOption3Label.AutoSize = true;
            TTSOption3Label.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TTSOption3Label.Location = new System.Drawing.Point(21, 595);
            TTSOption3Label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            TTSOption3Label.Name = "TTSOption3Label";
            TTSOption3Label.Size = new System.Drawing.Size(225, 25);
            TTSOption3Label.TabIndex = 35;
            TTSOption3Label.Text = "TTS Output Voice Option 3";
            // 
            // TTSOutputVoiceOption2
            // 
            TTSOutputVoiceOption2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TTSOutputVoiceOption2.FormattingEnabled = true;
            TTSOutputVoiceOption2.Location = new System.Drawing.Point(261, 542);
            TTSOutputVoiceOption2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TTSOutputVoiceOption2.Name = "TTSOutputVoiceOption2";
            TTSOutputVoiceOption2.Size = new System.Drawing.Size(400, 33);
            TTSOutputVoiceOption2.TabIndex = 34;
            TTSOutputVoiceOption2.TextChanged += TTSOutputVoiceOption2_TextChanged;
            TTSOutputVoiceOption2.Validating += TTSOutputVoiceOption2_Validating;
            // 
            // TTSOption2Label
            // 
            TTSOption2Label.AutoSize = true;
            TTSOption2Label.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TTSOption2Label.Location = new System.Drawing.Point(19, 547);
            TTSOption2Label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            TTSOption2Label.Name = "TTSOption2Label";
            TTSOption2Label.Size = new System.Drawing.Size(225, 25);
            TTSOption2Label.TabIndex = 33;
            TTSOption2Label.Text = "TTS Output Voice Option 2";
            // 
            // TestVoiceButton
            // 
            TestVoiceButton.Location = new System.Drawing.Point(697, 382);
            TestVoiceButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            TestVoiceButton.Name = "TestVoiceButton";
            TestVoiceButton.Size = new System.Drawing.Size(107, 38);
            TestVoiceButton.TabIndex = 32;
            TestVoiceButton.Text = "Test voice";
            TestVoiceButton.UseVisualStyleBackColor = true;
            TestVoiceButton.Click += TestVoiceButton_Click;
            // 
            // DeletePersona
            // 
            DeletePersona.Location = new System.Drawing.Point(723, 87);
            DeletePersona.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            DeletePersona.Name = "DeletePersona";
            DeletePersona.Size = new System.Drawing.Size(107, 38);
            DeletePersona.TabIndex = 31;
            DeletePersona.Text = "Delete";
            DeletePersona.UseVisualStyleBackColor = true;
            DeletePersona.Click += DeletePersona_Click;
            // 
            // SavePersona
            // 
            SavePersona.Enabled = false;
            SavePersona.Location = new System.Drawing.Point(609, 85);
            SavePersona.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            SavePersona.Name = "SavePersona";
            SavePersona.Size = new System.Drawing.Size(107, 38);
            SavePersona.TabIndex = 30;
            SavePersona.Text = "Save";
            SavePersona.UseVisualStyleBackColor = true;
            SavePersona.Click += SavePersona_Click;
            // 
            // NewPersona
            // 
            NewPersona.Location = new System.Drawing.Point(493, 85);
            NewPersona.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            NewPersona.Name = "NewPersona";
            NewPersona.Size = new System.Drawing.Size(107, 38);
            NewPersona.TabIndex = 29;
            NewPersona.Text = "New";
            NewPersona.UseVisualStyleBackColor = true;
            NewPersona.Click += NewPersona_Click;
            // 
            // TTSOutputVoiceOption1
            // 
            TTSOutputVoiceOption1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TTSOutputVoiceOption1.FormattingEnabled = true;
            TTSOutputVoiceOption1.Location = new System.Drawing.Point(260, 493);
            TTSOutputVoiceOption1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TTSOutputVoiceOption1.Name = "TTSOutputVoiceOption1";
            TTSOutputVoiceOption1.Size = new System.Drawing.Size(400, 33);
            TTSOutputVoiceOption1.TabIndex = 28;
            TTSOutputVoiceOption1.SelectedIndexChanged += TTSOutputVoiceOption1_SelectedIndexChanged;
            TTSOutputVoiceOption1.TextChanged += TTSOutputVoiceOption1_TextChanged;
            TTSOutputVoiceOption1.Validating += TTSOutputVoiceOption1_Validating;
            // 
            // TTSOption1Label
            // 
            TTSOption1Label.AutoSize = true;
            TTSOption1Label.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TTSOption1Label.Location = new System.Drawing.Point(19, 502);
            TTSOption1Label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            TTSOption1Label.Name = "TTSOption1Label";
            TTSOption1Label.Size = new System.Drawing.Size(225, 25);
            TTSOption1Label.TabIndex = 27;
            TTSOption1Label.Text = "TTS Output Voice Option 1";
            // 
            // TTSOutputVoice
            // 
            TTSOutputVoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TTSOutputVoice.FormattingEnabled = true;
            TTSOutputVoice.Location = new System.Drawing.Point(260, 437);
            TTSOutputVoice.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TTSOutputVoice.Name = "TTSOutputVoice";
            TTSOutputVoice.Size = new System.Drawing.Size(400, 33);
            TTSOutputVoice.TabIndex = 26;
            TTSOutputVoice.SelectedValueChanged += TTSOutputVoice_SelectedValueChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label16.Location = new System.Drawing.Point(19, 450);
            label16.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(149, 25);
            label16.TabIndex = 25;
            label16.Text = "TTS Output Voice";
            // 
            // TTSProviderComboBox
            // 
            TTSProviderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TTSProviderComboBox.FormattingEnabled = true;
            TTSProviderComboBox.Location = new System.Drawing.Point(260, 382);
            TTSProviderComboBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TTSProviderComboBox.Name = "TTSProviderComboBox";
            TTSProviderComboBox.Size = new System.Drawing.Size(400, 33);
            TTSProviderComboBox.TabIndex = 24;
            TTSProviderComboBox.SelectedValueChanged += TTSProviderComboBox_SelectedValueChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(21, 387);
            label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(111, 25);
            label14.TabIndex = 23;
            label14.Text = "TTS Provider";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(21, 92);
            label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(123, 25);
            label13.TabIndex = 22;
            label13.Text = "Persona name";
            // 
            // PersonaComboBox
            // 
            PersonaComboBox.FormattingEnabled = true;
            PersonaComboBox.Location = new System.Drawing.Point(157, 87);
            PersonaComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            PersonaComboBox.Name = "PersonaComboBox";
            PersonaComboBox.Size = new System.Drawing.Size(328, 33);
            PersonaComboBox.TabIndex = 21;
            PersonaComboBox.SelectedValueChanged += PersonaComboBox_SelectedValueChanged;
            PersonaComboBox.Validating += PersonaComboBox_Validating;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label12.Location = new System.Drawing.Point(21, 178);
            label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(80, 25);
            label12.TabIndex = 20;
            label12.Text = "Role text";
            // 
            // PersonaRoleTextBox
            // 
            PersonaRoleTextBox.Location = new System.Drawing.Point(157, 153);
            PersonaRoleTextBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            PersonaRoleTextBox.Multiline = true;
            PersonaRoleTextBox.Name = "PersonaRoleTextBox";
            PersonaRoleTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            PersonaRoleTextBox.Size = new System.Drawing.Size(673, 191);
            PersonaRoleTextBox.TabIndex = 19;
            PersonaRoleTextBox.TabStop = false;
            PersonaRoleTextBox.Text = "placeholder";
            PersonaRoleTextBox.TextChanged += PersonaRoleTextBox_TextChanged;
            PersonaRoleTextBox.Validating += PersonaRoleTextBox_Validating;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(334, 15);
            label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(82, 25);
            label10.TabIndex = 0;
            label10.Text = "Personas";
            // 
            // SpeakerPanel
            // 
            SpeakerPanel.Controls.Add(OutputVolumeLabel);
            SpeakerPanel.Controls.Add(label30);
            SpeakerPanel.Controls.Add(label29);
            SpeakerPanel.Controls.Add(SpeakerDeviceVolumeTrackBar);
            SpeakerPanel.Controls.Add(TTSAudioOutputComboBox);
            SpeakerPanel.Controls.Add(TTSAudioOutputLabel);
            SpeakerPanel.Controls.Add(label6);
            SpeakerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            SpeakerPanel.Location = new System.Drawing.Point(0, 0);
            SpeakerPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            SpeakerPanel.Name = "SpeakerPanel";
            SpeakerPanel.Size = new System.Drawing.Size(847, 965);
            SpeakerPanel.TabIndex = 2;
            // 
            // OutputVolumeLabel
            // 
            OutputVolumeLabel.AutoSize = true;
            OutputVolumeLabel.Location = new System.Drawing.Point(437, 227);
            OutputVolumeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            OutputVolumeLabel.Name = "OutputVolumeLabel";
            OutputVolumeLabel.Size = new System.Drawing.Size(37, 25);
            OutputVolumeLabel.TabIndex = 23;
            OutputVolumeLabel.Text = "0%";
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Location = new System.Drawing.Point(236, 263);
            label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label30.Name = "label30";
            label30.Size = new System.Drawing.Size(487, 25);
            label30.TabIndex = 22;
            label30.Text = "Warning: changes the global Windows volume of the device";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Location = new System.Drawing.Point(74, 180);
            label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label29.Name = "label29";
            label29.Size = new System.Drawing.Size(183, 25);
            label29.TabIndex = 21;
            label29.Text = "Audio output volume";
            // 
            // SpeakerDeviceVolumeTrackBar
            // 
            SpeakerDeviceVolumeTrackBar.Location = new System.Drawing.Point(256, 173);
            SpeakerDeviceVolumeTrackBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            SpeakerDeviceVolumeTrackBar.Maximum = 100;
            SpeakerDeviceVolumeTrackBar.Name = "SpeakerDeviceVolumeTrackBar";
            SpeakerDeviceVolumeTrackBar.Size = new System.Drawing.Size(431, 69);
            SpeakerDeviceVolumeTrackBar.TabIndex = 20;
            SpeakerDeviceVolumeTrackBar.ValueChanged += SpeakerDeviceVolumeTrackBar_ValueChanged;
            // 
            // TTSAudioOutputComboBox
            // 
            TTSAudioOutputComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TTSAudioOutputComboBox.FormattingEnabled = true;
            TTSAudioOutputComboBox.Location = new System.Drawing.Point(257, 92);
            TTSAudioOutputComboBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TTSAudioOutputComboBox.Name = "TTSAudioOutputComboBox";
            TTSAudioOutputComboBox.Size = new System.Drawing.Size(425, 33);
            TTSAudioOutputComboBox.TabIndex = 19;
            TTSAudioOutputComboBox.SelectedIndexChanged += TTSAudioOutputComboBox_SelectedIndexChanged;
            // 
            // TTSAudioOutputLabel
            // 
            TTSAudioOutputLabel.AutoSize = true;
            TTSAudioOutputLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TTSAudioOutputLabel.Location = new System.Drawing.Point(70, 97);
            TTSAudioOutputLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            TTSAudioOutputLabel.Name = "TTSAudioOutputLabel";
            TTSAudioOutputLabel.Size = new System.Drawing.Size(122, 25);
            TTSAudioOutputLabel.TabIndex = 18;
            TTSAudioOutputLabel.Text = "Audio Output";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(320, 13);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(204, 25);
            label6.TabIndex = 0;
            label6.Text = "Speaker Output settings";
            // 
            // AzurePanel
            // 
            AzurePanel.Controls.Add(label2);
            AzurePanel.Controls.Add(AzureLanguageComboBox);
            AzurePanel.Controls.Add(TestAzureAPISettings);
            AzurePanel.Controls.Add(label17);
            AzurePanel.Controls.Add(AzureRegionTextBox);
            AzurePanel.Controls.Add(label18);
            AzurePanel.Controls.Add(AzureAPIKeyTextBox);
            AzurePanel.Controls.Add(label3);
            AzurePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            AzurePanel.Location = new System.Drawing.Point(0, 0);
            AzurePanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            AzurePanel.Name = "AzurePanel";
            AzurePanel.Size = new System.Drawing.Size(847, 965);
            AzurePanel.TabIndex = 1;
            AzurePanel.Validating += AzurePanel_Validating;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(19, 175);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(89, 25);
            label2.TabIndex = 24;
            label2.Text = "Language";
            // 
            // AzureLanguageComboBox
            // 
            AzureLanguageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            AzureLanguageComboBox.FormattingEnabled = true;
            AzureLanguageComboBox.Items.AddRange(new object[] { "da-DK", "de-AT", "de-CH", "de-DE", "en-GB", "en-US", "es-ES", "fr-BE", "fr-CA", "fr-FR", "ga-IE", "it-IT", "ja-JP", "nl-BE", "nl-NL", "pl-PL", "pt-PT", "sv-SE" });
            AzureLanguageComboBox.Location = new System.Drawing.Point(153, 165);
            AzureLanguageComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            AzureLanguageComboBox.Name = "AzureLanguageComboBox";
            AzureLanguageComboBox.Size = new System.Drawing.Size(287, 33);
            AzureLanguageComboBox.TabIndex = 23;
            // 
            // TestAzureAPISettings
            // 
            TestAzureAPISettings.Location = new System.Drawing.Point(467, 53);
            TestAzureAPISettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            TestAzureAPISettings.Name = "TestAzureAPISettings";
            TestAzureAPISettings.Size = new System.Drawing.Size(107, 38);
            TestAzureAPISettings.TabIndex = 22;
            TestAzureAPISettings.Text = "Test";
            TestAzureAPISettings.UseVisualStyleBackColor = true;
            TestAzureAPISettings.Click += TestAzureAPISettings_Click;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label17.Location = new System.Drawing.Point(16, 105);
            label17.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(67, 25);
            label17.TabIndex = 18;
            label17.Text = "Region";
            // 
            // AzureRegionTextBox
            // 
            AzureRegionTextBox.Location = new System.Drawing.Point(153, 105);
            AzureRegionTextBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            AzureRegionTextBox.Name = "AzureRegionTextBox";
            AzureRegionTextBox.Size = new System.Drawing.Size(287, 31);
            AzureRegionTextBox.TabIndex = 19;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label18.Location = new System.Drawing.Point(17, 55);
            label18.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(72, 25);
            label18.TabIndex = 20;
            label18.Text = "API Key";
            // 
            // AzureAPIKeyTextBox
            // 
            AzureAPIKeyTextBox.Location = new System.Drawing.Point(153, 55);
            AzureAPIKeyTextBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            AzureAPIKeyTextBox.Name = "AzureAPIKeyTextBox";
            AzureAPIKeyTextBox.PasswordChar = '*';
            AzureAPIKeyTextBox.Size = new System.Drawing.Size(287, 31);
            AzureAPIKeyTextBox.TabIndex = 21;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(301, 15);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(156, 25);
            label3.TabIndex = 0;
            label3.Text = "Azure API settings";
            // 
            // OllamaPanel
            // 
            OllamaPanel.Controls.Add(UseOllamaLLMCheckBox);
            OllamaPanel.Controls.Add(OllamaResponseLengthComboBox);
            OllamaPanel.Controls.Add(label20);
            OllamaPanel.Controls.Add(OllamaTestButton);
            OllamaPanel.Controls.Add(OllamaModelsTextLabel);
            OllamaPanel.Controls.Add(OllamaModelsComboBox);
            OllamaPanel.Controls.Add(OllamaURITextBox);
            OllamaPanel.Controls.Add(label19);
            OllamaPanel.Controls.Add(label15);
            OllamaPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            OllamaPanel.Location = new System.Drawing.Point(0, 0);
            OllamaPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            OllamaPanel.Name = "OllamaPanel";
            OllamaPanel.Size = new System.Drawing.Size(847, 965);
            OllamaPanel.TabIndex = 37;
            OllamaPanel.VisibleChanged += OllamaPanel_VisibleChanged;
            // 
            // UseOllamaLLMCheckBox
            // 
            UseOllamaLLMCheckBox.AutoSize = true;
            UseOllamaLLMCheckBox.Location = new System.Drawing.Point(89, 32);
            UseOllamaLLMCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            UseOllamaLLMCheckBox.Name = "UseOllamaLLMCheckBox";
            UseOllamaLLMCheckBox.Size = new System.Drawing.Size(151, 29);
            UseOllamaLLMCheckBox.TabIndex = 10;
            UseOllamaLLMCheckBox.Text = "Enable Ollama";
            UseOllamaLLMCheckBox.UseVisualStyleBackColor = true;
            UseOllamaLLMCheckBox.Click += UseOllamaLLMCheckBox_Click;
            // 
            // OllamaResponseLengthComboBox
            // 
            OllamaResponseLengthComboBox.FormattingEnabled = true;
            OllamaResponseLengthComboBox.Items.AddRange(new object[] { "Short", "Normal", "Long" });
            OllamaResponseLengthComboBox.Location = new System.Drawing.Point(289, 220);
            OllamaResponseLengthComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            OllamaResponseLengthComboBox.Name = "OllamaResponseLengthComboBox";
            OllamaResponseLengthComboBox.Size = new System.Drawing.Size(291, 33);
            OllamaResponseLengthComboBox.TabIndex = 9;
            OllamaResponseLengthComboBox.Text = "Normal";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new System.Drawing.Point(93, 228);
            label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label20.Name = "label20";
            label20.Size = new System.Drawing.Size(143, 25);
            label20.TabIndex = 8;
            label20.Text = "Response length";
            // 
            // OllamaTestButton
            // 
            OllamaTestButton.Location = new System.Drawing.Point(651, 105);
            OllamaTestButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            OllamaTestButton.Name = "OllamaTestButton";
            OllamaTestButton.Size = new System.Drawing.Size(107, 38);
            OllamaTestButton.TabIndex = 7;
            OllamaTestButton.Text = "Test";
            OllamaTestButton.UseVisualStyleBackColor = true;
            OllamaTestButton.Click += OllamaTestButton_Click;
            // 
            // OllamaModelsTextLabel
            // 
            OllamaModelsTextLabel.AutoSize = true;
            OllamaModelsTextLabel.Location = new System.Drawing.Point(89, 173);
            OllamaModelsTextLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            OllamaModelsTextLabel.Name = "OllamaModelsTextLabel";
            OllamaModelsTextLabel.Size = new System.Drawing.Size(71, 25);
            OllamaModelsTextLabel.TabIndex = 5;
            OllamaModelsTextLabel.Text = "Models";
            // 
            // OllamaModelsComboBox
            // 
            OllamaModelsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            OllamaModelsComboBox.FormattingEnabled = true;
            OllamaModelsComboBox.Location = new System.Drawing.Point(289, 167);
            OllamaModelsComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            OllamaModelsComboBox.Name = "OllamaModelsComboBox";
            OllamaModelsComboBox.Size = new System.Drawing.Size(291, 33);
            OllamaModelsComboBox.TabIndex = 4;
            // 
            // OllamaURITextBox
            // 
            OllamaURITextBox.Location = new System.Drawing.Point(286, 102);
            OllamaURITextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            OllamaURITextBox.Name = "OllamaURITextBox";
            OllamaURITextBox.Size = new System.Drawing.Size(294, 31);
            OllamaURITextBox.TabIndex = 2;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new System.Drawing.Point(84, 110);
            label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label19.Name = "label19";
            label19.Size = new System.Drawing.Size(101, 25);
            label19.TabIndex = 1;
            label19.Text = "Ollama URI";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(383, 20);
            label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(68, 25);
            label15.TabIndex = 0;
            label15.Text = "Ollama";
            // 
            // OBSPanel
            // 
            OBSPanel.Controls.Add(WebsourceServerEnable);
            OBSPanel.Controls.Add(label27);
            OBSPanel.Controls.Add(TwitchChatSoundSelectButton);
            OBSPanel.Controls.Add(label26);
            OBSPanel.Controls.Add(label25);
            OBSPanel.Controls.Add(WebsourceServer);
            OBSPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            OBSPanel.Location = new System.Drawing.Point(0, 0);
            OBSPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            OBSPanel.Name = "OBSPanel";
            OBSPanel.Size = new System.Drawing.Size(847, 965);
            OBSPanel.TabIndex = 36;
            OBSPanel.VisibleChanged += OBSPanel_VisibleChanged;
            // 
            // WebsourceServerEnable
            // 
            WebsourceServerEnable.AutoSize = true;
            WebsourceServerEnable.Location = new System.Drawing.Point(97, 65);
            WebsourceServerEnable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            WebsourceServerEnable.Name = "WebsourceServerEnable";
            WebsourceServerEnable.Size = new System.Drawing.Size(90, 29);
            WebsourceServerEnable.TabIndex = 11;
            WebsourceServerEnable.Text = "Enable";
            WebsourceServerEnable.UseVisualStyleBackColor = true;
            WebsourceServerEnable.Click += WebsourceServerEnable_Click;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new System.Drawing.Point(94, 160);
            label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label27.Name = "label27";
            label27.Size = new System.Drawing.Size(134, 25);
            label27.TabIndex = 10;
            label27.Text = "Webserver Files";
            // 
            // TwitchChatSoundSelectButton
            // 
            TwitchChatSoundSelectButton.Image = Properties.Resources.fileopenicon;
            TwitchChatSoundSelectButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchChatSoundSelectButton.Location = new System.Drawing.Point(320, 153);
            TwitchChatSoundSelectButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            TwitchChatSoundSelectButton.Name = "TwitchChatSoundSelectButton";
            TwitchChatSoundSelectButton.Size = new System.Drawing.Size(41, 38);
            TwitchChatSoundSelectButton.TabIndex = 9;
            BBBToolTip.SetToolTip(TwitchChatSoundSelectButton, "Open sound directory");
            TwitchChatSoundSelectButton.UseVisualStyleBackColor = true;
            TwitchChatSoundSelectButton.Click += TwitchChatSoundSelectButton_Click;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new System.Drawing.Point(323, 27);
            label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label26.Name = "label26";
            label26.Size = new System.Drawing.Size(46, 25);
            label26.TabIndex = 6;
            label26.Text = "OBS";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new System.Drawing.Point(94, 113);
            label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label25.Name = "label25";
            label25.Size = new System.Drawing.Size(152, 25);
            label25.TabIndex = 5;
            label25.Text = "Websource server";
            // 
            // WebsourceServer
            // 
            WebsourceServer.Location = new System.Drawing.Point(320, 105);
            WebsourceServer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            WebsourceServer.Name = "WebsourceServer";
            WebsourceServer.Size = new System.Drawing.Size(238, 31);
            WebsourceServer.TabIndex = 4;
            WebsourceServer.Validating += WebsourceServer_Validating;
            // 
            // NativeSpeechPanel
            // 
            NativeSpeechPanel.Controls.Add(label22);
            NativeSpeechPanel.Controls.Add(NativeSpeechRecognitionLanguageComboBox);
            NativeSpeechPanel.Controls.Add(label21);
            NativeSpeechPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            NativeSpeechPanel.Location = new System.Drawing.Point(0, 0);
            NativeSpeechPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            NativeSpeechPanel.Name = "NativeSpeechPanel";
            NativeSpeechPanel.Size = new System.Drawing.Size(847, 965);
            NativeSpeechPanel.TabIndex = 10;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new System.Drawing.Point(43, 85);
            label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label22.Name = "label22";
            label22.Size = new System.Drawing.Size(242, 25);
            label22.TabIndex = 2;
            label22.Text = "Speech recognition language";
            BBBToolTip.SetToolTip(label22, "This list is influenced by the installed languages");
            // 
            // NativeSpeechRecognitionLanguageComboBox
            // 
            NativeSpeechRecognitionLanguageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            NativeSpeechRecognitionLanguageComboBox.FormattingEnabled = true;
            NativeSpeechRecognitionLanguageComboBox.Location = new System.Drawing.Point(294, 83);
            NativeSpeechRecognitionLanguageComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            NativeSpeechRecognitionLanguageComboBox.Name = "NativeSpeechRecognitionLanguageComboBox";
            NativeSpeechRecognitionLanguageComboBox.Size = new System.Drawing.Size(171, 33);
            NativeSpeechRecognitionLanguageComboBox.TabIndex = 1;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new System.Drawing.Point(350, 18);
            label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label21.Name = "label21";
            label21.Size = new System.Drawing.Size(124, 25);
            label21.TabIndex = 0;
            label21.Text = "Native Speech";
            // 
            // MicrophonePanel
            // 
            MicrophonePanel.Controls.Add(PTTKeyLabel);
            MicrophonePanel.Controls.Add(MicrophoneHotkeyEditbox);
            MicrophonePanel.Controls.Add(VoiceInputLabel);
            MicrophonePanel.Controls.Add(SoundInputDevices);
            MicrophonePanel.Controls.Add(MicrophoneHotkeySet);
            MicrophonePanel.Controls.Add(label1);
            MicrophonePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            MicrophonePanel.Location = new System.Drawing.Point(0, 0);
            MicrophonePanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            MicrophonePanel.Name = "MicrophonePanel";
            MicrophonePanel.Size = new System.Drawing.Size(847, 965);
            MicrophonePanel.TabIndex = 0;
            // 
            // PTTKeyLabel
            // 
            PTTKeyLabel.AutoSize = true;
            PTTKeyLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            PTTKeyLabel.Location = new System.Drawing.Point(53, 153);
            PTTKeyLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            PTTKeyLabel.Name = "PTTKeyLabel";
            PTTKeyLabel.Size = new System.Drawing.Size(170, 25);
            PTTKeyLabel.TabIndex = 25;
            PTTKeyLabel.Text = "Push-To-Talk hotkey";
            // 
            // MicrophoneHotkeyEditbox
            // 
            MicrophoneHotkeyEditbox.Location = new System.Drawing.Point(260, 148);
            MicrophoneHotkeyEditbox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            MicrophoneHotkeyEditbox.Name = "MicrophoneHotkeyEditbox";
            MicrophoneHotkeyEditbox.ReadOnly = true;
            MicrophoneHotkeyEditbox.Size = new System.Drawing.Size(313, 31);
            MicrophoneHotkeyEditbox.TabIndex = 24;
            // 
            // VoiceInputLabel
            // 
            VoiceInputLabel.AutoSize = true;
            VoiceInputLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            VoiceInputLabel.Location = new System.Drawing.Point(53, 102);
            VoiceInputLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            VoiceInputLabel.Name = "VoiceInputLabel";
            VoiceInputLabel.Size = new System.Drawing.Size(101, 25);
            VoiceInputLabel.TabIndex = 23;
            VoiceInputLabel.Text = "Voice Input";
            // 
            // SoundInputDevices
            // 
            SoundInputDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            SoundInputDevices.FormattingEnabled = true;
            SoundInputDevices.Location = new System.Drawing.Point(260, 87);
            SoundInputDevices.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            SoundInputDevices.Name = "SoundInputDevices";
            SoundInputDevices.Size = new System.Drawing.Size(425, 33);
            SoundInputDevices.TabIndex = 22;
            SoundInputDevices.SelectedIndexChanged += SoundInputDevices_SelectedIndexChanged;
            // 
            // MicrophoneHotkeySet
            // 
            MicrophoneHotkeySet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            MicrophoneHotkeySet.Location = new System.Drawing.Point(604, 143);
            MicrophoneHotkeySet.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            MicrophoneHotkeySet.Name = "MicrophoneHotkeySet";
            MicrophoneHotkeySet.Size = new System.Drawing.Size(83, 45);
            MicrophoneHotkeySet.TabIndex = 21;
            MicrophoneHotkeySet.Text = "Set";
            MicrophoneHotkeySet.UseVisualStyleBackColor = true;
            MicrophoneHotkeySet.Click += MicrophoneHotkeySet_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(323, 15);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(177, 25);
            label1.TabIndex = 0;
            label1.Text = "Microphone Settings";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1143, 965);
            Controls.Add(splitContainer1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "SettingsForm";
            Text = "Settings";
            FormClosing += SettingsForm_FormClosing;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            TwitchPanel.ResumeLayout(false);
            TwitchPanel.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            EventSubGroupbox.ResumeLayout(false);
            EventSubGroupbox.PerformLayout();
            TwitchAPITestGroupBox.ResumeLayout(false);
            TwitchAPITestGroupBox.PerformLayout();
            ElevenLabsPanel.ResumeLayout(false);
            ElevenLabsPanel.PerformLayout();
            OpenAIChatGPTPanel.ResumeLayout(false);
            OpenAIChatGPTPanel.PerformLayout();
            PersonasPanel.ResumeLayout(false);
            PersonasPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PitchTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)RateTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)VolumeTrackBar).EndInit();
            SpeakerPanel.ResumeLayout(false);
            SpeakerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SpeakerDeviceVolumeTrackBar).EndInit();
            AzurePanel.ResumeLayout(false);
            AzurePanel.PerformLayout();
            OllamaPanel.ResumeLayout(false);
            OllamaPanel.PerformLayout();
            OBSPanel.ResumeLayout(false);
            OBSPanel.PerformLayout();
            NativeSpeechPanel.ResumeLayout(false);
            NativeSpeechPanel.PerformLayout();
            MicrophonePanel.ResumeLayout(false);
            MicrophonePanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView MenuTreeView;
        private System.Windows.Forms.Panel MicrophonePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel AzurePanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel SpeakerPanel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel OpenAIChatGPTPanel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel TwitchPanel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel PersonasPanel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label PTTKeyLabel;
        private System.Windows.Forms.TextBox MicrophoneHotkeyEditbox;
        private System.Windows.Forms.Label VoiceInputLabel;
        private System.Windows.Forms.ComboBox SoundInputDevices;
        private System.Windows.Forms.Button MicrophoneHotkeySet;
        private System.Windows.Forms.ComboBox TTSAudioOutputComboBox;
        private System.Windows.Forms.Label TTSAudioOutputLabel;
        private System.Windows.Forms.TextBox PersonaRoleTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox PersonaComboBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox TTSProviderComboBox;
        private System.Windows.Forms.ComboBox TTSOutputVoiceOption1;
        private System.Windows.Forms.Label TTSOption1Label;
        private System.Windows.Forms.ComboBox TTSOutputVoice;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button DeletePersona;
        private System.Windows.Forms.Button SavePersona;
        private System.Windows.Forms.Button NewPersona;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox AzureRegionTextBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox AzureAPIKeyTextBox;
        private System.Windows.Forms.Button TestAzureAPISettings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox AzureLanguageComboBox;
        private System.Windows.Forms.Button TestVoiceButton;
        private System.Windows.Forms.Label LLMMaxTokensHelpText;
        private System.Windows.Forms.Label LLMTempHelpText;
        private System.Windows.Forms.Label LLMMaxTokenLabel;
        private System.Windows.Forms.Label LLMTempLabel;
        private System.Windows.Forms.TextBox GPTMaxTokensTextBox;
        private System.Windows.Forms.TextBox GPTTemperatureTextBox;
        private System.Windows.Forms.ComboBox GPTModelComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button GPTTestButton;
        private System.Windows.Forms.TextBox GPTAPIKeyTextBox;
        private System.Windows.Forms.Label GPTAPIKeyLabel;
        private System.Windows.Forms.GroupBox EventSubGroupbox;
        private System.Windows.Forms.CheckBox TwitchMockEventSub;
        private System.Windows.Forms.Button TwitchEventSubTestButton;
        private System.Windows.Forms.GroupBox TwitchAPITestGroupBox;
        private System.Windows.Forms.TextBox TwitchTestSendText;
        private System.Windows.Forms.CheckBox TwitchSendTextCheckBox;
        private System.Windows.Forms.Button TwitchAPITestButton;
        private System.Windows.Forms.ToolTip BBBToolTip;
        private System.Windows.Forms.Panel ElevenLabsPanel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button ElevenLabsTestButton;
        private System.Windows.Forms.TextBox ElevenlabsAPIKeyTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox TTSOutputVoiceOption3;
        private System.Windows.Forms.Label TTSOption3Label;
        private System.Windows.Forms.ComboBox TTSOutputVoiceOption2;
        private System.Windows.Forms.Label TTSOption2Label;
        private System.Windows.Forms.Panel OllamaPanel;
        private System.Windows.Forms.TextBox OllamaURITextBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label OllamaModelsTextLabel;
        private System.Windows.Forms.ComboBox OllamaModelsComboBox;
        private System.Windows.Forms.Button OllamaTestButton;
        private System.Windows.Forms.ComboBox OllamaResponseLengthComboBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel NativeSpeechPanel;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox NativeSpeechRecognitionLanguageComboBox;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox WhisperSpeechRecognitionComboBox;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.CheckBox UseOllamaLLMCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel OBSPanel;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox WebsourceServer;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button TwitchChatSoundSelectButton;
        private System.Windows.Forms.CheckBox WebsourceServerEnable;
        private System.Windows.Forms.ComboBox TwitchAuthServerConfig;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button TwitchAuthorizeButton;
        private System.Windows.Forms.TextBox TwitchBroadcasterChannel;
        private System.Windows.Forms.TextBox TwitchBroadcasterAccessToken;
        private System.Windows.Forms.Label TwitchChannelNameLabel;
        private System.Windows.Forms.Label TwitchAccesstokenLabel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button AuthorizeBotTwitch;
        private System.Windows.Forms.TextBox TwitchBotName;
        private System.Windows.Forms.TextBox TwitchBotAuthKey;
        private System.Windows.Forms.Label TwitchBotNameLabel;
        private System.Windows.Forms.Label TwitchBotAccessTokenLabel;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TrackBar RateTrackBar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar VolumeTrackBar;
        private System.Windows.Forms.Label TTSSpeedLevel;
        private System.Windows.Forms.Label TTSVoiceLevel;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TrackBar SpeakerDeviceVolumeTrackBar;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label OutputVolumeLabel;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label TTSPitchLevel;
        private System.Windows.Forms.TrackBar PitchTrackBar;
        private System.Windows.Forms.Label label31;
    }
}