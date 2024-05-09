using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanterBrain_Buddy.TwitchEventhandlers
{
    public class OnSubscriptionGiftEventArgs(string userThatGifted, string message, string giftAmount) : EventArgs
    {
        private readonly string EventSubscriptionGifter = userThatGifted;
        private readonly string EventSubscriptionMessage = message;
        private readonly string EventSubscriptionAmount = giftAmount;

        public string[] GetSubscriptionGiftInfo()
        {
            return [EventSubscriptionGifter, EventSubscriptionMessage, EventSubscriptionAmount];
        }
    }
}
