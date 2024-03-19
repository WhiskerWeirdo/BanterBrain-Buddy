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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.githubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.SettingsTab = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.MicroPhoneHotkeyLabel = new System.Windows.Forms.Label();
            this.MicrophoneHotkeyEditbox = new System.Windows.Forms.TextBox();
            this.VoiceInputLabel = new System.Windows.Forms.Label();
            this.SoundInputDevices = new System.Windows.Forms.ComboBox();
            this.MicrophoneHotkeySet = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
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
            this.GPTAPIKeyTextBox = new System.Windows.Forms.TextBox();
            this.GPTAPIKeyLabel = new System.Windows.Forms.Label();
            this.GPTProviderComboBox = new System.Windows.Forms.ComboBox();
            this.GPTProviderLabel = new System.Windows.Forms.Label();
            this.STTGroupBox = new System.Windows.Forms.GroupBox();
            this.STTRegionEditbox = new System.Windows.Forms.TextBox();
            this.STTRegionLabel = new System.Windows.Forms.Label();
            this.STTAPIKeyLabel = new System.Windows.Forms.Label();
            this.STTAPIKeyEditbox = new System.Windows.Forms.TextBox();
            this.STTTestButton = new System.Windows.Forms.Button();
            this.STTProviderLabel = new System.Windows.Forms.Label();
            this.STTProviderBox = new System.Windows.Forms.ComboBox();
            this.STTTestOutput = new System.Windows.Forms.TextBox();
            this.MainTab = new System.Windows.Forms.TabPage();
            this.TextLog = new System.Windows.Forms.TextBox();
            this.ProgramFlowTest = new System.Windows.Forms.Button();
            this.BBBTabs = new System.Windows.Forms.TabControl();
            this.STTHintText = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SettingsTab.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.STTGroupBox.SuspendLayout();
            this.MainTab.SuspendLayout();
            this.BBBTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
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
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.comboBox1);
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
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox2, "comboBox2");
            this.comboBox2.Name = "comboBox2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.Name = "comboBox1";
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
            this.groupBox1.Controls.Add(this.GPTAPIKeyTextBox);
            this.groupBox1.Controls.Add(this.GPTAPIKeyLabel);
            this.groupBox1.Controls.Add(this.GPTProviderComboBox);
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
            // GPTAPIKeyTextBox
            // 
            resources.ApplyResources(this.GPTAPIKeyTextBox, "GPTAPIKeyTextBox");
            this.GPTAPIKeyTextBox.Name = "GPTAPIKeyTextBox";
            // 
            // GPTAPIKeyLabel
            // 
            resources.ApplyResources(this.GPTAPIKeyLabel, "GPTAPIKeyLabel");
            this.GPTAPIKeyLabel.Name = "GPTAPIKeyLabel";
            // 
            // GPTProviderComboBox
            // 
            this.GPTProviderComboBox.FormattingEnabled = true;
            this.GPTProviderComboBox.Items.AddRange(new object[] {
            resources.GetString("GPTProviderComboBox.Items"),
            resources.GetString("GPTProviderComboBox.Items1")});
            resources.ApplyResources(this.GPTProviderComboBox, "GPTProviderComboBox");
            this.GPTProviderComboBox.Name = "GPTProviderComboBox";
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
            this.MainTab.Controls.Add(this.TextLog);
            this.MainTab.Controls.Add(this.ProgramFlowTest);
            resources.ApplyResources(this.MainTab, "MainTab");
            this.MainTab.Name = "MainTab";
            this.MainTab.UseVisualStyleBackColor = true;
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
            this.BBBTabs.Name = "BBBTabs";
            this.BBBTabs.SelectedIndex = 0;
            // 
            // STTHintText
            // 
            resources.ApplyResources(this.STTHintText, "STTHintText");
            this.STTHintText.Name = "STTHintText";
            // 
            // BBB
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.BBBTabs);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "BBB";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem githubToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private TabPage SettingsTab;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private ComboBox LLMModelComboBox;
        private Label label4;
        private TextBox LLMTestOutputbox;
        private Label label1;
        private TextBox LLMRoleTextBox;
        private Button GPTTestButton;
        private TextBox GPTAPIKeyTextBox;
        private Label GPTAPIKeyLabel;
        private ComboBox GPTProviderComboBox;
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
        private ComboBox comboBox2;
        private Label label3;
        private ComboBox comboBox1;
        private Label label2;
        private TextBox TTSTestTextBox;
        private Button TTSTestButton;
        private ComboBox TTSAudioOutputComboBox;
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
    }
}

