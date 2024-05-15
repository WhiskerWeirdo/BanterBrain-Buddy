using NAudio.Wave;
using OpenAI_API;
using OpenAI_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BanterBrain_Buddy
{
    /// <summary>
    /// CODING RULES:
    /// •	Local variables, private instance, static fields and method parameters should be camelCase.
    /// •	Methods, constants, properties, events and classes should be PascalCase.
    /// </summary>

    internal class OpenAI
    {
        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private int SelectedOutputDevice;
        private OpenAIAPI _OpenAPI;
        private OpenAI_API.Chat.Conversation _Chat;

        private void SetSelectedOutputDevice(string OutputDevice)
        {
            _bBBlog.Info($"Setting selected output device for Native TTS to: {OutputDevice}");
            for (int i = 0; i < NAudio.Wave.WaveOut.DeviceCount; i++)
            {
                var tmpOut = NAudio.Wave.WaveOut.GetCapabilities(i).ProductName;
                if (OutputDevice.StartsWith(tmpOut))
                {
                    _bBBlog.Debug($"Selected outputdevice = {tmpOut}");
                    SelectedOutputDevice = i;
                }
            }
        }
#pragma warning disable CA1822 // Mark members as static
        public async Task<bool> OpenAICheckAPIKey()
#pragma warning restore CA1822 // Mark members as static
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.GPTAPIKey))
            {
                _bBBlog.Error("OpenAI API Key is missing or bad");
                return false;
            } else
            {
                if (_OpenAPI == null)
                    _OpenAPI = new(Properties.Settings.Default.GPTAPIKey);
                //do actual api key check here
                if (await _OpenAPI.Auth.ValidateAPIKey())
                {
                    _bBBlog.Info("OpenAI API Key is valid");
                    return true;
                    }
                else
                {
                    _bBBlog.Error("OpenAI API Key is invalid");
                    return false;
                }
            }
            return true;
        }
#pragma warning disable CA1822 // Mark members as static
        public async Task<string> OpenAISTT(string audioFile)
#pragma warning restore CA1822 // Mark members as static
        {
            _bBBlog.Info("Sending to OpenAI STT with language: " + Properties.Settings.Default.WhisperSpeechRecognitionComboBox);
            string ISOLanguage;
            switch (Properties.Settings.Default.WhisperSpeechRecognitionComboBox)
            {
                case "Dutch":
                    ISOLanguage = "nl";
                    break;
                case "Danish":
                    ISOLanguage = "da";
                    break;
                case "English":
                    ISOLanguage = "en";
                    break;
                case "French":
                    ISOLanguage = "fr";
                    break;
                case "German":
                    ISOLanguage = "de";
                    break;
                case "Italian":
                    ISOLanguage = "it";
                    break;
                case "Japanese":
                    ISOLanguage = "ja";
                    break;
                case "Norwegian":
                    ISOLanguage = "no";
                    break;
                case "Polish":
                    ISOLanguage = "pl";
                    break;
                case "Swedish":
                    ISOLanguage = "sv";
                    break;
                default:
                    _bBBlog.Error("OpenAI STT language not supported, using english");
                    ISOLanguage = "en";
                    break;
            }

            if (_OpenAPI == null)
                _OpenAPI = new(Properties.Settings.Default.GPTAPIKey);
            var STTResult = await _OpenAPI.Transcriptions.GetTextAsync(audioFile, ISOLanguage);

            if (STTResult == null)
            {
                _bBBlog.Error("OpenAI STT failed");
                return null;
            }
            return STTResult;
        }

        public async Task<bool> OpenAITTS(string text, string outputDevice, string voice)
        {
            SetSelectedOutputDevice(outputDevice);
            if (_OpenAPI == null)
                _OpenAPI = new(Properties.Settings.Default.GPTAPIKey);
            //alloy, echo, fable, onyx, nova, and shimmer
            _OpenAPI.TextToSpeech.DefaultTTSRequestArgs.Voice = voice;
            //we use WAV streams format
            _OpenAPI.TextToSpeech.DefaultTTSRequestArgs.ResponseFormat = "wav";
            var TTSResult = await _OpenAPI.TextToSpeech.GetSpeechAsStreamAsync(text);
            if (TTSResult == null)
            {
                _bBBlog.Error("OpenAI TTS failed");
                return false;
            }
            var waveOut = new WaveOut
            {
                DeviceNumber = SelectedOutputDevice
            };
            //it has to be 24000, 16, 1 for some reason?
            var waveStream = new RawSourceWaveStream(TTSResult, new WaveFormat(24000, 16, 1))
            {
                //reset the stream to the beginning or you wont hear anything
                Position = 0
            };
            waveOut.Init(waveStream);

            waveOut.Play();
            while (waveOut.PlaybackState != PlaybackState.Stopped)
            {
                await Task.Delay(500);
            }
            return true;
        }

        [SupportedOSPlatform("windows6.1")]
#pragma warning disable CA1822 // Mark members as static
        public async Task<string> GetOpenAIIGPTResponse(String UserInput, string tmpPersonaRoletext)
#pragma warning restore CA1822 // Mark members as static
        {
            string gPTOutputText = "";
            _bBBlog.Info("Sending to OpenAI GPT LLM: " + UserInput);

            if (_OpenAPI == null)
                _OpenAPI = new(Properties.Settings.Default.GPTAPIKey);

            if (_Chat == null)
            {
                _Chat = _OpenAPI.Chat.CreateConversation();

            }
            _Chat.Model = Model.ChatGPTTurbo;
            _Chat.RequestParameters.Temperature = Properties.Settings.Default.GPTTemperature;
            _Chat.RequestParameters.MaxTokens = Properties.Settings.Default.GPTMaxTokens;
            //mood is setting the system text description
            //this is the persona role
            _bBBlog.Info("SystemRole: " + tmpPersonaRoletext);
            _Chat.AppendSystemMessage(tmpPersonaRoletext);

            _Chat.AppendUserInput(UserInput);
            try
            {
                _bBBlog.Info("ChatGPT response: ");
                await _Chat.StreamResponseFromChatbotAsync(res =>
                {
                    gPTOutputText += res;
                });
                _bBBlog.Info(gPTOutputText);
                _bBBlog.Info("GPT Response done");
                return gPTOutputText;
            }
            catch (System.Security.Authentication.AuthenticationException ex)
            {
                MessageBox.Show(ex.Message, "GPT API Auth error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _bBBlog.Error("GPT API Auth error: " + ex.Message);
                return null;
            }

        }

        public OpenAI()
        {
        }



    }
}
