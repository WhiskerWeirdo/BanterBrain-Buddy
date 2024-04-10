using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BanterBrain_Buddy
{
    public class TwitchAPI
    {
        private static readonly log4net.ILog BBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string TwitchAuthToken { get;  private set; }
        private bool TwitchAuthRequestResult { get; set; }

        public TwitchAPI()
        {
            TwitchAuthToken = "";
            TwitchAuthRequestResult = false;
        }

        public async Task<bool> GetTwitchAuthToken(List<string> scopes)
        {
            await ReqTwitchAuthToken(scopes);
            return TwitchAuthRequestResult;
        }

        private async Task ReqTwitchAuthToken(List<string> scopes)
        {
            //lets read this from a file, so its easier for other people to change.
            string TwitchAuthRedirect = null;
            string TwitchAuthClientId = null;

            //todo: error handling
            using (StreamReader r = new(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\settings.json"))
            {
                string json = r.ReadToEnd();
                dynamic data = JObject.Parse(json);
                TwitchAuthRedirect = data.TwitchAuthRedirect;
                TwitchAuthClientId = data.TwitchAuthClientId;
            }

            // create twitch api instance
            var TwitchAPI = new TwitchLib.Api.TwitchAPI();
            TwitchAPI.Settings.ClientId = TwitchAuthClientId;

            // start local web server
            var server = new TwitchAuthWebserver(TwitchAuthRedirect);

            //implicit flow is rather simple compared to client cred
            var tImplicit = new Thread(() => Process.Start(new ProcessStartInfo($"{GetImplicitCodeUrl(TwitchAuthClientId, TwitchAuthRedirect, scopes)}") { UseShellExecute = true }));
            tImplicit.Start();
            var authImplicit = await server.ImplicitListen();

            if (authImplicit != null)
            {
                TwitchAuthRequestResult = true;
                // update TwitchLib's api with the recently acquired access token
                TwitchAPI.Settings.AccessToken = authImplicit.Code;

                //also save this the global to return
                TwitchAuthToken = authImplicit.Code;

                // get the auth'd user to test the access token's validity
                var user = (await TwitchAPI.Helix.Users.GetUsersAsync()).Users[0];

                // print out all the data we've got
                BBBlog.Info($"Authorization success!\n\nUser: {user.DisplayName} (id: {user.Id})\n");
            } else
            {
                TwitchAuthRequestResult =false;
            }
        }

        private static string GetAuthorizationCodeUrl(string clientId, string redirectUri, List<string> scopes)
        {
            var scopesStr = String.Join("+", scopes);

            return "https://id.twitch.tv/oauth2/authorize?" +
                   $"client_id={clientId}&" +
                   $"redirect_uri={redirectUri}&" +
                   "response_type=code&" +
                   $"scope={scopesStr}";
        }

        private static string GetImplicitCodeUrl(string clientId, string redirectUri, List<string> scopes)
        {
            var scopesStr = String.Join("+", scopes);

            return "https://id.twitch.tv/oauth2/authorize?" +
                   $"client_id={clientId}&" +
                   $"redirect_uri={redirectUri}&" +
                   "response_type=token&" +
                   $"scope={scopesStr}";
        }

    }
}
