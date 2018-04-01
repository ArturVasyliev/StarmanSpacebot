using Newtonsoft.Json;

namespace StarmanLibrary.Integrations.SpaceX
{
    public class Headquarters
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
