using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanterBrain_Buddy.TwitchEventhandlers
{
    public class OnChatEventArgs(string userChatted, string chatMessage) : EventArgs
    {
        private string EventChatUser = userChatted;
        private string EventChatMessage = chatMessage;

        public string[] GetChatInfo()
        {
            return [EventChatUser, EventChatMessage];
        }
    }
}
