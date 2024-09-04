using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BanterBrain_Buddy
{
    public partial class WordFilterForm : Form
    {
        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [SupportedOSPlatform("windows6.1")]
        public WordFilterForm()
        {
            InitializeComponent();
            _bBBlog.Info("WordFilterForm constructor");
            LoadFilteredWords();
        }

        [SupportedOSPlatform("windows6.1")]
        private void LoadFilteredWords()
        {
            _bBBlog.Info("LoadFilteredWords");
            //load the bad words from the file
            //we should do the file creation in the main form!

            var tmpFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BanterBrain\\WordFilter.txt";
            if (!File.Exists(tmpFile))
            {
                _bBBlog.Error($"Word Filter file not found, creating it");
                //File.Create(tmpFile);
                File.WriteAllText(tmpFile, "fucker,motherfucker,asshole,nigger,nigga,loser,retard,moron,cunt,slut,fag,whore");
            }
            //load the file into the textbox
            BadWordFilterBox.Text = File.ReadAllText(tmpFile);
            //to prevent anything from being automatically selected
            BadWordFilterBox.SelectionStart = BadWordFilterBox.Text.Length;

        }

        [SupportedOSPlatform("windows6.1")]
        private void BadWordFilterBox_TextChanged_1(object sender, EventArgs e)
        {
        }

        [SupportedOSPlatform("windows6.1")]
        private void WordFilterForm_FormClosing(object sender, EventArgs e)
        {
            _bBBlog.Info("WordFilterForm form closing we need to save!");
            //first of all we need to load the text into a string we can manipulate
            string badWords = BadWordFilterBox.Text;

            //now we need to make sure that spaces before or after a comma are removed, thanks copilot for the regex
            Regex.Replace(badWords, @"\s*,\s*", ",");

            //remove any empty stuff at the end
            badWords = badWords.TrimEnd();
            //now one last check, if the last character is a comma, remove it
            if (badWords.EndsWith(","))
            {
                badWords = badWords.Remove(badWords.Length - 1);
            }
            
            var tmpFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BanterBrain\\WordFilter.txt";
            File.WriteAllText(tmpFile, badWords);
        }

        [SupportedOSPlatform("windows6.1")]
        private void BadWordFilterBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                //this will prevent the enter key from being added to the textbox
                e.Handled = true;
            }
        }
    }
}
