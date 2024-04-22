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
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Speech-to-Text");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Text-to-Speech");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Speaker");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Voice settings", new System.Windows.Forms.TreeNode[] { treeNode1, treeNode2, treeNode3, treeNode4 });
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("OpenAI ChatGPT");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("LLM settings", new System.Windows.Forms.TreeNode[] { treeNode6 });
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Twitch connection");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Twitch triggers");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Twitch ", new System.Windows.Forms.TreeNode[] { treeNode8, treeNode9 });
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Streaming settings", new System.Windows.Forms.TreeNode[] { treeNode10 });
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Persona's");
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            treeView1 = new System.Windows.Forms.TreeView();
            panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
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
            splitContainer1.Panel1.Controls.Add(treeView1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Size = new System.Drawing.Size(800, 450);
            splitContainer1.SplitterDistance = 204;
            splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            treeView1.Location = new System.Drawing.Point(0, 0);
            treeView1.Name = "treeView1";
            treeNode1.Name = "Microphone";
            treeNode1.Text = "Microphone";
            treeNode2.Name = "SpeechToText";
            treeNode2.Text = "Speech-to-Text";
            treeNode3.Name = "TextToSpeech";
            treeNode3.Text = "Text-to-Speech";
            treeNode4.Name = "SpeakerSettings";
            treeNode4.Text = "Speaker";
            treeNode5.Name = "VoiceSettings";
            treeNode5.Text = "Voice settings";
            treeNode6.Name = "OpenAIChatGPT";
            treeNode6.Text = "OpenAI ChatGPT";
            treeNode7.Name = "LLMSettings";
            treeNode7.Text = "LLM settings";
            treeNode8.Name = "TwitchConnectionSettings";
            treeNode8.Text = "Twitch connection";
            treeNode9.Name = "TwitchTriggerSettings";
            treeNode9.Text = "Twitch triggers";
            treeNode10.Name = "TwitchSettings";
            treeNode10.Text = "Twitch ";
            treeNode11.Name = "Streaming Settings";
            treeNode11.Text = "Streaming settings";
            treeNode12.Name = "PersonasSettings";
            treeNode12.Text = "Persona's";
            treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { treeNode5, treeNode7, treeNode11, treeNode12 });
            treeView1.PathSeparator = "";
            treeView1.Size = new System.Drawing.Size(204, 450);
            treeView1.TabIndex = 0;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // panel1
            // 
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(592, 450);
            panel1.TabIndex = 0;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(splitContainer1);
            Name = "SettingsForm";
            Text = "Settings";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel1;
    }
}