﻿namespace BanterBrain_Buddy
{
    partial class WordFilterForm
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
            BadWordFilterBox = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // BadWordFilterBox
            // 
            BadWordFilterBox.Location = new System.Drawing.Point(12, 67);
            BadWordFilterBox.Multiline = true;
            BadWordFilterBox.Name = "BadWordFilterBox";
            BadWordFilterBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            BadWordFilterBox.Size = new System.Drawing.Size(398, 187);
            BadWordFilterBox.TabIndex = 0;
            BadWordFilterBox.TextChanged += BadWordFilterBox_TextChanged_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 17);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(250, 15);
            label1.TabIndex = 1;
            label1.Text = "Separate bad words or sentences by commas. ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 49);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(329, 15);
            label2.TabIndex = 2;
            label2.Text = "Messages that include a bad word will be ignored completely";
            // 
            // WordFilterForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(415, 266);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(BadWordFilterBox);
            Name = "WordFilterForm";
            Text = "Bad word filter";
            FormClosing += this.WordFilterForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox BadWordFilterBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}