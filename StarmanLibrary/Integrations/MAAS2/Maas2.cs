using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Text;

namespace StarmanLibrary.Integrations.MAAS2
{
    //https://api.maas2.jiinxt.com/(int)
    public class Maas2
    {
        private static readonly string _baseUrl = new Configuration().Maas2BaseUrl;

        public static Maas2Response GetCurrentMarsStatus()
        {
            return GetMarsStatusBySol(0);
        }

        public static Maas2Response GetMarsStatusBySol(int sol)
        {
            string requestUrl = sol <= 0 ? _baseUrl : _baseUrl + $"/{sol}";

            HttpWebRequest request = WebRequest.CreateHttp(requestUrl);
            WebResponse response = request.GetResponse();

            string responseMessage;
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8, true))
            {
                responseMessage = sr.ReadToEnd();
            }

            JObject obj = JObject.Parse(responseMessage);
            Maas2Response resp = JsonConvert.DeserializeObject<Maas2Response>(obj.ToString());

            return resp;
        }
    }
}
