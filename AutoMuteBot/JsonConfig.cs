using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AutoMuteBot
{
    public class JsonConfig
    {
        public string Token;
        public List<ulong> Channels;
        public float MuteSeconds;
        public List<ulong> BypassRoles;
        public bool MuteWhenChannelEmpty = false;
    
    }

}
