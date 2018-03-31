using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarmanLibrary.Integrations.OpenNotify
{
    public class ISSData
    {
        [JsonProperty("iss_position")]
        public ISSPosition Position { get; set; }

        [JsonProperty("timestamp")]
        public int TimeStamp { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
