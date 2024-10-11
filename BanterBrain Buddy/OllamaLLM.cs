using ElevenLabs.History;
using OllamaSharp;
using OllamaSharp.Models.Chat;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Extensions.System;

namespace BanterBrain_Buddy
{
    internal class OllamaLLM
    {
        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly OllamaApiClient _ollama;
        private Chat chat;
        private IEnumerable<Message> history;
        private SettingsManager UserSettingsManager = SettingsManager.Instance;

        public async Task<List<string>> OllamaLLMGetModels()
        {
            if (_ollama == null)
            {
                _bBBlog.Error("OllamaApiClient not initialized");
                return null;
            }

            List<string> models = [];
            _bBBlog.Debug("OllamaLLMGetModels");

            IEnumerable<OllamaSharp.Models.Model> localModels;
            try
            {
                localModels = await _ollama.ListLocalModels();
            }
            catch (Exception e)
            {
                _bBBlog.Error("OllamaLLMGetModels failed: " + e.Message);
                return null;
            }
                if (!localModels.Any())
            {
                _bBBlog.Error("No local models found");
                return null;
            }
            foreach (var model in localModels)
            {
                _bBBlog.Debug("Model found: " + model.Name);
                models.Add(model.Name);
            }
            return models;
        }

        public async Task<string> OllamaGetResponse(string Text, string tmpRoleText)//, Chat chat)
        {
            if (_ollama == null)
            {
                _bBBlog.Error("OllamaApiClient not initialized");
                return null;
            }

            string response = "";
            int sentenceLength = 1;
            switch (UserSettingsManager.Settings.OllamaResponseLengthComboBox)
            {
                case "Short":
                    _bBBlog.Info("Ollama short response selected");
                    sentenceLength = 2;
                    break;
                case "Normal":
                    _bBBlog.Info("Ollama normal response selected");
                    sentenceLength = 4;
                    break;
                case "Long":
                    _bBBlog.Info("Ollama long response selected");
                    sentenceLength = 8;
                    break;
            }
            string tmpSetupString = tmpRoleText + ". Make your response a maximum of " + sentenceLength + " sentences! Absolutely not longer! How would you then respond to: " + Text;
            _ollama.SelectedModel = UserSettingsManager.Settings.OllamaSelectedModel;
            if (_ollama.SelectedModel == null)
            {
                _bBBlog.Error("OllamaGetResponse failed, no model selected from saved settings.");
                return null;
            }
            _bBBlog.Info("Sending to OpenAI GPT LLM: " + Text);
            _bBBlog.Info("SystemRole: " + tmpRoleText + " \r\nModel: " + _ollama.SelectedModel);
            
            //chat ??= _ollama.Chat(stream => response += stream.Message?.Content);
            //chat ??= _ollama.Chat();
            if (chat != null)
            {
                _bBBlog.Info("Chat already initialized");
            }
            else
            {
                _bBBlog.Info("Chat not initialized");
            }
            chat ??= new Chat(_ollama);
            _bBBlog.Info("Chat initialized");
            //if we have an existing conversation
            try
            {
                //history = chat.Send(tmpSetupString);
                await foreach (var message in chat.Send(tmpSetupString))
                {
                    response += message;

                }
                //response = history.LastOrDefault().Content.ToString();
                _bBBlog.Info("Ollama response: " + response);
            }
            catch (Exception e)
            {
                _bBBlog.Error("OllamaGetResponse failed: " + e.Message);
                return null;
            }
            
            _bBBlog.Info("Ollama response: " + response);
            return response;
        }

        public async Task<bool> OllamaVerify()
        {
            _bBBlog.Info("OllamaVerify called");

            var result = await OllamaLLMGetModels();
            
            if (result == null || result.Count == 0)
            {
                _bBBlog.Error("OllamaVerify failed, no models found or not running on URI");
                return false;
            }
            if (!result.Contains(UserSettingsManager.Settings.OllamaSelectedModel))
            {
                _bBBlog.Error("OllamaVerify failed, selected model not found");
                return false;
            }
            return true;
        }

        //we reset the conversation
        public void OllamaChatReset()
        {
            if (chat != null)
            {
                _bBBlog.Info("OllamaChatReset called, forcefully resetting the history");
                chat = new Chat(_ollama);

            }
            else
            {
                _bBBlog.Debug("OllamaChatReset failed, chat was not initialized anyway");
            }
        }

        public OllamaLLM(string LocalUri)
        {
            _bBBlog.Info("OllamaLLM init on Uri: " + LocalUri);
            _ollama = new OllamaApiClient(new Uri(LocalUri));
        }
    }
}
