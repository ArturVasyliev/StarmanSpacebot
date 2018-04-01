using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarmanLibrary.Integrations.SpaceX
{
    public class Mass
    {
        [JsonProperty("kg")]
        public double Kilograms { get; set; }

        [JsonProperty("lb")]
        public double Pounds { get; set; }
    }
}
