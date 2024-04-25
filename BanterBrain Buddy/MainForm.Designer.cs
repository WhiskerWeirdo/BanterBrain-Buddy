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
            SelectedPersonaComboBox = new ComboBox();
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
            TwitchGiftedSub = new CheckBox();
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
            fileToolStripMenuItem = new ToolStripMenuItem();
            ExitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            GithubToolStripMenuItem = new ToolStripMenuItem();
            DiscordToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            seToolStripMenuItem = new ToolStripMenuItem();
            BBBToolTip = new ToolTip(components);
            MainTab.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox7.SuspendLayout();
            BBBTabs.SuspendLayout();
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
            groupBox3.Controls.Add(SelectedPersonaComboBox);
            resources.ApplyResources(groupBox3, "groupBox3");
            groupBox3.Name = "groupBox3";
            groupBox3.TabStop = false;
            // 
            // SelectedPersonaComboBox
            // 
            SelectedPersonaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            SelectedPersonaComboBox.FormattingEnabled = true;
            resources.ApplyResources(SelectedPersonaComboBox, "SelectedPersonaComboBox");
            SelectedPersonaComboBox.Name = "SelectedPersonaComboBox";
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
            BBBTabs.Controls.Add(StreaminSettingsTab);
            BBBTabs.Name = "BBBTabs";
            BBBTabs.SelectedIndex = 0;
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
            TwitchChannelPointCheckBox.Click += TwitchChannelPointCheckBox_Click;
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
            groupBox6.Controls.Add(TwitchGiftedSub);
            groupBox6.Controls.Add(TwitchSubscribed);
            resources.ApplyResources(groupBox6, "groupBox6");
            groupBox6.Name = "groupBox6";
            groupBox6.TabStop = false;
            // 
            // TwitchGiftedSub
            // 
            resources.ApplyResources(TwitchGiftedSub, "TwitchGiftedSub");
            TwitchGiftedSub.Name = "TwitchGiftedSub";
            BBBToolTip.SetToolTip(TwitchGiftedSub, resources.GetString("TwitchGiftedSub.ToolTip"));
            TwitchGiftedSub.UseVisualStyleBackColor = true;
            TwitchGiftedSub.Click += TwitchGiftedSub_Click;
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
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, seToolStripMenuItem, helpToolStripMenuItem });
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Name = "menuStrip1";
            // 
            // seToolStripMenuItem
            // 
            seToolStripMenuItem.Name = "seToolStripMenuItem";
            resources.ApplyResources(seToolStripMenuItem, "seToolStripMenuItem");
            seToolStripMenuItem.Click += seToolStripMenuItem_Click;
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
        private Button TwitchAuthorizeButton;
        private GroupBox TwitchAPITestGroupBox;
        private TextBox TwitchTestSendText;
        private CheckBox TwitchSendTextCheckBox;
        private Button TwitchTestButton;
        private Button DisconnectTwitchButton;
        private CheckBox TwitchCheckAuthAtStartup;
        private CheckBox TwitchEnableCheckbox;
        private Label TwitchEventSubStatusLabel;
        private TextBox TwitchEventSubStatusTextBox;
        private Button EventSubTest;
        private CheckBox TwitchReadChatCheckBox;
        private GroupBox EventSubGroupbox;
        private GroupBox groupBox7;
        private CheckBox TwitchCheerCheckBox;
        private GroupBox groupBox8;
        private Label label9;
        private TextBox TwitchCustomRewardName;
        private CheckBox TwitchChannelPointCheckBox;
        private CheckBox TwitchMockEventSub;
        private ToolTip BBBToolTip;
        private ToolStripMenuItem seToolStripMenuItem;
        private GroupBox groupBox2;
        private ComboBox STTSelectedComboBox;
        private GroupBox groupBox3;
        private ComboBox SelectedPersonaComboBox;
    }
}

