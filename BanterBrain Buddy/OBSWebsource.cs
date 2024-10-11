using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// CODING RULES:
/// •	Local variables, private instance, static fields and method parameters should be camelCase.
/// •	Public instance fields, methods, constants, properties, events and classes should be PascalCase.
/// </summary>

namespace BanterBrain_Buddy
{

    internal class OBSWebsource
    {
        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private HttpListener listener;
        private Thread listenerThread;
        private SettingsManager UserSettingsManager = SettingsManager.Instance;

        private void StartListener()
        {
            while (listener.IsListening)
            {
                var context = listener.GetContext();
                ThreadPool.QueueUserWorkItem(o => HandleRequest(context));
            }
        }

        public void OBSStartServer()
        {
            // Stop the current listener and listener thread if they're running
            if (listener?.IsListening == true)
            {
                listener.Stop();
                listenerThread.Join();  // Wait for the listener thread to finish
            }

            listener = new HttpListener();
            listener.Prefixes.Add(UserSettingsManager.Settings.WebsourceServer);
            listener.Start();

            listenerThread = new Thread(StartListener);
            listenerThread.Start();
        }

        private void HandleRequest(HttpListenerContext context)
        {
            string filepath = "";
            string responseString = "";

            switch (context.Request.RawUrl)
            {
                case "/timer":
                    filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/html/timer.html";
                    break;
                case "/test":
                    filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/html/test.html";
                    break;
                default:
                    context.Response.StatusCode = 404;
                    return;
            }

            if (!File.Exists(filepath))
            {
                context.Response.StatusCode = 404;
                return;
            }

            responseString = File.ReadAllText(filepath);

            // Modify the response string
            responseString = responseString.Replace("PLACEHOLDER", "New Text");

            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.OutputStream.Close();
        }


        public OBSWebsource()
        {
            _bBBlog.Info("OBSWebsource starting");
        }

    }
}
