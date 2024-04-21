using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanterBrain_Buddy.TwitchEventhandlers
{
    public class OnChannelPointRedemptionEventArgs(string userRedeemed, string redeemedMessage) : EventArgs
    {
        private string EventChannelPointRedemptionUser = userRedeemed;
        private string EventChannelPointRedemptionMessage = redeemedMessage;

        public string[] GetChannelPointRedemptionInfo()
        {
            return [EventChannelPointRedemptionUser, EventChannelPointRedemptionMessage];
        }
    }
}
