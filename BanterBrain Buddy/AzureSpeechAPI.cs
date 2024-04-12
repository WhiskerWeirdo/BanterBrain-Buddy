using CSCore.CoreAudioAPI;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BanterBrain_Buddy
{
    internal class AzureSpeechAPI
    {
        private static readonly log4net.ILog BBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string AzureAPIKey { get; set; }
        public string AzureRegion { get; set; }
        public string AzureLanguage { get; set; }

        //for the Azure STT input device selection and general speech config
        private SpeechConfig azureSpeechConfig ;
        private AudioConfig azureAudioConfig;
        private Microsoft.CognitiveServices.Speech.SpeechRecognizer azureSpeechRecognizer;

        private MMDevice _selectedDevice;
        private MMDevice SelectedInputDevice
        {
            get { return _selectedDevice; }
            set
            {
                _selectedDevice = value;
            }
        }

        /// <summary>
        /// This method gets all the voices and styles available for the region and language
        /// </summary>
        /// <returns>List<AzureVoices></returns>
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

        //this sets "SelectedInputDevice" to the correct input/microphone or capture device
        private void SetSelectedInputDevice(string InputDevice)
        {
            var devices = MMDeviceEnumerator.EnumerateDevices(DataFlow.Capture, DeviceState.Active);
            foreach (var device in devices)
            {
                if (device.FriendlyName == InputDevice)
                {
                    SelectedInputDevice = device;
                }
            }
        }

        /// <summary>
        /// Initializes the Azure Speech to Text API with the selected input device
        /// </summary>
        /// <param name="InputDevice">This is the name of the selected sound capture device</param>
        public void AzureSTTInit(string InputDevice)
        {
            BBBlog.Info("Starting Azure Speech to Text, Initializing");
            azureSpeechConfig = SpeechConfig.FromSubscription(AzureAPIKey, AzureRegion);
            azureSpeechConfig.SpeechRecognitionLanguage = AzureLanguage; //default language

            SetSelectedInputDevice(InputDevice);
            BBBlog.Info("selected audio input device for Azure: " + SelectedInputDevice);
            azureAudioConfig = AudioConfig.FromMicrophoneInput(SelectedInputDevice.DeviceID);
            azureSpeechRecognizer = new SpeechRecognizer(azureSpeechConfig, azureAudioConfig);
        }


        /// <summary>
        /// this is the recognizer loop for Azure STT, it returns the speech recognized as text
        /// or null when an error occurs
        /// </summary>
        /// <returns>Recognized text, NOMATCH or null</returns>
        public async Task<string> RecognizeSpeechAsync()
        {
            BBBlog.Info("Starting Azure Speech to Text, Recognizing");
            var tmpResult = await azureSpeechRecognizer.RecognizeOnceAsync();

            //returns: result text, NOMATCH or null
            var returnResult = AzureOutputSpeechRecognitionResult(tmpResult);

            return returnResult;
        }

        private string AzureOutputSpeechRecognitionResult(SpeechRecognitionResult speechRecognitionResult)
        {
            switch (speechRecognitionResult.Reason)
            {
                case ResultReason.RecognizedSpeech:
                    BBBlog.Info($"RECOGNIZED: Text={speechRecognitionResult.Text}");
                    return speechRecognitionResult.Text;
                case ResultReason.NoMatch:
                    BBBlog.Info("NOMATCH: Speech could not be recognized.");
                    return "NOMATCH";
                case ResultReason.Canceled:
                    var cancellation = CancellationDetails.FromResult(speechRecognitionResult);
             
                    BBBlog.Info($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {   
                        BBBlog.Error($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        BBBlog.Error($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        BBBlog.Error($"CANCELED: Did you set the correct API resource key and region values?");
                    }
                    //whatever the cancel reason, its an error so return null. 
                    //user should checklogfile for more info
                    return null;
            }
            return null;
        }

        /// <summary>
        /// We need to at least know the API key, the  region and the language to use this class.
        /// </summary>
        /// <param name="AzAPIKey">The API key from Azure needed for Speech services</param>
        /// <param name="AzRegion">The region the key is valid for</param>
        /// <param name="AzLanguage">The language requested for the Speech service</param>
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
