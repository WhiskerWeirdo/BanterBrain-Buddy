using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

/// <summary>
/// CODING RULES:
/// •	Local variables, private instance, static fields and method parameters should be camelCase.
/// •	Methods, constants, properties, events and classes should be PascalCase.
/// •	Global private instance fields should be in camelCase prefixed with an underscore.
/// </summary>

namespace BanterBrain_Buddy
{
    public partial class HotkeyForm : Form
    {
        public HotkeyForm()
        {
            InitializeComponent();
        }

        private IKeyboardMouseEvents m_GlobalHook;

        public void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.KeyDown += GlobalHookKeyDown;
        }

        List<Keys> KeyCombo = [];

        public List<Keys> ReturnValue1 { get; set; }
        private void GlobalHookKeyDown(object sender, KeyEventArgs e)
        {
            //TODO: ALT is a bad idea, so fix that sometime
            
            //only allow 1 ascii char
            if (e.KeyValue < 128 && !KeyCombo.Contains(e.KeyCode))
            {
                KeyCombo.Add(e.KeyCode);
                CombiText.Text += (char)e.KeyValue;
                this.ReturnValue1 = KeyCombo;
                Unsubscribe();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            if (!KeyCombo.Contains(e.KeyCode))
              KeyCombo.Add(e.KeyCode);

            //Escape
            if (e.KeyValue == 27)
            {
                this.ReturnValue1 = null;
                Unsubscribe();
                this.Close();
            }

        }

        public void Unsubscribe()
        {
            m_GlobalHook.KeyDown -= GlobalHookKeyDown;
            //It is recommened to dispose it
            m_GlobalHook.Dispose();
        }

        private void HotkeyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Just incase something left over.
            Unsubscribe();
        }

        private void HotkeyForm_Shown(object sender, EventArgs e)
        {
            Subscribe();
        }

    }
}
