using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanterBrain_Buddy.TwitchEventhandlers
{
    public class OnChannelPointCustomRedemptionEventArgs(string userRedeemed, string redeemedMessage) : EventArgs
    {
        private readonly string EventChannelPointRedemptionUser = userRedeemed;
        private readonly string EventChannelPointRedemptionMessage = redeemedMessage;

        public string[] GetChannelPointCustomRedemptionInfo()
        {
            return [EventChannelPointRedemptionUser, EventChannelPointRedemptionMessage];
        }
    }
}
