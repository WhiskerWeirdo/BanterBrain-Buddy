using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanterBrain_Buddy.TwitchEventhandlers
{
    public class OnSubscribeEventArgs(string userSubscribed, string broadcaster) : EventArgs
    {
        private string EventSubscriptionUser = userSubscribed;
        private string EventSubscriptionBroadcaster = broadcaster;

        public string[] GetSubscribeInfo()
        {
            return [EventSubscriptionUser, EventSubscriptionBroadcaster];
        }
    }
}
