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
            MainTab = new TabPage();
            groupBox3 = new GroupBox();
            BroadcasterSelectedPersonaComboBox = new ComboBox();
            groupBox2 = new GroupBox();
            STTSelectedComboBox = new ComboBox();
            groupBox7 = new GroupBox();
            MainRecordingStart = new Button();
            TwitchEventSubStatusTextBox = new TextBox();
            TwitchEventSubStatusLabel = new Label();
            TwitchAPIStatusTextBox = new TextBox();
            TwitchStatusLabel = new Label();
            TextLog = new TextBox();
            BBBTabs = new TabControl();
            StreamingSettingsTab = new TabPage();
            LLMGroupSettings = new GroupBox();
            LLMResponseSelecter = new ComboBox();
            label7 = new Label();
            groupBox4 = new GroupBox();
            TwitchResponseSettings = new GroupBox();
            TwitchResponseToChatDelayTextBox = new TextBox();
            label8 = new Label();
            TwitchResponseToChatCheckBox = new CheckBox();
            TwitchSoundsSettings = new GroupBox();
            TwitchChatSoundSelectButton = new Button();
            TwitchSubscriptionSoundTextBox = new ComboBox();
            TwitchCheeringSoundTextBox = new ComboBox();
            TwitchChannelSoundTextBox = new ComboBox();
            TwitchChatSoundTextBox = new ComboBox();
            TwitchSubscriptionSoundCheckBox = new CheckBox();
            TwitchCheeringSoundCheckBox = new CheckBox();
            TwitchChannelSoundCheckBox = new CheckBox();
            TwitchChatSoundCheckBox = new CheckBox();
            TwitchAutoStart = new CheckBox();
            TwitchStartButton = new Button();
            TwitchChannelPointsSettings = new GroupBox();
            TwitchChannelPointTTSResponseOnlyRadioButton = new RadioButton();
            TwitchChannelPointTTSEverythingRadioButton = new RadioButton();
            label4 = new Label();
            TwitchChannelPointPersonaComboBox = new ComboBox();
            label9 = new Label();
            TwitchCustomRewardName = new TextBox();
            TwitchChannelPointCheckBox = new CheckBox();
            TwitchEnableCheckbox = new CheckBox();
            TwitchSubscriberSettings = new GroupBox();
            TwitchSubscriptionTTSResponseOnlyRadioButton = new RadioButton();
            TwitchSubscriptionTTSEverythingRadioButton = new RadioButton();
            label2 = new Label();
            TwitchGiftedSub = new CheckBox();
            TwitchSubscriptionPersonaComboBox = new ComboBox();
            TwitchSubscribed = new CheckBox();
            TwitchCheerSettings = new GroupBox();
            TwitchCheeringTTSResponseOnlyRadioButton = new RadioButton();
            TwitchCheeringTTSEverythingRadioButton = new RadioButton();
            label3 = new Label();
            TwitchCheeringPersonaComboBox = new ComboBox();
            TwitchCheerCheckBox = new CheckBox();
            label5 = new Label();
            TwitchMinBits = new TextBox();
            TwitchTriggerSettings = new GroupBox();
            TwitchChatTTSResponseOnlyRadioButton = new RadioButton();
            TwitchChatTTSEverythingRadioButton = new RadioButton();
            label1 = new Label();
            TwitchChatPersonaComboBox = new ComboBox();
            TwitchReadChatCheckBox = new CheckBox();
            label6 = new Label();
            TwitchChatCommandDelay = new TextBox();
            TwitchNeedsSubscriber = new CheckBox();
            TwitchCommandTrigger = new TextBox();
            TwitchCommandTriggerLabel = new Label();
            fileToolStripMenuItem = new ToolStripMenuItem();
            ExitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            logfileDirectoryToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            DiscordToolStripMenuItem = new ToolStripMenuItem();
            GithubToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            seToolStripMenuItem = new ToolStripMenuItem();
            BBBToolTip = new ToolTip(components);
            MainTab.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox7.SuspendLayout();
            BBBTabs.SuspendLayout();
            StreamingSettingsTab.SuspendLayout();
            LLMGroupSettings.SuspendLayout();
            groupBox4.SuspendLayout();
            TwitchResponseSettings.SuspendLayout();
            TwitchSoundsSettings.SuspendLayout();
            TwitchChannelPointsSettings.SuspendLayout();
            TwitchSubscriberSettings.SuspendLayout();
            TwitchCheerSettings.SuspendLayout();
            TwitchTriggerSettings.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // MainTab
            // 
            MainTab.Controls.Add(groupBox3);
            MainTab.Controls.Add(groupBox2);
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
            // groupBox3
            // 
            groupBox3.Controls.Add(BroadcasterSelectedPersonaComboBox);
            resources.ApplyResources(groupBox3, "groupBox3");
            groupBox3.Name = "groupBox3";
            groupBox3.TabStop = false;
            // 
            // BroadcasterSelectedPersonaComboBox
            // 
            BroadcasterSelectedPersonaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            BroadcasterSelectedPersonaComboBox.FormattingEnabled = true;
            resources.ApplyResources(BroadcasterSelectedPersonaComboBox, "BroadcasterSelectedPersonaComboBox");
            BroadcasterSelectedPersonaComboBox.Name = "BroadcasterSelectedPersonaComboBox";
            BroadcasterSelectedPersonaComboBox.SelectedIndexChanged += BroadcasterSelectedPersonaComboBox_SelectedIndexChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(STTSelectedComboBox);
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // STTSelectedComboBox
            // 
            STTSelectedComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            STTSelectedComboBox.FormattingEnabled = true;
            resources.ApplyResources(STTSelectedComboBox, "STTSelectedComboBox");
            STTSelectedComboBox.Name = "STTSelectedComboBox";
            STTSelectedComboBox.SelectedIndexChanged += STTSelectedComboBox_SelectedIndexChanged;
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
            TextLog.ReadOnly = true;
            // 
            // BBBTabs
            // 
            resources.ApplyResources(BBBTabs, "BBBTabs");
            BBBTabs.Controls.Add(MainTab);
            BBBTabs.Controls.Add(StreamingSettingsTab);
            BBBTabs.Name = "BBBTabs";
            BBBTabs.SelectedIndex = 0;
            BBBTabs.SelectedIndexChanged += BBBTabs_SelectedIndexChanged;
            // 
            // StreamingSettingsTab
            // 
            StreamingSettingsTab.Controls.Add(LLMGroupSettings);
            StreamingSettingsTab.Controls.Add(groupBox4);
            resources.ApplyResources(StreamingSettingsTab, "StreamingSettingsTab");
            StreamingSettingsTab.Name = "StreamingSettingsTab";
            StreamingSettingsTab.UseVisualStyleBackColor = true;
            // 
            // LLMGroupSettings
            // 
            LLMGroupSettings.Controls.Add(LLMResponseSelecter);
            LLMGroupSettings.Controls.Add(label7);
            resources.ApplyResources(LLMGroupSettings, "LLMGroupSettings");
            LLMGroupSettings.Name = "LLMGroupSettings";
            LLMGroupSettings.TabStop = false;
            // 
            // LLMResponseSelecter
            // 
            LLMResponseSelecter.DropDownStyle = ComboBoxStyle.DropDownList;
            LLMResponseSelecter.FormattingEnabled = true;
            resources.ApplyResources(LLMResponseSelecter, "LLMResponseSelecter");
            LLMResponseSelecter.Name = "LLMResponseSelecter";
            LLMResponseSelecter.SelectedIndexChanged += LLMResponseSelecter_SelectedIndexChanged;
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(TwitchResponseSettings);
            groupBox4.Controls.Add(TwitchSoundsSettings);
            groupBox4.Controls.Add(TwitchAutoStart);
            groupBox4.Controls.Add(TwitchStartButton);
            groupBox4.Controls.Add(TwitchChannelPointsSettings);
            groupBox4.Controls.Add(TwitchEnableCheckbox);
            groupBox4.Controls.Add(TwitchSubscriberSettings);
            groupBox4.Controls.Add(TwitchCheerSettings);
            groupBox4.Controls.Add(TwitchTriggerSettings);
            resources.ApplyResources(groupBox4, "groupBox4");
            groupBox4.Name = "groupBox4";
            groupBox4.TabStop = false;
            // 
            // TwitchResponseSettings
            // 
            TwitchResponseSettings.Controls.Add(TwitchResponseToChatDelayTextBox);
            TwitchResponseSettings.Controls.Add(label8);
            TwitchResponseSettings.Controls.Add(TwitchResponseToChatCheckBox);
            resources.ApplyResources(TwitchResponseSettings, "TwitchResponseSettings");
            TwitchResponseSettings.Name = "TwitchResponseSettings";
            TwitchResponseSettings.TabStop = false;
            // 
            // TwitchResponseToChatDelayTextBox
            // 
            resources.ApplyResources(TwitchResponseToChatDelayTextBox, "TwitchResponseToChatDelayTextBox");
            TwitchResponseToChatDelayTextBox.Name = "TwitchResponseToChatDelayTextBox";
            BBBToolTip.SetToolTip(TwitchResponseToChatDelayTextBox, resources.GetString("TwitchResponseToChatDelayTextBox.ToolTip"));
            TwitchResponseToChatDelayTextBox.KeyPress += TwitchResponseToChatDelayTextBox_KeyPress;
            TwitchResponseToChatDelayTextBox.Validating += TwitchResponseToChatDelayTextBox_Validating;
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            // 
            // TwitchResponseToChatCheckBox
            // 
            resources.ApplyResources(TwitchResponseToChatCheckBox, "TwitchResponseToChatCheckBox");
            TwitchResponseToChatCheckBox.Name = "TwitchResponseToChatCheckBox";
            TwitchResponseToChatCheckBox.UseVisualStyleBackColor = true;
            TwitchResponseToChatCheckBox.CheckedChanged += TwitchResponseToChatCheckBox_CheckedChanged;
            // 
            // TwitchSoundsSettings
            // 
            TwitchSoundsSettings.Controls.Add(TwitchChatSoundSelectButton);
            TwitchSoundsSettings.Controls.Add(TwitchSubscriptionSoundTextBox);
            TwitchSoundsSettings.Controls.Add(TwitchCheeringSoundTextBox);
            TwitchSoundsSettings.Controls.Add(TwitchChannelSoundTextBox);
            TwitchSoundsSettings.Controls.Add(TwitchChatSoundTextBox);
            TwitchSoundsSettings.Controls.Add(TwitchSubscriptionSoundCheckBox);
            TwitchSoundsSettings.Controls.Add(TwitchCheeringSoundCheckBox);
            TwitchSoundsSettings.Controls.Add(TwitchChannelSoundCheckBox);
            TwitchSoundsSettings.Controls.Add(TwitchChatSoundCheckBox);
            resources.ApplyResources(TwitchSoundsSettings, "TwitchSoundsSettings");
            TwitchSoundsSettings.Name = "TwitchSoundsSettings";
            TwitchSoundsSettings.TabStop = false;
            // 
            // TwitchChatSoundSelectButton
            // 
            TwitchChatSoundSelectButton.Image = Properties.Resources.fileopenicon;
            resources.ApplyResources(TwitchChatSoundSelectButton, "TwitchChatSoundSelectButton");
            TwitchChatSoundSelectButton.Name = "TwitchChatSoundSelectButton";
            BBBToolTip.SetToolTip(TwitchChatSoundSelectButton, resources.GetString("TwitchChatSoundSelectButton.ToolTip"));
            TwitchChatSoundSelectButton.UseVisualStyleBackColor = true;
            TwitchChatSoundSelectButton.Click += TwitchChatSoundSelectButton_Click;
            // 
            // TwitchSubscriptionSoundTextBox
            // 
            TwitchSubscriptionSoundTextBox.DropDownStyle = ComboBoxStyle.DropDownList;
            TwitchSubscriptionSoundTextBox.FormattingEnabled = true;
            resources.ApplyResources(TwitchSubscriptionSoundTextBox, "TwitchSubscriptionSoundTextBox");
            TwitchSubscriptionSoundTextBox.Name = "TwitchSubscriptionSoundTextBox";
            // 
            // TwitchCheeringSoundTextBox
            // 
            TwitchCheeringSoundTextBox.DropDownStyle = ComboBoxStyle.DropDownList;
            TwitchCheeringSoundTextBox.FormattingEnabled = true;
            resources.ApplyResources(TwitchCheeringSoundTextBox, "TwitchCheeringSoundTextBox");
            TwitchCheeringSoundTextBox.Name = "TwitchCheeringSoundTextBox";
            // 
            // TwitchChannelSoundTextBox
            // 
            TwitchChannelSoundTextBox.DropDownStyle = ComboBoxStyle.DropDownList;
            TwitchChannelSoundTextBox.FormattingEnabled = true;
            resources.ApplyResources(TwitchChannelSoundTextBox, "TwitchChannelSoundTextBox");
            TwitchChannelSoundTextBox.Name = "TwitchChannelSoundTextBox";
            // 
            // TwitchChatSoundTextBox
            // 
            TwitchChatSoundTextBox.DropDownStyle = ComboBoxStyle.DropDownList;
            TwitchChatSoundTextBox.FormattingEnabled = true;
            resources.ApplyResources(TwitchChatSoundTextBox, "TwitchChatSoundTextBox");
            TwitchChatSoundTextBox.Name = "TwitchChatSoundTextBox";
            TwitchChatSoundTextBox.Click += TwitchChatSoundTextBox_Click;
            // 
            // TwitchSubscriptionSoundCheckBox
            // 
            resources.ApplyResources(TwitchSubscriptionSoundCheckBox, "TwitchSubscriptionSoundCheckBox");
            TwitchSubscriptionSoundCheckBox.Name = "TwitchSubscriptionSoundCheckBox";
            TwitchSubscriptionSoundCheckBox.UseVisualStyleBackColor = true;
            TwitchSubscriptionSoundCheckBox.CheckedChanged += TwitchSubscriptionSoundCheckBox_CheckedChanged;
            // 
            // TwitchCheeringSoundCheckBox
            // 
            resources.ApplyResources(TwitchCheeringSoundCheckBox, "TwitchCheeringSoundCheckBox");
            TwitchCheeringSoundCheckBox.Name = "TwitchCheeringSoundCheckBox";
            TwitchCheeringSoundCheckBox.UseVisualStyleBackColor = true;
            TwitchCheeringSoundCheckBox.CheckedChanged += TwitchCheeringSoundCheckBox_CheckedChanged;
            // 
            // TwitchChannelSoundCheckBox
            // 
            resources.ApplyResources(TwitchChannelSoundCheckBox, "TwitchChannelSoundCheckBox");
            TwitchChannelSoundCheckBox.Name = "TwitchChannelSoundCheckBox";
            TwitchChannelSoundCheckBox.UseVisualStyleBackColor = true;
            TwitchChannelSoundCheckBox.CheckedChanged += TwitchChannelSoundCheckBox_CheckedChanged;
            // 
            // TwitchChatSoundCheckBox
            // 
            resources.ApplyResources(TwitchChatSoundCheckBox, "TwitchChatSoundCheckBox");
            TwitchChatSoundCheckBox.Name = "TwitchChatSoundCheckBox";
            TwitchChatSoundCheckBox.UseVisualStyleBackColor = true;
            TwitchChatSoundCheckBox.CheckedChanged += TwitchChatSoundCheckBox_CheckedChanged;
            // 
            // TwitchAutoStart
            // 
            resources.ApplyResources(TwitchAutoStart, "TwitchAutoStart");
            TwitchAutoStart.Name = "TwitchAutoStart";
            TwitchAutoStart.UseVisualStyleBackColor = true;
            // 
            // TwitchStartButton
            // 
            TwitchStartButton.AllowDrop = true;
            resources.ApplyResources(TwitchStartButton, "TwitchStartButton");
            TwitchStartButton.Name = "TwitchStartButton";
            TwitchStartButton.UseVisualStyleBackColor = true;
            TwitchStartButton.Click += TwitchStartButton_Click;
            // 
            // TwitchChannelPointsSettings
            // 
            TwitchChannelPointsSettings.Controls.Add(TwitchChannelPointTTSResponseOnlyRadioButton);
            TwitchChannelPointsSettings.Controls.Add(TwitchChannelPointTTSEverythingRadioButton);
            TwitchChannelPointsSettings.Controls.Add(label4);
            TwitchChannelPointsSettings.Controls.Add(TwitchChannelPointPersonaComboBox);
            TwitchChannelPointsSettings.Controls.Add(label9);
            TwitchChannelPointsSettings.Controls.Add(TwitchCustomRewardName);
            TwitchChannelPointsSettings.Controls.Add(TwitchChannelPointCheckBox);
            resources.ApplyResources(TwitchChannelPointsSettings, "TwitchChannelPointsSettings");
            TwitchChannelPointsSettings.Name = "TwitchChannelPointsSettings";
            TwitchChannelPointsSettings.TabStop = false;
            // 
            // TwitchChannelPointTTSResponseOnlyRadioButton
            // 
            resources.ApplyResources(TwitchChannelPointTTSResponseOnlyRadioButton, "TwitchChannelPointTTSResponseOnlyRadioButton");
            TwitchChannelPointTTSResponseOnlyRadioButton.Name = "TwitchChannelPointTTSResponseOnlyRadioButton";
            TwitchChannelPointTTSResponseOnlyRadioButton.TabStop = true;
            BBBToolTip.SetToolTip(TwitchChannelPointTTSResponseOnlyRadioButton, resources.GetString("TwitchChannelPointTTSResponseOnlyRadioButton.ToolTip"));
            TwitchChannelPointTTSResponseOnlyRadioButton.UseVisualStyleBackColor = true;
            // 
            // TwitchChannelPointTTSEverythingRadioButton
            // 
            resources.ApplyResources(TwitchChannelPointTTSEverythingRadioButton, "TwitchChannelPointTTSEverythingRadioButton");
            TwitchChannelPointTTSEverythingRadioButton.Name = "TwitchChannelPointTTSEverythingRadioButton";
            TwitchChannelPointTTSEverythingRadioButton.TabStop = true;
            BBBToolTip.SetToolTip(TwitchChannelPointTTSEverythingRadioButton, resources.GetString("TwitchChannelPointTTSEverythingRadioButton.ToolTip"));
            TwitchChannelPointTTSEverythingRadioButton.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // TwitchChannelPointPersonaComboBox
            // 
            TwitchChannelPointPersonaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            TwitchChannelPointPersonaComboBox.FormattingEnabled = true;
            resources.ApplyResources(TwitchChannelPointPersonaComboBox, "TwitchChannelPointPersonaComboBox");
            TwitchChannelPointPersonaComboBox.Name = "TwitchChannelPointPersonaComboBox";
            TwitchChannelPointPersonaComboBox.SelectedIndexChanged += TwitchChannelPointPersonaComboBox_SelectedIndexChanged;
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
            TwitchChannelPointCheckBox.Click += TwitchChannelPointCheckBox_Click;
            // 
            // TwitchEnableCheckbox
            // 
            resources.ApplyResources(TwitchEnableCheckbox, "TwitchEnableCheckbox");
            TwitchEnableCheckbox.Name = "TwitchEnableCheckbox";
            TwitchEnableCheckbox.UseVisualStyleBackColor = true;
            TwitchEnableCheckbox.CheckedChanged += TwitchEnableCheckbox_CheckedChanged;
            TwitchEnableCheckbox.Click += TwitchEnableCheckbox_Click;
            // 
            // TwitchSubscriberSettings
            // 
            TwitchSubscriberSettings.Controls.Add(TwitchSubscriptionTTSResponseOnlyRadioButton);
            TwitchSubscriberSettings.Controls.Add(TwitchSubscriptionTTSEverythingRadioButton);
            TwitchSubscriberSettings.Controls.Add(label2);
            TwitchSubscriberSettings.Controls.Add(TwitchGiftedSub);
            TwitchSubscriberSettings.Controls.Add(TwitchSubscriptionPersonaComboBox);
            TwitchSubscriberSettings.Controls.Add(TwitchSubscribed);
            resources.ApplyResources(TwitchSubscriberSettings, "TwitchSubscriberSettings");
            TwitchSubscriberSettings.Name = "TwitchSubscriberSettings";
            TwitchSubscriberSettings.TabStop = false;
            // 
            // TwitchSubscriptionTTSResponseOnlyRadioButton
            // 
            resources.ApplyResources(TwitchSubscriptionTTSResponseOnlyRadioButton, "TwitchSubscriptionTTSResponseOnlyRadioButton");
            TwitchSubscriptionTTSResponseOnlyRadioButton.Name = "TwitchSubscriptionTTSResponseOnlyRadioButton";
            TwitchSubscriptionTTSResponseOnlyRadioButton.TabStop = true;
            BBBToolTip.SetToolTip(TwitchSubscriptionTTSResponseOnlyRadioButton, resources.GetString("TwitchSubscriptionTTSResponseOnlyRadioButton.ToolTip"));
            TwitchSubscriptionTTSResponseOnlyRadioButton.UseVisualStyleBackColor = true;
            // 
            // TwitchSubscriptionTTSEverythingRadioButton
            // 
            resources.ApplyResources(TwitchSubscriptionTTSEverythingRadioButton, "TwitchSubscriptionTTSEverythingRadioButton");
            TwitchSubscriptionTTSEverythingRadioButton.Name = "TwitchSubscriptionTTSEverythingRadioButton";
            TwitchSubscriptionTTSEverythingRadioButton.TabStop = true;
            BBBToolTip.SetToolTip(TwitchSubscriptionTTSEverythingRadioButton, resources.GetString("TwitchSubscriptionTTSEverythingRadioButton.ToolTip"));
            TwitchSubscriptionTTSEverythingRadioButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // TwitchGiftedSub
            // 
            resources.ApplyResources(TwitchGiftedSub, "TwitchGiftedSub");
            TwitchGiftedSub.Name = "TwitchGiftedSub";
            BBBToolTip.SetToolTip(TwitchGiftedSub, resources.GetString("TwitchGiftedSub.ToolTip"));
            TwitchGiftedSub.UseVisualStyleBackColor = true;
            TwitchGiftedSub.Click += TwitchGiftedSub_Click;
            // 
            // TwitchSubscriptionPersonaComboBox
            // 
            TwitchSubscriptionPersonaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            TwitchSubscriptionPersonaComboBox.FormattingEnabled = true;
            resources.ApplyResources(TwitchSubscriptionPersonaComboBox, "TwitchSubscriptionPersonaComboBox");
            TwitchSubscriptionPersonaComboBox.Name = "TwitchSubscriptionPersonaComboBox";
            TwitchSubscriptionPersonaComboBox.SelectedIndexChanged += TwitchSubscriptionPersonaComboBox_SelectedIndexChanged;
            // 
            // TwitchSubscribed
            // 
            resources.ApplyResources(TwitchSubscribed, "TwitchSubscribed");
            TwitchSubscribed.Name = "TwitchSubscribed";
            BBBToolTip.SetToolTip(TwitchSubscribed, resources.GetString("TwitchSubscribed.ToolTip"));
            TwitchSubscribed.UseVisualStyleBackColor = true;
            TwitchSubscribed.Click += TwitchSubscribed_Click;
            // 
            // TwitchCheerSettings
            // 
            TwitchCheerSettings.Controls.Add(TwitchCheeringTTSResponseOnlyRadioButton);
            TwitchCheerSettings.Controls.Add(TwitchCheeringTTSEverythingRadioButton);
            TwitchCheerSettings.Controls.Add(label3);
            TwitchCheerSettings.Controls.Add(TwitchCheeringPersonaComboBox);
            TwitchCheerSettings.Controls.Add(TwitchCheerCheckBox);
            TwitchCheerSettings.Controls.Add(label5);
            TwitchCheerSettings.Controls.Add(TwitchMinBits);
            resources.ApplyResources(TwitchCheerSettings, "TwitchCheerSettings");
            TwitchCheerSettings.Name = "TwitchCheerSettings";
            TwitchCheerSettings.TabStop = false;
            // 
            // TwitchCheeringTTSResponseOnlyRadioButton
            // 
            resources.ApplyResources(TwitchCheeringTTSResponseOnlyRadioButton, "TwitchCheeringTTSResponseOnlyRadioButton");
            TwitchCheeringTTSResponseOnlyRadioButton.Name = "TwitchCheeringTTSResponseOnlyRadioButton";
            TwitchCheeringTTSResponseOnlyRadioButton.TabStop = true;
            BBBToolTip.SetToolTip(TwitchCheeringTTSResponseOnlyRadioButton, resources.GetString("TwitchCheeringTTSResponseOnlyRadioButton.ToolTip"));
            TwitchCheeringTTSResponseOnlyRadioButton.UseVisualStyleBackColor = true;
            // 
            // TwitchCheeringTTSEverythingRadioButton
            // 
            resources.ApplyResources(TwitchCheeringTTSEverythingRadioButton, "TwitchCheeringTTSEverythingRadioButton");
            TwitchCheeringTTSEverythingRadioButton.Name = "TwitchCheeringTTSEverythingRadioButton";
            TwitchCheeringTTSEverythingRadioButton.TabStop = true;
            BBBToolTip.SetToolTip(TwitchCheeringTTSEverythingRadioButton, resources.GetString("TwitchCheeringTTSEverythingRadioButton.ToolTip"));
            TwitchCheeringTTSEverythingRadioButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // TwitchCheeringPersonaComboBox
            // 
            TwitchCheeringPersonaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            TwitchCheeringPersonaComboBox.FormattingEnabled = true;
            resources.ApplyResources(TwitchCheeringPersonaComboBox, "TwitchCheeringPersonaComboBox");
            TwitchCheeringPersonaComboBox.Name = "TwitchCheeringPersonaComboBox";
            TwitchCheeringPersonaComboBox.SelectedIndexChanged += TwitchCheeringPersonaComboBox_SelectedIndexChanged;
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
            TwitchMinBits.KeyPress += TwitchMinBits_KeyPress;
            TwitchMinBits.Validating += TwitchMinBits_Validating;
            // 
            // TwitchTriggerSettings
            // 
            TwitchTriggerSettings.Controls.Add(TwitchChatTTSResponseOnlyRadioButton);
            TwitchTriggerSettings.Controls.Add(TwitchChatTTSEverythingRadioButton);
            TwitchTriggerSettings.Controls.Add(label1);
            TwitchTriggerSettings.Controls.Add(TwitchChatPersonaComboBox);
            TwitchTriggerSettings.Controls.Add(TwitchReadChatCheckBox);
            TwitchTriggerSettings.Controls.Add(label6);
            TwitchTriggerSettings.Controls.Add(TwitchChatCommandDelay);
            TwitchTriggerSettings.Controls.Add(TwitchNeedsSubscriber);
            TwitchTriggerSettings.Controls.Add(TwitchCommandTrigger);
            TwitchTriggerSettings.Controls.Add(TwitchCommandTriggerLabel);
            resources.ApplyResources(TwitchTriggerSettings, "TwitchTriggerSettings");
            TwitchTriggerSettings.Name = "TwitchTriggerSettings";
            TwitchTriggerSettings.TabStop = false;
            // 
            // TwitchChatTTSResponseOnlyRadioButton
            // 
            resources.ApplyResources(TwitchChatTTSResponseOnlyRadioButton, "TwitchChatTTSResponseOnlyRadioButton");
            TwitchChatTTSResponseOnlyRadioButton.Name = "TwitchChatTTSResponseOnlyRadioButton";
            TwitchChatTTSResponseOnlyRadioButton.TabStop = true;
            BBBToolTip.SetToolTip(TwitchChatTTSResponseOnlyRadioButton, resources.GetString("TwitchChatTTSResponseOnlyRadioButton.ToolTip"));
            TwitchChatTTSResponseOnlyRadioButton.UseVisualStyleBackColor = true;
            // 
            // TwitchChatTTSEverythingRadioButton
            // 
            resources.ApplyResources(TwitchChatTTSEverythingRadioButton, "TwitchChatTTSEverythingRadioButton");
            TwitchChatTTSEverythingRadioButton.Name = "TwitchChatTTSEverythingRadioButton";
            TwitchChatTTSEverythingRadioButton.TabStop = true;
            BBBToolTip.SetToolTip(TwitchChatTTSEverythingRadioButton, resources.GetString("TwitchChatTTSEverythingRadioButton.ToolTip"));
            TwitchChatTTSEverythingRadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // TwitchChatPersonaComboBox
            // 
            TwitchChatPersonaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            TwitchChatPersonaComboBox.FormattingEnabled = true;
            resources.ApplyResources(TwitchChatPersonaComboBox, "TwitchChatPersonaComboBox");
            TwitchChatPersonaComboBox.Name = "TwitchChatPersonaComboBox";
            TwitchChatPersonaComboBox.SelectedIndexChanged += TwitchChatPersonaComboBox_SelectedIndexChanged;
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
            TwitchChatCommandDelay.KeyPress += TwitchChatCommandDelay_KeyPress;
            TwitchChatCommandDelay.Validating += TwitchChatCommandDelay_Validating;
            // 
            // TwitchNeedsSubscriber
            // 
            resources.ApplyResources(TwitchNeedsSubscriber, "TwitchNeedsSubscriber");
            TwitchNeedsSubscriber.Name = "TwitchNeedsSubscriber";
            BBBToolTip.SetToolTip(TwitchNeedsSubscriber, resources.GetString("TwitchNeedsSubscriber.ToolTip"));
            TwitchNeedsSubscriber.UseVisualStyleBackColor = true;
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
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { logfileDirectoryToolStripMenuItem, toolStripMenuItem1, DiscordToolStripMenuItem, GithubToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // logfileDirectoryToolStripMenuItem
            // 
            logfileDirectoryToolStripMenuItem.Name = "logfileDirectoryToolStripMenuItem";
            resources.ApplyResources(logfileDirectoryToolStripMenuItem, "logfileDirectoryToolStripMenuItem");
            logfileDirectoryToolStripMenuItem.Click += LogfileDirectoryToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // DiscordToolStripMenuItem
            // 
            DiscordToolStripMenuItem.Name = "DiscordToolStripMenuItem";
            resources.ApplyResources(DiscordToolStripMenuItem, "DiscordToolStripMenuItem");
            DiscordToolStripMenuItem.Click += DiscordToolStripMenuItem_Click;
            // 
            // GithubToolStripMenuItem
            // 
            GithubToolStripMenuItem.Name = "GithubToolStripMenuItem";
            resources.ApplyResources(GithubToolStripMenuItem, "GithubToolStripMenuItem");
            GithubToolStripMenuItem.Click += GithubToolStripMenuItem_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, seToolStripMenuItem, helpToolStripMenuItem });
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Name = "menuStrip1";
            // 
            // seToolStripMenuItem
            // 
            seToolStripMenuItem.Name = "seToolStripMenuItem";
            resources.ApplyResources(seToolStripMenuItem, "seToolStripMenuItem");
            seToolStripMenuItem.Click += SeToolStripMenuItem_Click;
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
            VisibleChanged += BBB_VisibleChanged;
            MainTab.ResumeLayout(false);
            MainTab.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            BBBTabs.ResumeLayout(false);
            StreamingSettingsTab.ResumeLayout(false);
            LLMGroupSettings.ResumeLayout(false);
            LLMGroupSettings.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            TwitchResponseSettings.ResumeLayout(false);
            TwitchResponseSettings.PerformLayout();
            TwitchSoundsSettings.ResumeLayout(false);
            TwitchSoundsSettings.PerformLayout();
            TwitchChannelPointsSettings.ResumeLayout(false);
            TwitchChannelPointsSettings.PerformLayout();
            TwitchSubscriberSettings.ResumeLayout(false);
            TwitchSubscriberSettings.PerformLayout();
            TwitchCheerSettings.ResumeLayout(false);
            TwitchCheerSettings.PerformLayout();
            TwitchTriggerSettings.ResumeLayout(false);
            TwitchTriggerSettings.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TabPage MainTab;
        private Button MainRecordingStart;
        private TabControl BBBTabs;
        private TextBox TextLog;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem ExitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem GithubToolStripMenuItem;
        private ToolStripMenuItem DiscordToolStripMenuItem;
        private MenuStrip menuStrip1;
        private TabPage StreamingSettingsTab;
        private GroupBox groupBox4;
        private TextBox TwitchAPIStatusTextBox;
        private Label TwitchStatusLabel;
        private GroupBox TwitchCheerSettings;
        private GroupBox TwitchTriggerSettings;
        private CheckBox TwitchNeedsSubscriber;
        private TextBox TwitchCommandTrigger;
        private Label TwitchCommandTriggerLabel;
        private GroupBox TwitchSubscriberSettings;
        private CheckBox TwitchSubscribed;
        private Label label5;
        private TextBox TwitchMinBits;
        private Label label6;
        private TextBox TwitchChatCommandDelay;
        private CheckBox TwitchGiftedSub;
        private CheckBox TwitchEnableCheckbox;
        private Label TwitchEventSubStatusLabel;
        private TextBox TwitchEventSubStatusTextBox;
        private CheckBox TwitchReadChatCheckBox;
        private GroupBox groupBox7;
        private CheckBox TwitchCheerCheckBox;
        private GroupBox TwitchChannelPointsSettings;
        private Label label9;
        private TextBox TwitchCustomRewardName;
        private CheckBox TwitchChannelPointCheckBox;
        private ToolTip BBBToolTip;
        private ToolStripMenuItem seToolStripMenuItem;
        private GroupBox groupBox2;
        private ComboBox STTSelectedComboBox;
        private GroupBox groupBox3;
        private ComboBox BroadcasterSelectedPersonaComboBox;
        private Label label1;
        private ComboBox TwitchChatPersonaComboBox;
        private Label label4;
        private ComboBox TwitchChannelPointPersonaComboBox;
        private Label label2;
        private ComboBox TwitchSubscriptionPersonaComboBox;
        private Label label3;
        private ComboBox TwitchCheeringPersonaComboBox;
        private Button TwitchStartButton;
        private CheckBox TwitchAutoStart;
        private GroupBox LLMGroupSettings;
        private ComboBox LLMResponseSelecter;
        private Label label7;
        private GroupBox TwitchSoundsSettings;
        private CheckBox TwitchSubscriptionSoundCheckBox;
        private CheckBox TwitchCheeringSoundCheckBox;
        private CheckBox TwitchChannelSoundCheckBox;
        private CheckBox TwitchChatSoundCheckBox;
        private Button TwitchChatSoundSelectButton;
        private ComboBox TwitchChatSoundTextBox;
        private ComboBox TwitchChannelSoundTextBox;
        private ComboBox TwitchSubscriptionSoundTextBox;
        private ComboBox TwitchCheeringSoundTextBox;
        private GroupBox TwitchResponseSettings;
        private TextBox TwitchResponseToChatDelayTextBox;
        private Label label8;
        private CheckBox TwitchResponseToChatCheckBox;
        private RadioButton TwitchSubscriptionTTSResponseOnlyRadioButton;
        private RadioButton TwitchSubscriptionTTSEverythingRadioButton;
        private RadioButton TwitchChannelPointTTSResponseOnlyRadioButton;
        private RadioButton TwitchChannelPointTTSEverythingRadioButton;
        private RadioButton TwitchCheeringTTSResponseOnlyRadioButton;
        private RadioButton TwitchCheeringTTSEverythingRadioButton;
        private RadioButton TwitchChatTTSResponseOnlyRadioButton;
        private RadioButton TwitchChatTTSEverythingRadioButton;
        private ToolStripMenuItem logfileDirectoryToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
    }
}

