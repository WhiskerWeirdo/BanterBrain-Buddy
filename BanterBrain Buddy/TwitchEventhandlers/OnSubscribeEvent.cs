using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanterBrain_Buddy.TwitchEventhandlers
{
    public class OnSubscribeEventArgs(string userSubscribed, string data) : EventArgs
    {
        private readonly string EventSubscriptionUser = userSubscribed;
        private readonly string EventSubscriptionData = data;

        public string[] GetSubscribeInfo()
        {
            return [EventSubscriptionUser, EventSubscriptionData];
        }
    }
}
