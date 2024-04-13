using System.Collections.Generic;

/// <summary>
/// CODING RULES:
/// •	Local variables, private instance, static fields and method parameters should be camelCase.
/// •	Methods, constants, properties, events and classes should be PascalCase.
/// •	Global private instance fields should be in camelCase prefixed with an underscore.
/// </summary>

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
