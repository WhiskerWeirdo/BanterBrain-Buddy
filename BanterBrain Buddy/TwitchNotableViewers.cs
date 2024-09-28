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

//alright here we add, edit and remove specific viewers. When added you can add a flavour text to be send to the LLM for a viewer-customized response
namespace BanterBrain_Buddy
{
    public partial class TwitchNotableViewers : Form
    {
        public TwitchNotableViewers()
        {
            InitializeComponent();
        }

        //aight so first we load any previously saved viewers from the json file in format "Viewer name" : "Flavour text"
        //location of the file containing the saved viewers is %AppData%\BanterBrain Buddy\viewers.json
        
        [SupportedOSPlatform("windows6.1")]
        private void TwitchNotableViewers_Load(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BanterBrain Buddy\\viewers.json";
            //aight now load the viewers from the file into TwitchNotableViewerClass
            List<TwitchNotableViewerClass> viewers = new List<TwitchNotableViewerClass>();
            if (System.IO.File.Exists(path))
            {
                string[] lines = System.IO.File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(':');
                    TwitchNotableViewerClass viewer = new TwitchNotableViewerClass();
                    viewer.ViewerName = parts[0];
                    viewer.FlavourText = parts[1];
                    viewers.Add(viewer);
                }
            }
            else
            {
                //we create the file if it doesn't exist
                System.IO.File.Create(path);
            }

        }
    }
}
