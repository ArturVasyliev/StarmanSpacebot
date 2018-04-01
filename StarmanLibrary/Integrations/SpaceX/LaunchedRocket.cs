using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarmanLibrary.Integrations.SpaceX
{
    public class LaunchedRocket
    {
        [JsonProperty("rocket_id")]
        public string Id { get; set; }

        [JsonProperty("rocket_name")]
        public string Name { get; set; }
    }
}
