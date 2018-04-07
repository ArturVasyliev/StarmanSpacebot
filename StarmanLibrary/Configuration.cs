using Newtonsoft.Json.Linq;
using System.IO;

namespace StarmanLibrary
{
    public class Configuration
    {
        public readonly string TelegramBotAPIKey;
        public readonly string GoogleMapsAPIKey;
        public readonly string NasaAPIKey;

        public readonly string Maas2BaseUrl;
        public readonly string SpacexBaseUrl;
        public readonly string OpenNotifyBaseUrl;
        public readonly string GoogleMapsBaseUrl;
        public readonly string BurningSoulBaseUrl;
        public readonly string NasaBaseUrlAPOD;
        public readonly string NasaBaseUrlEPIC;

        public Configuration()
        {
            string configuration;

            using (StreamReader sr = new StreamReader("starman.json"))
            {
                configuration = sr.ReadToEnd();
            }

            JObject obj = JObject.Parse(configuration);
            TelegramBotAPIKey = (string)obj["TelegramBotAPIKey"];
            GoogleMapsAPIKey = (string)obj["GoogleMapsAPIKey"];
            NasaAPIKey = (string)obj["NasaAPIKey"];
            Maas2BaseUrl = (string)obj["Maas2BaseUrl"];
            SpacexBaseUrl = (string)obj["SpacexBaseUrl"];
            OpenNotifyBaseUrl = (string)obj["OpenNotifyBaseUrl"];
            GoogleMapsBaseUrl = (string)obj["GoogleMapsBaseUrl"];
            BurningSoulBaseUrl = (string)obj["BurningSoulBaseUrl"];
            NasaBaseUrlAPOD = (string)obj["NasaBaseUrlAPOD"];
            NasaBaseUrlEPIC = (string)obj["NasaBaseUrlEPIC"];
        }
    }
}
