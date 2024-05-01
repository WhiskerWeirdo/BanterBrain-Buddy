using ElevenLabs;
using ElevenLabs.User;
using ElevenLabs.Voices;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanterBrain_Buddy
{
    internal class ElLabs
    {
        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string _elevelLabsAPIKey { get; set; }

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


        public async Task<bool> ElevenLabsTTS(string text, string outputDevice, string tmpVoice, int similarity, int stability, int style)
        {
            var api = new ElevenLabsClient(_elevelLabsAPIKey);
            _bBBlog.Info($"ElevenLabsTTS called with voice: {tmpVoice}");
            SetSelectedOutputDevice(outputDevice);

            //we need to find teh voice, and then use the ID to get the voice settings
            var allVoices = await api.VoicesEndpoint.GetAllVoicesAsync();
            foreach (var aVoice in allVoices)
            {
                if (aVoice.Name == tmpVoice)
                {
                    tmpVoice = aVoice.Id;
                    break;
                }
            }

            var voice = await api.VoicesEndpoint.GetVoiceAsync(tmpVoice);
            VoiceSettings voiceSettings = new VoiceSettings(similarity/100f, stability/100f, false, style/100f);
            var editVoice = api.VoicesEndpoint.EditVoiceSettingsAsync(tmpVoice, voiceSettings);
            if (editVoice.Result)
            {
                _bBBlog.Info($"Voice settings updated for {voice.Name}");
            }
            else
            {
                _bBBlog.Error($"Voice settings update failed for {voice.Name}");
                return false;
            }
            //this returns in mp3 format...pfft
            var convertedText = await api.TextToSpeechEndpoint.TextToSpeechAsync(text, voice, voiceSettings);
            var mp3Data = convertedText.ClipData.ToArray();
            MemoryStream ms = new MemoryStream(mp3Data);
            ms.Position = 0;
            var WavStream = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(ms));
            var waveOut = new WaveOut();
            waveOut.DeviceNumber = SelectedOutputDevice;
            //it has to be 24000, 16, 1 for some reason?
            var waveStream = new RawSourceWaveStream(WavStream, new WaveFormat(44100, 16, 1));
          
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

        public async Task<List<string>> TTSGetElevenLabsVoices()
        {
            List<string> ElevenLabVoiceList = new List<string>();
            var api = new ElevenLabsClient(_elevelLabsAPIKey);
            var tier = await api.UserEndpoint.GetSubscriptionInfoAsync();
            _bBBlog.Info($"Tier: {tier.Tier} for {tier.VoiceLimit}");
            var voices = await api.VoicesEndpoint.GetAllVoicesAsync();
            _bBBlog.Info($"Voices found: {voices.Count()}");

            foreach (var voice in voices)
            {
                ElevenLabVoiceList.Add(voice.Name);
            }
            return ElevenLabVoiceList;
        }

        public async Task<bool> ElevenLabsAPIKeyTest()
        {
            _bBBlog.Info("ElevenLabsAPIKeyTest called");
            if (_elevelLabsAPIKey.Length > 1)
            {

                var api = new ElevenLabsClient(_elevelLabsAPIKey);
                SubscriptionInfo result = null;
                try
                {
                    result = await api.UserEndpoint.GetSubscriptionInfoAsync();
                } catch (Exception ex)
                {
                    _bBBlog.Error($"ElevenLabsAPIKeyTest failed, no key added. {ex.Message}");
                    return false;
                }
                return true;
            }
            else
            {
                _bBBlog.Error("ElevenLabsAPIKeyTest failed, no key added");
                return false;
            }
        }

        public ElLabs(string elevelLabsAPIKey)
        {
            _bBBlog.Info("ElevenLabs called");
            _elevelLabsAPIKey = elevelLabsAPIKey;
        }
    }
}
