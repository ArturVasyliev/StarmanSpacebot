using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarmanLibrary.Integrations.GoogleMaps
{
    public class LocationResult
    {
        [JsonProperty("results")]
        public Address[] Addresses { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public void SetCoordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
