using Newtonsoft.Json;
using System;

namespace StarmanLibrary.Integrations.NASA
{
    public class EarthPicture
    {
        [JsonProperty("image")]
        public string ImageTitle { get; set; }

        [JsonProperty("centroid_coordinates")]
        public GeoLocation Location { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("caption")]
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }
    }
}
