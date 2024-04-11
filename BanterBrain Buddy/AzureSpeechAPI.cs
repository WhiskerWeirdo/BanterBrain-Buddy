using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanterBrain_Buddy
{
    internal class AzureSpeechAPI
    {
        private static readonly log4net.ILog BBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string AzureAPIKey { get; set; }
        public string AzureRegion { get; set; }
        public string AzureLanguage { get; set; }


        //this gets all the voices available for the region and language
        //returns the  List<AzureVoices>
        public async Task<List<AzureVoices>> TTSGetAzureVoices()
        {
            List<AzureVoices> AzureRegionVoicesList = [];
            BBBlog.Info("Finding TTS Azure voices available");
            SpeechConfig speechConfig = SpeechConfig.FromSubscription(AzureAPIKey, AzureRegion);
            BBBlog.Info("Azure authorizationToken: " + speechConfig.AuthorizationToken);
            var speechSynthesizer = new Microsoft.CognitiveServices.Speech.SpeechSynthesizer(speechConfig, null as AudioConfig);
            SynthesisVoicesResult result = await speechSynthesizer.GetVoicesAsync();

            //if we got any voices back we add them to the list
            if (result.Reason == ResultReason.VoicesListRetrieved)
            {
                //remove the old listed items
                AzureRegionVoicesList.Clear();
                BBBlog.Info($"Found {result.Voices.Count} voices");
                foreach (var voice in result.Voices)
                {
                    var tmpVoice = new AzureVoices()
                    {
                        Locale = voice.Locale,
                        LocalName = voice.LocalName,
                        Name = voice.Name,
                        Gender = voice.Gender.ToString(),
                        StyleList = new List<string>(voice.StyleList),
                        LocaleDisplayname = new RegionInfo(voice.Locale).ThreeLetterISORegionName
                    };
                    AzureRegionVoicesList.Add(tmpVoice);
                }
                return AzureRegionVoicesList;
            } else //no voices back means something is definately bad
            {
                BBBlog.Error("Problem retreiving Azure API voicelist. Is your API key or subscription information still valid?");
            }
            return null;
        }

        //when we make a new instance of this class, we need to at least know the API key, the  region and the language to use.
        public AzureSpeechAPI(string AzAPIKey, string AzRegion, string AzLanguage)
        {
            //first we convert the passed local strings to global
            AzureAPIKey = AzAPIKey;
            BBBlog.Debug("Azure API Key: " + AzureAPIKey);
            AzureRegion = AzRegion;
            BBBlog.Debug("Azure Region: " + AzureRegion);
            AzureLanguage = AzLanguage;
            BBBlog.Debug("Azure Language: " + AzureLanguage);
        }
    }
}
