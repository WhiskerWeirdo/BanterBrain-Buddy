using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanterBrain_Buddy
{
    internal class Personas
    {
        public string Name { get; set; }
        public string RoleText { get; set; }
        public string VoiceProvider { get; set; }
        public string VoiceName { get; set; }
        public List<string> VoiceOptions { get; set; }
    }
}
