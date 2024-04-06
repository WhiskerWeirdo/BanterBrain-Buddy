using System;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Events;

namespace BanterBrain_Buddy
{
    internal class TwitchClient
    {
        //set logger
        private static readonly log4net.ILog BBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private TwitchLib.Client.TwitchClient client;
        public string TwitchUsername { get; set; }
        public string TwitchAccessToken { get; set; }
        public string TwitchJoinChannel { get; set; }
        public char TwitchChatIdentifier { get; set; }
        public string TwitchChatCommand { get; set; }
        private ConnectionCredentials TwitchConnCredentials { get; set; }
        private bool TwitchConnected { get; set; }

        private bool TwitchAuthFailed;
        private bool TwitchAuthSucceeded;

        private WebSocketClient customClient;

        public bool IsTwitchConnected()
        {
            return TwitchConnected;
        }
        public void SetTwitchCredentials (string TwUsername, string TwAccessToken) 
        {
            if (TwUsername == null || TwAccessToken ==null || TwAccessToken.Length <1 || TwUsername.Length < 1)
            {
                BBBlog.Error("Twitch username or accesstoken cannot be empty or smaller than 1");
                return;
            }
            TwitchUsername = TwUsername;
            TwitchAccessToken = TwAccessToken;
            TwitchConnCredentials = new ConnectionCredentials(TwitchUsername, TwitchAccessToken);
        }

        //this holds the result of if the channel can be joined when testing

        public string TwitchChanJoinedTest()
        {
            if (TwitchAuthFailed)
                return "failed";
            if (TwitchAuthSucceeded)
                return "success";
            return "unknown";
        }

        public void SetTwitchChannel (string TwChannel)
        {
            if (TwChannel == null || TwChannel.Length < 1)
            {
                BBBlog.Error("Channel name cannot be empty or smaller than 1");
                return;
            }
            TwitchJoinChannel = TwChannel;
        }

        private Task TwitchOnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            return Task.CompletedTask;
        }
        public void SetTwitchAddChatCommandIdentifier (char twChatIdent, string twChatCommand)
        {
            try
            {
                TwitchChatIdentifier = twChatIdent;
                TwitchChatCommand = twChatCommand;

<<<<<<< HEAD
=======
                // client.AddChatCommandIdentifier(TwitchChatIdentifier);
                // respond to: <twChatIdent><twChatCommand> "message"
                //client.OnChatCommandReceived += TwitchOnChatCommandReceived;
>>>>>>> c71ce93 (rebuilding the Twitch part into a seperate class. Connection test works in the new format. Did require using the 4.x preview versions of Twitchlib though.)
                client.OnChatCommandReceived += (o, a) =>
                {
                    if (a.Command.CommandText == twChatCommand)
                    {
                        client.SendMessage(TwitchJoinChannel, $"<placeholder>You send command {twChatCommand} with param: " + a.Command.ArgumentsAsString);
                    }
                    return Task.CompletedTask;
                };
            }
            catch (Exception e)
            {
                BBBlog.Error("Error assigning Twitch Command Identifier." + e.Message);
                return;
            }
        }

        //setup the initial twitch startup info needed for connecting instead of seperately
        public void SetTwitchSetStartupInfo(string TwUsername, string TwAccessToken, string TwChannel)
        {
            SetTwitchCredentials(TwUsername, TwAccessToken);
            SetTwitchChannel(TwChannel);
            BBBlog.Info("Twitch startup info set");
        }

        //you dont have to set the chat command stuff if you dont want to
        public void SetTwitchSetStartupInfo (string TwUsername, string TwAccessToken, string TwChannel, char twChatIdent, string twChatCommand)
        {
            SetTwitchCredentials(TwUsername, TwAccessToken);
            SetTwitchChannel(TwChannel);
            SetTwitchAddChatCommandIdentifier(twChatIdent, twChatCommand);
            BBBlog.Info("Twitch startup info set with Chat Command");
        }

        public bool TwitchClientInit()
        {
            //check if we have everything set that's mandatory
            if (TwitchUsername.Length < 1 && TwitchAccessToken.Length <1 )
            {
                BBBlog.Error("No Twitch Username or AccessToken assigned, please set them using TwitchCredentials or TwitchSetStartupInfo");
                return false;
            }

            client.Initialize(TwitchConnCredentials, TwitchJoinChannel);
            while (!client.IsInitialized)
            {
                Thread.Sleep(500);
            }
            BBBlog.Info("Twitch client initialized");

            //error handlers
            SetTwitchClientErrorhandlers();

            //Connection evets
            client.OnConnected += (o, a) =>
            {
                TwitchConnected = true;
                BBBlog.Info($"Twitch OnConnected => Connected to {a.AutoJoinChannel}");
                return Task.CompletedTask;
            };

            client.OnMessageReceived += (o, a) =>
            {
                BBBlog.Info("Twitch onMessageReceived => " + a.ChatMessage);
                return Task.CompletedTask;
            };

            client.OnJoinedChannel += (o, a) =>
            {
                TwitchAuthSucceeded = true;
                BBBlog.Info($"Twitch OnJoinedChannel => joined to {a.Channel}");
                return Task.CompletedTask;
            };
            BBBlog.Info("initialize finished");
            return true;
        }

        public void TwitchDoConnect()
        {
            BBBlog.Info("Twitch Connecting");
            client.ConnectAsync();
           // BBBlog.Info("Twitch connected: " + TwitchConnected);
        }

        public void SetTwitchClientOnjoinMessage (string message)
        {
            client.OnJoinedChannel += (o, a) =>
            {
                client.SendMessage(a.Channel, message);
                BBBlog.Info("Twitch mesage I send: " + a.Channel + " => " + message);
                return Task.CompletedTask;
            };
        }

        private void TwitchKill()
        {
            client.Disconnect();
            //customClient.Close();
            customClient.Dispose();
        }
        //various twitch error messages.
        private void SetTwitchClientErrorhandlers()
        {
            //connection and login
            client.OnConnectionError += (o, a) =>
            {
                BBBlog.Error("Twitch OnConnectionError =>" + a.Error);
                client.Disconnect();
                TwitchConnected = false;
                TwitchKill();
                return Task.CompletedTask;
            };
            client.OnIncorrectLogin += (o, a) =>
            {
                TwitchConnected = false;
                TwitchAuthFailed = true;
                BBBlog.Error("Twitch OnIncorrectLogin => " + a.Exception);
                TwitchKill();
                return Task.CompletedTask;
            };
            client.OnDisconnected += (o, a) =>
            {
                BBBlog.Error("Twitch onDisconnected => I got disconnected");
                TwitchConnected = false;
                return Task.CompletedTask;
            };
            //Channel joining issues
            client.OnFailureToReceiveJoinConfirmation += (o, a) =>
            {
                BBBlog.Error("Twitch OnFailureToReceiveJoinConfirmation => " + a.Exception);
                return Task.CompletedTask;
            };

            //channel permissions & events
            client.OnNoPermissionError += (o, a) =>
            {
                BBBlog.Error("Twitch OnNoPermissionError => " + a.ToString());
            };
            client.OnRateLimit += (o, a) =>
            {
                BBBlog.Error("Twitch OnRateLimit => " + a.Message);
                return Task.CompletedTask;
            };
            client.OnBanned += (o, a) =>
            {
                BBBlog.Error("Twitch onBanned => " + a.Message);
                return Task.CompletedTask;
            };
            client.OnSlowMode += (o, a) =>
            {
                BBBlog.Error("Twitch OnSlowMode => " + a.Message);
                return Task.CompletedTask;
            };
            client.OnSubsOnly += (o, a) =>
            {
                BBBlog.Error("Twitch OnSubsOnly => " + a.Message);
                return Task.CompletedTask;
            };
            //account verification not met
            client.OnRequiresVerifiedEmail += (o, a) =>
            {
                BBBlog.Error("Twitch OnRequiresVerifiedEmail => " + a.Message);
                return Task.CompletedTask;
            };
            client.OnRequiresVerifiedPhoneNumber += (o, a) =>
            {
                BBBlog.Error("Twitch OnRequiresVerifiedPhoneNumber => " + a.Message);
                return Task.CompletedTask;
            };

            //general error
            client.OnError += (o, a) =>
            {
                BBBlog.Error("Twitch OnError => " + a.Exception.Message);
                return Task.CompletedTask;
            };
        }

        public TwitchClient() {
            BBBlog.Info("TwitchClient called and created.");
            TwitchConnected = false;
            TwitchAuthFailed = false;
            TwitchAuthSucceeded = false;

            customClient = new WebSocketClient();
            client = new TwitchLib.Client.TwitchClient(customClient);
        }
    }
}
