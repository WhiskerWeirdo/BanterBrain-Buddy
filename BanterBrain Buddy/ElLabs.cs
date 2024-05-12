using ElevenLabs;
using ElevenLabs.User;
using ElevenLabs.Voices;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace BanterBrain_Buddy
{
    internal class ElLabs
    {
        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string ElevelLabsAPIKey { get; set; }
        //private List<string> _elevenLabVoiceList;
        private readonly Dictionary<string, string> _elevenLabVoiceList;

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

        [SupportedOSPlatform("windows6.1")]
        public async Task<bool> ElevenLabsTTS(string text, string outputDevice, string tmpVoice, int similarity, int stability, int style)
        {
            var api = new ElevenLabsClient(ElevelLabsAPIKey);
            _bBBlog.Info($"ElevenLabsTTS called with voice: {tmpVoice}");
            SetSelectedOutputDevice(outputDevice);

            _bBBlog.Info($"count of voices: " + _elevenLabVoiceList.Count);
            var voiceId = _elevenLabVoiceList[tmpVoice];
            var voice = await api.VoicesEndpoint.GetVoiceAsync(voiceId);
            VoiceSettings voiceSettings = new(similarity / 100f, stability / 100f, false, style / 100f);
            // var voiceSettings = await api.VoicesEndpoint.GetDefaultVoiceSettingsAsync();
            var editVoice = api.VoicesEndpoint.EditVoiceSettingsAsync(voiceId, voiceSettings);
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
            MemoryStream ms = new(mp3Data)
            {
                Position = 0
            };
            var WavStream = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(ms));
            var waveOut = new WaveOut
            {
                DeviceNumber = SelectedOutputDevice
            };
            //it has to be 24000, 16, 1 for some reason?
            var waveStream = new RawSourceWaveStream(WavStream, new WaveFormat(44100, 16, 1))
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

        public async Task<List<string>> TTSGetElevenLabsVoices()
        {
            List<string> ElevenLabVoices = [];

            //we dont need to re-do this if its alrready filled
            if (_elevenLabVoiceList.Count > 0)
            {
                foreach (var voice in _elevenLabVoiceList)
                {
                    ElevenLabVoices.Add(voice.Key);
                }
                return ElevenLabVoices;
            }

            var api = new ElevenLabsClient(ElevelLabsAPIKey);
            var tier = await api.UserEndpoint.GetSubscriptionInfoAsync();
            _bBBlog.Info($"Tier: {tier.Tier} for {tier.VoiceLimit}");
            //get all the voices but, lets also accept a timeout incase theres issues
            var getAllVoicesTask = api.VoicesEndpoint.GetAllVoicesAsync();

            //lets wait 10 seconds for the voices to come back
            var timeoutTask = Task.Delay(15000);

            var completedTask = await Task.WhenAny(getAllVoicesTask, timeoutTask);

            if (completedTask == getAllVoicesTask)
            {
                var voices = await getAllVoicesTask;
                _bBBlog.Info($"Voices found: {voices.Count}");
                // Process the voices...
                foreach (var voice in voices)
                {
                    _elevenLabVoiceList.Add(voice.Name, voice.Id);
                }

                if (_elevenLabVoiceList.Count > 0)
                {
                    foreach (var voice in _elevenLabVoiceList)
                    {
                        ElevenLabVoices.Add(voice.Key);
                    }
                }
                return ElevenLabVoices;
            }
            else
            {
                // Timeout occurred
                // Handle the timeout scenario...
                _bBBlog.Error("Timeout after 15 seconds occurred while fetching ElevenLab voices");
                return null; // or return null, throw an exception, etc.
            }
        }

  
        public async Task<bool> ElevenLabsAPIKeyTest()
        {
            _bBBlog.Info("ElevenLabsAPIKeyTest called");
            if (ElevelLabsAPIKey.Length > 1)
            {
                var api = new ElevenLabsClient(ElevelLabsAPIKey);
                try
                {
                    SubscriptionInfo result = await api.UserEndpoint.GetSubscriptionInfoAsync();
                }
                catch (Exception ex)
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
            this.ElevelLabsAPIKey = elevelLabsAPIKey;
            _elevenLabVoiceList = [];
        }
    }
}
