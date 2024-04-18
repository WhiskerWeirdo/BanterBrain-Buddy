using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanterBrain_Buddy.TwitchEventhandlers
{
    public class OnChatEventArgs : EventArgs
    {
        private string EventChatUser;
        private string EventChatMessage;

        public OnChatEventArgs(string userChatted, string chatMessage)
        {
            EventChatUser = userChatted;
            EventChatMessage = chatMessage;
        }

        public string[] GetChatInfo()
        {
            return [EventChatUser, EventChatMessage];
        }
    }
}
