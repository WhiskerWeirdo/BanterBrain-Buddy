using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Versioning;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// CODING RULES:
/// •	Local variables, private instance, static fields and method parameters should be camelCase.
/// •	Methods, constants, properties, events and classes should be PascalCase.
/// •	Global private instance fields should be in camelCase prefixed with an underscore.
/// </summary>

namespace BanterBrain_Buddy
{
    internal class NativeSpeech
    {
        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SpeechSynthesizer _nativeSynthesizer;
        private MemoryStream _nativeAudioStream;

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
        public async Task<bool> NativeSpeak(string TextToSay)
        {
            _nativeSynthesizer.Speak(TextToSay);
            //TODO: handle issues with device not being available or not responsive
            _bBBlog.Info("Device out: " + SelectedOutputDevice);
            var waveOut = new WaveOut();
            waveOut.DeviceNumber = SelectedOutputDevice;
            var waveStream = new RawSourceWaveStream(_nativeAudioStream, new WaveFormat(24000,16,1));
            waveStream.Position = 0;
            waveOut.Init(waveStream);

            waveOut.Play();
            while (waveOut.PlaybackState != PlaybackState.Stopped)
            {
                await Task.Delay(500);
            }
            return true;
        }


        /// <summary>
        /// initialize the native Windows TTS engine
        /// </summary>
        /// <param name="OutputDevice">The text of the selected output device. Limited to 32 characters (Windows limition)</param>
        /// <returns></returns>
        [SupportedOSPlatform("windows6.1")]
        public async Task NativeTTSInit(string VoiceUsed, string OutputDevice)
        {
            _bBBlog.Info("Starting Native Text To Speech, Initializing");
            _bBBlog.Debug("Init Native Output Device: " + OutputDevice + " using: " + VoiceUsed);
            SetSelectedOutputDevice(OutputDevice);
            _nativeSynthesizer = new();
            string selectedVoice = VoiceUsed.Substring(0, VoiceUsed.IndexOf("-"));
            _bBBlog.Debug("Init Native Voice: " + selectedVoice);
            _nativeSynthesizer.SelectVoice(selectedVoice);
            _nativeAudioStream = new();
            _nativeSynthesizer.SetOutputToWaveStream(_nativeAudioStream);
        }

        [SupportedOSPlatform("windows6.1")]
        public async Task<List<NativeVoices>> TTSNativeGetVoices()
        {
            List<NativeVoices> nativeVoicesList = [];
            var synthesizer = new SpeechSynthesizer();
            foreach (var voice in synthesizer.GetInstalledVoices())
            {
                var info = voice.VoiceInfo;
                //_bBBlog.Info($"Native TTS Name: {info.Name}");

                var tmpVoice = new NativeVoices()
                {
                    Name = info.Name,
                    Culture = info.Culture.ToString(),
                    Gender = info.Gender.ToString()
                };
                nativeVoicesList.Add(tmpVoice);

            }
            return nativeVoicesList;
        }

        public NativeSpeech()
        {
        }

    }
}
