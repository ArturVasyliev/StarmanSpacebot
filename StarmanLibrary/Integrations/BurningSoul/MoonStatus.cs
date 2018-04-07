using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarmanLibrary.Integrations.BurningSoul
{
    public class MoonStatus
    {
        //The age of the Moon is the number of days since the last new moon
        [JsonProperty("age")]
        public double Age { get; set; }

        //Percent of illuminated surface on the moon
        [JsonProperty("illumination")]
        public double Illumination { get; set; }

        //Phase of the moon
        [JsonProperty("stage")]
        public string Stage { get; set; }

        //Lunar distance
        [JsonProperty("DFCOE")]
        public double DistanceFromCoreOfEarth { get; set; }

        [JsonProperty("DFS")]
        public double DistanceFromSun { get; set; }
    }
}
