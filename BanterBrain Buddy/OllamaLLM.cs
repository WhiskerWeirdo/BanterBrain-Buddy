using OllamaSharp;
using OllamaSharp.Models.Chat;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BanterBrain_Buddy
{
    internal class OllamaLLM
    {
        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly OllamaApiClient _ollama;

        public async Task<List<string>> OllamaLLMGetModels()
        {
            if (_ollama == null)
            {
                _bBBlog.Error("OllamaApiClient not initialized");
                return null;
            }

            List<string> models = new List<string>();
            _bBBlog.Debug("OllamaLLMGetModels");

            var localModels = await _ollama.ListLocalModels();
            if (localModels.Count() == 0)
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

        public async Task<string> OllamaGetResponse(string Text, string selectedModel)
        {
            if (_ollama == null)
            {
                _bBBlog.Error("OllamaApiClient not initialized");
                return null;
            }
            string response ="";
            _ollama.SelectedModel = selectedModel;
            //we start a new context every time
            var chat = _ollama.Chat(stream => response += stream.Message?.Content);
            _bBBlog.Debug("OllamaGetResponse sending: " + Text + "using model: " + _ollama.SelectedModel);
            await chat.SendAs("user",Text);
            return response;
        }

        public OllamaLLM(string LocalUri)
        {
            _bBBlog.Info("OllamaLLM init on Uri: "+ LocalUri);
            _ollama = new OllamaApiClient(new Uri(LocalUri));
        }
    }
}
