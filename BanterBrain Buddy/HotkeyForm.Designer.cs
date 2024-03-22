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
            this.HotkeyPressed = new System.Windows.Forms.Label();
            this.CombiText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // HotkeyPressed
            // 
            this.HotkeyPressed.AutoSize = true;
            this.HotkeyPressed.Location = new System.Drawing.Point(40, 24);
            this.HotkeyPressed.Name = "HotkeyPressed";
            this.HotkeyPressed.Size = new System.Drawing.Size(216, 13);
            this.HotkeyPressed.TabIndex = 0;
            this.HotkeyPressed.Text = "Press key combination. Press ESC to cancel";
            // 
            // CombiText
            // 
            this.CombiText.AutoSize = true;
            this.CombiText.Location = new System.Drawing.Point(71, 76);
            this.CombiText.Name = "CombiText";
            this.CombiText.Size = new System.Drawing.Size(0, 13);
            this.CombiText.TabIndex = 1;
            // 
            // HotkeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 57);
            this.Controls.Add(this.CombiText);
            this.Controls.Add(this.HotkeyPressed);
            this.Name = "HotkeyForm";
            this.Text = "Hotkey setting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HotkeyForm_FormClosing);
            this.Shown += new System.EventHandler(this.HotkeyForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label HotkeyPressed;
        private System.Windows.Forms.Label CombiText;
    }
}