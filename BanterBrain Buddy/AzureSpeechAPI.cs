﻿using CSCore.CoreAudioAPI;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

/// <summary>
/// CODING RULES:
/// •	Local variables, private instance, static fields and method parameters should be camelCase.
/// •	Methods, constants, properties, events and classes should be PascalCase.
/// •	Global private instance fields should be in camelCase prefixed with an underscore.
/// </summary>

namespace BanterBrain_Buddy
{
    internal class AzureSpeechAPI
    {
        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string AzureAPIKey { get; set; }
        public string AzureRegion { get; set; }
        public string AzureLanguage { get; set; }

        private SpeechConfig _azureSpeechConfig;
        private AudioConfig _azureAudioConfig;

        private Microsoft.CognitiveServices.Speech.SpeechRecognizer _azureSpeechRecognizer;
        private string _azureVoiceName { get; set; }
        private string _azureVoiceOptions { get; set; }

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
            List<AzureVoices> azureRegionVoicesList = [];
            _bBBlog.Info("Finding TTS Azure voices available");
            _bBBlog.Debug("Get voices Azure API Key: " + AzureAPIKey);
            _bBBlog.Debug("Get voices Azure Region: " + AzureRegion);

            SpeechConfig speechConfig = SpeechConfig.FromSubscription(AzureAPIKey, AzureRegion);
            var speechSynthesizer = new Microsoft.CognitiveServices.Speech.SpeechSynthesizer(speechConfig, null as AudioConfig);
            SynthesisVoicesResult result = await speechSynthesizer.GetVoicesAsync();

            //if we got any voices back we add them to the list
            if (result.Reason == ResultReason.VoicesListRetrieved)
            {
                //remove the old listed items
                azureRegionVoicesList.Clear();
                _bBBlog.Info($"Found {result.Voices.Count} voices");
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
                    azureRegionVoicesList.Add(tmpVoice);
                }
                return azureRegionVoicesList;
            } else //no voices back means something is definately bad
            {
                _bBBlog.Error("Problem retreiving Azure API voicelist. Is your API key or subscription information still valid?");
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
                    _bBBlog.Debug($"Selected inputdevice = {device.FriendlyName}");
                    SelectedInputDevice = device;
                }
            }
        }

        private void SetSelectedOutputDevice(string OutputDevice)
        {
            _bBBlog.Info($"Setting selected output device for Azure TTS to: {OutputDevice}");
            var devices = MMDeviceEnumerator.EnumerateDevices(DataFlow.Render, DeviceState.Active);
            foreach (var device in devices)
            {
                if (device.FriendlyName.StartsWith(OutputDevice))
                {
                    _bBBlog.Debug($"Selected outputdevice = {device.FriendlyName}");
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
            _bBBlog.Info("Starting Azure Text To Speech, Initializing");
            _bBBlog.Debug("Init Azure API Key: " + AzureAPIKey);
            _bBBlog.Debug("Init Azure Region: " + AzureRegion);
            _bBBlog.Debug("Init Azure Output Device: " + OutputDevice);
            SetSelectedOutputDevice(OutputDevice);

            _azureSpeechConfig = SpeechConfig.FromSubscription(AzureAPIKey, AzureRegion);

            //set the options that we can just pass along, this holds the style of the voice
            _azureVoiceOptions = TTSVoiceOptions;

            //now find the correct name associated with the selected voice
            var azureRegionVoicesList = await TTSGetAzureVoices();

            //we need to do some parsing. Azure voices are in the format of "en-US-Guy-Azure" and the text we get is not.
            //what is returned is the usable voicename for Azure.
            foreach (var azureRegionVoice in azureRegionVoicesList)
            {
                if (AzureVoiceParseName == (azureRegionVoice.LocaleDisplayname + "-" + azureRegionVoice.Gender + "-" + azureRegionVoice.LocalName))
                {
                    
                    _azureVoiceName = azureRegionVoice.Name;
                    _bBBlog.Debug($"Azure Voice found. Assigning {_azureVoiceName}");
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
            _bBBlog.Info($"Starting Azure Text To Speech, Speaking with: {_azureVoiceName}");
            if (!string.IsNullOrEmpty(_azureVoiceName))
            {
                string SSMLText =
                "<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xmlns:mstts=\"https://www.w3.org/2001/mstts\" xml:lang=\"zh-CN\">\r\n   " +
                $" <voice name=\"{_azureVoiceName}\">\r\n       " +
                $" <mstts:express-as style=\"{_azureVoiceOptions}\" styledegree=\"2\">\r\n            " +
                $"{TextToSay}\r\n        " +
                "</mstts:express-as>\r\n    " +
                "</voice>\r\n" +
                "</speak>";

                //now lets speak the SSML and handle the result 
                _azureSpeechConfig.SpeechSynthesisVoiceName = _azureVoiceName;
                var tmpAudioConfig = AudioConfig.FromSpeakerOutput(SelectedOutputDevice.DeviceID);
                var speechSynthesizer = new Microsoft.CognitiveServices.Speech.SpeechSynthesizer(_azureSpeechConfig, tmpAudioConfig);
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
                _bBBlog.Error("Cannot find selected voice in the list. Is there a problem with your API key or subscription?");
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
                    _bBBlog.Info($"Speech synthesized for text: [{text}]");
                    return true;
                case ResultReason.Canceled:
                    var cancellation = SpeechSynthesisCancellationDetails.FromResult(speechSynthesisResult);
                    _bBBlog.Info($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        _bBBlog.Error($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        _bBBlog.Error($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                        _bBBlog.Error($"CANCELED: Did you set the speech resource key and region values?");
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
            _bBBlog.Info("Starting Azure Speech to Text, Initializing");
            _bBBlog.Debug("Init Azure API Key: " + AzureAPIKey);
            _bBBlog.Debug("Init Azure Region: " + AzureRegion);
            _azureSpeechConfig = SpeechConfig.FromSubscription(AzureAPIKey, AzureRegion);
            _azureSpeechConfig.SpeechRecognitionLanguage = AzureLanguage; //default language

            SetSelectedInputDevice(InputDevice);
            _bBBlog.Info("selected audio input device for Azure: " + SelectedInputDevice);
            _azureAudioConfig = AudioConfig.FromMicrophoneInput(SelectedInputDevice.DeviceID);
            _azureSpeechRecognizer = new SpeechRecognizer(_azureSpeechConfig, _azureAudioConfig);
        }

        /// <summary>
        /// this is the recognizer loop for Azure STT, it returns the speech recognized as text
        /// or null when an error occurs
        /// </summary>
        /// <returns>Recognized text, NOMATCH or null</returns>
        public async Task<string> RecognizeSpeechAsync()
        {
            _bBBlog.Info("Starting Azure Speech to Text, Recognizing");
            var tmpResult = await _azureSpeechRecognizer.RecognizeOnceAsync();
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
                    _bBBlog.Info($"RECOGNIZED: Text={speechRecognitionResult.Text}");
                    return speechRecognitionResult.Text;
                case ResultReason.NoMatch:
                    _bBBlog.Info("NOMATCH: Speech could not be recognized.");
                    return "NOMATCH";
                case ResultReason.Canceled:
                    var cancellation = CancellationDetails.FromResult(speechRecognitionResult);

                    _bBBlog.Info($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        _bBBlog.Error($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        _bBBlog.Error($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        _bBBlog.Error($"CANCELED: Did you set the correct API resource key and region values?");
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
            _bBBlog.Debug("Azure API Key: " + AzureAPIKey);
            AzureRegion = AzRegion;
            _bBBlog.Debug("Azure Region: " + AzureRegion);
            AzureLanguage = AzLanguage;
            _bBBlog.Debug("Azure Language: " + AzureLanguage);
        }
    }
}
