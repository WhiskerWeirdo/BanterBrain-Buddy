using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanterBrain_Buddy
{
    internal class AzureVoices
    {
        public string Gender { get; set; }
        public string Locale { get; set; }
        public string LocaleDisplayname { get; set; }

        public string LocalName { get; set; }
        public string Name { get; set; }
        public List<string> StyleList { get; set; }
    }
}
