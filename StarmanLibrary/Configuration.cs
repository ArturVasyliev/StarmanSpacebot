using Newtonsoft.Json.Linq;
using System.IO;

namespace StarmanLibrary
{
    public class Configuration
    {
        public readonly string TelegramBotAPIKey;
        public string Maas2BaseUrl { get; set; }
        public string SpacexBaseUrl { get; set; }
        public string OpenNotifyBaseUrl { get; set; }

        public Configuration()
        {
            string configuration;

            using (StreamReader sr = new StreamReader("starman.json"))
            {
                configuration = sr.ReadToEnd();
            }

            JObject obj = JObject.Parse(configuration);
            TelegramBotAPIKey = (string)obj["TelegramBotAPIKey"];
            Maas2BaseUrl = (string)obj["Maas2BaseUrl"];
            SpacexBaseUrl = (string)obj["SpacexBaseUrl"];
            OpenNotifyBaseUrl = (string)obj["OpenNotifyBaseUrl"];
        }
    }
}
