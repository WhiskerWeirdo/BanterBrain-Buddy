using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Events;
using TwitchLib.Communication.Events;

namespace BanterBrain_Buddy
{
    internal class TwitchClient
    {
        private static readonly log4net.ILog BBBlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private TwitchLib.Client.TwitchClient TwClient;
        private string TwUsernameG;
        private string TwPasswordG;
        private string TwChannelG;

        public TwitchClient(string TwUsername, string TwAccessToken, string TwChannel) {
            TwUsernameG = TwUsername;
            TwPasswordG = TwAccessToken;
            TwChannelG = TwChannel;

            TwClient = new TwitchLib.Client.TwitchClient();
            TwitchLib.Client.Models.ConnectionCredentials TwitchConnCredentials = new(TwUsernameG, TwPasswordG);
            TwClient.Initialize(TwitchConnCredentials);
            BBBlog.Info("Twitch Initialized");
            TwitchSetEventHandlers();
        }

        public async Task TwitchDoConnect()
        {
            await TwClient.ConnectAsync();
            if (TwClient.IsConnected)
            {
                BBBlog.Info("Client is actually connected, now attempting to join channel " + TwChannelG);
                await TwClient.JoinChannelAsync(TwChannelG);
            }
        }

        //here we set all the event handlers for the class.
        private void TwitchSetEventHandlers()
        {
            TwClient.OnIncorrectLogin += TwClient_OnIncorrectLogin;
            TwClient.OnConnected += TwClient_OnConnected;
            TwClient.OnJoinedChannel += TwClient_OnJoinedChannel;
            TwClient.OnMessageReceived += TwClient_OnMessageReceived;
            TwClient.OnConnectionError += TwClient_OnConnectionError;
            TwClient.OnDisconnected += TwClient_OnDisconnected;

            TwClient.OnFailureToReceiveJoinConfirmation += TwClient_OnFailureToReceiveJoinConfirmation;
            TwClient.OnNoPermissionError += TwClient_OnNoPermissionError;
            TwClient.OnError += TwClient_OnError;
            TwClient.OnReconnected += TwClient_OnReconnected;

        }

        /// HERE BE EVENTHANDLERS
        /// 
        private Task TwClient_OnConnectionError(object sender, OnConnectionErrorArgs e)
        {
            BBBlog.Info("Twitch OnJoinedChannel => " + e.Error.Message);
            return Task.CompletedTask;
        }

        private Task TwClient_OnError(object sender, OnErrorEventArgs e)
        {
            BBBlog.Info("Twitch OnError => " + e.Exception);
            return Task.CompletedTask;
        }

        private Task TwClient_OnReconnected(object sender, TwitchLib.Client.Events.OnConnectedEventArgs e)
        {
            BBBlog.Info("Twitch OnReconnected => " + e.ToString());
            return Task.CompletedTask;
        }

        private Task TwClient_OnDisconnected(object sender, OnDisconnectedEventArgs e)
        {
            BBBlog.Info("Twitch OnDisconnected => " + e.ToString());
            return Task.CompletedTask;
        }

        private Task TwClient_OnFailureToReceiveJoinConfirmation(object sender, OnFailureToReceiveJoinConfirmationArgs e)
        {
            BBBlog.Info("Twitch OnFailureToReceiveJoinConfirmation => " + e.Exception);
            return Task.CompletedTask;
        }
        private Task TwClient_OnNoPermissionError(object sender, NoticeEventArgs e)
        {
            BBBlog.Info("Twitch OnNoPermissionError => " + e.Message);
            return Task.CompletedTask;
        }

        private Task TwClient_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            BBBlog.Info("Twitch OnJoinedChannel => " + e.Channel);
            return Task.CompletedTask;
        }

        private Task TwClient_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            BBBlog.Info($"Twitch OnMessageReceived => #{e.ChatMessage.Channel} {e.ChatMessage.Username}: {e.ChatMessage.Message}");
            return Task.CompletedTask;
        }

        private async Task TwClient_OnConnected(object sender, TwitchLib.Client.Events.OnConnectedEventArgs e)
        {
            BBBlog.Info($"Twitch OnConnected => Connected.");
            await Task.CompletedTask;
        }

        private async Task TwClient_OnIncorrectLogin(object sender, TwitchLib.Client.Events.OnIncorrectLoginArgs e)
        {
            BBBlog.Info($"Twitch OnIncorrectLogin => " + e.Exception);
            await TwClient.DisconnectAsync();
        }
    }
}
