using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarmanLibrary.Integrations.GoogleMaps
{
    public class Address
    {
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }
    }
}
