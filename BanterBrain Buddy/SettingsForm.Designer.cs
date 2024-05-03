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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Microphone");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Speaker");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Sound & Voice settings", new System.Windows.Forms.TreeNode[] { treeNode1, treeNode2 });
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Azure");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("OpenAI");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("ElevenLabs");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Ollama LLM");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("API Settings", new System.Windows.Forms.TreeNode[] { treeNode4, treeNode5, treeNode6, treeNode7 });
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Twitch ");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Streaming settings", new System.Windows.Forms.TreeNode[] { treeNode9 });
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Persona's");
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            MenuTreeView = new System.Windows.Forms.TreeView();
            OllamaPanel = new System.Windows.Forms.Panel();
            OllamaTestButton = new System.Windows.Forms.Button();
            OllamaModelsTextLabel = new System.Windows.Forms.Label();
            OllamaModelsComboBox = new System.Windows.Forms.ComboBox();
            UseOllamaLLMCheckBox = new System.Windows.Forms.CheckBox();
            OllamaURITextBox = new System.Windows.Forms.TextBox();
            label19 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            ElevenLabsPanel = new System.Windows.Forms.Panel();
            ElevenLabsTestButton = new System.Windows.Forms.Button();
            ElevenlabsAPIKeyTextBox = new System.Windows.Forms.TextBox();
            label11 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
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
            MicrophonePanel = new System.Windows.Forms.Panel();
            PTTKeyLabel = new System.Windows.Forms.Label();
            MicrophoneHotkeyEditbox = new System.Windows.Forms.TextBox();
            VoiceInputLabel = new System.Windows.Forms.Label();
            SoundInputDevices = new System.Windows.Forms.ComboBox();
            MicrophoneHotkeySet = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            AzurePanel = new System.Windows.Forms.Panel();
            label2 = new System.Windows.Forms.Label();
            AzureLanguageComboBox = new System.Windows.Forms.ComboBox();
            TestAzureAPISettings = new System.Windows.Forms.Button();
            label17 = new System.Windows.Forms.Label();
            AzureRegionTextBox = new System.Windows.Forms.TextBox();
            label18 = new System.Windows.Forms.Label();
            AzureAPIKeyTextBox = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            SpeakerPanel = new System.Windows.Forms.Panel();
            TTSAudioOutputComboBox = new System.Windows.Forms.ComboBox();
            TTSAudioOutputLabel = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            BBBToolTip = new System.Windows.Forms.ToolTip(components);
            label20 = new System.Windows.Forms.Label();
            OllamaResponseLengthComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            OllamaPanel.SuspendLayout();
            ElevenLabsPanel.SuspendLayout();
            OpenAIChatGPTPanel.SuspendLayout();
            PersonasPanel.SuspendLayout();
            TwitchPanel.SuspendLayout();
            EventSubGroupbox.SuspendLayout();
            TwitchAPITestGroupBox.SuspendLayout();
            MicrophonePanel.SuspendLayout();
            AzurePanel.SuspendLayout();
            SpeakerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(MenuTreeView);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(OllamaPanel);
            splitContainer1.Panel2.Controls.Add(ElevenLabsPanel);
            splitContainer1.Panel2.Controls.Add(OpenAIChatGPTPanel);
            splitContainer1.Panel2.Controls.Add(PersonasPanel);
            splitContainer1.Panel2.Controls.Add(TwitchPanel);
            splitContainer1.Panel2.Controls.Add(MicrophonePanel);
            splitContainer1.Panel2.Controls.Add(AzurePanel);
            splitContainer1.Panel2.Controls.Add(SpeakerPanel);
            splitContainer1.Size = new System.Drawing.Size(800, 450);
            splitContainer1.SplitterDistance = 203;
            splitContainer1.TabIndex = 0;
            // 
            // MenuTreeView
            // 
            MenuTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            MenuTreeView.Location = new System.Drawing.Point(0, 0);
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
            treeNode5.Text = "OpenAI";
            treeNode6.Name = "ElevenLabs";
            treeNode6.Text = "ElevenLabs";
            treeNode7.Name = "OllamaLLM";
            treeNode7.Text = "Ollama LLM";
            treeNode8.Name = "APISettings";
            treeNode8.Text = "API Settings";
            treeNode9.Name = "Twitch";
            treeNode9.Text = "Twitch ";
            treeNode10.Name = "Streaming Settings";
            treeNode10.Text = "Streaming settings";
            treeNode11.Name = "Personas";
            treeNode11.Text = "Persona's";
            MenuTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { treeNode3, treeNode8, treeNode10, treeNode11 });
            MenuTreeView.PathSeparator = "";
            MenuTreeView.Size = new System.Drawing.Size(203, 450);
            MenuTreeView.TabIndex = 0;
            MenuTreeView.BeforeSelect += MenuTreeView_BeforeSelect;
            MenuTreeView.AfterSelect += treeView1_AfterSelect;
            // 
            // OllamaPanel
            // 
            OllamaPanel.Controls.Add(OllamaResponseLengthComboBox);
            OllamaPanel.Controls.Add(label20);
            OllamaPanel.Controls.Add(OllamaTestButton);
            OllamaPanel.Controls.Add(OllamaModelsTextLabel);
            OllamaPanel.Controls.Add(OllamaModelsComboBox);
            OllamaPanel.Controls.Add(UseOllamaLLMCheckBox);
            OllamaPanel.Controls.Add(OllamaURITextBox);
            OllamaPanel.Controls.Add(label19);
            OllamaPanel.Controls.Add(label15);
            OllamaPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            OllamaPanel.Location = new System.Drawing.Point(0, 0);
            OllamaPanel.Name = "OllamaPanel";
            OllamaPanel.Size = new System.Drawing.Size(593, 450);
            OllamaPanel.TabIndex = 37;
            OllamaPanel.VisibleChanged += OllamaPanel_VisibleChanged;
            // 
            // OllamaTestButton
            // 
            OllamaTestButton.Location = new System.Drawing.Point(456, 63);
            OllamaTestButton.Name = "OllamaTestButton";
            OllamaTestButton.Size = new System.Drawing.Size(75, 23);
            OllamaTestButton.TabIndex = 7;
            OllamaTestButton.Text = "Test";
            OllamaTestButton.UseVisualStyleBackColor = true;
            OllamaTestButton.Click += OllamaTestButton_Click;
            // 
            // OllamaModelsTextLabel
            // 
            OllamaModelsTextLabel.AutoSize = true;
            OllamaModelsTextLabel.Location = new System.Drawing.Point(62, 104);
            OllamaModelsTextLabel.Name = "OllamaModelsTextLabel";
            OllamaModelsTextLabel.Size = new System.Drawing.Size(46, 15);
            OllamaModelsTextLabel.TabIndex = 5;
            OllamaModelsTextLabel.Text = "Models";
            // 
            // OllamaModelsComboBox
            // 
            OllamaModelsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            OllamaModelsComboBox.FormattingEnabled = true;
            OllamaModelsComboBox.Location = new System.Drawing.Point(202, 100);
            OllamaModelsComboBox.Name = "OllamaModelsComboBox";
            OllamaModelsComboBox.Size = new System.Drawing.Size(205, 23);
            OllamaModelsComboBox.TabIndex = 4;
            // 
            // UseOllamaLLMCheckBox
            // 
            UseOllamaLLMCheckBox.AutoSize = true;
            UseOllamaLLMCheckBox.Location = new System.Drawing.Point(59, 15);
            UseOllamaLLMCheckBox.Name = "UseOllamaLLMCheckBox";
            UseOllamaLLMCheckBox.Size = new System.Drawing.Size(86, 19);
            UseOllamaLLMCheckBox.TabIndex = 3;
            UseOllamaLLMCheckBox.Text = "Use Ollama";
            UseOllamaLLMCheckBox.UseVisualStyleBackColor = true;
            UseOllamaLLMCheckBox.CheckedChanged += UseOllamaLLMCheckBox_CheckedChanged;
            // 
            // OllamaURITextBox
            // 
            OllamaURITextBox.Location = new System.Drawing.Point(200, 61);
            OllamaURITextBox.Name = "OllamaURITextBox";
            OllamaURITextBox.Size = new System.Drawing.Size(207, 23);
            OllamaURITextBox.TabIndex = 2;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new System.Drawing.Point(59, 66);
            label19.Name = "label19";
            label19.Size = new System.Drawing.Size(66, 15);
            label19.TabIndex = 1;
            label19.Text = "Ollama URI";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(268, 12);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(45, 15);
            label15.TabIndex = 0;
            label15.Text = "Ollama";
            // 
            // ElevenLabsPanel
            // 
            ElevenLabsPanel.Controls.Add(ElevenLabsTestButton);
            ElevenLabsPanel.Controls.Add(ElevenlabsAPIKeyTextBox);
            ElevenLabsPanel.Controls.Add(label11);
            ElevenLabsPanel.Controls.Add(label8);
            ElevenLabsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            ElevenLabsPanel.Location = new System.Drawing.Point(0, 0);
            ElevenLabsPanel.Name = "ElevenLabsPanel";
            ElevenLabsPanel.Size = new System.Drawing.Size(593, 450);
            ElevenLabsPanel.TabIndex = 40;
            // 
            // ElevenLabsTestButton
            // 
            ElevenLabsTestButton.Location = new System.Drawing.Point(434, 39);
            ElevenLabsTestButton.Name = "ElevenLabsTestButton";
            ElevenLabsTestButton.Size = new System.Drawing.Size(75, 23);
            ElevenLabsTestButton.TabIndex = 3;
            ElevenLabsTestButton.Text = "Test";
            ElevenLabsTestButton.UseVisualStyleBackColor = true;
            ElevenLabsTestButton.Click += ElevenLabsTestButton_Click;
            // 
            // ElevenlabsAPIKeyTextBox
            // 
            ElevenlabsAPIKeyTextBox.Location = new System.Drawing.Point(182, 39);
            ElevenlabsAPIKeyTextBox.Name = "ElevenlabsAPIKeyTextBox";
            ElevenlabsAPIKeyTextBox.PasswordChar = '*';
            ElevenlabsAPIKeyTextBox.Size = new System.Drawing.Size(225, 23);
            ElevenlabsAPIKeyTextBox.TabIndex = 2;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(41, 45);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(47, 15);
            label11.TabIndex = 1;
            label11.Text = "API Key";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(179, 10);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(65, 15);
            label8.TabIndex = 0;
            label8.Text = "ElevenLabs";
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
            OpenAIChatGPTPanel.Name = "OpenAIChatGPTPanel";
            OpenAIChatGPTPanel.Size = new System.Drawing.Size(593, 450);
            OpenAIChatGPTPanel.TabIndex = 3;
            // 
            // UseGPTLLMCheckBox
            // 
            UseGPTLLMCheckBox.AutoSize = true;
            UseGPTLLMCheckBox.Location = new System.Drawing.Point(37, 11);
            UseGPTLLMCheckBox.Name = "UseGPTLLMCheckBox";
            UseGPTLLMCheckBox.Size = new System.Drawing.Size(94, 19);
            UseGPTLLMCheckBox.TabIndex = 39;
            UseGPTLLMCheckBox.Text = "Use ChatGPT";
            UseGPTLLMCheckBox.UseVisualStyleBackColor = true;
            // 
            // LLMMaxTokensHelpText
            // 
            LLMMaxTokensHelpText.AutoSize = true;
            LLMMaxTokensHelpText.BackColor = System.Drawing.Color.Gold;
            LLMMaxTokensHelpText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            LLMMaxTokensHelpText.Location = new System.Drawing.Point(387, 116);
            LLMMaxTokensHelpText.Name = "LLMMaxTokensHelpText";
            LLMMaxTokensHelpText.Size = new System.Drawing.Size(20, 15);
            LLMMaxTokensHelpText.TabIndex = 38;
            LLMMaxTokensHelpText.Text = "[?]";
            BBBToolTip.SetToolTip(LLMMaxTokensHelpText, "Default: 100. Max: 4096. Tokens are aproximately the amount of words. More tokens means longer words, but might also cost more");
            // 
            // LLMTempHelpText
            // 
            LLMTempHelpText.AutoSize = true;
            LLMTempHelpText.BackColor = System.Drawing.Color.Gold;
            LLMTempHelpText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            LLMTempHelpText.Location = new System.Drawing.Point(262, 118);
            LLMTempHelpText.Name = "LLMTempHelpText";
            LLMTempHelpText.Size = new System.Drawing.Size(20, 15);
            LLMTempHelpText.TabIndex = 37;
            LLMTempHelpText.Text = "[?]";
            BBBToolTip.SetToolTip(LLMTempHelpText, "Default: 0. Suggested: 0,9. Max: 2. The higher the temperature the more likely the answer uses more diverse words, but also is more likely to make mistakes");
            // 
            // LLMMaxTokenLabel
            // 
            LLMMaxTokenLabel.AutoSize = true;
            LLMMaxTokenLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            LLMMaxTokenLabel.Location = new System.Drawing.Point(307, 116);
            LLMMaxTokenLabel.Name = "LLMMaxTokenLabel";
            LLMMaxTokenLabel.Size = new System.Drawing.Size(68, 15);
            LLMMaxTokenLabel.TabIndex = 36;
            LLMMaxTokenLabel.Text = "Max tokens";
            // 
            // LLMTempLabel
            // 
            LLMTempLabel.AutoSize = true;
            LLMTempLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            LLMTempLabel.Location = new System.Drawing.Point(183, 118);
            LLMTempLabel.Name = "LLMTempLabel";
            LLMTempLabel.Size = new System.Drawing.Size(73, 15);
            LLMTempLabel.TabIndex = 35;
            LLMTempLabel.Text = "Temperature";
            // 
            // GPTMaxTokensTextBox
            // 
            GPTMaxTokensTextBox.Location = new System.Drawing.Point(307, 136);
            GPTMaxTokensTextBox.Name = "GPTMaxTokensTextBox";
            GPTMaxTokensTextBox.Size = new System.Drawing.Size(100, 23);
            GPTMaxTokensTextBox.TabIndex = 34;
            GPTMaxTokensTextBox.Text = "100";
            // 
            // GPTTemperatureTextBox
            // 
            GPTTemperatureTextBox.Location = new System.Drawing.Point(183, 136);
            GPTTemperatureTextBox.Name = "GPTTemperatureTextBox";
            GPTTemperatureTextBox.Size = new System.Drawing.Size(100, 23);
            GPTTemperatureTextBox.TabIndex = 33;
            GPTTemperatureTextBox.Text = "0";
            BBBToolTip.SetToolTip(GPTTemperatureTextBox, "Default: 0. Maximum 2");
            // 
            // GPTModelComboBox
            // 
            GPTModelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            GPTModelComboBox.FormattingEnabled = true;
            GPTModelComboBox.Items.AddRange(new object[] { "gpt-3.5-turbo" });
            GPTModelComboBox.Location = new System.Drawing.Point(183, 82);
            GPTModelComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GPTModelComboBox.Name = "GPTModelComboBox";
            GPTModelComboBox.Size = new System.Drawing.Size(224, 23);
            GPTModelComboBox.TabIndex = 32;
            GPTModelComboBox.UseWaitCursor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label4.Location = new System.Drawing.Point(37, 82);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(65, 15);
            label4.TabIndex = 31;
            label4.Text = "GPT Model";
            // 
            // GPTTestButton
            // 
            GPTTestButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            GPTTestButton.Location = new System.Drawing.Point(437, 47);
            GPTTestButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GPTTestButton.Name = "GPTTestButton";
            GPTTestButton.Size = new System.Drawing.Size(88, 27);
            GPTTestButton.TabIndex = 29;
            GPTTestButton.Text = "Test";
            GPTTestButton.UseVisualStyleBackColor = true;
            GPTTestButton.Click += GPTTestButton_Click;
            // 
            // GPTAPIKeyTextBox
            // 
            GPTAPIKeyTextBox.Location = new System.Drawing.Point(183, 47);
            GPTAPIKeyTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GPTAPIKeyTextBox.Name = "GPTAPIKeyTextBox";
            GPTAPIKeyTextBox.PasswordChar = '*';
            GPTAPIKeyTextBox.Size = new System.Drawing.Size(224, 23);
            GPTAPIKeyTextBox.TabIndex = 28;
            // 
            // GPTAPIKeyLabel
            // 
            GPTAPIKeyLabel.AutoSize = true;
            GPTAPIKeyLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            GPTAPIKeyLabel.Location = new System.Drawing.Point(37, 51);
            GPTAPIKeyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            GPTAPIKeyLabel.Name = "GPTAPIKeyLabel";
            GPTAPIKeyLabel.Size = new System.Drawing.Size(47, 15);
            GPTAPIKeyLabel.TabIndex = 27;
            GPTAPIKeyLabel.Text = "API Key";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(213, 9);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(47, 15);
            label7.TabIndex = 0;
            label7.Text = "OpenAI";
            // 
            // PersonasPanel
            // 
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
            PersonasPanel.Name = "PersonasPanel";
            PersonasPanel.Size = new System.Drawing.Size(593, 450);
            PersonasPanel.TabIndex = 6;
            PersonasPanel.VisibleChanged += PersonasPanel_VisibleChanged;
            // 
            // TTSOutputVoiceOption3
            // 
            TTSOutputVoiceOption3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TTSOutputVoiceOption3.FormattingEnabled = true;
            TTSOutputVoiceOption3.Location = new System.Drawing.Point(183, 354);
            TTSOutputVoiceOption3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TTSOutputVoiceOption3.Name = "TTSOutputVoiceOption3";
            TTSOutputVoiceOption3.Size = new System.Drawing.Size(281, 23);
            TTSOutputVoiceOption3.TabIndex = 36;
            TTSOutputVoiceOption3.TextChanged += TTSOutputVoiceOption3_TextChanged;
            // 
            // TTSOption3Label
            // 
            TTSOption3Label.AutoSize = true;
            TTSOption3Label.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TTSOption3Label.Location = new System.Drawing.Point(15, 357);
            TTSOption3Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            TTSOption3Label.Name = "TTSOption3Label";
            TTSOption3Label.Size = new System.Drawing.Size(146, 15);
            TTSOption3Label.TabIndex = 35;
            TTSOption3Label.Text = "TTS Output Voice Option 3";
            // 
            // TTSOutputVoiceOption2
            // 
            TTSOutputVoiceOption2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TTSOutputVoiceOption2.FormattingEnabled = true;
            TTSOutputVoiceOption2.Location = new System.Drawing.Point(183, 325);
            TTSOutputVoiceOption2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TTSOutputVoiceOption2.Name = "TTSOutputVoiceOption2";
            TTSOutputVoiceOption2.Size = new System.Drawing.Size(281, 23);
            TTSOutputVoiceOption2.TabIndex = 34;
            TTSOutputVoiceOption2.TextChanged += TTSOutputVoiceOption2_TextChanged;
            // 
            // TTSOption2Label
            // 
            TTSOption2Label.AutoSize = true;
            TTSOption2Label.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TTSOption2Label.Location = new System.Drawing.Point(13, 328);
            TTSOption2Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            TTSOption2Label.Name = "TTSOption2Label";
            TTSOption2Label.Size = new System.Drawing.Size(146, 15);
            TTSOption2Label.TabIndex = 33;
            TTSOption2Label.Text = "TTS Output Voice Option 2";
            // 
            // TestVoiceButton
            // 
            TestVoiceButton.Location = new System.Drawing.Point(488, 229);
            TestVoiceButton.Name = "TestVoiceButton";
            TestVoiceButton.Size = new System.Drawing.Size(75, 23);
            TestVoiceButton.TabIndex = 32;
            TestVoiceButton.Text = "Test voice";
            TestVoiceButton.UseVisualStyleBackColor = true;
            TestVoiceButton.Click += TestVoiceButton_Click;
            // 
            // DeletePersona
            // 
            DeletePersona.Location = new System.Drawing.Point(506, 52);
            DeletePersona.Name = "DeletePersona";
            DeletePersona.Size = new System.Drawing.Size(75, 23);
            DeletePersona.TabIndex = 31;
            DeletePersona.Text = "Delete";
            DeletePersona.UseVisualStyleBackColor = true;
            DeletePersona.Click += DeletePersona_Click;
            // 
            // SavePersona
            // 
            SavePersona.Enabled = false;
            SavePersona.Location = new System.Drawing.Point(426, 51);
            SavePersona.Name = "SavePersona";
            SavePersona.Size = new System.Drawing.Size(75, 23);
            SavePersona.TabIndex = 30;
            SavePersona.Text = "Save";
            SavePersona.UseVisualStyleBackColor = true;
            SavePersona.Click += SavePersona_Click;
            // 
            // NewPersona
            // 
            NewPersona.Location = new System.Drawing.Point(345, 51);
            NewPersona.Name = "NewPersona";
            NewPersona.Size = new System.Drawing.Size(75, 23);
            NewPersona.TabIndex = 29;
            NewPersona.Text = "New";
            NewPersona.UseVisualStyleBackColor = true;
            NewPersona.Click += NewPersona_Click;
            // 
            // TTSOutputVoiceOption1
            // 
            TTSOutputVoiceOption1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TTSOutputVoiceOption1.FormattingEnabled = true;
            TTSOutputVoiceOption1.Location = new System.Drawing.Point(182, 296);
            TTSOutputVoiceOption1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TTSOutputVoiceOption1.Name = "TTSOutputVoiceOption1";
            TTSOutputVoiceOption1.Size = new System.Drawing.Size(281, 23);
            TTSOutputVoiceOption1.TabIndex = 28;
            TTSOutputVoiceOption1.SelectedIndexChanged += TTSOutputVoiceOption1_SelectedIndexChanged;
            TTSOutputVoiceOption1.TextChanged += TTSOutputVoiceOption1_TextChanged;
            // 
            // TTSOption1Label
            // 
            TTSOption1Label.AutoSize = true;
            TTSOption1Label.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TTSOption1Label.Location = new System.Drawing.Point(13, 301);
            TTSOption1Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            TTSOption1Label.Name = "TTSOption1Label";
            TTSOption1Label.Size = new System.Drawing.Size(146, 15);
            TTSOption1Label.TabIndex = 27;
            TTSOption1Label.Text = "TTS Output Voice Option 1";
            // 
            // TTSOutputVoice
            // 
            TTSOutputVoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TTSOutputVoice.FormattingEnabled = true;
            TTSOutputVoice.Location = new System.Drawing.Point(182, 262);
            TTSOutputVoice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TTSOutputVoice.Name = "TTSOutputVoice";
            TTSOutputVoice.Size = new System.Drawing.Size(281, 23);
            TTSOutputVoice.TabIndex = 26;
            TTSOutputVoice.SelectedValueChanged += TTSOutputVoice_SelectedValueChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label16.Location = new System.Drawing.Point(13, 270);
            label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(97, 15);
            label16.TabIndex = 25;
            label16.Text = "TTS Output Voice";
            // 
            // TTSProviderComboBox
            // 
            TTSProviderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TTSProviderComboBox.FormattingEnabled = true;
            TTSProviderComboBox.Location = new System.Drawing.Point(182, 229);
            TTSProviderComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TTSProviderComboBox.Name = "TTSProviderComboBox";
            TTSProviderComboBox.Size = new System.Drawing.Size(281, 23);
            TTSProviderComboBox.TabIndex = 24;
            TTSProviderComboBox.SelectedValueChanged += TTSProviderComboBox_SelectedValueChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(15, 232);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(72, 15);
            label14.TabIndex = 23;
            label14.Text = "TTS Provider";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(15, 55);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(82, 15);
            label13.TabIndex = 22;
            label13.Text = "Persona name";
            // 
            // PersonaComboBox
            // 
            PersonaComboBox.FormattingEnabled = true;
            PersonaComboBox.Location = new System.Drawing.Point(110, 52);
            PersonaComboBox.Name = "PersonaComboBox";
            PersonaComboBox.Size = new System.Drawing.Size(231, 23);
            PersonaComboBox.TabIndex = 21;
            PersonaComboBox.SelectedValueChanged += PersonaComboBox_SelectedValueChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label12.Location = new System.Drawing.Point(15, 107);
            label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(53, 15);
            label12.TabIndex = 20;
            label12.Text = "Role text";
            // 
            // PersonaRoleTextBox
            // 
            PersonaRoleTextBox.Location = new System.Drawing.Point(110, 92);
            PersonaRoleTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            PersonaRoleTextBox.Multiline = true;
            PersonaRoleTextBox.Name = "PersonaRoleTextBox";
            PersonaRoleTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            PersonaRoleTextBox.Size = new System.Drawing.Size(472, 116);
            PersonaRoleTextBox.TabIndex = 19;
            PersonaRoleTextBox.TabStop = false;
            PersonaRoleTextBox.Text = "placeholder";
            PersonaRoleTextBox.TextChanged += PersonaRoleTextBox_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(234, 9);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(54, 15);
            label10.TabIndex = 0;
            label10.Text = "Personas";
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
            TwitchPanel.Name = "TwitchPanel";
            TwitchPanel.Size = new System.Drawing.Size(593, 450);
            TwitchPanel.TabIndex = 5;
            TwitchPanel.VisibleChanged += TwitchPanel_VisibleChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label5.Location = new System.Drawing.Point(48, 55);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(60, 15);
            label5.TabIndex = 34;
            label5.Text = "Username";
            // 
            // TwitchUsername
            // 
            TwitchUsername.Location = new System.Drawing.Point(144, 52);
            TwitchUsername.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TwitchUsername.Name = "TwitchUsername";
            TwitchUsername.Size = new System.Drawing.Size(190, 23);
            TwitchUsername.TabIndex = 33;
            // 
            // EventSubGroupbox
            // 
            EventSubGroupbox.Controls.Add(TwitchMockEventSub);
            EventSubGroupbox.Controls.Add(TwitchEventSubTestButton);
            EventSubGroupbox.Location = new System.Drawing.Point(52, 276);
            EventSubGroupbox.Name = "EventSubGroupbox";
            EventSubGroupbox.Size = new System.Drawing.Size(344, 56);
            EventSubGroupbox.TabIndex = 32;
            EventSubGroupbox.TabStop = false;
            EventSubGroupbox.Text = "EventSub Test";
            // 
            // TwitchMockEventSub
            // 
            TwitchMockEventSub.AutoSize = true;
            TwitchMockEventSub.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchMockEventSub.Location = new System.Drawing.Point(213, 22);
            TwitchMockEventSub.Name = "TwitchMockEventSub";
            TwitchMockEventSub.Size = new System.Drawing.Size(113, 19);
            TwitchMockEventSub.TabIndex = 2;
            TwitchMockEventSub.Text = "MOCK EventSub";
            TwitchMockEventSub.UseVisualStyleBackColor = true;
            // 
            // TwitchEventSubTestButton
            // 
            TwitchEventSubTestButton.Enabled = false;
            TwitchEventSubTestButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchEventSubTestButton.Location = new System.Drawing.Point(6, 22);
            TwitchEventSubTestButton.Name = "TwitchEventSubTestButton";
            TwitchEventSubTestButton.Size = new System.Drawing.Size(87, 23);
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
            TwitchAPITestGroupBox.Location = new System.Drawing.Point(52, 178);
            TwitchAPITestGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TwitchAPITestGroupBox.Name = "TwitchAPITestGroupBox";
            TwitchAPITestGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TwitchAPITestGroupBox.Size = new System.Drawing.Size(344, 92);
            TwitchAPITestGroupBox.TabIndex = 31;
            TwitchAPITestGroupBox.TabStop = false;
            TwitchAPITestGroupBox.Text = "API Test";
            // 
            // TwitchTestSendText
            // 
            TwitchTestSendText.Location = new System.Drawing.Point(18, 58);
            TwitchTestSendText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TwitchTestSendText.Name = "TwitchTestSendText";
            TwitchTestSendText.Size = new System.Drawing.Size(307, 23);
            TwitchTestSendText.TabIndex = 20;
            TwitchTestSendText.Text = "Hello! I am BanterBrain Buddy https://banterbrain.tv";
            // 
            // TwitchSendTextCheckBox
            // 
            TwitchSendTextCheckBox.AutoSize = true;
            TwitchSendTextCheckBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchSendTextCheckBox.Location = new System.Drawing.Point(7, 20);
            TwitchSendTextCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TwitchSendTextCheckBox.Name = "TwitchSendTextCheckBox";
            TwitchSendTextCheckBox.Size = new System.Drawing.Size(186, 19);
            TwitchSendTextCheckBox.TabIndex = 19;
            TwitchSendTextCheckBox.Text = "Send Message on join channel";
            TwitchSendTextCheckBox.UseVisualStyleBackColor = true;
            // 
            // TwitchAPITestButton
            // 
            TwitchAPITestButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchAPITestButton.Location = new System.Drawing.Point(238, 15);
            TwitchAPITestButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TwitchAPITestButton.Name = "TwitchAPITestButton";
            TwitchAPITestButton.Size = new System.Drawing.Size(88, 27);
            TwitchAPITestButton.TabIndex = 18;
            TwitchAPITestButton.Text = "Test Credentials";
            TwitchAPITestButton.UseVisualStyleBackColor = true;
            TwitchAPITestButton.Click += TwitchTestButton_Click;
            // 
            // TwitchAuthorizeButton
            // 
            TwitchAuthorizeButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchAuthorizeButton.Location = new System.Drawing.Point(52, 145);
            TwitchAuthorizeButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TwitchAuthorizeButton.Name = "TwitchAuthorizeButton";
            TwitchAuthorizeButton.Size = new System.Drawing.Size(122, 27);
            TwitchAuthorizeButton.TabIndex = 30;
            TwitchAuthorizeButton.Text = "Authorize to Twitch";
            TwitchAuthorizeButton.UseVisualStyleBackColor = true;
            TwitchAuthorizeButton.Click += TwitchAuthorizeButton_Click;
            // 
            // TwitchChannel
            // 
            TwitchChannel.Location = new System.Drawing.Point(144, 114);
            TwitchChannel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TwitchChannel.Name = "TwitchChannel";
            TwitchChannel.Size = new System.Drawing.Size(190, 23);
            TwitchChannel.TabIndex = 29;
            // 
            // TwitchAccessToken
            // 
            TwitchAccessToken.Location = new System.Drawing.Point(144, 84);
            TwitchAccessToken.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TwitchAccessToken.Name = "TwitchAccessToken";
            TwitchAccessToken.PasswordChar = '*';
            TwitchAccessToken.Size = new System.Drawing.Size(190, 23);
            TwitchAccessToken.TabIndex = 28;
            // 
            // TwitchChannelNameLabel
            // 
            TwitchChannelNameLabel.AutoSize = true;
            TwitchChannelNameLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchChannelNameLabel.Location = new System.Drawing.Point(48, 122);
            TwitchChannelNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            TwitchChannelNameLabel.Name = "TwitchChannelNameLabel";
            TwitchChannelNameLabel.Size = new System.Drawing.Size(51, 15);
            TwitchChannelNameLabel.TabIndex = 27;
            TwitchChannelNameLabel.Text = "Channel";
            // 
            // TwitchAccesstokenLabel
            // 
            TwitchAccesstokenLabel.AutoSize = true;
            TwitchAccesstokenLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TwitchAccesstokenLabel.Location = new System.Drawing.Point(48, 88);
            TwitchAccesstokenLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            TwitchAccesstokenLabel.Name = "TwitchAccesstokenLabel";
            TwitchAccesstokenLabel.Size = new System.Drawing.Size(77, 15);
            TwitchAccesstokenLabel.TabIndex = 26;
            TwitchAccesstokenLabel.Text = "Access Token";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(213, 9);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(86, 15);
            label9.TabIndex = 0;
            label9.Text = "Twitch Settings";
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
            MicrophonePanel.Name = "MicrophonePanel";
            MicrophonePanel.Size = new System.Drawing.Size(593, 450);
            MicrophonePanel.TabIndex = 0;
            // 
            // PTTKeyLabel
            // 
            PTTKeyLabel.AutoSize = true;
            PTTKeyLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            PTTKeyLabel.Location = new System.Drawing.Point(37, 92);
            PTTKeyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            PTTKeyLabel.Name = "PTTKeyLabel";
            PTTKeyLabel.Size = new System.Drawing.Size(114, 15);
            PTTKeyLabel.TabIndex = 25;
            PTTKeyLabel.Text = "Push-To-Talk hotkey";
            // 
            // MicrophoneHotkeyEditbox
            // 
            MicrophoneHotkeyEditbox.Location = new System.Drawing.Point(182, 89);
            MicrophoneHotkeyEditbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MicrophoneHotkeyEditbox.Name = "MicrophoneHotkeyEditbox";
            MicrophoneHotkeyEditbox.ReadOnly = true;
            MicrophoneHotkeyEditbox.Size = new System.Drawing.Size(220, 23);
            MicrophoneHotkeyEditbox.TabIndex = 24;
            // 
            // VoiceInputLabel
            // 
            VoiceInputLabel.AutoSize = true;
            VoiceInputLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            VoiceInputLabel.Location = new System.Drawing.Point(37, 61);
            VoiceInputLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            VoiceInputLabel.Name = "VoiceInputLabel";
            VoiceInputLabel.Size = new System.Drawing.Size(66, 15);
            VoiceInputLabel.TabIndex = 23;
            VoiceInputLabel.Text = "Voice Input";
            // 
            // SoundInputDevices
            // 
            SoundInputDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            SoundInputDevices.FormattingEnabled = true;
            SoundInputDevices.Location = new System.Drawing.Point(182, 52);
            SoundInputDevices.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            SoundInputDevices.Name = "SoundInputDevices";
            SoundInputDevices.Size = new System.Drawing.Size(299, 23);
            SoundInputDevices.TabIndex = 22;
            SoundInputDevices.SelectedIndexChanged += SoundInputDevices_SelectedIndexChanged;
            // 
            // MicrophoneHotkeySet
            // 
            MicrophoneHotkeySet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            MicrophoneHotkeySet.Location = new System.Drawing.Point(423, 86);
            MicrophoneHotkeySet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MicrophoneHotkeySet.Name = "MicrophoneHotkeySet";
            MicrophoneHotkeySet.Size = new System.Drawing.Size(58, 27);
            MicrophoneHotkeySet.TabIndex = 21;
            MicrophoneHotkeySet.Text = "Set";
            MicrophoneHotkeySet.UseVisualStyleBackColor = true;
            MicrophoneHotkeySet.Click += MicrophoneHotkeySet_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(226, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(117, 15);
            label1.TabIndex = 0;
            label1.Text = "Microphone Settings";
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
            AzurePanel.Name = "AzurePanel";
            AzurePanel.Size = new System.Drawing.Size(593, 450);
            AzurePanel.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(13, 105);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(59, 15);
            label2.TabIndex = 24;
            label2.Text = "Language";
            // 
            // AzureLanguageComboBox
            // 
            AzureLanguageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            AzureLanguageComboBox.FormattingEnabled = true;
            AzureLanguageComboBox.Items.AddRange(new object[] { "en-US" });
            AzureLanguageComboBox.Location = new System.Drawing.Point(107, 99);
            AzureLanguageComboBox.Name = "AzureLanguageComboBox";
            AzureLanguageComboBox.Size = new System.Drawing.Size(202, 23);
            AzureLanguageComboBox.TabIndex = 23;
            // 
            // TestAzureAPISettings
            // 
            TestAzureAPISettings.Location = new System.Drawing.Point(234, 138);
            TestAzureAPISettings.Name = "TestAzureAPISettings";
            TestAzureAPISettings.Size = new System.Drawing.Size(75, 23);
            TestAzureAPISettings.TabIndex = 22;
            TestAzureAPISettings.Text = "Test";
            TestAzureAPISettings.UseVisualStyleBackColor = true;
            TestAzureAPISettings.Click += TestAzureAPISettings_Click;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label17.Location = new System.Drawing.Point(11, 63);
            label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(44, 15);
            label17.TabIndex = 18;
            label17.Text = "Region";
            // 
            // AzureRegionTextBox
            // 
            AzureRegionTextBox.Location = new System.Drawing.Point(107, 63);
            AzureRegionTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            AzureRegionTextBox.Name = "AzureRegionTextBox";
            AzureRegionTextBox.Size = new System.Drawing.Size(202, 23);
            AzureRegionTextBox.TabIndex = 19;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label18.Location = new System.Drawing.Point(12, 33);
            label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(47, 15);
            label18.TabIndex = 20;
            label18.Text = "API Key";
            // 
            // AzureAPIKeyTextBox
            // 
            AzureAPIKeyTextBox.Location = new System.Drawing.Point(107, 33);
            AzureAPIKeyTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            AzureAPIKeyTextBox.Name = "AzureAPIKeyTextBox";
            AzureAPIKeyTextBox.PasswordChar = '*';
            AzureAPIKeyTextBox.Size = new System.Drawing.Size(202, 23);
            AzureAPIKeyTextBox.TabIndex = 21;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(211, 9);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(102, 15);
            label3.TabIndex = 0;
            label3.Text = "Azure API settings";
            // 
            // SpeakerPanel
            // 
            SpeakerPanel.Controls.Add(TTSAudioOutputComboBox);
            SpeakerPanel.Controls.Add(TTSAudioOutputLabel);
            SpeakerPanel.Controls.Add(label6);
            SpeakerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            SpeakerPanel.Location = new System.Drawing.Point(0, 0);
            SpeakerPanel.Name = "SpeakerPanel";
            SpeakerPanel.Size = new System.Drawing.Size(593, 450);
            SpeakerPanel.TabIndex = 2;
            // 
            // TTSAudioOutputComboBox
            // 
            TTSAudioOutputComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TTSAudioOutputComboBox.FormattingEnabled = true;
            TTSAudioOutputComboBox.Location = new System.Drawing.Point(180, 55);
            TTSAudioOutputComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TTSAudioOutputComboBox.Name = "TTSAudioOutputComboBox";
            TTSAudioOutputComboBox.Size = new System.Drawing.Size(299, 23);
            TTSAudioOutputComboBox.TabIndex = 19;
            // 
            // TTSAudioOutputLabel
            // 
            TTSAudioOutputLabel.AutoSize = true;
            TTSAudioOutputLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            TTSAudioOutputLabel.Location = new System.Drawing.Point(49, 58);
            TTSAudioOutputLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            TTSAudioOutputLabel.Name = "TTSAudioOutputLabel";
            TTSAudioOutputLabel.Size = new System.Drawing.Size(80, 15);
            TTSAudioOutputLabel.TabIndex = 18;
            TTSAudioOutputLabel.Text = "Audio Output";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(224, 8);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(133, 15);
            label6.TabIndex = 0;
            label6.Text = "Speaker Output settings";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new System.Drawing.Point(65, 137);
            label20.Name = "label20";
            label20.Size = new System.Drawing.Size(94, 15);
            label20.TabIndex = 8;
            label20.Text = "Response length";
            // 
            // OllamaResponseLengthComboBox
            // 
            OllamaResponseLengthComboBox.FormattingEnabled = true;
            OllamaResponseLengthComboBox.Items.AddRange(new object[] { "Short", "Normal", "Long" });
            OllamaResponseLengthComboBox.Location = new System.Drawing.Point(202, 132);
            OllamaResponseLengthComboBox.Name = "OllamaResponseLengthComboBox";
            OllamaResponseLengthComboBox.Size = new System.Drawing.Size(205, 23);
            OllamaResponseLengthComboBox.TabIndex = 9;
            OllamaResponseLengthComboBox.Text = "Normal";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(splitContainer1);
            Name = "SettingsForm";
            Text = "Settings";
            FormClosing += SettingsForm_FormClosing;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            OllamaPanel.ResumeLayout(false);
            OllamaPanel.PerformLayout();
            ElevenLabsPanel.ResumeLayout(false);
            ElevenLabsPanel.PerformLayout();
            OpenAIChatGPTPanel.ResumeLayout(false);
            OpenAIChatGPTPanel.PerformLayout();
            PersonasPanel.ResumeLayout(false);
            PersonasPanel.PerformLayout();
            TwitchPanel.ResumeLayout(false);
            TwitchPanel.PerformLayout();
            EventSubGroupbox.ResumeLayout(false);
            EventSubGroupbox.PerformLayout();
            TwitchAPITestGroupBox.ResumeLayout(false);
            TwitchAPITestGroupBox.PerformLayout();
            MicrophonePanel.ResumeLayout(false);
            MicrophonePanel.PerformLayout();
            AzurePanel.ResumeLayout(false);
            AzurePanel.PerformLayout();
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
        private System.Windows.Forms.CheckBox UseOllamaLLMCheckBox;
        private System.Windows.Forms.Button OllamaTestButton;
        private System.Windows.Forms.ComboBox OllamaResponseLengthComboBox;
        private System.Windows.Forms.Label label20;
    }
}