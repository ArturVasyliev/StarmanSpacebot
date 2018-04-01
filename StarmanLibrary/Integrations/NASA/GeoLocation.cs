using Newtonsoft.Json;

namespace StarmanLibrary.Integrations.NASA
{
    public class GeoLocation
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }
}
