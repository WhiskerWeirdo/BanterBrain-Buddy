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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BBB));
            this.SettingsTabs = new System.Windows.Forms.TabControl();
            this.VoiceTab = new System.Windows.Forms.TabPage();
            this.MicroPhoneHotkeyLabel = new System.Windows.Forms.Label();
            this.MicrophoneHotkeyEditbox = new System.Windows.Forms.TextBox();
            this.VoiceInputLabel = new System.Windows.Forms.Label();
            this.SoundInputDevices = new System.Windows.Forms.ComboBox();
            this.MicrophoneHotkeySet = new System.Windows.Forms.Button();
            this.SSTTab = new System.Windows.Forms.TabPage();
            this.STTTestOutput = new System.Windows.Forms.TextBox();
            this.STTRegionEditbox = new System.Windows.Forms.TextBox();
            this.STTRegionLabel = new System.Windows.Forms.Label();
            this.STTAPIKeyLabel = new System.Windows.Forms.Label();
            this.STTAPIKeyEditbox = new System.Windows.Forms.TextBox();
            this.STTTestButton = new System.Windows.Forms.Button();
            this.STTProviderLabel = new System.Windows.Forms.Label();
            this.STTProviderBox = new System.Windows.Forms.ComboBox();
            this.GPTTab = new System.Windows.Forms.TabPage();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.GPTTestButton = new System.Windows.Forms.Button();
            this.GPTAPIKeyTextBox = new System.Windows.Forms.TextBox();
            this.GPTAPIKeyLabel = new System.Windows.Forms.Label();
            this.GPTProviderComboBox = new System.Windows.Forms.ComboBox();
            this.GPTProviderLabel = new System.Windows.Forms.Label();
            this.TTSTab = new System.Windows.Forms.TabPage();
            this.TTSAudioOutputComboBox = new System.Windows.Forms.ComboBox();
            this.TTSProviderComboBox = new System.Windows.Forms.ComboBox();
            this.TTSAudioOutputLabel = new System.Windows.Forms.Label();
            this.TTSProviderLabel = new System.Windows.Forms.Label();
            this.TwitchTab = new System.Windows.Forms.TabPage();
            this.YoutubeTab = new System.Windows.Forms.TabPage();
            this.Live2DTab = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.githubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.SettingsTabs.SuspendLayout();
            this.VoiceTab.SuspendLayout();
            this.SSTTab.SuspendLayout();
            this.GPTTab.SuspendLayout();
            this.TTSTab.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SettingsTabs
            // 
            resources.ApplyResources(this.SettingsTabs, "SettingsTabs");
            this.SettingsTabs.Controls.Add(this.VoiceTab);
            this.SettingsTabs.Controls.Add(this.SSTTab);
            this.SettingsTabs.Controls.Add(this.GPTTab);
            this.SettingsTabs.Controls.Add(this.TTSTab);
            this.SettingsTabs.Controls.Add(this.TwitchTab);
            this.SettingsTabs.Controls.Add(this.YoutubeTab);
            this.SettingsTabs.Controls.Add(this.Live2DTab);
            this.SettingsTabs.Name = "SettingsTabs";
            this.SettingsTabs.SelectedIndex = 0;
            // 
            // VoiceTab
            // 
            this.VoiceTab.Controls.Add(this.MicroPhoneHotkeyLabel);
            this.VoiceTab.Controls.Add(this.MicrophoneHotkeyEditbox);
            this.VoiceTab.Controls.Add(this.VoiceInputLabel);
            this.VoiceTab.Controls.Add(this.SoundInputDevices);
            this.VoiceTab.Controls.Add(this.MicrophoneHotkeySet);
            resources.ApplyResources(this.VoiceTab, "VoiceTab");
            this.VoiceTab.Name = "VoiceTab";
            this.VoiceTab.UseVisualStyleBackColor = true;
            this.VoiceTab.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // MicroPhoneHotkeyLabel
            // 
            resources.ApplyResources(this.MicroPhoneHotkeyLabel, "MicroPhoneHotkeyLabel");
            this.MicroPhoneHotkeyLabel.Name = "MicroPhoneHotkeyLabel";
            this.MicroPhoneHotkeyLabel.Click += new System.EventHandler(this.label2_Click);
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
            this.VoiceInputLabel.Click += new System.EventHandler(this.label1_Click);
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
            this.MicrophoneHotkeySet.Click += new System.EventHandler(this.button1_Click);
            // 
            // SSTTab
            // 
            this.SSTTab.Controls.Add(this.STTTestOutput);
            this.SSTTab.Controls.Add(this.STTRegionEditbox);
            this.SSTTab.Controls.Add(this.STTRegionLabel);
            this.SSTTab.Controls.Add(this.STTAPIKeyLabel);
            this.SSTTab.Controls.Add(this.STTAPIKeyEditbox);
            this.SSTTab.Controls.Add(this.STTTestButton);
            this.SSTTab.Controls.Add(this.STTProviderLabel);
            this.SSTTab.Controls.Add(this.STTProviderBox);
            resources.ApplyResources(this.SSTTab, "SSTTab");
            this.SSTTab.Name = "SSTTab";
            this.SSTTab.UseVisualStyleBackColor = true;
            // 
            // STTTestOutput
            // 
            resources.ApplyResources(this.STTTestOutput, "STTTestOutput");
            this.STTTestOutput.Name = "STTTestOutput";
            this.STTTestOutput.ReadOnly = true;
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
            resources.GetString("STTProviderBox.Items3")});
            resources.ApplyResources(this.STTProviderBox, "STTProviderBox");
            this.STTProviderBox.Name = "STTProviderBox";
            this.STTProviderBox.SelectedIndexChanged += new System.EventHandler(this.SSTProviderBox_SelectedIndexChanged);
            // 
            // GPTTab
            // 
            this.GPTTab.Controls.Add(this.textBox2);
            this.GPTTab.Controls.Add(this.label1);
            this.GPTTab.Controls.Add(this.textBox1);
            this.GPTTab.Controls.Add(this.GPTTestButton);
            this.GPTTab.Controls.Add(this.GPTAPIKeyTextBox);
            this.GPTTab.Controls.Add(this.GPTAPIKeyLabel);
            this.GPTTab.Controls.Add(this.GPTProviderComboBox);
            this.GPTTab.Controls.Add(this.GPTProviderLabel);
            resources.ApplyResources(this.GPTTab, "GPTTab");
            this.GPTTab.Name = "GPTTab";
            this.GPTTab.UseVisualStyleBackColor = true;
            this.GPTTab.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // GPTTestButton
            // 
            resources.ApplyResources(this.GPTTestButton, "GPTTestButton");
            this.GPTTestButton.Name = "GPTTestButton";
            this.GPTTestButton.UseVisualStyleBackColor = true;
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
            // TTSTab
            // 
            this.TTSTab.Controls.Add(this.TTSAudioOutputComboBox);
            this.TTSTab.Controls.Add(this.TTSProviderComboBox);
            this.TTSTab.Controls.Add(this.TTSAudioOutputLabel);
            this.TTSTab.Controls.Add(this.TTSProviderLabel);
            resources.ApplyResources(this.TTSTab, "TTSTab");
            this.TTSTab.Name = "TTSTab";
            this.TTSTab.UseVisualStyleBackColor = true;
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
            this.TTSProviderLabel.Click += new System.EventHandler(this.label8_Click);
            // 
            // TwitchTab
            // 
            resources.ApplyResources(this.TwitchTab, "TwitchTab");
            this.TwitchTab.Name = "TwitchTab";
            this.TwitchTab.UseVisualStyleBackColor = true;
            // 
            // YoutubeTab
            // 
            resources.ApplyResources(this.YoutubeTab, "YoutubeTab");
            this.YoutubeTab.Name = "YoutubeTab";
            this.YoutubeTab.UseVisualStyleBackColor = true;
            // 
            // Live2DTab
            // 
            resources.ApplyResources(this.Live2DTab, "Live2DTab");
            this.Live2DTab.Name = "Live2DTab";
            this.Live2DTab.UseVisualStyleBackColor = true;
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
            // BBB
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.SettingsTabs);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "BBB";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SettingsTabs.ResumeLayout(false);
            this.VoiceTab.ResumeLayout(false);
            this.VoiceTab.PerformLayout();
            this.SSTTab.ResumeLayout(false);
            this.SSTTab.PerformLayout();
            this.GPTTab.ResumeLayout(false);
            this.GPTTab.PerformLayout();
            this.TTSTab.ResumeLayout(false);
            this.TTSTab.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl SettingsTabs;
        private System.Windows.Forms.TabPage VoiceTab;
        private System.Windows.Forms.TabPage SSTTab;
        private System.Windows.Forms.Button MicrophoneHotkeySet;
        private System.Windows.Forms.Label VoiceInputLabel;
        private System.Windows.Forms.ComboBox SoundInputDevices;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private TabPage GPTTab;
        private TabPage TTSTab;
        private TabPage TwitchTab;
        private Label MicroPhoneHotkeyLabel;
        private TextBox MicrophoneHotkeyEditbox;
        private Label STTProviderLabel;
        private ComboBox STTProviderBox;
        private TabPage YoutubeTab;
        private TabPage Live2DTab;
        private Button STTTestButton;
        private Label STTAPIKeyLabel;
        private TextBox STTAPIKeyEditbox;
        private TextBox STTRegionEditbox;
        private Label STTRegionLabel;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem githubToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private Button GPTTestButton;
        private TextBox GPTAPIKeyTextBox;
        private Label GPTAPIKeyLabel;
        private ComboBox GPTProviderComboBox;
        private Label GPTProviderLabel;
        private Label TTSProviderLabel;
        private Label TTSAudioOutputLabel;
        private ComboBox TTSProviderComboBox;
        private ComboBox TTSAudioOutputComboBox;
        private TextBox STTTestOutput;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private TextBox textBox2;
        private Label label1;
        private TextBox textBox1;
    }
}

