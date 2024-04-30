namespace BanterBrain_Buddy
{
    partial class HotkeyForm
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
            HotkeyPressed = new System.Windows.Forms.Label();
            CombiText = new System.Windows.Forms.Label();
            DontPingTextBox = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // HotkeyPressed
            // 
            HotkeyPressed.AutoSize = true;
            HotkeyPressed.Location = new System.Drawing.Point(45, 9);
            HotkeyPressed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            HotkeyPressed.Name = "HotkeyPressed";
            HotkeyPressed.Size = new System.Drawing.Size(233, 15);
            HotkeyPressed.TabIndex = 2;
            HotkeyPressed.Text = "Press key combination. Press ESC to cancel";
            // 
            // CombiText
            // 
            CombiText.AutoSize = true;
            CombiText.Location = new System.Drawing.Point(85, 43);
            CombiText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            CombiText.Name = "CombiText";
            CombiText.Size = new System.Drawing.Size(0, 15);
            CombiText.TabIndex = 1;
            // 
            // DontPingTextBox
            // 
            DontPingTextBox.Location = new System.Drawing.Point(218, 31);
            DontPingTextBox.Name = "DontPingTextBox";
            DontPingTextBox.Size = new System.Drawing.Size(100, 23);
            DontPingTextBox.TabIndex = 0;
            DontPingTextBox.Visible = false;
            // 
            // HotkeyForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(342, 66);
            Controls.Add(DontPingTextBox);
            Controls.Add(CombiText);
            Controls.Add(HotkeyPressed);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "HotkeyForm";
            Text = "Hotkey setting";
            FormClosing += HotkeyForm_FormClosing;
            Shown += HotkeyForm_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label HotkeyPressed;
        private System.Windows.Forms.Label CombiText;
        private System.Windows.Forms.TextBox DontPingTextBox;
    }
}