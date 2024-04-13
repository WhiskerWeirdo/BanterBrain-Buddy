using CSCore.MediaFoundation;
using CSCore.SoundOut;
using System.IO;
using System.Runtime.Versioning;
using System.Speech.Synthesis;
using System.Threading.Tasks;

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

        private WaveOutDevice _selectedOutputDevice;
        private WaveOutDevice SelectedOutputDevice
        {
            get { return _selectedOutputDevice; }
            set
            {
                _selectedOutputDevice = value;
            }
        }

        private void SetSelectedOutputDevice(string OutputDevice)
        {
            _bBBlog.Info($"Setting selected output device for Native TTS to: {OutputDevice}");
            foreach (var device in WaveOutDevice.EnumerateDevices())
            {
                if (device.Name.StartsWith(OutputDevice))
                {
                    _bBBlog.Debug($"Selected outputdevice = {device.Name}");
                    SelectedOutputDevice = device;
                }
            }
        }


        [SupportedOSPlatform("windows6.1")]
        public async Task<bool> NativeSpeak(string TextToSay)
        {
            _nativeSynthesizer.Speak(TextToSay);
            var waveOut = new WaveOut { Device = new WaveOutDevice(SelectedOutputDevice.DeviceId) };
            using var waveSource = new MediaFoundationDecoder(_nativeAudioStream);
            waveOut.Initialize(waveSource);
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
        public async Task NativeTTSInit(string OutputDevice)
        {
            _bBBlog.Info("Starting Native Text To Speech, Initializing");
            _bBBlog.Debug("Init Native Output Device: " + OutputDevice);
            SetSelectedOutputDevice(OutputDevice);
            _nativeSynthesizer = new();
            _nativeAudioStream = new();
            _nativeSynthesizer.SetOutputToWaveStream(_nativeAudioStream);
        }

        public NativeSpeech()
        {
        }

    }
}
