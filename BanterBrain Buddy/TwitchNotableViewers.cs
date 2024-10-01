using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//alright here we add, edit and remove specific viewers. When added you can add a flavour text to be send to the LLM for a _viewer-customized response
namespace BanterBrain_Buddy
{
    [SupportedOSPlatform("windows10.0.10240")]
    public partial class TwitchNotableViewers : Form
    {

        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private TwitchNotableViewerClass _viewer;
        private List<TwitchNotableViewerClass> viewers = [];

        public TwitchNotableViewers()
        {
            InitializeComponent();
            TwitchNotableViewers_LoadFromFile();
            DisplayViewers();
        }

        //aight so first we load any previously saved viewers from the json file in format "Viewer name" : "Flavour text"
        //location of the file containing the saved viewers is %AppData%\BanterBrain Buddy\viewers.json
        //we make this public because we need to access it from the main form at the start of the program anyway
        [SupportedOSPlatform("windows10.0.10240")]
        public void TwitchNotableViewers_LoadFromFile()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BanterBrain\\viewers.json";
            //aight now load the viewers from the file into TwitchNotableViewerClass

            if (System.IO.File.Exists(path))
            {
                _bBBlog.Info("Viewers file found, loading viewers");
                string[] lines = System.IO.File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(':');
                    _viewer ??= new TwitchNotableViewerClass();
                    _viewer.ViewerName = parts[0];
                    _viewer.FlavourText = parts[1];
                    viewers.Add(_viewer);
                }
            }
            else
            {
                //we create the file if it doesn't exist
                _bBBlog.Info("Viewers file not found, creating new file");
                System.IO.File.Create(path);
            }
        }

        [SupportedOSPlatform("windows10.0.10240")]
        //So now we have the viewers loaded into the viewers list, we can display them in the listbox
        public void DisplayViewers()
        {
            if (viewers.Count > 0)
            {
                _bBBlog.Info("Adding viewers in the listbox");
                TwitchNotableViewersComboBox.Items.Clear();
                foreach (TwitchNotableViewerClass viewer in viewers)
                {
                    TwitchNotableViewersComboBox.Items.Add(viewer.ViewerName);
                }
            }
            else
            {
                _bBBlog.Info("No viewers found, lets add some!");
            }
        }

        [SupportedOSPlatform("windows10.0.10240")]
        //here we load the flavour text of the selected _viewer into the textbox so it can be edited or deleted when the combobox selection changes
        private void TwitchNotableViewersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _bBBlog.Info("Selected viewer changed, loading flavour text");
            TwitchFlavourTextBox.Text = "";
            //if there is a flavour text, load it into the textbox
            if (viewers[TwitchNotableViewersComboBox.SelectedIndex].FlavourText.Length > 1)
            {
                TwitchFlavourTextBox.Text = viewers[TwitchNotableViewersComboBox.SelectedIndex].FlavourText;
            }

        }


        //Here we add a _viewer to the list, first we check if it exists, if it does we dont do anything else we create a new _viewer and add it to the list
        //and add the flavour text. We also update the class with the new information.

        //here we remove a _viewer from the list, update the class and then save the class to the json file


    }
}
