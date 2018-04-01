﻿using Newtonsoft.Json;

namespace StarmanLibrary.Integrations.NASA
{
    public class AstroPicOfTheDay
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("hdurl")]
        public string Url { get; set; }

        [JsonProperty("explanation")]
        public string Description { get; set; }

    }
}
