﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BanterBrain_Buddy.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.9.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string VoiceInput {
            get {
                return ((string)(this["VoiceInput"]));
            }
            set {
                this["VoiceInput"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("LControlKey + D")]
        public string PTTHotkey {
            get {
                return ((string)(this["PTTHotkey"]));
            }
            set {
                this["PTTHotkey"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("gpt-3.5-turbo")]
        public string GPTModel {
            get {
                return ((string)(this["GPTModel"]));
            }
            set {
                this["GPTModel"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string GPTAPIKey {
            get {
                return ((string)(this["GPTAPIKey"]));
            }
            set {
                this["GPTAPIKey"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TwitchUsername {
            get {
                return ((string)(this["TwitchUsername"]));
            }
            set {
                this["TwitchUsername"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TwitchAccessToken {
            get {
                return ((string)(this["TwitchAccessToken"]));
            }
            set {
                this["TwitchAccessToken"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TwitchChannel {
            get {
                return ((string)(this["TwitchChannel"]));
            }
            set {
                this["TwitchChannel"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("$BBB")]
        public string TwitchCommandTrigger {
            get {
                return ((string)(this["TwitchCommandTrigger"]));
            }
            set {
                this["TwitchCommandTrigger"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Hello! I am BanterBrain Buddy https://banterbrain.tv")]
        public string TwitchTestSendText {
            get {
                return ((string)(this["TwitchTestSendText"]));
            }
            set {
                this["TwitchTestSendText"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TwitchSendTextCheckBox {
            get {
                return ((bool)(this["TwitchSendTextCheckBox"]));
            }
            set {
                this["TwitchSendTextCheckBox"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("300")]
        public int TwitchChatCommandDelay {
            get {
                return ((int)(this["TwitchChatCommandDelay"]));
            }
            set {
                this["TwitchChatCommandDelay"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TwitchNeedsFollower {
            get {
                return ((bool)(this["TwitchNeedsFollower"]));
            }
            set {
                this["TwitchNeedsFollower"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TwitchNeedsSubscriber {
            get {
                return ((bool)(this["TwitchNeedsSubscriber"]));
            }
            set {
                this["TwitchNeedsSubscriber"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100")]
        public int TwitchMinBits {
            get {
                return ((int)(this["TwitchMinBits"]));
            }
            set {
                this["TwitchMinBits"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TwitchSubscribed {
            get {
                return ((bool)(this["TwitchSubscribed"]));
            }
            set {
                this["TwitchSubscribed"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TwitchCommunitySubs {
            get {
                return ((bool)(this["TwitchCommunitySubs"]));
            }
            set {
                this["TwitchCommunitySubs"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TwitchGiftedSub {
            get {
                return ((bool)(this["TwitchGiftedSub"]));
            }
            set {
                this["TwitchGiftedSub"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string AzureAPIKeyTextBox {
            get {
                return ((string)(this["AzureAPIKeyTextBox"]));
            }
            set {
                this["AzureAPIKeyTextBox"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string AzureRegionTextBox {
            get {
                return ((string)(this["AzureRegionTextBox"]));
            }
            set {
                this["AzureRegionTextBox"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("en-US")]
        public string AzureLanguageComboBox {
            get {
                return ((string)(this["AzureLanguageComboBox"]));
            }
            set {
                this["AzureLanguageComboBox"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TwitchEnable {
            get {
                return ((bool)(this["TwitchEnable"]));
            }
            set {
                this["TwitchEnable"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TwitchCheckAuthAtStartup {
            get {
                return ((bool)(this["TwitchCheckAuthAtStartup"]));
            }
            set {
                this["TwitchCheckAuthAtStartup"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TwitchReadChatCheckBox {
            get {
                return ((bool)(this["TwitchReadChatCheckBox"]));
            }
            set {
                this["TwitchReadChatCheckBox"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TwitchCheerCheckbox {
            get {
                return ((bool)(this["TwitchCheerCheckbox"]));
            }
            set {
                this["TwitchCheerCheckbox"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TTSAPIKeyTextBoxEnabled {
            get {
                return ((bool)(this["TTSAPIKeyTextBoxEnabled"]));
            }
            set {
                this["TTSAPIKeyTextBoxEnabled"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TTSRegionTextBoxEnabled {
            get {
                return ((bool)(this["TTSRegionTextBoxEnabled"]));
            }
            set {
                this["TTSRegionTextBoxEnabled"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TwitchChannelPointCheckBox {
            get {
                return ((bool)(this["TwitchChannelPointCheckBox"]));
            }
            set {
                this["TwitchChannelPointCheckBox"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TwitchCustomRewardName {
            get {
                return ((string)(this["TwitchCustomRewardName"]));
            }
            set {
                this["TwitchCustomRewardName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100")]
        public int GPTMaxTokens {
            get {
                return ((int)(this["GPTMaxTokens"]));
            }
            set {
                this["GPTMaxTokens"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public float GPTTemperature {
            get {
                return ((float)(this["GPTTemperature"]));
            }
            set {
                this["GPTTemperature"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TTSNativeVoiceSelected {
            get {
                return ((string)(this["TTSNativeVoiceSelected"]));
            }
            set {
                this["TTSNativeVoiceSelected"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string STTSelectedProvider {
            get {
                return ((string)(this["STTSelectedProvider"]));
            }
            set {
                this["STTSelectedProvider"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string MainSelectedPersona {
            get {
                return ((string)(this["MainSelectedPersona"]));
            }
            set {
                this["MainSelectedPersona"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TTSAudioOutput {
            get {
                return ((string)(this["TTSAudioOutput"]));
            }
            set {
                this["TTSAudioOutput"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SelectedLLM {
            get {
                return ((string)(this["SelectedLLM"]));
            }
            set {
                this["SelectedLLM"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TwitchChatPersona {
            get {
                return ((string)(this["TwitchChatPersona"]));
            }
            set {
                this["TwitchChatPersona"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TwitchChannelPointPersona {
            get {
                return ((string)(this["TwitchChannelPointPersona"]));
            }
            set {
                this["TwitchChannelPointPersona"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TwitchCheeringPersona {
            get {
                return ((string)(this["TwitchCheeringPersona"]));
            }
            set {
                this["TwitchCheeringPersona"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TwitchSubscriptionPersona {
            get {
                return ((string)(this["TwitchSubscriptionPersona"]));
            }
            set {
                this["TwitchSubscriptionPersona"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TwitchAutoStart {
            get {
                return ((bool)(this["TwitchAutoStart"]));
            }
            set {
                this["TwitchAutoStart"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ElevenLabsAPIkey {
            get {
                return ((string)(this["ElevenLabsAPIkey"]));
            }
            set {
                this["ElevenLabsAPIkey"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:11434")]
        public string OllamaURI {
            get {
                return ((string)(this["OllamaURI"]));
            }
            set {
                this["OllamaURI"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string OllamaSelectedModel {
            get {
                return ((string)(this["OllamaSelectedModel"]));
            }
            set {
                this["OllamaSelectedModel"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool UseOllamaLLMCheckBox {
            get {
                return ((bool)(this["UseOllamaLLMCheckBox"]));
            }
            set {
                this["UseOllamaLLMCheckBox"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Normal")]
        public string OllamaResponseLengthComboBox {
            get {
                return ((string)(this["OllamaResponseLengthComboBox"]));
            }
            set {
                this["OllamaResponseLengthComboBox"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TwitchChatSound {
            get {
                return ((string)(this["TwitchChatSound"]));
            }
            set {
                this["TwitchChatSound"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TwitchChatSoundCheckBox {
            get {
                return ((bool)(this["TwitchChatSoundCheckBox"]));
            }
            set {
                this["TwitchChatSoundCheckBox"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TwitchChannelSoundCheckBox {
            get {
                return ((bool)(this["TwitchChannelSoundCheckBox"]));
            }
            set {
                this["TwitchChannelSoundCheckBox"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TwitchChannelSound {
            get {
                return ((string)(this["TwitchChannelSound"]));
            }
            set {
                this["TwitchChannelSound"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TwitchCheeringSoundCheckBox {
            get {
                return ((bool)(this["TwitchCheeringSoundCheckBox"]));
            }
            set {
                this["TwitchCheeringSoundCheckBox"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TwitchCheeringSound {
            get {
                return ((string)(this["TwitchCheeringSound"]));
            }
            set {
                this["TwitchCheeringSound"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TwitchSubscriptionSoundCheckBox {
            get {
                return ((bool)(this["TwitchSubscriptionSoundCheckBox"]));
            }
            set {
                this["TwitchSubscriptionSoundCheckBox"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TwitchSubscriptionSoundTextBox {
            get {
                return ((string)(this["TwitchSubscriptionSoundTextBox"]));
            }
            set {
                this["TwitchSubscriptionSoundTextBox"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public string TwitchResponseToChatDelayTextBox {
            get {
                return ((string)(this["TwitchResponseToChatDelayTextBox"]));
            }
            set {
                this["TwitchResponseToChatDelayTextBox"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TwitchResponseToChatCheckBox {
            get {
                return ((bool)(this["TwitchResponseToChatCheckBox"]));
            }
            set {
                this["TwitchResponseToChatCheckBox"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool TwitchSubscriptionTTSResponseOnlyRadioButton {
            get {
                return ((bool)(this["TwitchSubscriptionTTSResponseOnlyRadioButton"]));
            }
            set {
                this["TwitchSubscriptionTTSResponseOnlyRadioButton"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool TwitchCheeringTTSResponseOnlyRadioButton {
            get {
                return ((bool)(this["TwitchCheeringTTSResponseOnlyRadioButton"]));
            }
            set {
                this["TwitchCheeringTTSResponseOnlyRadioButton"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool TwitchChannelPointTTSResponseOnlyRadioButton {
            get {
                return ((bool)(this["TwitchChannelPointTTSResponseOnlyRadioButton"]));
            }
            set {
                this["TwitchChannelPointTTSResponseOnlyRadioButton"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool TwitchChatTTSResponseOnlyRadioButton {
            get {
                return ((bool)(this["TwitchChatTTSResponseOnlyRadioButton"]));
            }
            set {
                this["TwitchChatTTSResponseOnlyRadioButton"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("en-US")]
        public string NativeSpeechRecognitionLanguageComboBox {
            get {
                return ((string)(this["NativeSpeechRecognitionLanguageComboBox"]));
            }
            set {
                this["NativeSpeechRecognitionLanguageComboBox"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("English")]
        public string WhisperSpeechRecognitionComboBox {
            get {
                return ((string)(this["WhisperSpeechRecognitionComboBox"]));
            }
            set {
                this["WhisperSpeechRecognitionComboBox"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:8080")]
        public string TwitchAuthServerConfig {
            get {
                return ((string)(this["TwitchAuthServerConfig"]));
            }
            set {
                this["TwitchAuthServerConfig"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:9138")]
        public string WebsourceServer {
            get {
                return ((string)(this["WebsourceServer"]));
            }
            set {
                this["WebsourceServer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool WebsourceServerEnable {
            get {
                return ((bool)(this["WebsourceServerEnable"]));
            }
            set {
                this["WebsourceServerEnable"] = value;
            }
        }
    }
}
