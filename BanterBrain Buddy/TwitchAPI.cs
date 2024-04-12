using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
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

        private bool TwitchDoAutomatedCheck { get; set; }

        private static TwitchLib.Api.TwitchAPI GTwitchAPI;
        //if this is set, we need to send a test message to a channel on join.
        public string TwitchSendTestMessageOnJoin { get; set; }

        public TwitchAPI()
        {
            TwitchAuthToken = "";
            TwitchAuthRequestResult = false;
            TwitchReadSettings();
            TwitchDoAutomatedCheck = true;
            GTwitchAPI = new TwitchLib.Api.TwitchAPI();
        }

        //if test is validated and Twitch is enabled, we need to check if the access token is still valid every hour
        //this is a Twitch rule, see https://dev.twitch.tv/docs/authentication/validate-tokens/
        public async Task<bool> CheckHourlyAccessToken()
        {
            while (TwitchDoAutomatedCheck)
            {
                //45 minutes = 2700000
                await Task.Delay(2700000);
                if (!await ValidateAccessToken(TwitchAuthToken))
                {
                    BBBlog.Error($"Hourly check! Access token {TwitchAuthToken} is invalid, please re-authenticate");
                    return false;
                }
                else
                    BBBlog.Info("Hourly check! Access Token is validated and valid");
            }
            return true;
        }

        public void StopHourlyAccessTokenCheck()
        {
            BBBlog.Info("Stopping hourly Twitch access token validation");
            TwitchDoAutomatedCheck = false;
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
            using StreamReader r = new(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\settings.json");
            //lets read the file and parse the json safely ;)
            //need to do error handling if file does not exist
            var JsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(r.ReadToEnd());
            bool test = JsonData.TryGetValue("TwitchAuthRedirect", out string tmpVal);
            if (!test)
            {
                BBBlog.Error("TwitchAuthRedirect not found in settings.json");
            }
            else
            {
                TwitchAuthRedirect = tmpVal;
            }

            test = JsonData.TryGetValue("TwitchAuthClientId", out tmpVal);
            if (!test)
            {
                BBBlog.Error("TwitchAuthClientId not found in settings.json");
            }
            else
            {
                TwitchAuthClientId = tmpVal;
            }
        }

        /// <summary>
        /// This validates the access token using the Twitch API
        /// </summary>
        /// <param name="TwAuthToken">The access token</param>
        /// <returns>true if token valid, false if invalid</returns>
        public async Task<bool> ValidateAccessToken(string TwAuthToken)
        {

            GTwitchAPI.Settings.ClientId = TwitchAuthClientId;
            GTwitchAPI.Settings.AccessToken = TwAuthToken;
            var ValidateTokenTest = await GTwitchAPI.Auth.ValidateAccessTokenAsync(TwAuthToken);
            if (ValidateTokenTest == null)
            {
                BBBlog.Error("Access token is invalid, please re-authenticate");
                return false;
            }
            else
            {
                BBBlog.Info("Access Token is validated and valid");
                TwitchAuthToken = TwAuthToken;
                return true;
            }
        }

        //auth token, username
        /// <summary>
        /// We do a validate token and then check if we can get the user to verify not just the token but also the user
        /// </summary>
        /// <param name="TwAuthToken">The Twitch Access token</param>
        /// <param name="TwUsername">The Twitch user name</param>
        /// <param name="TwChannelName">The channel name entered on the GUI</param>
        /// <returns>true if both tests pass, false if not</returns>
        public async Task<bool> CheckAuthCodeAPI(string TwAuthToken, string TwUsername, string TwChannelName)
        {
            BBBlog.Info($"Checking Authorization code using the API");

            GTwitchAPI.Settings.ClientId = TwitchAuthClientId;
            GTwitchAPI.Settings.AccessToken = TwAuthToken;
            BBBlog.Debug($"clientid: {GTwitchAPI.Settings.ClientId} accesstoken: {GTwitchAPI.Settings.AccessToken}");
            //before we do anything else, we validate the access token
            //if null, then the token is no longer valid and we need to let the user know
            if (!await ValidateAccessToken(TwAuthToken))
            {
                BBBlog.Error("Access token is invalid, please re-authenticate");
                return false;
            }
            //assuming token itself is valid, lets continue
            try
            {
                BBBlog.Info("Trying to see if I can getting the current user using Helix call.");
                await Task.Delay(500);
                var Broadcaster = (await GTwitchAPI.Helix.Users.GetUsersAsync(null, [TwChannelName] )).Users;
                var MessageSender = (await GTwitchAPI.Helix.Users.GetUsersAsync(null, [TwUsername] )).Users;

 
                BBBlog.Info("Broadcaster: " + Broadcaster[0].Login +" id:"+ Broadcaster[0].Id);
                BBBlog.Info("Message sender: " + MessageSender[0].Login + " id:" + MessageSender[0].Id);
                //do we need to send a message also?
                if (TwitchSendTestMessageOnJoin != null)
                {
                    BBBlog.Info("Sending message to channel");
                    await GTwitchAPI.Helix.Chat.SendChatMessage(Broadcaster[0].Id.ToString(), MessageSender[0].Id.ToString(), TwitchSendTestMessageOnJoin, null, TwAuthToken);
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

        /// <summary>
        /// This is the function that will request the auth token from Twitch
        /// </summary>
        /// <param name="scopes"></param>
        private async Task ReqTwitchAuthToken(List<string> scopes)
        {
            // create twitch api instance
            GTwitchAPI.Settings.ClientId = TwitchAuthClientId;

            // start local web server
            var server = new TwitchAuthWebserver(TwitchAuthRedirect);

            //implicit flow is rather simple compared to client cred
            var tImplicit = new Thread(() => Process.Start(new ProcessStartInfo($"{GetOAUTHCodeUrl(TwitchAuthClientId, TwitchAuthRedirect, scopes, "Implicit")}") { UseShellExecute = true }));
            tImplicit.Start();
            

            var authImplicit = await server.ImplicitListen();

            if (authImplicit != null)
            {
                TwitchAuthRequestResult = true;
                // update TwitchLib's api with the recently acquired access token
                GTwitchAPI.Settings.AccessToken = authImplicit.Code;

                //also save this the global to return
                TwitchAuthToken = authImplicit.Code;

                // get the auth'd user to test the access token's validity
                var user = (await GTwitchAPI.Helix.Users.GetUsersAsync()).Users[0];

                // print out all the data we've got
                BBBlog.Info($"Authorization success! User: {user.DisplayName} (id: {user.Id})");
            } else
            {
                TwitchAuthRequestResult =false;
            }
        }

        private static string GetOAUTHCodeUrl(string clientId, string redirectUri, List<string> scopes, string OAUTHType)
        {
            string scopesStr = String.Join("+", scopes);
            string responseType;

            if (string.Equals(OAUTHType,"auth", StringComparison.OrdinalIgnoreCase))
                responseType = "code";
            else if (string.Equals(OAUTHType, "implicit", StringComparison.OrdinalIgnoreCase))
                responseType = "token";
            else
            {
                BBBlog.Error("Unknown OAUTHType: " + OAUTHType);
                return null;
            }

            return "https://id.twitch.tv/oauth2/authorize?" +
                    $"client_id={clientId}&" +
                    $"redirect_uri={redirectUri}&" +
                    $"response_type={responseType}&" +
                    $"scope={scopesStr}";
        }
    }
}
