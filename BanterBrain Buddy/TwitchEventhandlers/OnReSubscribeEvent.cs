using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanterBrain_Buddy.TwitchEventhandlers
{
    public class OnReSubscribeEventArgs(string userReSubscribed, string message, string monthsSubbed) : EventArgs
    {
        private readonly string EventSubscriptionUser = userReSubscribed;
        private readonly string EventSubscriptionMessage = message;
        private readonly string EventSubscriptionMonths = monthsSubbed;

        public string[] GetSubscribeInfo()
        {
            return [EventSubscriptionUser, EventSubscriptionMessage, EventSubscriptionMonths];
        }
    }
}
