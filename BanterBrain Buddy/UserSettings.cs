﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace BanterBrain_Buddy
{
    public class UserSettings
    {
        //alright since Settings.settings is being a bitch, we do it ourselves in a json file
        public string VoiceInput {get;set;}
        public string PTTHotkey {get;set;}
        public string GPTModel {get;set;}
        public string GPTAPIKey {get;set;}
        public string TwitchBotName {get;set;}
        public string TwitchAccessToken {get;set;}
        public string TwitchChannel {get;set;}
        public string TwitchCommandTrigger {get;set;}
        public string TwitchTestSendText {get;set;}
        public bool TwitchSendTextCheckBox {get;set;}
        public int TwitchChatCommandDelay {get;set;}
        public bool TwitchNeedsFollower {get;set;}
        public bool TwitchNeedsSubscriber {get;set;}
        public int TwitchMinBits {get;set;}
        public bool TwitchSubscribed {get;set;}
        public bool TwitchCommunitySubs {get;set;}
        public bool TwitchGiftedSub { get;set;}
        public string AzureAPIKeyTextBox {get;set;}
        public string AzureRegionTextBox {get;set;}
        public string AzureLanguageComboBox { get; set; }
        public bool TwitchEnable { get; set; }
        public bool TwitchCheckAuthAtStartup { get; set; }
        public bool TwitchReadChatCheckBox { get; set; }
        public bool TwitchCheerCheckbox { get; set; }
        public bool TTSAPIKeyTextBoxEnabled { get; set; }
        public bool TTSRegionTextBoxEnabled { get; set; }
        public bool TwitchChannelPointCheckBox { get; set; }
        public string TwitchCustomRewardName { get; set; }
        public int GPTMaxTokens { get; set; }
        public float GPTTemperature { get; set; }
        public string TTSNativeVoiceSelected { get; set; }
        public string STTSelectedProvider { get; set; }
        public string MainSelectedPersona { get; set; }
        public string TTSAudioOutput { get; set; }
        public string SelectedLLM { get; set; }
        public string TwitchChatPersona { get; set; }
        public string TwitchChannelPointPersona { get; set; }
        public string TwitchCheeringPersona { get; set; }
        public string TwitchSubscriptionPersona { get; set; }
        public bool TwitchAutoStart { get; set; }
        public string ElevenLabsAPIkey { get; set; }
        public string OllamaURI { get; set; }
        public string OllamaSelectedModel { get; set; }
        public bool UseOllamaLLMCheckBox { get; set; }
        public string OllamaResponseLengthComboBox { get; set; }
        public string TwitchChatSound { get; set; }
        public bool TwitchChatSoundCheckBox { get; set; }
        public bool TwitchChannelSoundCheckBox { get; set; }
        public string TwitchChannelSound { get; set; }
        public bool TwitchCheeringSoundCheckBox { get; set; }
        public string TwitchCheeringSound { get; set; }
        public bool TwitchSubscriptionSoundCheckBox { get; set; }
        public string TwitchSubscriptionSoundTextBox { get; set; }
        public string TwitchResponseToChatDelayTextBox { get; set; }
        public bool TwitchResponseToChatCheckBox { get; set; }
        public bool TwitchSubscriptionTTSResponseOnlyRadioButton { get; set; }
        public bool TwitchCheeringTTSResponseOnlyRadioButton { get; set; }
        public bool TwitchChannelPointTTSResponseOnlyRadioButton { get; set; }
        public bool TwitchChatTTSResponseOnlyRadioButton { get; set; }
        public string NativeSpeechRecognitionLanguageComboBox { get; set; }
        public string WhisperSpeechRecognitionComboBox { get; set; }
        public string TwitchAuthServerConfig { get; set; }
        public string WebsourceServer { get; set; }
        public bool WebsourceServerEnable { get; set; }
        public bool DelayFinishToChatcCheckBox { get; set; }
        public string TwitchDelayMessageTextBox { get; set; }
        public string StreamerNameTextBox { get; set; }
        public string LogDir { get; set; }
        public string TwitchBotAuthKey { get; set; }
        public string TwitchLLMLanguageComboBox { get; set; }
        public string ElevenLabsModel { get; set; }
        public bool BadWordFilter { get; set; }

    }

    public class SettingsManager
    {
        private static readonly Lazy<SettingsManager> instance = new Lazy<SettingsManager>(() => new SettingsManager());
        private UserSettings settings; 
        private readonly string settingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BanterBrain\\settings.json";

        private SettingsManager()
        {
            LoadSettings();
        }

        public static SettingsManager Instance => instance.Value;
        public UserSettings Settings => settings;

        private void LoadSettings()
        {
            if (File.Exists(settingsFilePath))
            {
                var json = File.ReadAllText(settingsFilePath);
                settings = JsonConvert.DeserializeObject<UserSettings>(json);
            }
            else
            {
                settings = new UserSettings();
                //then we should also write it to the file even if it contains no information yet, just so we have the file.
                SaveSettings();
            }
        }

        public void SaveSettings()
        {
            var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(settingsFilePath, json);
        }
    }
}