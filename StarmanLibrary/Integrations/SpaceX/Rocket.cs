using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarmanLibrary.Integrations.SpaceX
{
    public class Rocket
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("stages")]
        public int StagesAmount { get; set; }

        [JsonProperty("cost_per_launch")]
        public int CostPerLaunch { get; set; }

        [JsonProperty("success_rate_pct")]
        public int SuccessRatePercent { get; set; }

        [JsonProperty("first_flight")]
        public DateTime FirstFlightDate { get; set; }

        [JsonProperty("height")]
        public Height Height { get; set; }

        [JsonProperty("diameter")]
        public Diameter Diameter { get; set; }

        [JsonProperty("mass")]
        public Mass Mass { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

    }
}
