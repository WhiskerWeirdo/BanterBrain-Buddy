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

namespace BanterBrain_Buddy
{
    public partial class WordFilterForm : Form
    {
        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public WordFilterForm()
        {
            InitializeComponent();
        }

        [SupportedOSPlatform("windows6.1")]
        private void BadWordFilterBox_TextChanged_1(object sender, EventArgs e)
        {
            _bBBlog.Info("BadWordFilterBox_TextChanged");
            //need to set boolean to save when exiting
        }

        [SupportedOSPlatform("windows6.1")]
        private void WordFilterForm_FormClosing(object sender, EventArgs e)
        {
            _bBBlog.Info("WordFilterForm form closing we need to save!");
        }
    }
}
