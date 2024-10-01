using Newtonsoft.Json;
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
using TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions;

//alright here we add, edit and remove specific viewers. When added you can add a flavour text to be send to the LLM for a _viewer-customized response
namespace BanterBrain_Buddy
{
    [SupportedOSPlatform("windows10.0.10240")]
    public partial class TwitchNotableViewers : Form
    {

        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private TwitchNotableViewerClass _viewer;
        private List<TwitchNotableViewerClass> viewers = [];
        private bool FlavourTextEdited = false;

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
                _bBBlog.Info("Viewers file found, loading viewers from json file");
                string json = System.IO.File.ReadAllText(path);
                viewers = JsonConvert.DeserializeObject<List<TwitchNotableViewerClass>>(json) ?? new List<TwitchNotableViewerClass>();
            }
            else
            {
                //we create the file if it doesn't exist
                _bBBlog.Info("Viewers file not found, creating new file");
                System.IO.File.Create(path).Dispose();
            }

            //we just loaded it, so nothing is changed!
            FlavourTextEdited = false;
            ViewerSaveButton.Enabled = false;
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
                _bBBlog.Info("No viewers found, so why not add some!");
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

        //here we save the current class to the json file
        [SupportedOSPlatform("windows10.0.10240")]
        public void TwitchNotableViewers_SaveToFile()
        {
            if (viewers.Count < 1)
            {
                _bBBlog.Info("No viewers to save.");
                return;
            }

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BanterBrain\\viewers.json";
            _bBBlog.Info("Saving viewers to file");
            // Serialize the list of viewers to JSON format
            string json = JsonConvert.SerializeObject(viewers, Formatting.Indented);

            // Write the JSON string to the file
            System.IO.File.WriteAllText(path, json);

        }


        private void ViewerAddButton_Click(object sender, EventArgs e)
        {
            if (FlavourTextEdited)
            {
                //first lets ask if they wanna save what they got since aparently its changed
            } else
            {
                //lets empty all the text box and combobox
            }

        }

        private void ViewerSaveButton_Click(object sender, EventArgs e)
        {
            if (!FlavourTextEdited)
            {
                _bBBlog.Info("No changes to save");
                return;
            }
            //first we check if the viewer already exists, set focus to the flavour text box
            if (viewers.Any(x => x.ViewerName == TwitchNotableViewersComboBox.Text))
            {
                _bBBlog.Info("Viewer already exists, please select it from the list to edit");
                TwitchFlavourTextBox.Focus();
                return;
            }
            else
            {
                _bBBlog.Info("Adding new viewer to the collection");
                //disable the event for selected index changed
                TwitchNotableViewersComboBox.SelectedIndexChanged -= TwitchNotableViewersComboBox_SelectedIndexChanged;
                _viewer = new TwitchNotableViewerClass();
                _viewer.ViewerName = TwitchNotableViewersComboBox.Text;
                viewers.Add(_viewer);
                TwitchNotableViewersComboBox.Items.Add(_viewer.ViewerName);
                //set the selected index to the new viewer
                TwitchNotableViewersComboBox.SelectedIndex = TwitchNotableViewersComboBox.Items.Count - 1;
                TwitchFlavourTextBox.Focus();
                TwitchNotableViewersComboBox.SelectedIndexChanged += TwitchNotableViewersComboBox_SelectedIndexChanged;
                //aaand we save it to the file
                TwitchNotableViewers_SaveToFile();
                FlavourTextEdited = false;
                ViewerSaveButton.Enabled = false;
            }
        }

        private void TwitchFlavourTextBox_TextChanged(object sender, EventArgs e)
        {
            if (TwitchNotableViewersComboBox.Text.Length > 1)
            {
                //if the text is changed, we set the flag to true and enable the save button
                FlavourTextEdited = true;
                ViewerSaveButton.Enabled = true;
            }
        }

        //if the delete button is clicked, we remove the viewer from the list and the combobox
        //and then save that to the file


    }
}
