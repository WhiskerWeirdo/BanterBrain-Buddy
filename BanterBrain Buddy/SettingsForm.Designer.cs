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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Microphone");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Speaker");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Sound & Voice settings", new System.Windows.Forms.TreeNode[] { treeNode1, treeNode2 });
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Azure");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("OpenAI ChatGPT");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("API Settings", new System.Windows.Forms.TreeNode[] { treeNode4, treeNode5 });
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Twitch ");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Streaming settings", new System.Windows.Forms.TreeNode[] { treeNode7 });
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Persona's");
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            MenuTreeView = new System.Windows.Forms.TreeView();
            TwitchPanel = new System.Windows.Forms.Panel();
            label5 = new System.Windows.Forms.Label();
            TwitchUsername = new System.Windows.Forms.TextBox();
            EventSubGroupbox = new System.Windows.Forms.GroupBox();
            TwitchMockEventSub = new System.Windows.Forms.CheckBox();
            TwitchEventSubTestButton = new System.Windows.Forms.Button();
            TwitchAPITestGroupBox = new System.Windows.Forms.GroupBox();
            TwitchTestSendText = new System.Windows.Forms.TextBox();
            TwitchSendTextCheckBox = new System.Windows.Forms.CheckBox();
            TwitchAPITestButton = new System.Windows.Forms.Button();
            TwitchAuthorizeButton = new System.Windows.Forms.Button();
            TwitchChannel = new System.Windows.Forms.TextBox();
            TwitchAccessToken = new System.Windows.Forms.TextBox();
            TwitchChannelNameLabel = new System.Windows.Forms.Label();
            TwitchAccesstokenLabel = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            OpenAIChatGPTPanel = new System.Windows.Forms.Panel();
            UseGPTLLMCheckBox = new System.Windows.Forms.CheckBox();
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
            TestVoiceButton = new System.Windows.Forms.Button();
            DeletePersona = new System.Windows.Forms.Button();
            SavePersona = new System.Windows.Forms.Button();
            NewPersona = new System.Windows.Forms.Button();
            TTSOutputVoiceOption1 = new System.Windows.Forms.ComboBox();
            label15 = new System.Windows.Forms.Label();
            TTSOutputVoice = new System.Windows.Forms.ComboBox();
            label16 = new System.Windows.Forms.Label();
            TTSProviderComboBox = new System.Windows.Forms.ComboBox();
            label14 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            PersonaComboBox = new System.Windows.Forms.ComboBox();
            label12 = new System.Windows.Forms.Label();
            PersonaRoleTextBox = new System.Windows.Forms.TextBox();
            label10 = new System.Windows.Forms.Label();
            AzurePanel = new System.Windows.Forms.Panel();
            label2 = new System.Windows.Forms.Label();
            AzureLanguageComboBox = new System.Windows.Forms.ComboBox();
            TestAzureAPISettings = new System.Windows.Forms.Button();
            label17 = new System.Windows.Forms.Label();
            AzureRegionTextBox = new System.Windows.Forms.TextBox();
            label18 = new System.Windows.Forms.Label();
            AzureAPIKeyTextBox = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            MicrophonePanel = new System.Windows.Forms.Panel();
            PTTKeyLabel = new System.Windows.Forms.Label();
            MicrophoneHotkeyEditbox = new System.Windows.Forms.TextBox();
            VoiceInputLabel = new System.Windows.Forms.Label();
            SoundInputDevices = new System.Windows.Forms.ComboBox();
            MicrophoneHotkeySet = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            SpeakerPanel = new System.Windows.Forms.Panel();
            TTSAudioOutputComboBox = new System.Windows.Forms.ComboBox();
            TTSAudioOutputLabel = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            TwitchPanel.SuspendLayout();
            EventSubGroupbox.SuspendLayout();
            TwitchAPITestGroupBox.SuspendLayout();
            OpenAIChatGPTPanel.SuspendLayout();
            PersonasPanel.SuspendLayout();
            AzurePanel.SuspendLayout();
            MicrophonePanel.SuspendLayout();
            SpeakerPanel.SuspendLayout();
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
            splitContainer1.Panel2.Controls.Add(OpenAIChatGPTPanel);
            splitContainer1.Panel2.Controls.Add(PersonasPanel);
            splitContainer1.Panel2.Controls.Add(AzurePanel);
            splitContainer1.Panel2.Controls.Add(MicrophonePanel);
            splitContainer1.Panel2.Controls.Add(SpeakerPanel);
            splitContainer1.Size = new System.Drawing.Size(1143, 750);
            splitContainer1.SplitterDistance = 291;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 0;
            // 
            // MenuTreeView
            // 
            MenuTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            MenuTreeView.Location = new System.Drawing.Point(0, 0);
            MenuTreeView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            MenuTreeView.Name = "MenuTreeView";
            treeNode1.Name = "Microphone";
            treeNode1.Text = "Microphone";
            treeNode2.Name = "Speaker";
            treeNode2.Text = "Speaker";
            treeNode3.Name = "VoiceSettings";
            treeNode3.Text = "Sound & Voice settings";
            treeNode4.Name = "Azure";
            treeNode4.Text = "Azure";
            treeNode5.Name = "OpenAIChatGPT";
            treeNode5.Text = "OpenAI ChatGPT";
            treeNode6.Name = "APISettings";
            treeNode6.Text = "API Settings";
            treeNode7.Name = "Twitch";
            treeNode7.Text = "Twitch ";
            treeNode8.Name = "Streaming Settings";
            treeNode8.Text = "Streaming settings";
            treeNode9.Name = "Personas";
            treeNode9.Text = "Persona's";
            MenuTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { treeNode3, treeNode6, treeNode8, treeNode9 });
            MenuTreeView.PathSeparator = "";
            MenuTreeView.Size = new System.Drawing.Size(291, 750);
            MenuTreeView.TabIndex = 0;
            MenuTreeView.AfterSelect += treeView1_AfterSelect;
            // 
            // TwitchPanel
            // 
            TwitchPanel.Controls.Add(label5);
            TwitchPanel.Controls.Add(TwitchUsername);
            TwitchPanel.Controls.Add(EventSubGroupbox);
            TwitchPanel.Controls.Add(TwitchAPITestGroupBox);
            TwitchPanel.Controls.Add(TwitchAuthorizeButton);
            TwitchPanel.Controls.Add(TwitchChannel);
            TwitchPanel.Controls.Add(TwitchAccessToken);
            TwitchPanel.Controls.Add(TwitchChannelNameLabel);
            TwitchPanel.Controls.Add(TwitchAccesstokenLabel);
            TwitchPanel.Controls.Add(label9);
            TwitchPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            TwitchPanel.Location = new System.Drawing.Point(0, 0);
            TwitchPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            TwitchPanel.Name = "TwitchPanel";
            TwitchPanel.Size = new System.Drawing.Size(846, 750);
            TwitchPanel.TabIndex = 5;
            TwitchPanel.VisibleChanged += TwitchPanel_VisibleChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label5.Location = new System.Drawing.Point(69, 92);
            label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(91, 25);
            label5.TabIndex = 34;
            label5.Text = "Username";
            // 
            // TwitchUsername
            // 
            TwitchUsername.Location = new System.Drawing.Point(206, 87);
            TwitchUsername.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TwitchUsername.Name = "TwitchUsername";
            TwitchUsername.Size = new System.Drawing.Size(270, 31);
            TwitchUsername.TabIndex = 33;
            // 
            // EventSubGroupbox
            // 
            EventSubGroupbox.Controls.Add(TwitchMockEventSub);
            EventSubGroupbox.Controls.Add(TwitchEventSubTestButton);
            EventSubGroupbox.Location = new System.Drawing.Point(74, 460);
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
            TwitchAPITestGroupBox.Location = new System.Drawing.Point(74, 297);
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
            // TwitchAuthorizeButton
            // 
            TwitchAuthorizeButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchAuthorizeButton.Location = new System.Drawing.Point(74, 242);
            TwitchAuthorizeButton.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TwitchAuthorizeButton.Name = "TwitchAuthorizeButton";
            TwitchAuthorizeButton.Size = new System.Drawing.Size(174, 45);
            TwitchAuthorizeButton.TabIndex = 30;
            TwitchAuthorizeButton.Text = "Authorize to Twitch";
            TwitchAuthorizeButton.UseVisualStyleBackColor = true;
            TwitchAuthorizeButton.Click += TwitchAuthorizeButton_Click;
            // 
            // TwitchChannel
            // 
            TwitchChannel.Location = new System.Drawing.Point(206, 190);
            TwitchChannel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TwitchChannel.Name = "TwitchChannel";
            TwitchChannel.Size = new System.Drawing.Size(270, 31);
            TwitchChannel.TabIndex = 29;
            // 
            // TwitchAccessToken
            // 
            TwitchAccessToken.Location = new System.Drawing.Point(206, 140);
            TwitchAccessToken.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TwitchAccessToken.Name = "TwitchAccessToken";
            TwitchAccessToken.PasswordChar = '*';
            TwitchAccessToken.Size = new System.Drawing.Size(270, 31);
            TwitchAccessToken.TabIndex = 28;
            // 
            // TwitchChannelNameLabel
            // 
            TwitchChannelNameLabel.AutoSize = true;
            TwitchChannelNameLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchChannelNameLabel.Location = new System.Drawing.Point(69, 203);
            TwitchChannelNameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            TwitchChannelNameLabel.Name = "TwitchChannelNameLabel";
            TwitchChannelNameLabel.Size = new System.Drawing.Size(75, 25);
            TwitchChannelNameLabel.TabIndex = 27;
            TwitchChannelNameLabel.Text = "Channel";
            // 
            // TwitchAccesstokenLabel
            // 
            TwitchAccesstokenLabel.AutoSize = true;
            TwitchAccesstokenLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchAccesstokenLabel.Location = new System.Drawing.Point(69, 147);
            TwitchAccesstokenLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            TwitchAccesstokenLabel.Name = "TwitchAccesstokenLabel";
            TwitchAccesstokenLabel.Size = new System.Drawing.Size(116, 25);
            TwitchAccesstokenLabel.TabIndex = 26;
            TwitchAccesstokenLabel.Text = "Access Token";
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
            // OpenAIChatGPTPanel
            // 
            OpenAIChatGPTPanel.Controls.Add(UseGPTLLMCheckBox);
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
            OpenAIChatGPTPanel.Size = new System.Drawing.Size(846, 750);
            OpenAIChatGPTPanel.TabIndex = 3;
            // 
            // UseGPTLLMCheckBox
            // 
            UseGPTLLMCheckBox.AutoSize = true;
            UseGPTLLMCheckBox.Location = new System.Drawing.Point(53, 18);
            UseGPTLLMCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            UseGPTLLMCheckBox.Name = "UseGPTLLMCheckBox";
            UseGPTLLMCheckBox.Size = new System.Drawing.Size(139, 29);
            UseGPTLLMCheckBox.TabIndex = 39;
            UseGPTLLMCheckBox.Text = "Use ChatGPT";
            UseGPTLLMCheckBox.UseVisualStyleBackColor = true;
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
            // 
            // GPTTemperatureTextBox
            // 
            GPTTemperatureTextBox.Location = new System.Drawing.Point(261, 227);
            GPTTemperatureTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            GPTTemperatureTextBox.Name = "GPTTemperatureTextBox";
            GPTTemperatureTextBox.Size = new System.Drawing.Size(141, 31);
            GPTTemperatureTextBox.TabIndex = 33;
            GPTTemperatureTextBox.Text = "0";
            // 
            // GPTModelComboBox
            // 
            GPTModelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            GPTModelComboBox.FormattingEnabled = true;
            GPTModelComboBox.Items.AddRange(new object[] { "gpt-3.5-turbo" });
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
            label4.Size = new System.Drawing.Size(63, 25);
            label4.TabIndex = 31;
            label4.Text = "Model";
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
            label7.Size = new System.Drawing.Size(145, 25);
            label7.TabIndex = 0;
            label7.Text = "OpenAI ChatGPT";
            // 
            // PersonasPanel
            // 
            PersonasPanel.Controls.Add(TestVoiceButton);
            PersonasPanel.Controls.Add(DeletePersona);
            PersonasPanel.Controls.Add(SavePersona);
            PersonasPanel.Controls.Add(NewPersona);
            PersonasPanel.Controls.Add(TTSOutputVoiceOption1);
            PersonasPanel.Controls.Add(label15);
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
            PersonasPanel.Size = new System.Drawing.Size(846, 750);
            PersonasPanel.TabIndex = 6;
            PersonasPanel.VisibleChanged += PersonasPanel_VisibleChanged;
            // 
            // TestVoiceButton
            // 
            TestVoiceButton.Location = new System.Drawing.Point(529, 553);
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
            DeletePersona.Enabled = false;
            DeletePersona.Location = new System.Drawing.Point(590, 88);
            DeletePersona.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            DeletePersona.Name = "DeletePersona";
            DeletePersona.Size = new System.Drawing.Size(107, 38);
            DeletePersona.TabIndex = 31;
            DeletePersona.Text = "Delete";
            DeletePersona.UseVisualStyleBackColor = true;
            // 
            // SavePersona
            // 
            SavePersona.Location = new System.Drawing.Point(474, 87);
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
            NewPersona.Location = new System.Drawing.Point(359, 87);
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
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label15.Location = new System.Drawing.Point(19, 502);
            label15.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(225, 25);
            label15.TabIndex = 27;
            label15.Text = "TTS Output Voice Option 1";
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
            TTSOutputVoice.SelectedIndexChanged += TTSOutputVoice_SelectedIndexChanged;
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
            TTSProviderComboBox.Items.AddRange(new object[] { "Native", "Azure" });
            TTSProviderComboBox.Location = new System.Drawing.Point(260, 382);
            TTSProviderComboBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            TTSProviderComboBox.Name = "TTSProviderComboBox";
            TTSProviderComboBox.Size = new System.Drawing.Size(400, 33);
            TTSProviderComboBox.TabIndex = 24;
            TTSProviderComboBox.SelectedIndexChanged += TTSProviderComboBox_SelectedIndexChanged;
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
            PersonaComboBox.Size = new System.Drawing.Size(171, 33);
            PersonaComboBox.TabIndex = 21;
            PersonaComboBox.SelectedIndexChanged += PersonaComboBox_SelectedIndexChanged;
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
            AzurePanel.Size = new System.Drawing.Size(846, 750);
            AzurePanel.TabIndex = 1;
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
            AzureLanguageComboBox.Items.AddRange(new object[] { "en-US" });
            AzureLanguageComboBox.Location = new System.Drawing.Point(153, 165);
            AzureLanguageComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            AzureLanguageComboBox.Name = "AzureLanguageComboBox";
            AzureLanguageComboBox.Size = new System.Drawing.Size(287, 33);
            AzureLanguageComboBox.TabIndex = 23;
            // 
            // TestAzureAPISettings
            // 
            TestAzureAPISettings.Location = new System.Drawing.Point(334, 230);
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
            MicrophonePanel.Size = new System.Drawing.Size(846, 750);
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
            MicrophoneHotkeySet.Enabled = false;
            MicrophoneHotkeySet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            MicrophoneHotkeySet.Location = new System.Drawing.Point(604, 143);
            MicrophoneHotkeySet.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            MicrophoneHotkeySet.Name = "MicrophoneHotkeySet";
            MicrophoneHotkeySet.Size = new System.Drawing.Size(83, 45);
            MicrophoneHotkeySet.TabIndex = 21;
            MicrophoneHotkeySet.Text = "Set";
            MicrophoneHotkeySet.UseVisualStyleBackColor = true;
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
            // SpeakerPanel
            // 
            SpeakerPanel.Controls.Add(TTSAudioOutputComboBox);
            SpeakerPanel.Controls.Add(TTSAudioOutputLabel);
            SpeakerPanel.Controls.Add(label6);
            SpeakerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            SpeakerPanel.Location = new System.Drawing.Point(0, 0);
            SpeakerPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            SpeakerPanel.Name = "SpeakerPanel";
            SpeakerPanel.Size = new System.Drawing.Size(846, 750);
            SpeakerPanel.TabIndex = 2;
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
            // SettingsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1143, 750);
            Controls.Add(splitContainer1);
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
            EventSubGroupbox.ResumeLayout(false);
            EventSubGroupbox.PerformLayout();
            TwitchAPITestGroupBox.ResumeLayout(false);
            TwitchAPITestGroupBox.PerformLayout();
            OpenAIChatGPTPanel.ResumeLayout(false);
            OpenAIChatGPTPanel.PerformLayout();
            PersonasPanel.ResumeLayout(false);
            PersonasPanel.PerformLayout();
            AzurePanel.ResumeLayout(false);
            AzurePanel.PerformLayout();
            MicrophonePanel.ResumeLayout(false);
            MicrophonePanel.PerformLayout();
            SpeakerPanel.ResumeLayout(false);
            SpeakerPanel.PerformLayout();
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
        private System.Windows.Forms.Label label15;
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
        private System.Windows.Forms.CheckBox UseGPTLLMCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TwitchUsername;
        private System.Windows.Forms.GroupBox EventSubGroupbox;
        private System.Windows.Forms.CheckBox TwitchMockEventSub;
        private System.Windows.Forms.Button TwitchEventSubTestButton;
        private System.Windows.Forms.GroupBox TwitchAPITestGroupBox;
        private System.Windows.Forms.TextBox TwitchTestSendText;
        private System.Windows.Forms.CheckBox TwitchSendTextCheckBox;
        private System.Windows.Forms.Button TwitchAPITestButton;
        private System.Windows.Forms.Button TwitchAuthorizeButton;
        private System.Windows.Forms.TextBox TwitchChannel;
        private System.Windows.Forms.TextBox TwitchAccessToken;
        private System.Windows.Forms.Label TwitchChannelNameLabel;
        private System.Windows.Forms.Label TwitchAccesstokenLabel;
    }
}