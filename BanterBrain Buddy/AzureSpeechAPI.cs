using CSCore.CoreAudioAPI;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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

        private SpeechConfig azureSpeechConfig;
        private AudioConfig azureAudioConfig;
        private Microsoft.CognitiveServices.Speech.SpeechRecognizer azureSpeechRecognizer;
        private string AzureVoiceName { get; set; }
        private string AzureVoiceOptions { get; set; }

        //for the Azure STT input/output device selection and general speech config
        private MMDevice _selectedInputDevice;
        private MMDevice SelectedInputDevice
        {
            get { return _selectedInputDevice; }
            set
            {
                _selectedInputDevice = value;
            }
        }

        private MMDevice _selectedOutputDevice;
        private MMDevice SelectedOutputDevice
        {
            get { return _selectedOutputDevice; }
            set
            {
                _selectedOutputDevice = value;
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
            BBBlog.Debug("Get voices Azure API Key: " + AzureAPIKey);
            BBBlog.Debug("Get voices Azure Region: " + AzureRegion);

            SpeechConfig speechConfig = SpeechConfig.FromSubscription(AzureAPIKey, AzureRegion);
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
                    BBBlog.Debug($"Selected inputdevice = {device.FriendlyName}");
                    SelectedInputDevice = device;
                }
            }
        }

        private void SetSelectedOutputDevice(string OutputDevice)
        {
            BBBlog.Info($"Setting selected output device for Azure TTS to: {OutputDevice}");
            var devices = MMDeviceEnumerator.EnumerateDevices(DataFlow.Render, DeviceState.Active);
            foreach (var device in devices)
            {
                if (device.FriendlyName.StartsWith(OutputDevice))
                {
                    BBBlog.Debug($"Selected outputdevice = {device.FriendlyName}");
                    SelectedOutputDevice = device;
                }
            }
        }


        /// <summary>
        /// Initializes the Azure Text to Speech API with the selected voice, style and output device
        /// </summary>
        /// <param name="AzureVoiceName">This holds the azure voice. This need to be parsed to be usable for Azure</param>
        /// <param name="TTSVoiceOptions">This holds the style of the voice (if available)</param>
        /// <param name="OutputDevice">This is the output device selected in the GUI</param>
        public async Task AzureTTSInit(string AzureVoiceParseName, string TTSVoiceOptions, string OutputDevice)
        {
            BBBlog.Info("Starting Azure Text To Speech, Initializing");
            BBBlog.Debug("Init Azure API Key: " + AzureAPIKey);
            BBBlog.Debug("Init Azure Region: " + AzureRegion);
            BBBlog.Debug("Init Output Device: " + OutputDevice);
            SetSelectedOutputDevice(OutputDevice);

            azureSpeechConfig = SpeechConfig.FromSubscription(AzureAPIKey, AzureRegion);

            //set the options that we can just pass along, this holds the style of the voice
            AzureVoiceOptions = TTSVoiceOptions;

            //now find the correct name associated with the selected voice
            var AzureRegionVoicesList = await TTSGetAzureVoices();

            //we need to do some parsing. Azure voices are in the format of "en-US-Guy-Azure" and the text we get is not.
            //what is returned is the usable voicename for Azure.
            foreach (var AzureRegionVoice in AzureRegionVoicesList)
            {
                if (AzureVoiceParseName == (AzureRegionVoice.LocaleDisplayname + "-" + AzureRegionVoice.Gender + "-" + AzureRegionVoice.LocalName))
                {
                    
                    AzureVoiceName = AzureRegionVoice.Name;
                    BBBlog.Debug($"Azure Voice found. Assigning {AzureVoiceName}");
                    return;
                }
            }
  
        }

        /// <summary>
        /// This method speaks the text passed to it using the Azure TTS API using SSML so it support styles
        /// </summary>
        /// <param name="TextToSay">The text to be spoken by the Azure TTS</param>
        /// <returns>True if no error, False if error</returns>
        public async Task<bool> AzureSpeak(string TextToSay)
        {
            BBBlog.Info($"Starting Azure Text To Speech, Speaking with: {AzureVoiceName}");
            if (!string.IsNullOrEmpty(AzureVoiceName))
            {
                string SSMLText =
                "<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xmlns:mstts=\"https://www.w3.org/2001/mstts\" xml:lang=\"zh-CN\">\r\n   " +
                $" <voice name=\"{AzureVoiceName}\">\r\n       " +
                $" <mstts:express-as style=\"{AzureVoiceOptions}\" styledegree=\"2\">\r\n            " +
                $"{TextToSay}\r\n        " +
                "</mstts:express-as>\r\n    " +
                "</voice>\r\n" +
                "</speak>";

                //now lets speak the SSML and handle the result 
                azureSpeechConfig.SpeechSynthesisVoiceName = AzureVoiceName;
                var tmpAudioConfig = AudioConfig.FromSpeakerOutput(SelectedOutputDevice.DeviceID);
                var speechSynthesizer = new Microsoft.CognitiveServices.Speech.SpeechSynthesizer(azureSpeechConfig, tmpAudioConfig);
                var speechSynthesisResult = await speechSynthesizer.SpeakSsmlAsync(SSMLText);
                var result = TTSAzureOutputSpeechSynthesisResult(speechSynthesisResult, TextToSay);
                if (result)
                {
                    return true;
                } else
                {
                    return false;
                }
            } else
            {
                BBBlog.Error("Cannot find selected voice in the list. Is there a problem with your API key or subscription?");
                return false;
            }
        }


        /// <summary>
        /// This parses the output of the Azure TTS and returns 
        /// </summary>
        /// <param name="speechSynthesisResult">Holds the result of the action of SSML speaking</param>
        /// <param name="text">The text that has been spoken</param>
        /// <returns>True if no error, false if not </returns>
        private static bool TTSAzureOutputSpeechSynthesisResult(SpeechSynthesisResult speechSynthesisResult, string text)
        {
            switch (speechSynthesisResult.Reason)
            {
                case ResultReason.SynthesizingAudioCompleted:
                    BBBlog.Info($"Speech synthesized for text: [{text}]");
                    return true;
                case ResultReason.Canceled:
                    var cancellation = SpeechSynthesisCancellationDetails.FromResult(speechSynthesisResult);
                    BBBlog.Info($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        BBBlog.Error($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        BBBlog.Error($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                        BBBlog.Error($"CANCELED: Did you set the speech resource key and region values?");
                    }
                    return false;
                default:
                    break;
            }
            return false;
        }


        /// <summary>
        /// Initializes the Azure Speech to Text API with the selected input device
        /// </summary>
        /// <param name="InputDevice">This is the name of the selected sound capture device</param>
        public void AzureSTTInit(string InputDevice)
        {
            BBBlog.Info("Starting Azure Speech to Text, Initializing");
            BBBlog.Debug("Init Azure API Key: " + AzureAPIKey);
            BBBlog.Debug("Init Azure Region: " + AzureRegion);
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
            var returnResult = AzureOutputSpeechRecognitionResult(tmpResult);
            return returnResult;
        }

        /// <summary>
        /// This method stops the Azure STT recognizer and parses the result for errors or recognized text
        /// </summary>
        /// <param name="speechRecognitionResult"></param>
        /// <returns>Recognized text, NOMATCH or null</returns>
        private static string AzureOutputSpeechRecognitionResult(SpeechRecognitionResult speechRecognitionResult)
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
