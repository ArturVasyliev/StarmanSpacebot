using Newtonsoft.Json;

namespace StarmanLibrary.Integrations.GoogleMaps
{
    public class Address
    {
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }
    }
}
