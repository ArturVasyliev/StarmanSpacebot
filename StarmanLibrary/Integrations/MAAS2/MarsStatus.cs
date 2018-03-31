using Newtonsoft.Json;
using System.Net;

namespace StarmanLibrary.Integrations.MAAS2
{
    public class MarsStatus
    {
        [JsonProperty("status")]
        public HttpStatusCode HttpStatus { get; set; }

        [JsonProperty("sol")]
        public int Sol { get; set; }

        [JsonProperty("season")]
        public string Season { get; set; }

        [JsonProperty("min_temp")]
        public int MinTemp { get; set; }

        [JsonProperty("max_temp")]
        public int MaxTemp { get; set; }

        [JsonProperty("atmo_opacity")]
        public string Opacity { get; set; }

        [JsonProperty("sunrise")]
        public string SunriseTime { get; set; }

        [JsonProperty("sunset")]
        public string SunsetTime { get; set; }

        [JsonProperty("unitOfMeasure")]
        public string TempUnitOfMeasure { get; set; }
    }
}
