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
        private bool AddingNewViewer = false;
        private int CurrSelectedViewerIndex;

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
                //set the selected index to the first viewer, but we need to disable the eventhandlers
                TwitchFlavourTextBox.TextChanged -= TwitchFlavourTextBox_TextChanged;
                TwitchNotableViewersComboBox.SelectedIndex = 0;
                TwitchFlavourTextBox.TextChanged += TwitchFlavourTextBox_TextChanged;
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
            if (CurrSelectedViewerIndex == TwitchNotableViewersComboBox.SelectedIndex && TwitchFlavourTextBox.Text.Length > 1)
            {
                _bBBlog.Info("Selected viewer not changed, no need to load flavour text");
                return;
            }

            _bBBlog.Info("Selected viewer changed, loading flavour text");
            TwitchFlavourTextBox.TextChanged -= TwitchFlavourTextBox_TextChanged;
            TwitchFlavourTextBox.Text = "";
            //if there is a flavour text, load it into the textbox
            if (viewers[TwitchNotableViewersComboBox.SelectedIndex].FlavourText == null)
            {
                _bBBlog.Debug("No flavour text found, this should not happen but sure whatever!");
                return;
            }
            else if (viewers[TwitchNotableViewersComboBox.SelectedIndex].FlavourText.Length > 1)
            {
                TwitchFlavourTextBox.Text = viewers[TwitchNotableViewersComboBox.SelectedIndex].FlavourText;
                FlavourTextEdited = false;
                ViewerSaveButton.Enabled = false;
            }
            CurrSelectedViewerIndex = TwitchNotableViewersComboBox.SelectedIndex;
            TwitchFlavourTextBox.TextChanged += TwitchFlavourTextBox_TextChanged;
        }

        //here we save the current class to the json file
        [SupportedOSPlatform("windows10.0.10240")]
        public void TwitchNotableViewers_SaveToFile()
        {

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BanterBrain\\viewers.json";
            _bBBlog.Info("Saving viewers to file");
            // Serialize the list of viewers to JSON format
            string json = JsonConvert.SerializeObject(viewers, Formatting.Indented);

            // Write the JSON string to the file
            System.IO.File.WriteAllText(path, json);

        }


        private void ViewerAddButton_Click(object sender, EventArgs e)
        {
            _bBBlog.Debug("Add viewer button clicked");
            if (FlavourTextEdited)
            {
                _bBBlog.Info("Flavour text edited, saving first");
                //first lets ask if they wanna save what they got since aparently its changed
                ViewerSaveButton_Click(sender, e);
            }
            _bBBlog.Info("No changes to save, adding new viewer");
            //lets empty all the text box and combobox
            TwitchFlavourTextBox.Text = "";
            TwitchNotableViewersComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            TwitchNotableViewersComboBox.Focus();
            TwitchNotableViewersComboBox.Text = "";
            ViewerAddButton.Enabled = false;
            ViewerDeleteButton.Enabled = false;
            AddingNewViewer = true;
        }

        private void ViewerSaveButton_Click(object sender, EventArgs e)
        {
            if (TwitchNotableViewersComboBox.Text.Length < 1 || TwitchFlavourTextBox.Text.Length < 1)
            {
                _bBBlog.Info("That cant be empty buddy! Try again!");
                MessageBox.Show("Viewer name or flavour text cannot be empty", "Error", MessageBoxButtons.OK);
                //leets reload the original flavour text if there was any previously saved
                if (viewers[TwitchNotableViewersComboBox.SelectedIndex] != null)
                {
                    TwitchFlavourTextBox.TextChanged -= TwitchFlavourTextBox_TextChanged;
                    TwitchFlavourTextBox.Text = viewers[TwitchNotableViewersComboBox.SelectedIndex].FlavourText;
                    TwitchFlavourTextBox.TextChanged += TwitchFlavourTextBox_TextChanged;
                }
                return;
            }
            if (!FlavourTextEdited)
            {
                _bBBlog.Info("No changes to save");
                return;
            }
            //first we check if the viewer already exists, set focus to the flavour text box
            if (viewers.Any(x => x.ViewerName == TwitchNotableViewersComboBox.Text))
            {
                _bBBlog.Info("Viewer already exists, and flavour text changed. Saving to file");
                TwitchFlavourTextBox.Focus();
                //ok so aparently the focustext changed, so lets update the viewer and save it to the file
                _viewer = viewers.Find(x => x.ViewerName == TwitchNotableViewersComboBox.Text);
                _viewer.FlavourText = TwitchFlavourTextBox.Text;
                TwitchNotableViewers_SaveToFile();
                FlavourTextEdited = false;
                ViewerSaveButton.Enabled = false;
                ViewerAddButton.Enabled = true;
                ViewerDeleteButton.Enabled = true;
                return;
            }
            else
            {
                _bBBlog.Info("Adding new viewer to the collection");
                //disable the event for selected index changed
                TwitchNotableViewersComboBox.SelectedIndexChanged -= TwitchNotableViewersComboBox_SelectedIndexChanged;
                TwitchFlavourTextBox.TextChanged -= TwitchFlavourTextBox_TextChanged;
                _viewer = new TwitchNotableViewerClass();
                _viewer.ViewerName = TwitchNotableViewersComboBox.Text;
                _viewer.FlavourText = TwitchFlavourTextBox.Text;
                viewers.Add(_viewer);
                TwitchNotableViewersComboBox.Items.Add(_viewer.ViewerName);
                //set the selected index to the new viewer
                TwitchNotableViewersComboBox.SelectedIndex = TwitchNotableViewersComboBox.Items.Count - 1;
                TwitchFlavourTextBox.Focus();
                TwitchNotableViewersComboBox.SelectedIndexChanged += TwitchNotableViewersComboBox_SelectedIndexChanged;
                TwitchFlavourTextBox.TextChanged += TwitchFlavourTextBox_TextChanged;
                //aaand we save it to the file
                TwitchNotableViewers_SaveToFile();
                FlavourTextEdited = false;
                ViewerSaveButton.Enabled = false;
                ViewerAddButton.Enabled = true;
                ViewerDeleteButton.Enabled = true;
                //change it back to list so you cant just type there
                TwitchNotableViewersComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        private void TwitchFlavourTextBox_TextChanged(object sender, EventArgs e)
        {
            if (TwitchNotableViewersComboBox.Text.Length > 1)
            {
                _bBBlog.Debug("Flavourtext text changed handler");
                //if the text is changed, we set the flag to true and enable the save button
                FlavourTextEdited = true;
                ViewerSaveButton.Enabled = true;
            }
        }

        private void ViewerDeleteButton_Click(object sender, EventArgs e)
        {
            //here we delete the viewer from the list and the combobox, then save it to the file, byebye viewer!
            _bBBlog.Info("Delete viewer button clicked");
            if (viewers.Count < 1)
            {
                _bBBlog.Info("No viewers to delete");
                return;
            }

            //now first lets ask if they are sure
            DialogResult dialogResult = MessageBox.Show($"Are you sure you want to delete viewer {TwitchNotableViewersComboBox.Text}?", "Delete viewer", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                _bBBlog.Info("User decided not to delete the viewer");
                return;
            }
            //disable the event for selected index changed
            TwitchNotableViewersComboBox.SelectedIndexChanged -= TwitchNotableViewersComboBox_SelectedIndexChanged;
            TwitchFlavourTextBox.TextChanged -= TwitchFlavourTextBox_TextChanged;
            //remove the viewer from the list
            viewers.RemoveAt(TwitchNotableViewersComboBox.SelectedIndex);
            TwitchFlavourTextBox.Text = "";
            //remove the viewer from the combobox
            TwitchNotableViewersComboBox.Items.RemoveAt(TwitchNotableViewersComboBox.SelectedIndex);
            //set the selected index to the first viewer
            if (TwitchNotableViewersComboBox.Items.Count == 0)
                _bBBlog.Debug("There is no viewer left, not setting index");
            else
                TwitchNotableViewersComboBox.SelectedIndex = 0;
            //save the changes to the file
            TwitchNotableViewers_SaveToFile();
            //enable the event for selected index changed
            TwitchNotableViewersComboBox.SelectedIndexChanged += TwitchNotableViewersComboBox_SelectedIndexChanged;
            TwitchFlavourTextBox.TextChanged += TwitchFlavourTextBox_TextChanged;
        }

        private void TwitchNotableViewersComboBox_Validating(object sender, CancelEventArgs e)
        {
            //aight did they change the text in the combobox? if so we need to ask if it needs to be saved
            //viewer needs to exist before call this
            //if (viewers.Any(x => x.ViewerName == TwitchNotableViewersComboBox.Text) &  FlavourTextEdited)
            if (FlavourTextEdited)
            {
                _bBBlog.Info("Viewer name of flavourtext changed, asking if they want to save");
                DialogResult dialogResult = MessageBox.Show($"Do you want to save viewer {TwitchNotableViewersComboBox.Text}?", "Save viewer", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    _bBBlog.Info("We dont want to save the changes lets just keep going then");
                    e.Cancel = false;
                }
                else
                    ViewerSaveButton_Click(sender, e);
            } /*if (AddingNewViewer && FlavourTextEdited)
            {
                _bBBlog.Info("new viewer so lets just save!");
                ViewerSaveButton_Click(sender, e);
                AddingNewViewer = false;
            }*/
            else
            {
                _bBBlog.Info("Viewer name not changed or flavourtext not changed, no need to save");
                e.Cancel = false;
            }
        }

    }
}
