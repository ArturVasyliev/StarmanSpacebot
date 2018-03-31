using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarmanLibrary.Integrations.OpenNotify
{
    public class ISSPosition
    {
        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }
    }
}
