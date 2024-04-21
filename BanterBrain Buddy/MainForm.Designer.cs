using System.Windows.Forms;

namespace BanterBrain_Buddy
{
    partial class BBB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BBB));
            SettingsTab = new TabPage();
            groupBox3 = new GroupBox();
            PTTKeyLabel = new Label();
            MicrophoneHotkeyEditbox = new TextBox();
            VoiceInputLabel = new Label();
            SoundInputDevices = new ComboBox();
            MicrophoneHotkeySet = new Button();
            groupBox2 = new GroupBox();
            label8 = new Label();
            TTSRegionTextBox = new TextBox();
            label7 = new Label();
            TTSAPIKeyTextBox = new TextBox();
            TTSOutputVoiceOption1 = new ComboBox();
            label3 = new Label();
            TTSOutputVoice = new ComboBox();
            label2 = new Label();
            TTSTestTextBox = new TextBox();
            TTSTestButton = new Button();
            TTSAudioOutputComboBox = new ComboBox();
            TTSProviderComboBox = new ComboBox();
            TTSAudioOutputLabel = new Label();
            TTSProviderLabel = new Label();
            STTGroupBox = new GroupBox();
            STTLanguageLabel = new Label();
            STTLanguageComboBox = new ComboBox();
            STTHintText = new Label();
            STTRegionEditbox = new TextBox();
            STTRegionLabel = new Label();
            STTAPIKeyLabel = new Label();
            STTAPIKeyEditbox = new TextBox();
            STTTestButton = new Button();
            STTProviderLabel = new Label();
            STTProviderBox = new ComboBox();
            STTTestOutput = new TextBox();
            MainTab = new TabPage();
            groupBox7 = new GroupBox();
            MainRecordingStart = new Button();
            TwitchEventSubStatusTextBox = new TextBox();
            TwitchEventSubStatusLabel = new Label();
            TwitchAPIStatusTextBox = new TextBox();
            TwitchStatusLabel = new Label();
            TextLog = new TextBox();
            BBBTabs = new TabControl();
            LLMTab = new TabPage();
            groupBox1 = new GroupBox();
            LLMMaxTokensHelpText = new Label();
            LLMTempHelpText = new Label();
            LLMMaxTokenLabel = new Label();
            LLMTempLabel = new Label();
            LLMMaxTokens = new TextBox();
            LLMTemperature = new TextBox();
            LLMModelComboBox = new ComboBox();
            label4 = new Label();
            LLMTestOutputbox = new TextBox();
            label1 = new Label();
            LLMRoleTextBox = new TextBox();
            GPTTestButton = new Button();
            LLMAPIKeyTextBox = new TextBox();
            GPTAPIKeyLabel = new Label();
            LLMProviderComboBox = new ComboBox();
            GPTProviderLabel = new Label();
            StreaminSettingsTab = new TabPage();
            groupBox4 = new GroupBox();
            groupBox8 = new GroupBox();
            label9 = new Label();
            TwitchCustomRewardName = new TextBox();
            TwitchChannelPointCheckBox = new CheckBox();
            EventSubGroupbox = new GroupBox();
            TwitchMockEventSub = new CheckBox();
            EventSubTest = new Button();
            TwitchEnableCheckbox = new CheckBox();
            TwitchCheckAuthAtStartup = new CheckBox();
            DisconnectTwitchButton = new Button();
            TwitchAPITestGroupBox = new GroupBox();
            TwitchTestSendText = new TextBox();
            TwitchSendTextCheckBox = new CheckBox();
            TwitchTestButton = new Button();
            TwitchAuthorizeButton = new Button();
            groupBox6 = new GroupBox();
            ResubscribedCheckBox = new CheckBox();
            TwitchGiftedSub = new CheckBox();
            TwitchCommunitySubs = new CheckBox();
            TwitchSubscribed = new CheckBox();
            groupBox5 = new GroupBox();
            TwitchCheerCheckBox = new CheckBox();
            label5 = new Label();
            TwitchMinBits = new TextBox();
            TwitchTriggerSettings = new GroupBox();
            TwitchReadChatCheckBox = new CheckBox();
            label6 = new Label();
            TwitchChatCommandDelay = new TextBox();
            TwitchNeedsSubscriber = new CheckBox();
            TwitchNeedsFollower = new CheckBox();
            TwitchCommandTrigger = new TextBox();
            TwitchCommandTriggerLabel = new Label();
            TwitchChannel = new TextBox();
            TwitchAccessToken = new TextBox();
            TwitchUsername = new TextBox();
            TwitchChannelNameLabel = new Label();
            TwitchAccesstokenLabel = new Label();
            TwitchUsernameLabel = new Label();
            PersonasTab = new TabPage();
            fileToolStripMenuItem = new ToolStripMenuItem();
            ExitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            GithubToolStripMenuItem = new ToolStripMenuItem();
            DiscordToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            BBBToolTip = new ToolTip(components);
            SettingsTab.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            STTGroupBox.SuspendLayout();
            MainTab.SuspendLayout();
            groupBox7.SuspendLayout();
            BBBTabs.SuspendLayout();
            LLMTab.SuspendLayout();
            groupBox1.SuspendLayout();
            StreaminSettingsTab.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox8.SuspendLayout();
            EventSubGroupbox.SuspendLayout();
            TwitchAPITestGroupBox.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox5.SuspendLayout();
            TwitchTriggerSettings.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // SettingsTab
            // 
            resources.ApplyResources(SettingsTab, "SettingsTab");
            SettingsTab.Controls.Add(groupBox3);
            SettingsTab.Controls.Add(groupBox2);
            SettingsTab.Controls.Add(STTGroupBox);
            SettingsTab.Name = "SettingsTab";
            SettingsTab.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(PTTKeyLabel);
            groupBox3.Controls.Add(MicrophoneHotkeyEditbox);
            groupBox3.Controls.Add(VoiceInputLabel);
            groupBox3.Controls.Add(SoundInputDevices);
            groupBox3.Controls.Add(MicrophoneHotkeySet);
            resources.ApplyResources(groupBox3, "groupBox3");
            groupBox3.Name = "groupBox3";
            groupBox3.TabStop = false;
            // 
            // PTTKeyLabel
            // 
            resources.ApplyResources(PTTKeyLabel, "PTTKeyLabel");
            PTTKeyLabel.Name = "PTTKeyLabel";
            // 
            // MicrophoneHotkeyEditbox
            // 
            resources.ApplyResources(MicrophoneHotkeyEditbox, "MicrophoneHotkeyEditbox");
            MicrophoneHotkeyEditbox.Name = "MicrophoneHotkeyEditbox";
            MicrophoneHotkeyEditbox.ReadOnly = true;
            // 
            // VoiceInputLabel
            // 
            resources.ApplyResources(VoiceInputLabel, "VoiceInputLabel");
            VoiceInputLabel.Name = "VoiceInputLabel";
            // 
            // SoundInputDevices
            // 
            SoundInputDevices.FormattingEnabled = true;
            resources.ApplyResources(SoundInputDevices, "SoundInputDevices");
            SoundInputDevices.Name = "SoundInputDevices";
            SoundInputDevices.SelectedIndexChanged += SoundInputDevices_SelectedIndexChanged;
            // 
            // MicrophoneHotkeySet
            // 
            resources.ApplyResources(MicrophoneHotkeySet, "MicrophoneHotkeySet");
            MicrophoneHotkeySet.Name = "MicrophoneHotkeySet";
            MicrophoneHotkeySet.UseVisualStyleBackColor = true;
            MicrophoneHotkeySet.Click += MicrophoneHotkeySet_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(TTSRegionTextBox);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(TTSAPIKeyTextBox);
            groupBox2.Controls.Add(TTSOutputVoiceOption1);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(TTSOutputVoice);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(TTSTestTextBox);
            groupBox2.Controls.Add(TTSTestButton);
            groupBox2.Controls.Add(TTSAudioOutputComboBox);
            groupBox2.Controls.Add(TTSProviderComboBox);
            groupBox2.Controls.Add(TTSAudioOutputLabel);
            groupBox2.Controls.Add(TTSProviderLabel);
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            // 
            // TTSRegionTextBox
            // 
            resources.ApplyResources(TTSRegionTextBox, "TTSRegionTextBox");
            TTSRegionTextBox.Name = "TTSRegionTextBox";
            TTSRegionTextBox.Leave += TTSRegionTextBox_Leave;
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            // 
            // TTSAPIKeyTextBox
            // 
            resources.ApplyResources(TTSAPIKeyTextBox, "TTSAPIKeyTextBox");
            TTSAPIKeyTextBox.Name = "TTSAPIKeyTextBox";
            // 
            // TTSOutputVoiceOption1
            // 
            TTSOutputVoiceOption1.FormattingEnabled = true;
            resources.ApplyResources(TTSOutputVoiceOption1, "TTSOutputVoiceOption1");
            TTSOutputVoiceOption1.Name = "TTSOutputVoiceOption1";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // TTSOutputVoice
            // 
            TTSOutputVoice.FormattingEnabled = true;
            resources.ApplyResources(TTSOutputVoice, "TTSOutputVoice");
            TTSOutputVoice.Name = "TTSOutputVoice";
            TTSOutputVoice.SelectedIndexChanged += TTSOutputVoice_SelectedIndexChanged;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // TTSTestTextBox
            // 
            resources.ApplyResources(TTSTestTextBox, "TTSTestTextBox");
            TTSTestTextBox.Name = "TTSTestTextBox";
            TTSTestTextBox.ReadOnly = true;
            // 
            // TTSTestButton
            // 
            resources.ApplyResources(TTSTestButton, "TTSTestButton");
            TTSTestButton.Name = "TTSTestButton";
            TTSTestButton.UseVisualStyleBackColor = true;
            TTSTestButton.Click += TTSTestButton_Click;
            // 
            // TTSAudioOutputComboBox
            // 
            TTSAudioOutputComboBox.FormattingEnabled = true;
            resources.ApplyResources(TTSAudioOutputComboBox, "TTSAudioOutputComboBox");
            TTSAudioOutputComboBox.Name = "TTSAudioOutputComboBox";
            // 
            // TTSProviderComboBox
            // 
            TTSProviderComboBox.FormattingEnabled = true;
            TTSProviderComboBox.Items.AddRange(new object[] { resources.GetString("TTSProviderComboBox.Items"), resources.GetString("TTSProviderComboBox.Items1") });
            resources.ApplyResources(TTSProviderComboBox, "TTSProviderComboBox");
            TTSProviderComboBox.Name = "TTSProviderComboBox";
            TTSProviderComboBox.SelectedIndexChanged += TTSProviderComboBox_SelectedIndexChanged;
            // 
            // TTSAudioOutputLabel
            // 
            resources.ApplyResources(TTSAudioOutputLabel, "TTSAudioOutputLabel");
            TTSAudioOutputLabel.Name = "TTSAudioOutputLabel";
            // 
            // TTSProviderLabel
            // 
            resources.ApplyResources(TTSProviderLabel, "TTSProviderLabel");
            TTSProviderLabel.Name = "TTSProviderLabel";
            // 
            // STTGroupBox
            // 
            resources.ApplyResources(STTGroupBox, "STTGroupBox");
            STTGroupBox.Controls.Add(STTLanguageLabel);
            STTGroupBox.Controls.Add(STTLanguageComboBox);
            STTGroupBox.Controls.Add(STTHintText);
            STTGroupBox.Controls.Add(STTRegionEditbox);
            STTGroupBox.Controls.Add(STTRegionLabel);
            STTGroupBox.Controls.Add(STTAPIKeyLabel);
            STTGroupBox.Controls.Add(STTAPIKeyEditbox);
            STTGroupBox.Controls.Add(STTTestButton);
            STTGroupBox.Controls.Add(STTProviderLabel);
            STTGroupBox.Controls.Add(STTProviderBox);
            STTGroupBox.Controls.Add(STTTestOutput);
            STTGroupBox.Name = "STTGroupBox";
            STTGroupBox.TabStop = false;
            // 
            // STTLanguageLabel
            // 
            resources.ApplyResources(STTLanguageLabel, "STTLanguageLabel");
            STTLanguageLabel.Name = "STTLanguageLabel";
            // 
            // STTLanguageComboBox
            // 
            STTLanguageComboBox.FormattingEnabled = true;
            STTLanguageComboBox.Items.AddRange(new object[] { resources.GetString("STTLanguageComboBox.Items") });
            resources.ApplyResources(STTLanguageComboBox, "STTLanguageComboBox");
            STTLanguageComboBox.Name = "STTLanguageComboBox";
            // 
            // STTHintText
            // 
            resources.ApplyResources(STTHintText, "STTHintText");
            STTHintText.Name = "STTHintText";
            // 
            // STTRegionEditbox
            // 
            resources.ApplyResources(STTRegionEditbox, "STTRegionEditbox");
            STTRegionEditbox.Name = "STTRegionEditbox";
            // 
            // STTRegionLabel
            // 
            resources.ApplyResources(STTRegionLabel, "STTRegionLabel");
            STTRegionLabel.Name = "STTRegionLabel";
            // 
            // STTAPIKeyLabel
            // 
            resources.ApplyResources(STTAPIKeyLabel, "STTAPIKeyLabel");
            STTAPIKeyLabel.Name = "STTAPIKeyLabel";
            // 
            // STTAPIKeyEditbox
            // 
            resources.ApplyResources(STTAPIKeyEditbox, "STTAPIKeyEditbox");
            STTAPIKeyEditbox.Name = "STTAPIKeyEditbox";
            // 
            // STTTestButton
            // 
            resources.ApplyResources(STTTestButton, "STTTestButton");
            STTTestButton.Name = "STTTestButton";
            STTTestButton.UseVisualStyleBackColor = true;
            STTTestButton.Click += STTTestButton_Click;
            // 
            // STTProviderLabel
            // 
            resources.ApplyResources(STTProviderLabel, "STTProviderLabel");
            STTProviderLabel.Name = "STTProviderLabel";
            // 
            // STTProviderBox
            // 
            STTProviderBox.FormattingEnabled = true;
            STTProviderBox.Items.AddRange(new object[] { resources.GetString("STTProviderBox.Items"), resources.GetString("STTProviderBox.Items1") });
            resources.ApplyResources(STTProviderBox, "STTProviderBox");
            STTProviderBox.Name = "STTProviderBox";
            STTProviderBox.SelectedIndexChanged += STTProviderBox_SelectedIndexChanged;
            // 
            // STTTestOutput
            // 
            resources.ApplyResources(STTTestOutput, "STTTestOutput");
            STTTestOutput.Name = "STTTestOutput";
            STTTestOutput.ReadOnly = true;
            // 
            // MainTab
            // 
            MainTab.Controls.Add(groupBox7);
            MainTab.Controls.Add(TwitchEventSubStatusTextBox);
            MainTab.Controls.Add(TwitchEventSubStatusLabel);
            MainTab.Controls.Add(TwitchAPIStatusTextBox);
            MainTab.Controls.Add(TwitchStatusLabel);
            MainTab.Controls.Add(TextLog);
            resources.ApplyResources(MainTab, "MainTab");
            MainTab.Name = "MainTab";
            MainTab.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(MainRecordingStart);
            resources.ApplyResources(groupBox7, "groupBox7");
            groupBox7.Name = "groupBox7";
            groupBox7.TabStop = false;
            // 
            // MainRecordingStart
            // 
            resources.ApplyResources(MainRecordingStart, "MainRecordingStart");
            MainRecordingStart.Name = "MainRecordingStart";
            MainRecordingStart.UseVisualStyleBackColor = true;
            MainRecordingStart.Click += MainRecordingStart_Click;
            // 
            // TwitchEventSubStatusTextBox
            // 
            TwitchEventSubStatusTextBox.BackColor = System.Drawing.Color.Red;
            TwitchEventSubStatusTextBox.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(TwitchEventSubStatusTextBox, "TwitchEventSubStatusTextBox");
            TwitchEventSubStatusTextBox.Name = "TwitchEventSubStatusTextBox";
            TwitchEventSubStatusTextBox.ReadOnly = true;
            // 
            // TwitchEventSubStatusLabel
            // 
            resources.ApplyResources(TwitchEventSubStatusLabel, "TwitchEventSubStatusLabel");
            TwitchEventSubStatusLabel.Name = "TwitchEventSubStatusLabel";
            // 
            // TwitchAPIStatusTextBox
            // 
            TwitchAPIStatusTextBox.BackColor = System.Drawing.Color.Red;
            TwitchAPIStatusTextBox.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(TwitchAPIStatusTextBox, "TwitchAPIStatusTextBox");
            TwitchAPIStatusTextBox.Name = "TwitchAPIStatusTextBox";
            TwitchAPIStatusTextBox.ReadOnly = true;
            // 
            // TwitchStatusLabel
            // 
            resources.ApplyResources(TwitchStatusLabel, "TwitchStatusLabel");
            TwitchStatusLabel.Name = "TwitchStatusLabel";
            // 
            // TextLog
            // 
            TextLog.BorderStyle = BorderStyle.None;
            resources.ApplyResources(TextLog, "TextLog");
            TextLog.Name = "TextLog";
            // 
            // BBBTabs
            // 
            resources.ApplyResources(BBBTabs, "BBBTabs");
            BBBTabs.Controls.Add(MainTab);
            BBBTabs.Controls.Add(SettingsTab);
            BBBTabs.Controls.Add(LLMTab);
            BBBTabs.Controls.Add(StreaminSettingsTab);
            BBBTabs.Controls.Add(PersonasTab);
            BBBTabs.Name = "BBBTabs";
            BBBTabs.SelectedIndex = 0;
            // 
            // LLMTab
            // 
            LLMTab.Controls.Add(groupBox1);
            resources.ApplyResources(LLMTab, "LLMTab");
            LLMTab.Name = "LLMTab";
            LLMTab.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(LLMMaxTokensHelpText);
            groupBox1.Controls.Add(LLMTempHelpText);
            groupBox1.Controls.Add(LLMMaxTokenLabel);
            groupBox1.Controls.Add(LLMTempLabel);
            groupBox1.Controls.Add(LLMMaxTokens);
            groupBox1.Controls.Add(LLMTemperature);
            groupBox1.Controls.Add(LLMModelComboBox);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(LLMTestOutputbox);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(LLMRoleTextBox);
            groupBox1.Controls.Add(GPTTestButton);
            groupBox1.Controls.Add(LLMAPIKeyTextBox);
            groupBox1.Controls.Add(GPTAPIKeyLabel);
            groupBox1.Controls.Add(LLMProviderComboBox);
            groupBox1.Controls.Add(GPTProviderLabel);
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // LLMMaxTokensHelpText
            // 
            resources.ApplyResources(LLMMaxTokensHelpText, "LLMMaxTokensHelpText");
            LLMMaxTokensHelpText.BackColor = System.Drawing.Color.Gold;
            LLMMaxTokensHelpText.Name = "LLMMaxTokensHelpText";
            BBBToolTip.SetToolTip(LLMMaxTokensHelpText, resources.GetString("LLMMaxTokensHelpText.ToolTip"));
            // 
            // LLMTempHelpText
            // 
            resources.ApplyResources(LLMTempHelpText, "LLMTempHelpText");
            LLMTempHelpText.BackColor = System.Drawing.Color.Gold;
            LLMTempHelpText.Name = "LLMTempHelpText";
            BBBToolTip.SetToolTip(LLMTempHelpText, resources.GetString("LLMTempHelpText.ToolTip"));
            // 
            // LLMMaxTokenLabel
            // 
            resources.ApplyResources(LLMMaxTokenLabel, "LLMMaxTokenLabel");
            LLMMaxTokenLabel.Name = "LLMMaxTokenLabel";
            // 
            // LLMTempLabel
            // 
            resources.ApplyResources(LLMTempLabel, "LLMTempLabel");
            LLMTempLabel.Name = "LLMTempLabel";
            // 
            // LLMMaxTokens
            // 
            resources.ApplyResources(LLMMaxTokens, "LLMMaxTokens");
            LLMMaxTokens.Name = "LLMMaxTokens";
            // 
            // LLMTemperature
            // 
            resources.ApplyResources(LLMTemperature, "LLMTemperature");
            LLMTemperature.Name = "LLMTemperature";
            // 
            // LLMModelComboBox
            // 
            LLMModelComboBox.FormattingEnabled = true;
            LLMModelComboBox.Items.AddRange(new object[] { resources.GetString("LLMModelComboBox.Items") });
            resources.ApplyResources(LLMModelComboBox, "LLMModelComboBox");
            LLMModelComboBox.Name = "LLMModelComboBox";
            LLMModelComboBox.UseWaitCursor = true;
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // LLMTestOutputbox
            // 
            resources.ApplyResources(LLMTestOutputbox, "LLMTestOutputbox");
            LLMTestOutputbox.Name = "LLMTestOutputbox";
            LLMTestOutputbox.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // LLMRoleTextBox
            // 
            resources.ApplyResources(LLMRoleTextBox, "LLMRoleTextBox");
            LLMRoleTextBox.Name = "LLMRoleTextBox";
            LLMRoleTextBox.TabStop = false;
            // 
            // GPTTestButton
            // 
            resources.ApplyResources(GPTTestButton, "GPTTestButton");
            GPTTestButton.Name = "GPTTestButton";
            GPTTestButton.UseVisualStyleBackColor = true;
            GPTTestButton.Click += GPTTestButton_Click;
            // 
            // LLMAPIKeyTextBox
            // 
            resources.ApplyResources(LLMAPIKeyTextBox, "LLMAPIKeyTextBox");
            LLMAPIKeyTextBox.Name = "LLMAPIKeyTextBox";
            // 
            // GPTAPIKeyLabel
            // 
            resources.ApplyResources(GPTAPIKeyLabel, "GPTAPIKeyLabel");
            GPTAPIKeyLabel.Name = "GPTAPIKeyLabel";
            // 
            // LLMProviderComboBox
            // 
            LLMProviderComboBox.FormattingEnabled = true;
            LLMProviderComboBox.Items.AddRange(new object[] { resources.GetString("LLMProviderComboBox.Items") });
            resources.ApplyResources(LLMProviderComboBox, "LLMProviderComboBox");
            LLMProviderComboBox.Name = "LLMProviderComboBox";
            // 
            // GPTProviderLabel
            // 
            resources.ApplyResources(GPTProviderLabel, "GPTProviderLabel");
            GPTProviderLabel.Name = "GPTProviderLabel";
            // 
            // StreaminSettingsTab
            // 
            StreaminSettingsTab.Controls.Add(groupBox4);
            resources.ApplyResources(StreaminSettingsTab, "StreaminSettingsTab");
            StreaminSettingsTab.Name = "StreaminSettingsTab";
            StreaminSettingsTab.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(groupBox8);
            groupBox4.Controls.Add(EventSubGroupbox);
            groupBox4.Controls.Add(TwitchEnableCheckbox);
            groupBox4.Controls.Add(TwitchCheckAuthAtStartup);
            groupBox4.Controls.Add(DisconnectTwitchButton);
            groupBox4.Controls.Add(TwitchAPITestGroupBox);
            groupBox4.Controls.Add(TwitchAuthorizeButton);
            groupBox4.Controls.Add(groupBox6);
            groupBox4.Controls.Add(groupBox5);
            groupBox4.Controls.Add(TwitchTriggerSettings);
            groupBox4.Controls.Add(TwitchChannel);
            groupBox4.Controls.Add(TwitchAccessToken);
            groupBox4.Controls.Add(TwitchUsername);
            groupBox4.Controls.Add(TwitchChannelNameLabel);
            groupBox4.Controls.Add(TwitchAccesstokenLabel);
            groupBox4.Controls.Add(TwitchUsernameLabel);
            resources.ApplyResources(groupBox4, "groupBox4");
            groupBox4.Name = "groupBox4";
            groupBox4.TabStop = false;
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(label9);
            groupBox8.Controls.Add(TwitchCustomRewardName);
            groupBox8.Controls.Add(TwitchChannelPointCheckBox);
            resources.ApplyResources(groupBox8, "groupBox8");
            groupBox8.Name = "groupBox8";
            groupBox8.TabStop = false;
            // 
            // label9
            // 
            resources.ApplyResources(label9, "label9");
            label9.Name = "label9";
            // 
            // TwitchCustomRewardName
            // 
            resources.ApplyResources(TwitchCustomRewardName, "TwitchCustomRewardName");
            TwitchCustomRewardName.Name = "TwitchCustomRewardName";
            // 
            // TwitchChannelPointCheckBox
            // 
            resources.ApplyResources(TwitchChannelPointCheckBox, "TwitchChannelPointCheckBox");
            TwitchChannelPointCheckBox.Name = "TwitchChannelPointCheckBox";
            TwitchChannelPointCheckBox.UseVisualStyleBackColor = true;
            // 
            // EventSubGroupbox
            // 
            EventSubGroupbox.Controls.Add(TwitchMockEventSub);
            EventSubGroupbox.Controls.Add(EventSubTest);
            resources.ApplyResources(EventSubGroupbox, "EventSubGroupbox");
            EventSubGroupbox.Name = "EventSubGroupbox";
            EventSubGroupbox.TabStop = false;
            // 
            // TwitchMockEventSub
            // 
            resources.ApplyResources(TwitchMockEventSub, "TwitchMockEventSub");
            TwitchMockEventSub.Name = "TwitchMockEventSub";
            BBBToolTip.SetToolTip(TwitchMockEventSub, resources.GetString("TwitchMockEventSub.ToolTip"));
            TwitchMockEventSub.UseVisualStyleBackColor = true;
            // 
            // EventSubTest
            // 
            resources.ApplyResources(EventSubTest, "EventSubTest");
            EventSubTest.Name = "EventSubTest";
            EventSubTest.UseVisualStyleBackColor = true;
            EventSubTest.Click += EventSubTest_Click;
            // 
            // TwitchEnableCheckbox
            // 
            resources.ApplyResources(TwitchEnableCheckbox, "TwitchEnableCheckbox");
            TwitchEnableCheckbox.Name = "TwitchEnableCheckbox";
            TwitchEnableCheckbox.UseVisualStyleBackColor = true;
            TwitchEnableCheckbox.Click += TwitchEnableCheckbox_Click;
            // 
            // TwitchCheckAuthAtStartup
            // 
            resources.ApplyResources(TwitchCheckAuthAtStartup, "TwitchCheckAuthAtStartup");
            TwitchCheckAuthAtStartup.Name = "TwitchCheckAuthAtStartup";
            TwitchCheckAuthAtStartup.UseVisualStyleBackColor = true;
            TwitchCheckAuthAtStartup.Click += TwitchCheckAuthAtStartup_Click;
            // 
            // DisconnectTwitchButton
            // 
            resources.ApplyResources(DisconnectTwitchButton, "DisconnectTwitchButton");
            DisconnectTwitchButton.Name = "DisconnectTwitchButton";
            DisconnectTwitchButton.UseVisualStyleBackColor = true;
            // 
            // TwitchAPITestGroupBox
            // 
            TwitchAPITestGroupBox.Controls.Add(TwitchTestSendText);
            TwitchAPITestGroupBox.Controls.Add(TwitchSendTextCheckBox);
            TwitchAPITestGroupBox.Controls.Add(TwitchTestButton);
            resources.ApplyResources(TwitchAPITestGroupBox, "TwitchAPITestGroupBox");
            TwitchAPITestGroupBox.Name = "TwitchAPITestGroupBox";
            TwitchAPITestGroupBox.TabStop = false;
            // 
            // TwitchTestSendText
            // 
            resources.ApplyResources(TwitchTestSendText, "TwitchTestSendText");
            TwitchTestSendText.Name = "TwitchTestSendText";
            // 
            // TwitchSendTextCheckBox
            // 
            resources.ApplyResources(TwitchSendTextCheckBox, "TwitchSendTextCheckBox");
            TwitchSendTextCheckBox.Checked = true;
            TwitchSendTextCheckBox.CheckState = CheckState.Checked;
            TwitchSendTextCheckBox.Name = "TwitchSendTextCheckBox";
            TwitchSendTextCheckBox.UseVisualStyleBackColor = true;
            // 
            // TwitchTestButton
            // 
            resources.ApplyResources(TwitchTestButton, "TwitchTestButton");
            TwitchTestButton.Name = "TwitchTestButton";
            TwitchTestButton.UseVisualStyleBackColor = true;
            TwitchTestButton.Click += TwitchTestButton_Click;
            // 
            // TwitchAuthorizeButton
            // 
            resources.ApplyResources(TwitchAuthorizeButton, "TwitchAuthorizeButton");
            TwitchAuthorizeButton.Name = "TwitchAuthorizeButton";
            TwitchAuthorizeButton.UseVisualStyleBackColor = true;
            TwitchAuthorizeButton.Click += TwitchAuthorizeButton_Click;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(ResubscribedCheckBox);
            groupBox6.Controls.Add(TwitchGiftedSub);
            groupBox6.Controls.Add(TwitchCommunitySubs);
            groupBox6.Controls.Add(TwitchSubscribed);
            resources.ApplyResources(groupBox6, "groupBox6");
            groupBox6.Name = "groupBox6";
            groupBox6.TabStop = false;
            // 
            // ResubscribedCheckBox
            // 
            resources.ApplyResources(ResubscribedCheckBox, "ResubscribedCheckBox");
            ResubscribedCheckBox.Name = "ResubscribedCheckBox";
            BBBToolTip.SetToolTip(ResubscribedCheckBox, resources.GetString("ResubscribedCheckBox.ToolTip"));
            ResubscribedCheckBox.UseVisualStyleBackColor = true;
            // 
            // TwitchGiftedSub
            // 
            resources.ApplyResources(TwitchGiftedSub, "TwitchGiftedSub");
            TwitchGiftedSub.Name = "TwitchGiftedSub";
            BBBToolTip.SetToolTip(TwitchGiftedSub, resources.GetString("TwitchGiftedSub.ToolTip"));
            TwitchGiftedSub.UseVisualStyleBackColor = true;
            // 
            // TwitchCommunitySubs
            // 
            resources.ApplyResources(TwitchCommunitySubs, "TwitchCommunitySubs");
            TwitchCommunitySubs.Name = "TwitchCommunitySubs";
            BBBToolTip.SetToolTip(TwitchCommunitySubs, resources.GetString("TwitchCommunitySubs.ToolTip"));
            TwitchCommunitySubs.UseVisualStyleBackColor = true;
            // 
            // TwitchSubscribed
            // 
            resources.ApplyResources(TwitchSubscribed, "TwitchSubscribed");
            TwitchSubscribed.Name = "TwitchSubscribed";
            BBBToolTip.SetToolTip(TwitchSubscribed, resources.GetString("TwitchSubscribed.ToolTip"));
            TwitchSubscribed.UseVisualStyleBackColor = true;
            TwitchSubscribed.Click += TwitchSubscribed_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(TwitchCheerCheckBox);
            groupBox5.Controls.Add(label5);
            groupBox5.Controls.Add(TwitchMinBits);
            resources.ApplyResources(groupBox5, "groupBox5");
            groupBox5.Name = "groupBox5";
            groupBox5.TabStop = false;
            // 
            // TwitchCheerCheckBox
            // 
            resources.ApplyResources(TwitchCheerCheckBox, "TwitchCheerCheckBox");
            TwitchCheerCheckBox.Name = "TwitchCheerCheckBox";
            BBBToolTip.SetToolTip(TwitchCheerCheckBox, resources.GetString("TwitchCheerCheckBox.ToolTip"));
            TwitchCheerCheckBox.UseVisualStyleBackColor = true;
            TwitchCheerCheckBox.Click += TwitchCheerCheckbox_Click;
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // TwitchMinBits
            // 
            resources.ApplyResources(TwitchMinBits, "TwitchMinBits");
            TwitchMinBits.Name = "TwitchMinBits";
            // 
            // TwitchTriggerSettings
            // 
            TwitchTriggerSettings.Controls.Add(TwitchReadChatCheckBox);
            TwitchTriggerSettings.Controls.Add(label6);
            TwitchTriggerSettings.Controls.Add(TwitchChatCommandDelay);
            TwitchTriggerSettings.Controls.Add(TwitchNeedsSubscriber);
            TwitchTriggerSettings.Controls.Add(TwitchNeedsFollower);
            TwitchTriggerSettings.Controls.Add(TwitchCommandTrigger);
            TwitchTriggerSettings.Controls.Add(TwitchCommandTriggerLabel);
            resources.ApplyResources(TwitchTriggerSettings, "TwitchTriggerSettings");
            TwitchTriggerSettings.Name = "TwitchTriggerSettings";
            TwitchTriggerSettings.TabStop = false;
            // 
            // TwitchReadChatCheckBox
            // 
            resources.ApplyResources(TwitchReadChatCheckBox, "TwitchReadChatCheckBox");
            TwitchReadChatCheckBox.Name = "TwitchReadChatCheckBox";
            BBBToolTip.SetToolTip(TwitchReadChatCheckBox, resources.GetString("TwitchReadChatCheckBox.ToolTip"));
            TwitchReadChatCheckBox.UseVisualStyleBackColor = true;
            TwitchReadChatCheckBox.Click += TwitchReadChatCheckBox_Click;
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // TwitchChatCommandDelay
            // 
            resources.ApplyResources(TwitchChatCommandDelay, "TwitchChatCommandDelay");
            TwitchChatCommandDelay.Name = "TwitchChatCommandDelay";
            // 
            // TwitchNeedsSubscriber
            // 
            resources.ApplyResources(TwitchNeedsSubscriber, "TwitchNeedsSubscriber");
            TwitchNeedsSubscriber.Name = "TwitchNeedsSubscriber";
            BBBToolTip.SetToolTip(TwitchNeedsSubscriber, resources.GetString("TwitchNeedsSubscriber.ToolTip"));
            TwitchNeedsSubscriber.UseVisualStyleBackColor = true;
            // 
            // TwitchNeedsFollower
            // 
            resources.ApplyResources(TwitchNeedsFollower, "TwitchNeedsFollower");
            TwitchNeedsFollower.Name = "TwitchNeedsFollower";
            BBBToolTip.SetToolTip(TwitchNeedsFollower, resources.GetString("TwitchNeedsFollower.ToolTip"));
            TwitchNeedsFollower.UseVisualStyleBackColor = true;
            // 
            // TwitchCommandTrigger
            // 
            resources.ApplyResources(TwitchCommandTrigger, "TwitchCommandTrigger");
            TwitchCommandTrigger.Name = "TwitchCommandTrigger";
            // 
            // TwitchCommandTriggerLabel
            // 
            resources.ApplyResources(TwitchCommandTriggerLabel, "TwitchCommandTriggerLabel");
            TwitchCommandTriggerLabel.Name = "TwitchCommandTriggerLabel";
            // 
            // TwitchChannel
            // 
            resources.ApplyResources(TwitchChannel, "TwitchChannel");
            TwitchChannel.Name = "TwitchChannel";
            // 
            // TwitchAccessToken
            // 
            resources.ApplyResources(TwitchAccessToken, "TwitchAccessToken");
            TwitchAccessToken.Name = "TwitchAccessToken";
            // 
            // TwitchUsername
            // 
            resources.ApplyResources(TwitchUsername, "TwitchUsername");
            TwitchUsername.Name = "TwitchUsername";
            // 
            // TwitchChannelNameLabel
            // 
            resources.ApplyResources(TwitchChannelNameLabel, "TwitchChannelNameLabel");
            TwitchChannelNameLabel.Name = "TwitchChannelNameLabel";
            // 
            // TwitchAccesstokenLabel
            // 
            resources.ApplyResources(TwitchAccesstokenLabel, "TwitchAccesstokenLabel");
            TwitchAccesstokenLabel.Name = "TwitchAccesstokenLabel";
            // 
            // TwitchUsernameLabel
            // 
            resources.ApplyResources(TwitchUsernameLabel, "TwitchUsernameLabel");
            TwitchUsernameLabel.Name = "TwitchUsernameLabel";
            // 
            // PersonasTab
            // 
            resources.ApplyResources(PersonasTab, "PersonasTab");
            PersonasTab.Name = "PersonasTab";
            PersonasTab.UseVisualStyleBackColor = true;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ExitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // ExitToolStripMenuItem
            // 
            ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            resources.ApplyResources(ExitToolStripMenuItem, "ExitToolStripMenuItem");
            ExitToolStripMenuItem.Click += ExitToolStripMenuItem_Click_1;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { GithubToolStripMenuItem, DiscordToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // GithubToolStripMenuItem
            // 
            GithubToolStripMenuItem.Name = "GithubToolStripMenuItem";
            resources.ApplyResources(GithubToolStripMenuItem, "GithubToolStripMenuItem");
            GithubToolStripMenuItem.Click += GithubToolStripMenuItem_Click;
            // 
            // DiscordToolStripMenuItem
            // 
            DiscordToolStripMenuItem.Name = "DiscordToolStripMenuItem";
            resources.ApplyResources(DiscordToolStripMenuItem, "DiscordToolStripMenuItem");
            DiscordToolStripMenuItem.Click += DiscordToolStripMenuItem_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Name = "menuStrip1";
            // 
            // BBB
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(BBBTabs);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "BBB";
            FormClosing += BBB_FormClosing;
            Load += BBB_Load;
            SettingsTab.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            STTGroupBox.ResumeLayout(false);
            STTGroupBox.PerformLayout();
            MainTab.ResumeLayout(false);
            MainTab.PerformLayout();
            groupBox7.ResumeLayout(false);
            BBBTabs.ResumeLayout(false);
            LLMTab.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            StreaminSettingsTab.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            EventSubGroupbox.ResumeLayout(false);
            EventSubGroupbox.PerformLayout();
            TwitchAPITestGroupBox.ResumeLayout(false);
            TwitchAPITestGroupBox.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            TwitchTriggerSettings.ResumeLayout(false);
            TwitchTriggerSettings.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TabPage SettingsTab;
        private GroupBox groupBox2;
        private GroupBox STTGroupBox;
        private Label STTRegionLabel;
        private Label STTAPIKeyLabel;
        private TextBox STTAPIKeyEditbox;
        private Button STTTestButton;
        private Label STTProviderLabel;
        private ComboBox STTProviderBox;
        private TextBox STTTestOutput;
        private TabPage MainTab;
        private Button MainRecordingStart;
        private TabControl BBBTabs;
        private ComboBox TTSOutputVoiceOption1;
        private Label label3;
        private ComboBox TTSOutputVoice;
        private Label label2;
        private TextBox TTSTestTextBox;
        private Button TTSTestButton;
        private ComboBox TTSProviderComboBox;
        private Label TTSAudioOutputLabel;
        private Label TTSProviderLabel;
        private GroupBox groupBox3;
        private Label PTTKeyLabel;
        private TextBox MicrophoneHotkeyEditbox;
        private Label VoiceInputLabel;
        private ComboBox SoundInputDevices;
        private Button MicrophoneHotkeySet;
        private TextBox TextLog;
        private Label STTHintText;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem ExitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem GithubToolStripMenuItem;
        private ToolStripMenuItem DiscordToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ComboBox TTSAudioOutputComboBox;
        private TabPage LLMTab;
        private TabPage StreaminSettingsTab;
        private GroupBox groupBox4;
        private TextBox TwitchChannel;
        private TextBox TwitchAccessToken;
        private TextBox TwitchUsername;
        private Label TwitchChannelNameLabel;
        private Label TwitchAccesstokenLabel;
        private Label TwitchUsernameLabel;
        private TextBox TwitchAPIStatusTextBox;
        private Label TwitchStatusLabel;
        private GroupBox groupBox5;
        private GroupBox TwitchTriggerSettings;
        private CheckBox TwitchNeedsSubscriber;
        private CheckBox TwitchNeedsFollower;
        private TextBox TwitchCommandTrigger;
        private Label TwitchCommandTriggerLabel;
        private GroupBox groupBox6;
        private CheckBox TwitchSubscribed;
        private Label label5;
        private TextBox TwitchMinBits;
        private Label label6;
        private TextBox TwitchChatCommandDelay;
        private CheckBox TwitchGiftedSub;
        private CheckBox TwitchCommunitySubs;
        private Button TwitchAuthorizeButton;
        private GroupBox TwitchAPITestGroupBox;
        private TextBox TwitchTestSendText;
        private CheckBox TwitchSendTextCheckBox;
        private Button TwitchTestButton;
        private GroupBox groupBox1;
        private ComboBox LLMModelComboBox;
        private Label label4;
        private TextBox LLMTestOutputbox;
        private Label label1;
        private TextBox LLMRoleTextBox;
        private Button GPTTestButton;
        private TextBox LLMAPIKeyTextBox;
        private Label GPTAPIKeyLabel;
        private ComboBox LLMProviderComboBox;
        private Label GPTProviderLabel;
        private Label label7;
        private TextBox TTSAPIKeyTextBox;
        private TextBox STTRegionEditbox;
        private Label label8;
        private TextBox TTSRegionTextBox;
        private Button DisconnectTwitchButton;
        private Label STTLanguageLabel;
        private ComboBox STTLanguageComboBox;
        private CheckBox TwitchCheckAuthAtStartup;
        private CheckBox TwitchEnableCheckbox;
        private Label TwitchEventSubStatusLabel;
        private TextBox TwitchEventSubStatusTextBox;
        private Button EventSubTest;
        private CheckBox TwitchReadChatCheckBox;
        private GroupBox EventSubGroupbox;
        private TabPage PersonasTab;
        private GroupBox groupBox7;
        private CheckBox TwitchCheerCheckBox;
        private GroupBox groupBox8;
        private Label label9;
        private TextBox TwitchCustomRewardName;
        private CheckBox TwitchChannelPointCheckBox;
        private CheckBox TwitchMockEventSub;
        private Label LLMMaxTokenLabel;
        private Label LLMTempLabel;
        private TextBox LLMMaxTokens;
        private TextBox LLMTemperature;
        private ToolTip BBBToolTip;
        private Label LLMTempHelpText;
        private Label LLMMaxTokensHelpText;
        private CheckBox ResubscribedCheckBox;
    }
}

