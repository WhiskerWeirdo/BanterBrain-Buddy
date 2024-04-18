using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanterBrain_Buddy.TwitchEventhandlers
{
    public  class OnCheerEventsArgs: EventArgs
    {
        private string EventCheerUser;
        private string EventCheerMessage;

            public OnCheerEventsArgs(string userCheered, string cheerMessage)
            {
                EventCheerUser = userCheered;
                EventCheerMessage = cheerMessage;
            }

            public string[] GetCheerInfo()
            {
                return [EventCheerUser, EventCheerMessage];
            }

    }

}
