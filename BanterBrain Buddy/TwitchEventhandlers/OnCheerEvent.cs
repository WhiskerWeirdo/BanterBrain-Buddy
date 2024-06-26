﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanterBrain_Buddy.TwitchEventhandlers
{
    public class OnCheerEventsArgs(string userCheered, string cheerMessage) : EventArgs
    {
        private readonly string EventCheerUser = userCheered;
        private readonly string EventCheerMessage = cheerMessage;

        public string[] GetCheerInfo()
        {
            return [EventCheerUser, EventCheerMessage];
        }

    }

}
