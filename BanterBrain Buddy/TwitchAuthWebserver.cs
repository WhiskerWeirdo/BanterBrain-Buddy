using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace BanterBrain_Buddy
{
    public class Authorization
    {
        public string Code { get; }

        public Authorization(string code)
        {
            Code = code;
        }
    }
    public class TwitchAuthWebserver
    {
        private HttpListener listener;

        public TwitchAuthWebserver(string uri)
        {
            listener = new HttpListener();
            listener.Prefixes.Add(uri);
        }

        public async Task<Authorization> Listen()
        {
            listener.Start();
            var result = await onRequest();
            return result;
        }

        private async Task<Authorization> onRequest()
        {
            while (listener.IsListening)
            {
                try
                {
                    HttpListenerContext ctx = await listener.GetContextAsync();
                    var req = ctx.Request;
                    var resp = ctx.Response;
                    using (var writer = new StreamWriter(resp.OutputStream))
                    {
                        if (req.QueryString.AllKeys.Any("code".Contains))
                        {
                            writer.WriteLine("Authorization started! Check your application! You can close this window!");
                            writer.Flush();
                            return new Authorization(req.QueryString["code"]);
                        }
                        else
                        {
                            writer.WriteLine("No code found in query string! Problem with OAUTH");
                            writer.Flush();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                
            }
            return null;
        }
    }
  
}
