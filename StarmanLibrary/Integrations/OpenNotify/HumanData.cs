using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarmanLibrary.Integrations.OpenNotify
{
    public class HumanData
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("craft")]
        public string SpaceCraft { get; set; }
    }
}
