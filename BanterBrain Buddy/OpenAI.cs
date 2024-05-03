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
    /// •	Global private instance fields should be in camelCase prefixed with an underscore.
    /// </summary>

    internal class OpenAI
    {
        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private int SelectedOutputDevice;

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
        public bool OpenAICheckAPIKey()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.GPTAPIKey))
            {
                _bBBlog.Error("OpenAI API Key is missing or bad");
                return false ;
            }
            return true;
        }
        public async Task<string> OpenAISTT(string audioFile)
        {
            OpenAIAPI api = new(Properties.Settings.Default.GPTAPIKey);
            var STTResult = await api.Transcriptions.GetTextAsync(audioFile);

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
            OpenAIAPI api = new(Properties.Settings.Default.GPTAPIKey);
            //alloy, echo, fable, onyx, nova, and shimmer
            api.TextToSpeech.DefaultTTSRequestArgs.Voice = voice;
            //we use WAV streams format
            api.TextToSpeech.DefaultTTSRequestArgs.ResponseFormat = "wav";
            var TTSResult = await api.TextToSpeech.GetSpeechAsStreamAsync(text);
            if (TTSResult == null)
            {
                _bBBlog.Error("OpenAI TTS failed");
                return false;
            }
            var waveOut = new WaveOut();
            waveOut.DeviceNumber = SelectedOutputDevice;
            //it has to be 24000, 16, 1 for some reason?
            var waveStream = new RawSourceWaveStream(TTSResult, new WaveFormat(24000, 16, 1));
            //reset the stream to the beginning or you wont hear anything
            waveStream.Position = 0;
            waveOut.Init(waveStream);

            waveOut.Play();
            while (waveOut.PlaybackState != PlaybackState.Stopped)
            {
                await Task.Delay(500);
            }
            return true;
        }

        [SupportedOSPlatform("windows6.1")]
        public async Task<string> GetOpenAIIGPTResponse(String UserInput, string tmpPersonaRoletext)
        {
            string gPTOutputText = "";
            _bBBlog.Info("Sending to OpenAI GPT LLM: " + UserInput);

            OpenAIAPI api = new(Properties.Settings.Default.GPTAPIKey);

            var chat = api.Chat.CreateConversation();
            chat.Model = Model.ChatGPTTurbo;
            chat.RequestParameters.Temperature = Properties.Settings.Default.GPTTemperature;
            chat.RequestParameters.MaxTokens = Properties.Settings.Default.GPTMaxTokens;

            //mood is setting the system text description
            //this is the persona role
            _bBBlog.Info("SystemRole: " + tmpPersonaRoletext);
            chat.AppendSystemMessage(tmpPersonaRoletext);

            chat.AppendUserInput(UserInput);
            try
            {
                _bBBlog.Info("ChatGPT response: ");
                await chat.StreamResponseFromChatbotAsync(res =>
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
