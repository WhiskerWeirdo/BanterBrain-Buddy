using OpenAI_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BanterBrain_Buddy
{
    /// <summary>
    /// CODING RULES:
    /// •	Local variables, private instance, static fields and method parameters should be camelCase.
    /// •	Methods, constants, properties, events and classes should be PascalCase.
    /// •	Global private instance fields should be in camelCase prefixed with an underscore.
    /// </summary>

    internal class OpenAI
    {
        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool OpenAICheckAPIKey()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.GPTAPIKey))
            {
                _bBBlog.Error("OpenAI API Key is missing or bad");
                return false ;
            }
            return true;
        }
        public async Task<string> OpenAISTT(string audioFile)
        {
            OpenAIAPI api = new(Properties.Settings.Default.GPTAPIKey);
            var STTResult = await api.Transcriptions.GetTextAsync(audioFile);
            if (STTResult == null)
            {
                _bBBlog.Error("OpenAI STT failed");
                return null;
            }
            return STTResult;
        }

        public OpenAI()
        {
        }



    }
}
