using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanterBrain_Buddy.TwitchEventhandlers
{
    public class OnSubscribeEventArgs(string userSubscribed, string data) : EventArgs
    {
        private string EventSubscriptionUser = userSubscribed;
        private string EventSubscriptionData = data;

        public string[] GetSubscribeInfo()
        {
            return [EventSubscriptionUser, EventSubscriptionData];
        }
    }
}
