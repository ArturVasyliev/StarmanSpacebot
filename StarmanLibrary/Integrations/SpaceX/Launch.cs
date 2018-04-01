using Newtonsoft.Json;
using System;

namespace StarmanLibrary.Integrations.SpaceX
{
    public class Launch
    {
        [JsonProperty("flight_number")]
        public int FlightNumber { get; set; }

        [JsonProperty("launch_date_utc")]
        public DateTime LaunchDate { get; set; }

        [JsonProperty("rocket")]
        public LaunchedRocket Rocket { get; set; }

        [JsonProperty("video_link")]
        public string VideoLink { get; set; }

        [JsonProperty("article_link")]
        public string ArticleLink { get; set; }

        [JsonProperty("details")]
        public string Description { get; set; }
    }
}
