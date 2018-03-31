using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarmanLibrary.Integrations.OpenNotify
{
    public class HumansInSpace
    {
        [JsonProperty("people")]
        public HumanData[] People { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
