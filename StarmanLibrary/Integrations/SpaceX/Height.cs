using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarmanLibrary.Integrations.SpaceX
{
    public class Height
    {
        [JsonProperty("meters")]
        public double Meters { get; set; }

        [JsonProperty("feet")]
        public double Feet { get; set; }
    }
}
