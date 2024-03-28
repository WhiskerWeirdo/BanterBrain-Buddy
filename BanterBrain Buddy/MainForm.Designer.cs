﻿using System.Windows.Forms;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BBB));
            this.SettingsTab = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.MicroPhoneHotkeyLabel = new System.Windows.Forms.Label();
            this.MicrophoneHotkeyEditbox = new System.Windows.Forms.TextBox();
            this.VoiceInputLabel = new System.Windows.Forms.Label();
            this.SoundInputDevices = new System.Windows.Forms.ComboBox();
            this.MicrophoneHotkeySet = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TTSOutputVoiceOptions = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TTSOutputVoice = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TTSTestTextBox = new System.Windows.Forms.TextBox();
            this.TTSTestButton = new System.Windows.Forms.Button();
            this.TTSAudioOutputComboBox = new System.Windows.Forms.ComboBox();
            this.TTSProviderComboBox = new System.Windows.Forms.ComboBox();
            this.TTSAudioOutputLabel = new System.Windows.Forms.Label();
            this.TTSProviderLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LLMModelComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LLMTestOutputbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LLMRoleTextBox = new System.Windows.Forms.TextBox();
            this.GPTTestButton = new System.Windows.Forms.Button();
            this.LLMAPIKeyTextBox = new System.Windows.Forms.TextBox();
            this.GPTAPIKeyLabel = new System.Windows.Forms.Label();
            this.LLMProviderComboBox = new System.Windows.Forms.ComboBox();
            this.GPTProviderLabel = new System.Windows.Forms.Label();
            this.STTGroupBox = new System.Windows.Forms.GroupBox();
            this.STTHintText = new System.Windows.Forms.Label();
            this.STTRegionEditbox = new System.Windows.Forms.TextBox();
            this.STTRegionLabel = new System.Windows.Forms.Label();
            this.STTAPIKeyLabel = new System.Windows.Forms.Label();
            this.STTAPIKeyEditbox = new System.Windows.Forms.TextBox();
            this.STTTestButton = new System.Windows.Forms.Button();
            this.STTProviderLabel = new System.Windows.Forms.Label();
            this.STTProviderBox = new System.Windows.Forms.ComboBox();
            this.STTTestOutput = new System.Windows.Forms.TextBox();
            this.MainTab = new System.Windows.Forms.TabPage();
            this.TwitchStatusTextBox = new System.Windows.Forms.TextBox();
            this.TwitchStatusLabel = new System.Windows.Forms.Label();
            this.TextLog = new System.Windows.Forms.TextBox();
            this.ProgramFlowTest = new System.Windows.Forms.Button();
            this.BBBTabs = new System.Windows.Forms.TabControl();
            this.LLMTab = new System.Windows.Forms.TabPage();
            this.StreaminSettingsTab = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.TwitchTriggerSettings = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.TwitchNeedsSubscriber = new System.Windows.Forms.CheckBox();
            this.TwitchNeedsFollower = new System.Windows.Forms.CheckBox();
            this.TwitchCommandTrigger = new System.Windows.Forms.TextBox();
            this.TwitchCommandTriggerLabel = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.TwitchTestButton = new System.Windows.Forms.Button();
            this.TwitchChannel = new System.Windows.Forms.TextBox();
            this.TwitchAccessToken = new System.Windows.Forms.TextBox();
            this.TwitchUsername = new System.Windows.Forms.TextBox();
            this.TwitchChannelNameLabel = new System.Windows.Forms.Label();
            this.TwitchAccesstokenLabel = new System.Windows.Forms.Label();
            this.TwitchUsernameLabel = new System.Windows.Forms.Label();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.githubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.TwitchAuthorizeButton = new System.Windows.Forms.Button();
            this.SettingsTab.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.STTGroupBox.SuspendLayout();
            this.MainTab.SuspendLayout();
            this.BBBTabs.SuspendLayout();
            this.StreaminSettingsTab.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.TwitchTriggerSettings.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SettingsTab
            // 
            resources.ApplyResources(this.SettingsTab, "SettingsTab");
            this.SettingsTab.Controls.Add(this.groupBox3);
            this.SettingsTab.Controls.Add(this.groupBox2);
            this.SettingsTab.Controls.Add(this.groupBox1);
            this.SettingsTab.Controls.Add(this.STTGroupBox);
            this.SettingsTab.Name = "SettingsTab";
            this.SettingsTab.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.MicroPhoneHotkeyLabel);
            this.groupBox3.Controls.Add(this.MicrophoneHotkeyEditbox);
            this.groupBox3.Controls.Add(this.VoiceInputLabel);
            this.groupBox3.Controls.Add(this.SoundInputDevices);
            this.groupBox3.Controls.Add(this.MicrophoneHotkeySet);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // MicroPhoneHotkeyLabel
            // 
            resources.ApplyResources(this.MicroPhoneHotkeyLabel, "MicroPhoneHotkeyLabel");
            this.MicroPhoneHotkeyLabel.Name = "MicroPhoneHotkeyLabel";
            // 
            // MicrophoneHotkeyEditbox
            // 
            resources.ApplyResources(this.MicrophoneHotkeyEditbox, "MicrophoneHotkeyEditbox");
            this.MicrophoneHotkeyEditbox.Name = "MicrophoneHotkeyEditbox";
            this.MicrophoneHotkeyEditbox.ReadOnly = true;
            // 
            // VoiceInputLabel
            // 
            resources.ApplyResources(this.VoiceInputLabel, "VoiceInputLabel");
            this.VoiceInputLabel.Name = "VoiceInputLabel";
            // 
            // SoundInputDevices
            // 
            this.SoundInputDevices.FormattingEnabled = true;
            resources.ApplyResources(this.SoundInputDevices, "SoundInputDevices");
            this.SoundInputDevices.Name = "SoundInputDevices";
            // 
            // MicrophoneHotkeySet
            // 
            resources.ApplyResources(this.MicrophoneHotkeySet, "MicrophoneHotkeySet");
            this.MicrophoneHotkeySet.Name = "MicrophoneHotkeySet";
            this.MicrophoneHotkeySet.UseVisualStyleBackColor = true;
            this.MicrophoneHotkeySet.Click += new System.EventHandler(this.MicrophoneHotkeySet_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TTSOutputVoiceOptions);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.TTSOutputVoice);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.TTSTestTextBox);
            this.groupBox2.Controls.Add(this.TTSTestButton);
            this.groupBox2.Controls.Add(this.TTSAudioOutputComboBox);
            this.groupBox2.Controls.Add(this.TTSProviderComboBox);
            this.groupBox2.Controls.Add(this.TTSAudioOutputLabel);
            this.groupBox2.Controls.Add(this.TTSProviderLabel);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // TTSOutputVoiceOptions
            // 
            this.TTSOutputVoiceOptions.FormattingEnabled = true;
            resources.ApplyResources(this.TTSOutputVoiceOptions, "TTSOutputVoiceOptions");
            this.TTSOutputVoiceOptions.Name = "TTSOutputVoiceOptions";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // TTSOutputVoice
            // 
            this.TTSOutputVoice.FormattingEnabled = true;
            resources.ApplyResources(this.TTSOutputVoice, "TTSOutputVoice");
            this.TTSOutputVoice.Name = "TTSOutputVoice";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // TTSTestTextBox
            // 
            resources.ApplyResources(this.TTSTestTextBox, "TTSTestTextBox");
            this.TTSTestTextBox.Name = "TTSTestTextBox";
            this.TTSTestTextBox.ReadOnly = true;
            // 
            // TTSTestButton
            // 
            resources.ApplyResources(this.TTSTestButton, "TTSTestButton");
            this.TTSTestButton.Name = "TTSTestButton";
            this.TTSTestButton.UseVisualStyleBackColor = true;
            this.TTSTestButton.Click += new System.EventHandler(this.TTSTestButton_Click);
            // 
            // TTSAudioOutputComboBox
            // 
            this.TTSAudioOutputComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.TTSAudioOutputComboBox, "TTSAudioOutputComboBox");
            this.TTSAudioOutputComboBox.Name = "TTSAudioOutputComboBox";
            // 
            // TTSProviderComboBox
            // 
            this.TTSProviderComboBox.FormattingEnabled = true;
            this.TTSProviderComboBox.Items.AddRange(new object[] {
            resources.GetString("TTSProviderComboBox.Items"),
            resources.GetString("TTSProviderComboBox.Items1"),
            resources.GetString("TTSProviderComboBox.Items2")});
            resources.ApplyResources(this.TTSProviderComboBox, "TTSProviderComboBox");
            this.TTSProviderComboBox.Name = "TTSProviderComboBox";
            // 
            // TTSAudioOutputLabel
            // 
            resources.ApplyResources(this.TTSAudioOutputLabel, "TTSAudioOutputLabel");
            this.TTSAudioOutputLabel.Name = "TTSAudioOutputLabel";
            // 
            // TTSProviderLabel
            // 
            resources.ApplyResources(this.TTSProviderLabel, "TTSProviderLabel");
            this.TTSProviderLabel.Name = "TTSProviderLabel";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LLMModelComboBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.LLMTestOutputbox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.LLMRoleTextBox);
            this.groupBox1.Controls.Add(this.GPTTestButton);
            this.groupBox1.Controls.Add(this.LLMAPIKeyTextBox);
            this.groupBox1.Controls.Add(this.GPTAPIKeyLabel);
            this.groupBox1.Controls.Add(this.LLMProviderComboBox);
            this.groupBox1.Controls.Add(this.GPTProviderLabel);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // LLMModelComboBox
            // 
            this.LLMModelComboBox.FormattingEnabled = true;
            this.LLMModelComboBox.Items.AddRange(new object[] {
            resources.GetString("LLMModelComboBox.Items"),
            resources.GetString("LLMModelComboBox.Items1")});
            resources.ApplyResources(this.LLMModelComboBox, "LLMModelComboBox");
            this.LLMModelComboBox.Name = "LLMModelComboBox";
            this.LLMModelComboBox.UseWaitCursor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // LLMTestOutputbox
            // 
            resources.ApplyResources(this.LLMTestOutputbox, "LLMTestOutputbox");
            this.LLMTestOutputbox.Name = "LLMTestOutputbox";
            this.LLMTestOutputbox.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // LLMRoleTextBox
            // 
            resources.ApplyResources(this.LLMRoleTextBox, "LLMRoleTextBox");
            this.LLMRoleTextBox.Name = "LLMRoleTextBox";
            this.LLMRoleTextBox.TabStop = false;
            // 
            // GPTTestButton
            // 
            resources.ApplyResources(this.GPTTestButton, "GPTTestButton");
            this.GPTTestButton.Name = "GPTTestButton";
            this.GPTTestButton.UseVisualStyleBackColor = true;
            this.GPTTestButton.Click += new System.EventHandler(this.GPTTestButton_Click);
            // 
            // LLMAPIKeyTextBox
            // 
            resources.ApplyResources(this.LLMAPIKeyTextBox, "LLMAPIKeyTextBox");
            this.LLMAPIKeyTextBox.Name = "LLMAPIKeyTextBox";
            // 
            // GPTAPIKeyLabel
            // 
            resources.ApplyResources(this.GPTAPIKeyLabel, "GPTAPIKeyLabel");
            this.GPTAPIKeyLabel.Name = "GPTAPIKeyLabel";
            // 
            // LLMProviderComboBox
            // 
            this.LLMProviderComboBox.FormattingEnabled = true;
            this.LLMProviderComboBox.Items.AddRange(new object[] {
            resources.GetString("LLMProviderComboBox.Items"),
            resources.GetString("LLMProviderComboBox.Items1")});
            resources.ApplyResources(this.LLMProviderComboBox, "LLMProviderComboBox");
            this.LLMProviderComboBox.Name = "LLMProviderComboBox";
            // 
            // GPTProviderLabel
            // 
            resources.ApplyResources(this.GPTProviderLabel, "GPTProviderLabel");
            this.GPTProviderLabel.Name = "GPTProviderLabel";
            // 
            // STTGroupBox
            // 
            resources.ApplyResources(this.STTGroupBox, "STTGroupBox");
            this.STTGroupBox.Controls.Add(this.STTHintText);
            this.STTGroupBox.Controls.Add(this.STTRegionEditbox);
            this.STTGroupBox.Controls.Add(this.STTRegionLabel);
            this.STTGroupBox.Controls.Add(this.STTAPIKeyLabel);
            this.STTGroupBox.Controls.Add(this.STTAPIKeyEditbox);
            this.STTGroupBox.Controls.Add(this.STTTestButton);
            this.STTGroupBox.Controls.Add(this.STTProviderLabel);
            this.STTGroupBox.Controls.Add(this.STTProviderBox);
            this.STTGroupBox.Controls.Add(this.STTTestOutput);
            this.STTGroupBox.Name = "STTGroupBox";
            this.STTGroupBox.TabStop = false;
            // 
            // STTHintText
            // 
            resources.ApplyResources(this.STTHintText, "STTHintText");
            this.STTHintText.Name = "STTHintText";
            // 
            // STTRegionEditbox
            // 
            resources.ApplyResources(this.STTRegionEditbox, "STTRegionEditbox");
            this.STTRegionEditbox.Name = "STTRegionEditbox";
            // 
            // STTRegionLabel
            // 
            resources.ApplyResources(this.STTRegionLabel, "STTRegionLabel");
            this.STTRegionLabel.Name = "STTRegionLabel";
            // 
            // STTAPIKeyLabel
            // 
            resources.ApplyResources(this.STTAPIKeyLabel, "STTAPIKeyLabel");
            this.STTAPIKeyLabel.Name = "STTAPIKeyLabel";
            // 
            // STTAPIKeyEditbox
            // 
            resources.ApplyResources(this.STTAPIKeyEditbox, "STTAPIKeyEditbox");
            this.STTAPIKeyEditbox.Name = "STTAPIKeyEditbox";
            // 
            // STTTestButton
            // 
            resources.ApplyResources(this.STTTestButton, "STTTestButton");
            this.STTTestButton.Name = "STTTestButton";
            this.STTTestButton.UseVisualStyleBackColor = true;
            this.STTTestButton.Click += new System.EventHandler(this.STTTestButton_Click);
            // 
            // STTProviderLabel
            // 
            resources.ApplyResources(this.STTProviderLabel, "STTProviderLabel");
            this.STTProviderLabel.Name = "STTProviderLabel";
            // 
            // STTProviderBox
            // 
            this.STTProviderBox.FormattingEnabled = true;
            this.STTProviderBox.Items.AddRange(new object[] {
            resources.GetString("STTProviderBox.Items"),
            resources.GetString("STTProviderBox.Items1"),
            resources.GetString("STTProviderBox.Items2"),
            resources.GetString("STTProviderBox.Items3"),
            resources.GetString("STTProviderBox.Items4")});
            resources.ApplyResources(this.STTProviderBox, "STTProviderBox");
            this.STTProviderBox.Name = "STTProviderBox";
            this.STTProviderBox.SelectedIndexChanged += new System.EventHandler(this.STTProviderBox_SelectedIndexChanged);
            // 
            // STTTestOutput
            // 
            resources.ApplyResources(this.STTTestOutput, "STTTestOutput");
            this.STTTestOutput.Name = "STTTestOutput";
            this.STTTestOutput.ReadOnly = true;
            // 
            // MainTab
            // 
            this.MainTab.Controls.Add(this.TwitchStatusTextBox);
            this.MainTab.Controls.Add(this.TwitchStatusLabel);
            this.MainTab.Controls.Add(this.TextLog);
            this.MainTab.Controls.Add(this.ProgramFlowTest);
            resources.ApplyResources(this.MainTab, "MainTab");
            this.MainTab.Name = "MainTab";
            this.MainTab.UseVisualStyleBackColor = true;
            // 
            // TwitchStatusTextBox
            // 
            resources.ApplyResources(this.TwitchStatusTextBox, "TwitchStatusTextBox");
            this.TwitchStatusTextBox.Name = "TwitchStatusTextBox";
            this.TwitchStatusTextBox.ReadOnly = true;
            // 
            // TwitchStatusLabel
            // 
            resources.ApplyResources(this.TwitchStatusLabel, "TwitchStatusLabel");
            this.TwitchStatusLabel.Name = "TwitchStatusLabel";
            // 
            // TextLog
            // 
            this.TextLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.TextLog, "TextLog");
            this.TextLog.Name = "TextLog";
            // 
            // ProgramFlowTest
            // 
            resources.ApplyResources(this.ProgramFlowTest, "ProgramFlowTest");
            this.ProgramFlowTest.Name = "ProgramFlowTest";
            this.ProgramFlowTest.UseVisualStyleBackColor = true;
            this.ProgramFlowTest.Click += new System.EventHandler(this.ProgramFlowTest_Click);
            // 
            // BBBTabs
            // 
            resources.ApplyResources(this.BBBTabs, "BBBTabs");
            this.BBBTabs.Controls.Add(this.MainTab);
            this.BBBTabs.Controls.Add(this.SettingsTab);
            this.BBBTabs.Controls.Add(this.LLMTab);
            this.BBBTabs.Controls.Add(this.StreaminSettingsTab);
            this.BBBTabs.Name = "BBBTabs";
            this.BBBTabs.SelectedIndex = 0;
            // 
            // LLMTab
            // 
            resources.ApplyResources(this.LLMTab, "LLMTab");
            this.LLMTab.Name = "LLMTab";
            this.LLMTab.UseVisualStyleBackColor = true;
            // 
            // StreaminSettingsTab
            // 
            this.StreaminSettingsTab.Controls.Add(this.groupBox4);
            resources.ApplyResources(this.StreaminSettingsTab, "StreaminSettingsTab");
            this.StreaminSettingsTab.Name = "StreaminSettingsTab";
            this.StreaminSettingsTab.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.TwitchAuthorizeButton);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.TwitchTriggerSettings);
            this.groupBox4.Controls.Add(this.checkBox1);
            this.groupBox4.Controls.Add(this.TwitchTestButton);
            this.groupBox4.Controls.Add(this.TwitchChannel);
            this.groupBox4.Controls.Add(this.TwitchAccessToken);
            this.groupBox4.Controls.Add(this.TwitchUsername);
            this.groupBox4.Controls.Add(this.TwitchChannelNameLabel);
            this.groupBox4.Controls.Add(this.TwitchAccesstokenLabel);
            this.groupBox4.Controls.Add(this.TwitchUsernameLabel);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.checkBox4);
            this.groupBox6.Controls.Add(this.checkBox3);
            this.groupBox6.Controls.Add(this.checkBox2);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // checkBox4
            // 
            resources.ApplyResources(this.checkBox4, "checkBox4");
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            resources.ApplyResources(this.checkBox3, "checkBox3");
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            resources.ApplyResources(this.checkBox2, "checkBox2");
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.textBox1);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // TwitchTriggerSettings
            // 
            this.TwitchTriggerSettings.Controls.Add(this.label6);
            this.TwitchTriggerSettings.Controls.Add(this.textBox2);
            this.TwitchTriggerSettings.Controls.Add(this.TwitchNeedsSubscriber);
            this.TwitchTriggerSettings.Controls.Add(this.TwitchNeedsFollower);
            this.TwitchTriggerSettings.Controls.Add(this.TwitchCommandTrigger);
            this.TwitchTriggerSettings.Controls.Add(this.TwitchCommandTriggerLabel);
            resources.ApplyResources(this.TwitchTriggerSettings, "TwitchTriggerSettings");
            this.TwitchTriggerSettings.Name = "TwitchTriggerSettings";
            this.TwitchTriggerSettings.TabStop = false;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            // 
            // TwitchNeedsSubscriber
            // 
            resources.ApplyResources(this.TwitchNeedsSubscriber, "TwitchNeedsSubscriber");
            this.TwitchNeedsSubscriber.Name = "TwitchNeedsSubscriber";
            this.TwitchNeedsSubscriber.UseVisualStyleBackColor = true;
            // 
            // TwitchNeedsFollower
            // 
            resources.ApplyResources(this.TwitchNeedsFollower, "TwitchNeedsFollower");
            this.TwitchNeedsFollower.Name = "TwitchNeedsFollower";
            this.TwitchNeedsFollower.UseVisualStyleBackColor = true;
            // 
            // TwitchCommandTrigger
            // 
            resources.ApplyResources(this.TwitchCommandTrigger, "TwitchCommandTrigger");
            this.TwitchCommandTrigger.Name = "TwitchCommandTrigger";
            // 
            // TwitchCommandTriggerLabel
            // 
            resources.ApplyResources(this.TwitchCommandTriggerLabel, "TwitchCommandTriggerLabel");
            this.TwitchCommandTriggerLabel.Name = "TwitchCommandTriggerLabel";
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // TwitchTestButton
            // 
            resources.ApplyResources(this.TwitchTestButton, "TwitchTestButton");
            this.TwitchTestButton.Name = "TwitchTestButton";
            this.TwitchTestButton.UseVisualStyleBackColor = true;
            this.TwitchTestButton.Click += new System.EventHandler(this.TwitchTestButton_Click);
            // 
            // TwitchChannel
            // 
            resources.ApplyResources(this.TwitchChannel, "TwitchChannel");
            this.TwitchChannel.Name = "TwitchChannel";
            // 
            // TwitchAccessToken
            // 
            resources.ApplyResources(this.TwitchAccessToken, "TwitchAccessToken");
            this.TwitchAccessToken.Name = "TwitchAccessToken";
            // 
            // TwitchUsername
            // 
            resources.ApplyResources(this.TwitchUsername, "TwitchUsername");
            this.TwitchUsername.Name = "TwitchUsername";
            // 
            // TwitchChannelNameLabel
            // 
            resources.ApplyResources(this.TwitchChannelNameLabel, "TwitchChannelNameLabel");
            this.TwitchChannelNameLabel.Name = "TwitchChannelNameLabel";
            // 
            // TwitchAccesstokenLabel
            // 
            resources.ApplyResources(this.TwitchAccesstokenLabel, "TwitchAccesstokenLabel");
            this.TwitchAccesstokenLabel.Name = "TwitchAccesstokenLabel";
            // 
            // TwitchUsernameLabel
            // 
            resources.ApplyResources(this.TwitchUsernameLabel, "TwitchUsernameLabel");
            this.TwitchUsernameLabel.Name = "TwitchUsernameLabel";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            resources.ApplyResources(this.ExitToolStripMenuItem, "ExitToolStripMenuItem");
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.githubToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // githubToolStripMenuItem
            // 
            this.githubToolStripMenuItem.Name = "githubToolStripMenuItem";
            resources.ApplyResources(this.githubToolStripMenuItem, "githubToolStripMenuItem");
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // TwitchAuthorizeButton
            // 
            resources.ApplyResources(this.TwitchAuthorizeButton, "TwitchAuthorizeButton");
            this.TwitchAuthorizeButton.Name = "TwitchAuthorizeButton";
            this.TwitchAuthorizeButton.UseVisualStyleBackColor = true;
            this.TwitchAuthorizeButton.Click += new System.EventHandler(this.TwitchAuthorizeButton_Click);
            // 
            // BBB
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BBBTabs);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "BBB";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BBB_FormClosing);
            this.SettingsTab.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.STTGroupBox.ResumeLayout(false);
            this.STTGroupBox.PerformLayout();
            this.MainTab.ResumeLayout(false);
            this.MainTab.PerformLayout();
            this.BBBTabs.ResumeLayout(false);
            this.StreaminSettingsTab.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.TwitchTriggerSettings.ResumeLayout(false);
            this.TwitchTriggerSettings.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TabPage SettingsTab;
        private GroupBox groupBox2;
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
        private GroupBox STTGroupBox;
        private TextBox STTRegionEditbox;
        private Label STTRegionLabel;
        private Label STTAPIKeyLabel;
        private TextBox STTAPIKeyEditbox;
        private Button STTTestButton;
        private Label STTProviderLabel;
        private ComboBox STTProviderBox;
        private TextBox STTTestOutput;
        private TabPage MainTab;
        private Button ProgramFlowTest;
        private TabControl BBBTabs;
        private ComboBox TTSOutputVoiceOptions;
        private Label label3;
        private ComboBox TTSOutputVoice;
        private Label label2;
        private TextBox TTSTestTextBox;
        private Button TTSTestButton;
        private ComboBox TTSProviderComboBox;
        private Label TTSAudioOutputLabel;
        private Label TTSProviderLabel;
        private GroupBox groupBox3;
        private Label MicroPhoneHotkeyLabel;
        private TextBox MicrophoneHotkeyEditbox;
        private Label VoiceInputLabel;
        private ComboBox SoundInputDevices;
        private Button MicrophoneHotkeySet;
        private TextBox TextLog;
        private Label STTHintText;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem ExitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem githubToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
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
        private Button TwitchTestButton;
        private CheckBox checkBox1;
        private TextBox TwitchStatusTextBox;
        private Label TwitchStatusLabel;
        private GroupBox groupBox5;
        private GroupBox TwitchTriggerSettings;
        private CheckBox TwitchNeedsSubscriber;
        private CheckBox TwitchNeedsFollower;
        private TextBox TwitchCommandTrigger;
        private Label TwitchCommandTriggerLabel;
        private GroupBox groupBox6;
        private CheckBox checkBox2;
        private Label label5;
        private TextBox textBox1;
        private Label label6;
        private TextBox textBox2;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private Button TwitchAuthorizeButton;
    }
}

