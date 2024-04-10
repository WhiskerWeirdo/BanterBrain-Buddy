using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api.Helix;

namespace BanterBrain_Buddy
{
    public class TwitchAPI
    {
        private static readonly log4net.ILog BBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string TwitchAuthToken { get;  private set; }
        private bool TwitchAuthRequestResult { get; set; }

        private static string TwitchAuthRedirect { get; set; }
        private static string TwitchAuthClientId { get; set; }

        //if this is set, we need to send a test message to a channel on join.
        public string TwitchSendTestMessageOnJoin { get; set; }

        public TwitchAPI()
        {
            TwitchAuthToken = "";
            TwitchAuthRequestResult = false;
            TwitchReadSettings();
        }

        public async Task<bool> GetTwitchAuthToken(List<string> scopes)
        {
            await ReqTwitchAuthToken(scopes);
            return TwitchAuthRequestResult;
        }

        //read the config file into globals
        private static void TwitchReadSettings()
        {
            //todo: error handling
            using (StreamReader r = new(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\settings.json"))
            {
                string json = r.ReadToEnd();
                dynamic data = JObject.Parse(json);
                TwitchAuthRedirect = data.TwitchAuthRedirect;
                TwitchAuthClientId = data.TwitchAuthClientId;
            }
        }

        //auth token, username
        public async Task<bool> CheckAuthCodeAPI(string TwAuthToken, string TwUsername, string TwChannelName)
        {
            BBBlog.Info($"Checking Authorization code using the API");

            var TwitchAPI = new TwitchLib.Api.TwitchAPI();
            TwitchAPI.Settings.ClientId = TwitchAuthClientId;
            TwitchAPI.Settings.AccessToken = TwAuthToken;
            BBBlog.Debug($"clientid: {TwitchAPI.Settings.ClientId} accesstoken: {TwitchAPI.Settings.AccessToken}");
            TwitchLib.Api.Helix.Models.Users.GetUsers.User user = new();
            try
            {
                BBBlog.Info("Trying to see if I can getting the current user using Helix call.");
                //user = (await TwitchAPI.Helix.Users.GetUsersAsync()).Users[0];
                await Task.Delay(500);
                var Broadcaster = (await TwitchAPI.Helix.Users.GetUsersAsync(null, new List<string> { TwChannelName })).Users;
                var MessageSender = (await TwitchAPI.Helix.Users.GetUsersAsync(null, new List<string> { TwUsername })).Users;
                //TODO if we got both back

                BBBlog.Info("Broadcaster: " + Broadcaster[0].Login +" id:"+ Broadcaster[0].Id);
                BBBlog.Info("Message sender: " + MessageSender[0].Login + " id:" + MessageSender[0].Id);
                //do we need to send a message also?
                if (TwitchSendTestMessageOnJoin != null)
                {
                    BBBlog.Info("Sending message to channel");
                    await TwitchAPI.Helix.Chat.SendChatMessage(Broadcaster[0].Id.ToString(), MessageSender[0].Id.ToString(), "API:" +TwitchSendTestMessageOnJoin, null, TwAuthToken);
                }

                BBBlog.Info("Authorization succeeded can read user so acces token is valid");
                return true;
            }
            catch (TwitchLib.Api.Core.Exceptions.BadScopeException exception)
            {
                BBBlog.Error("Bad scope Issue with access token: " +exception.Message);
                return false;
            } catch (HttpRequestException exception)
            {
                BBBlog.Error("HTTPrequest Issue with access token: " + exception.Message);
                return false;
            }
        }

        private async Task ReqTwitchAuthToken(List<string> scopes)
        {
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
                BBBlog.Info($"Authorization success! User: {user.DisplayName} (id: {user.Id})");
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
