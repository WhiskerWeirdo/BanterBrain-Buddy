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

        public async Task<List<string>> OllamaLLMGetModels()
        {
            if (_ollama == null)
            {
                _bBBlog.Error("OllamaApiClient not initialized");
                return null;
            }

            List<string> models = [];
            _bBBlog.Debug("OllamaLLMGetModels");

            var localModels = await _ollama.ListLocalModels();
            if (!localModels.Any())
            {
                _bBBlog.Error("No local models found");
                return null;
            }
            foreach (var model in localModels)
            {
                models.Add(model.Name);
            }
            return models;
        }

        public async Task<string> OllamaGetResponse(string Text, string tmpRoleText)
        {
            if (_ollama == null)
            {
                _bBBlog.Error("OllamaApiClient not initialized");
                return null;
            }

            string response = "";
            int sentenceLength = 1;
            switch (Properties.Settings.Default.OllamaResponseLengthComboBox)
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
            string tmpSetupString = tmpRoleText + ". Make your response a maximum of " + sentenceLength + " sentences! How would you then respond to: " + Text;
            _ollama.SelectedModel = Properties.Settings.Default.OllamaSelectedModel;
            if (_ollama.SelectedModel == null)
            {
                _bBBlog.Error("OllamaGetResponse failed, no model selected from saved settings.");
                return null;
            }
            _bBBlog.Info("Sending to OpenAI GPT LLM: " + Text);
            _bBBlog.Info("SystemRole: " + tmpRoleText + " \r\nModel: " + _ollama.SelectedModel);
            
            if (chat == null)
            {
                chat = _ollama.Chat(stream => response += stream.Message?.Content);
            }
            //if we have an existing conversation
            try
            {
                history = await chat.Send(Text);
                response = history.LastOrDefault().Content.ToString();
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
            if (result.Count == 0)
            {
                _bBBlog.Error("OllamaVerify failed, no models found");
                return false;
            }
            if (!result.Contains(Properties.Settings.Default.OllamaSelectedModel))
            {
                _bBBlog.Error("OllamaVerify failed, selected model not found");
                return false;
            }
            return true;
        }

        public OllamaLLM(string LocalUri)
        {
            _bBBlog.Info("OllamaLLM init on Uri: " + LocalUri);
            _ollama = new OllamaApiClient(new Uri(LocalUri));
        }
    }
}
