using Gma.System.MouseKeyHook;
using Gma.System.MouseKeyHook.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            // Note: for the application hook, use the Hook.AppEvents() instead
            m_GlobalHook = Hook.GlobalEvents();

            //m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            //m_GlobalHook.KeyPress += GlobalHookKeyPress;
            m_GlobalHook.KeyDown += GlobalHookKeyDown;
        }

        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("KeyPress: \t{0}", e.KeyChar);

        }

        List<int> KeyCombo = new List<int>();
        public List<int> ReturnValue1 { get; set; }
        private void GlobalHookKeyDown(object sender, KeyEventArgs e)
        {

            //only allow 1 ascii char
            if (e.KeyValue < 128 && !KeyCombo.Contains(e.KeyValue))
            {
                KeyCombo.Add(e.KeyValue);
                CombiText.Text += (char)e.KeyValue;
                this.ReturnValue1 = KeyCombo;
                Unsubscribe();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            if (e.KeyValue == 164 && !KeyCombo.Contains(e.KeyValue))
            {
                KeyCombo.Add(e.KeyValue);
               // CombiText.Text += " ALT ";
            } else if (e.KeyValue == 162 && !KeyCombo.Contains(e.KeyValue))
            {
                KeyCombo.Add(e.KeyValue);
               // CombiText.Text += " CTRL ";
            } else if (e.KeyValue == 160 && !KeyCombo.Contains(e.KeyValue))
            {
                KeyCombo.Add(e.KeyValue);
               // CombiText.Text += " SHIFT ";
            }
            //Escape
            if (e.KeyValue == 27)
            {
                Unsubscribe();
                this.Close();
            }

        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp);
            CombiText.Text += e.Button.ToString();
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress -= GlobalHookKeyPress;
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
