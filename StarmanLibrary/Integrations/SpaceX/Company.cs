using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarmanLibrary.Integrations.SpaceX
{
    public class Company
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("founder")]
        public string Founder { get; set; }

        [JsonProperty("founded")]
        public string CreationYear { get; set; }

        [JsonProperty("employees")]
        public int EmployeesAmount { get; set; }

        [JsonProperty("ceo")]
        public string CEO { get; set; }

        [JsonProperty("cto")]
        public string CTO { get; set; }

        [JsonProperty("coo")]
        public string COO { get; set; }

        [JsonProperty("cto_propulsion")]
        public string COOPropulsion { get; set; }

        [JsonProperty("valuation")]
        public int Valuation { get; set; }

        [JsonProperty("headquarters")]
        public Headquarters Headquarters { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }
    }
}
