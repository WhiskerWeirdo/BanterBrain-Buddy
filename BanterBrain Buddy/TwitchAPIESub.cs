using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.EventSub.Core.SubscriptionTypes.Channel;
using TwitchLib.EventSub.Websockets;
using TwitchLib.EventSub.Websockets.Core.EventArgs.Channel;
using TwitchLib.EventSub.Websockets.Core.EventArgs;


/// <summary>
/// CODING RULES:
/// •	Local variables, private instance, static fields and method parameters should be camelCase.
/// •	Methods, constants, properties, events and classes should be PascalCase.
/// •	Global private instance fields should be in camelCase prefixed with an underscore. Static fields not.
/// </summary>

namespace BanterBrain_Buddy
{
    public class TwitchAPIESub
    {
        private static readonly log4net.ILog _bBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string TwitchAccessToken { get;  private set; }
        private bool TwitchAuthRequestResult { get; set; }

        private static string _twitchAuthRedirect { get; set; }
        private static string _twitchAuthClientId { get; set; }

        private static string _twitchUserID { get; set; }
        private static string _twitchChannelID { get; set; }

        private bool _twitchDoAutomatedCheck { get; set; }

        private static TwitchLib.Api.TwitchAPI _gTwitchAPI;

        //if this is set, we need to send a test message to a channel on join.
        public string TwitchSendTestMessageOnJoin { get; set; }


        private readonly EventSubWebsocketClient _eventSubWebsocketClient;

        public TwitchAPIESub()
        {
            TwitchAccessToken = "";
            TwitchAuthRequestResult = false;
            TwitchReadSettings();
            _twitchDoAutomatedCheck = true;
            _gTwitchAPI = new TwitchLib.Api.TwitchAPI();
            _eventSubWebsocketClient = new EventSubWebsocketClient();
        }

        /// <summary>
        /// This will initialize the event handlers for the TwitchEventSub
        /// </summary>
        public async Task<bool> EventSubInit(string TwAuthToken, string TwUsername, string TwChannelName)
        {
            _bBBlog.Info("Set required globals for EventSub");
            TwitchAccessToken = TwAuthToken;
            if (await ValidateAccessToken(TwitchAccessToken))
            {
                var broadCaster = (await _gTwitchAPI.Helix.Users.GetUsersAsync(null, [TwChannelName])).Users;
                var messageSender = (await _gTwitchAPI.Helix.Users.GetUsersAsync(null, [TwUsername])).Users;
                _twitchUserID = messageSender[0].Id;
                _twitchChannelID = broadCaster[0].Id;

                _bBBlog.Info("TwitchEventSub eventhandlers start");
                _eventSubWebsocketClient.WebsocketConnected += EventSubOnWebsocketConnected;
                _eventSubWebsocketClient.WebsocketDisconnected += EventSubOnWebsocketDisconnected;
                _eventSubWebsocketClient.WebsocketReconnected += EventSubOnWebsocketReconnected;
                _eventSubWebsocketClient.ErrorOccurred += EventSubOnErrorOccurred;

                _eventSubWebsocketClient.ChannelFollow += EventSubOnChannelFollow;
                _eventSubWebsocketClient.ChannelChatMessage += EventSubOnChannelChatMessage;

                _bBBlog.Info("TwitchEventSub eventhandlers setting done");
                return true;
            } else
            {
                _bBBlog.Error("TwitchEventSub failed to initialize. Most likely due to failing key");
                return false;
            }
        }



        //if test is validated and Twitch is enabled, we need to check if the access token is still valid every hour
        //this is a Twitch rule, see https://dev.twitch.tv/docs/authentication/validate-tokens/
        public async Task<bool> CheckHourlyAccessToken()
        {
            while (_twitchDoAutomatedCheck)
            {
                //45 minutes = 2700000
                await Task.Delay(2700000);
                if (!await ValidateAccessToken(TwitchAccessToken))
                {
                    //technically sensitive data, so we need to log this as an error, even if its invalid lets not log it
                    _bBBlog.Error($"Hourly check! Access token is invalid, please re-authenticate");
                    return false;
                }
                else
                    _bBBlog.Info("Hourly check! Access Token is validated and valid");
            }
            return true;
        }

        public void StopHourlyAccessTokenCheck()
        {
            _bBBlog.Info("Stopping hourly Twitch access token validation");
            _twitchDoAutomatedCheck = false;
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
                _bBBlog.Error("TwitchAuthRedirect not found in settings.json");
            }
            else
            {
                _twitchAuthRedirect = tmpVal;
            }

            test = JsonData.TryGetValue("TwitchAuthClientId", out tmpVal);
            if (!test)
            {
                _bBBlog.Error("TwitchAuthClientId not found in settings.json");
            }
            else
            {
                _twitchAuthClientId = tmpVal;
            }
        }

        /// <summary>
        /// This validates the access token using the Twitch API
        /// </summary>
        /// <param name="TwAuthToken">The access token</param>
        /// <returns>true if token valid, false if invalid</returns>
        public async Task<bool> ValidateAccessToken(string TwAuthToken)
        {

            _gTwitchAPI.Settings.ClientId = _twitchAuthClientId;
            _gTwitchAPI.Settings.AccessToken = TwAuthToken;
            var validateTokenTest = await _gTwitchAPI.Auth.ValidateAccessTokenAsync(TwAuthToken);
            if (validateTokenTest == null)
            {
                _bBBlog.Error("Access token is invalid, please re-authenticate");
                return false;
            }
            else
            {
                _bBBlog.Info("Access Token is validated and valid");
                TwitchAccessToken = TwAuthToken;
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
            _bBBlog.Info($"Checking Authorization code using the API");

            _gTwitchAPI.Settings.ClientId = _twitchAuthClientId;
            _gTwitchAPI.Settings.AccessToken = TwAuthToken;
            _bBBlog.Debug($"clientid: {_gTwitchAPI.Settings.ClientId} ");
            //sensitive information 
            //"accesstoken: {_gTwitchAPI.Settings.AccessToken}");

            //before we do anything else, we validate the access token
            //if null, then the token is no longer valid and we need to let the user know
            if (!await ValidateAccessToken(TwAuthToken))
            {
                _bBBlog.Error("Access token is invalid, please re-authenticate");
                return false;
            }
            //assuming token itself is valid, lets continue
            try
            {
                _bBBlog.Info("Trying to see if I can getting the current user using Helix call.");
                await Task.Delay(500);
                var broadCaster = (await _gTwitchAPI.Helix.Users.GetUsersAsync(null, [TwChannelName] )).Users;
                var messageSender = (await _gTwitchAPI.Helix.Users.GetUsersAsync(null, [TwUsername] )).Users;
                _twitchUserID = messageSender[0].Id;
                _twitchChannelID = broadCaster[0].Id;
                _bBBlog.Info("Broadcaster: " + broadCaster[0].Login +" id:"+ broadCaster[0].Id);
                _bBBlog.Info("Message sender: " + messageSender[0].Login + " id:" + messageSender[0].Id);
                //do we need to send a message also?
                if (TwitchSendTestMessageOnJoin != null)
                {
                    _bBBlog.Info("Sending message to channel");
                    await _gTwitchAPI.Helix.Chat.SendChatMessage(broadCaster[0].Id.ToString(), messageSender[0].Id.ToString(), TwitchSendTestMessageOnJoin, null, TwAuthToken);
                }
                _bBBlog.Info("Authorization succeeded can read user so acces token is valid");
                return true;
            }
            catch (TwitchLib.Api.Core.Exceptions.BadScopeException exception)
            {
                _bBBlog.Error("Bad scope Issue with access token: " +exception.Message);
                return false;
            } catch (HttpRequestException exception)
            {
                _bBBlog.Error("HTTPrequest Issue with access token: " + exception.Message);
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
            _gTwitchAPI.Settings.ClientId = _twitchAuthClientId;

            // start local web server
            var server = new TwitchAuthWebserver(_twitchAuthRedirect);

            //implicit flow is rather simple compared to client cred
            var tImplicit = new Thread(() => Process.Start(new ProcessStartInfo($"{GetOAUTHCodeUrl(_twitchAuthClientId, _twitchAuthRedirect, scopes, "Implicit")}") { UseShellExecute = true }));
            tImplicit.Start();
            
            var authImplicit = await server.ImplicitListen();

            if (authImplicit != null)
            {
                TwitchAuthRequestResult = true;
                // update TwitchLib's api with the recently acquired access token
                _gTwitchAPI.Settings.AccessToken = authImplicit.Code;

                //also save this the global to return
                TwitchAccessToken = authImplicit.Code;

                // get the auth'd user to test the access token's validity
                var user = (await _gTwitchAPI.Helix.Users.GetUsersAsync()).Users[0];

                // print out all the data we've got
                _bBBlog.Info($"Authorization success! User: {user.DisplayName} (id: {user.Id})");
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
                _bBBlog.Error("Unknown OAUTHType: " + OAUTHType);
                return null;
            }

            return "https://id.twitch.tv/oauth2/authorize?" +
                    $"client_id={clientId}&" +
                    $"redirect_uri={redirectUri}&" +
                    $"response_type={responseType}&" +
                    $"scope={scopesStr}";
        }

        public async Task<bool> EventSubStartAsync()
        {
            var result = await _eventSubWebsocketClient.ConnectAsync();
            if (result)
            {
                _bBBlog.Info($"Websocket {_eventSubWebsocketClient.SessionId} connected!");
                return true;
            }
            else
            {
                _bBBlog.Error($"Websocket {_eventSubWebsocketClient.SessionId} failed to connect!");
                return false;
            }
        }

        public async Task EventSubStopAsync()
        {
            await _eventSubWebsocketClient.DisconnectAsync();
            _bBBlog.Info($"Websocket {_eventSubWebsocketClient.SessionId} manually stopped and disconnected.");
        }

        private async Task EventSubOnErrorOccurred(object sender, ErrorOccuredArgs e)
        {

            _bBBlog.Error($"Websocket {_eventSubWebsocketClient.SessionId} - Error occurred!");
        }

        private async Task EventSubOnChannelFollow(object sender, ChannelFollowArgs e)
        {
            var eventData = e.Notification.Payload.Event;

            _bBBlog.Info($"{eventData.UserName} followed {eventData.BroadcasterUserName} at {eventData.FollowedAt}");
        }

        private async Task EventSubOnChannelChatMessage(object sender, ChannelChatMessageArgs e)
        {
            var eventData = e.Notification.Payload.Event;

            _bBBlog.Info($"{eventData.ChatterUserName} said {eventData.Message.Text} in {eventData.BroadcasterUserName}'s chat.");
        }
        private async Task EventSubOnWebsocketConnected(object sender, WebsocketConnectedArgs e)
        {

            _bBBlog.Info($"Websocket {_eventSubWebsocketClient.SessionId} connected!");

            if (!e.IsRequestedReconnect)
            {
                // subscribe to topics using API Helix.EventSub.CreateEventSubSubscriptionAsync
                ///example
                ///Dictionary<string, string> conditions = new()
                ///{
                ///    { "broadcaster_user_id", "ID_OF_CHANNEL_TO_READ" },
                ///             { "user_id", "ID_OF_BOT" },
                ///};
                ///var response = await _twitchAPI.Helix.EventSub.CreateEventSubSubscriptionAsync("channel.chat.message", "1", conditions, 
                ///TwitchLib.Api.Core.Enums.EventSubTransportMethod.Websocket, _eventSubWebsocketClient.SessionId, clientId: "CLIENT_ID", accessToken: "ACCESS_TOKEN");
                ///
                //sooo we need the ID of the channel to read (username of the viewing channel)
                // the ID of the bot (username of the bot)
                // the client ID (_twitchAuthClientId)
                // the access token
                //_bBBlog.Info($"Subscribing to topics. - ClientID: {_twitchAuthClientId} -BroadcasterID: {_twitchChannelID} -UserID {_twitchUserID} "); // -Accesstoken {TwitchAccessToken}");

                Dictionary<string, string> conditions = new()
                {
                    { "broadcaster_user_id", _twitchChannelID },
                             { "user_id", _twitchUserID },
                };
                var response = await _gTwitchAPI.Helix.EventSub.CreateEventSubSubscriptionAsync("channel.chat.message", "1", conditions,
                TwitchLib.Api.Core.Enums.EventSubTransportMethod.Websocket, _eventSubWebsocketClient.SessionId, clientId: _twitchAuthClientId, accessToken: TwitchAccessToken);
                //_bBBlog.Info("subscribe response: " +response.ToString() + " " + response.Subscriptions);
                foreach (var sub in response.Subscriptions)
                {
                    _bBBlog.Info($"Sub Type: {sub.Type} is {sub.Status}"); 
                }; 
            }
        }

        private async Task EventSubOnWebsocketDisconnected(object sender, EventArgs e)
        {
            _bBBlog.Error($"Websocket {_eventSubWebsocketClient.SessionId} disconnected.");

            // Don't do this in production. You should implement a better reconnect strategy
            while (!await _eventSubWebsocketClient.ReconnectAsync())
            {
                await Task.Delay(1000);
            }
        }

        private async Task EventSubOnWebsocketReconnected(object sender, EventArgs e)
        {
            _bBBlog.Warn($"Websocket {_eventSubWebsocketClient.SessionId} reconnected");
        }
    }

}

