using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BanterBrain_Buddy
{
    
    public class Authorization(string code)
    {
        public string Code { get; } = code;
    }

    public class TwitchAuthWebserver
    {
        private static readonly log4net.ILog BBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly HttpListener listener;

        public TwitchAuthWebserver(string uri)
        {
            listener = new HttpListener();
            listener.Prefixes.Add(uri);
        }

        public async Task<Authorization> AuthorizationListen()
        {
            BBBlog.Info("Authorization code OAUTH starting");
            listener.Start();
            var result = await OnAuthorizationRequest();
            return result;
        }

        public async Task<Authorization> ImplicitListen()
        {
            BBBlog.Info("Implicit grant OAUTH starting");
            listener.Start();
            var result = await OnImplicitRequest();
            return result;
        }

        private async Task<Authorization> OnImplicitRequest()
        {
            bool FirstTime = true;
            while (listener.IsListening)
            {
                try
                {
                    HttpListenerContext ctx = await listener.GetContextAsync();
                    var req = ctx.Request;
                    var resp = ctx.Response;

                    using var writer = new StreamWriter(resp.OutputStream);
                    //so, with implicit flow, the first time we get back an URl that can only be read by the browser context
                    //because it has #, we rewrite that in a Uri resource (changing # to ?) and call ourselves again.
                    //The second time the parser will pick up the code correctly then.
                    if (FirstTime)
                    {
                        //its a hack cos implicit gets a #access_token instead of ?code so we do some rewrite magic
                        //and redirect ourselves to the new url
                        string response = "<!DOCTYPE html>\r\n" +
                            "<html>\r\n " +
                            "<p id=\"demo\">placeholder.</p>\r\n" +
                            "<body>\r\n" +
                            "<script>\r\n" +
                            "let Url = window.location.href;\r\n" +
                            "let newUrl = Url.replace(\"#\",\"?\");\r\n" +
                            "window.location.replace(newUrl);\r\n" +
                            "document.getElementById(\"demo\").innerHTML=`Browser should refresh if not click <a href=\\\"${newUrl}\\\">here</a>`;\r\n" +
                            "</script> \r\n" +
                            "</body>\r\n" +
                            "</html>";
                        byte[] encoded = Encoding.UTF8.GetBytes(response);
                        resp.ContentLength64 = encoded.Length;
                        resp.OutputStream.Write(encoded, 0, encoded.Length);
                        resp.OutputStream.Close();
                        FirstTime = false;
                    }
                    else if (req.QueryString.AllKeys.Any("access_token".Contains) && !FirstTime)
                    {
                        writer.WriteLine("Implicit grant started! Check your application! You can close this window!");
                        BBBlog.Info("OAUTH Implcit grant started! Check your application!");
                        writer.Flush();
                        return new Authorization(req.QueryString["access_token"]);
                    }
                    else
                    {
                        BBBlog.Info("No access_token found in query string! Problem with OAUTH");
                        writer.WriteLine("No access_code found in query string! Problem with OAUTH");
                        writer.Flush();
                    }
                }
                catch (Exception ex)
                {
                    BBBlog.Error("Webserver: " + ex.ToString());
                }

            }
            return null;
        }

        //this for general authorization style requests
        private async Task<Authorization> OnAuthorizationRequest()
        {
            while (listener.IsListening)
            {
                try
                {
                    HttpListenerContext ctx = await listener.GetContextAsync();
                    var req = ctx.Request;
                    var resp = ctx.Response;

                    using var writer = new StreamWriter(resp.OutputStream);
                    if (req.QueryString.AllKeys.Any("code".Contains))
                    {
                        writer.WriteLine("Authorization started! Check your application! You can close this window!");
                        BBBlog.Info("OAUTH Authorization started! Check your application!");
                        writer.Flush();
                        return new Authorization(req.QueryString["code"]);
                    }
                    else
                    {
                        BBBlog.Info("No code found in query string! Problem with OAUTH");
                        writer.WriteLine("No code found in query string! Problem with OAUTH");
                        writer.Flush();
                    }
                }
                catch (Exception ex)
                {
                    BBBlog.Error("Webserver: " +ex.ToString());
                }
                
            }
            return null;
        }
    }
  
}
