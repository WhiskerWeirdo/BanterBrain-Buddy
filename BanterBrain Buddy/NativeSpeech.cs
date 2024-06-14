using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Versioning;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// CODING RULES:
/// •	Local variables, private instance, static fields and method parameters should be camelCase.
/// •	Methods, constants, properties, events and classes should be PascalCase.
/// </summary>

namespace BanterBrain_Buddy
{
    internal class NativeSpeech
    {
        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SpeechSynthesizer _nativeSynthesizer;
        private MemoryStream _nativeAudioStream;

        private int SelectedOutputDevice;
        private string _sTTOutputText;
        private bool _sTTDone;
        private float _voiceVolume;
        private int _voiceRate;
        private int _voicePitch;
        private string _voiceCulture;


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
            //use promptbuilder for native SSML
            _voiceRate += 100;
            int voiceVolume = 100;
            //_voicePitch += 100;
            var tmpSSML = $"<voice xml:lang=\"{_voiceCulture}\">" + 
                $"<prosody rate=\"{_voiceRate}%\" volume=\"{voiceVolume}%\">{TextToSay}</prosody>" + "</voice>";
            _bBBlog.Debug("Native SSML: " + tmpSSML);
            PromptBuilder pb = new PromptBuilder();
            pb.AppendSsmlMarkup(tmpSSML);
            _nativeSynthesizer.Speak(pb);
            //TODO: handle issues with device not being available or not responsive
            //_bBBlog.Info("Device out: " + SelectedOutputDevice);
            var waveOut = new WaveOut
            {
                DeviceNumber = SelectedOutputDevice
            };


            //it has to be 24000, 16, 1 for some reason?
            var waveStream = new RawSourceWaveStream(_nativeAudioStream, new WaveFormat(24000, 16, 1))
            {
                //reset the stream to the beginning or you wont hear anything
                Position = 0
            };

            // Create a SampleChannel for volume control
            var sampleChannel = new SampleChannel(waveStream, false);

            // Use a VolumeSampleProvider to apply the volume multiplier
            var volumeProvider = new VolumeSampleProvider(sampleChannel)
            {
                Volume = _voiceVolume
            };

            var semitone = Math.Pow(2, 1.0 / 12);
            var upOneTone = semitone * semitone;
            var downOneTone = 1.0 / upOneTone;
            Double pitchTone = 1.0;
            //alright lets set the pitch tone
            if (_voicePitch == 0)
            {
                pitchTone = 1.0;
            }
            else if (_voicePitch > 0)
            {
                pitchTone = Math.Pow(upOneTone, _voicePitch/10);
            }
            else if (_voicePitch < 0)
            {
                pitchTone = Math.Pow(downOneTone, Math.Abs(_voicePitch)/10);
            }
            var pitch = new SmbPitchShiftingSampleProvider(volumeProvider);
            pitch.PitchFactor = (float)pitchTone;


            _bBBlog.Debug("Native Pitch: " + pitch.PitchFactor);
            if (pitch.PitchFactor == 1.0)
            {
                waveOut.Init(pitch);
            } else
            {
                //ok we need to chop off the start, because this buggers the audio due to artifacts from pitching
                waveOut.Init(pitch.Skip(TimeSpan.FromMilliseconds(300)));
            }

            //waveOut.Init(waveStream);
            
            waveOut.Play();
            while (waveOut.PlaybackState != PlaybackState.Stopped)
            {
                await Task.Delay(500);
            }
            return true;
        }

        public async Task<bool> NativePlayWaveFile(string _tmpWavFile)
        {
            SetSelectedOutputDevice(Properties.Settings.Default.TTSAudioOutput);
            _bBBlog.Debug("Playing Native Wave File: " + _tmpWavFile);
            var waveOut = new WaveOut
            {
                DeviceNumber = SelectedOutputDevice
            };
            var waveStream = new WaveFileReader(_tmpWavFile);
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
        public Task NativeTTSInit(string VoiceUsed, string OutputDevice, int VoiceVolume, int VoiceRate, int VoicePitch)
        {
            _bBBlog.Info("Starting Native Text To Speech, Initializing");
            _bBBlog.Debug("Init Native Output Device: " + OutputDevice + " using: " + VoiceUsed);
            //we get the culture from teh VoiceUsed string
            //which is basically the last part of the string after the second last -
            _voiceCulture = VoiceUsed[(VoiceUsed.LastIndexOf('-')-2)..];
            _bBBlog.Debug("Init Native Culture: " + _voiceCulture);
            SetSelectedOutputDevice(OutputDevice);
            _nativeSynthesizer = new();
            string selectedVoice = VoiceUsed[..VoiceUsed.IndexOf('-')];
            _bBBlog.Debug("Init Native Voice: " + selectedVoice);

            _nativeSynthesizer.SelectVoice(selectedVoice);

            // Ensure volumeControl is within the expected range
            VoiceVolume = Math.Clamp(VoiceVolume, -100, 100);

            // Convert volumeControl to a volume multiplier
            //float volumeMultiplier = (float)Math.Pow(10, volumeControl / 100.0); // Use logarithmic scale for volume control
            // Convert volumeControl to a volume multiplier
            _voiceVolume = 1.0f + (VoiceVolume / 100.0f);
            
            _voiceRate = VoiceRate;
            _voicePitch = VoicePitch;
            _nativeAudioStream = new();
            _nativeSynthesizer.SetOutputToWaveStream(_nativeAudioStream);
            return Task.CompletedTask;
        }

        [SupportedOSPlatform("windows6.1")]
        public static Task<List<NativeVoices>> TTSNativeGetVoices()
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
            return Task.FromResult(nativeVoicesList);
        }


        [SupportedOSPlatform("windows6.1")]
        private void NativeRecognizeCompletedHandler(object sender, RecognizeCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                _bBBlog.Error("Native STT Error encountered, " + e.Error.GetType().Name + " : " + e.Error.Message);
                _sTTOutputText = null;
            }
            if (e.Cancelled)
            {
                _bBBlog.Info("Native STT Operation cancelled");
                _sTTOutputText = null;
            }
            if (e.InputStreamEnded)
            {
                _bBBlog.Info("Mative STT recognize Stopped.");
            }

            _sTTDone = true;
        }

        [SupportedOSPlatform("windows6.1")]
        // Handle the SpeechRecognized event.  
        private void NativeSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result != null && e.Result.Text != null)
            {
                _bBBlog.Info("Native recognized text: " + e.Result.Text);
                _sTTOutputText += e.Result.Text + "\r\n";
            }
            else
            {
                _bBBlog.Info("Native recognized text not available.");
            }
        }

        [SupportedOSPlatform("windows6.1")]
        public async Task<string> NativeSpeechRecognizeStart(string _tmpWavFile)
        {

            string culture = Properties.Settings.Default.NativeSpeechRecognitionLanguageComboBox;
            if (culture.Length > 1)
            {
                _bBBlog.Info("Setting Native Speech Recognition Language to: " + culture);
            }
            else
            {
                _bBBlog.Info("Nothing set, so setting Native Speech Recognition Language to: en-US");
                culture = "en-US";
            }
            _bBBlog.Info("Starting Native Speech Recognition with language: " + culture);
            // Create an in-process speech recognizer for the en-US locale.  
            SpeechRecognitionEngine recognizer2 = new(new System.Globalization.CultureInfo(culture));
            // Create and load a dictation grammar.  
            recognizer2.LoadGrammar(new DictationGrammar());
            recognizer2.SetInputToWaveFile(_tmpWavFile);
            // Attach event handlers for the results of recognition.  
            recognizer2.SpeechRecognized +=
              new EventHandler<SpeechRecognizedEventArgs>(NativeSpeechRecognized);
            recognizer2.RecognizeCompleted +=
              new EventHandler<RecognizeCompletedEventArgs>(NativeRecognizeCompletedHandler);

            _bBBlog.Info("Starting asynchronous Native recognition... on " + _tmpWavFile);

            _sTTDone = false;
            recognizer2.RecognizeAsync(RecognizeMode.Multiple);
            while (!_sTTDone)
            {
                await Task.Delay(500);
            }
            _bBBlog.Info("Native STT done.");
            recognizer2.Dispose();
            return _sTTOutputText;
        }

        public NativeSpeech()
        {
            _bBBlog.Info("Native Speech Engine Initialized");
            _sTTDone = false;
        }

    }
}
