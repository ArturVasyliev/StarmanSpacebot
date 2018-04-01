using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace StarmanLibrary.Integrations
{
    public class ApiRequestProvider<T>
    {
        private string _apiUrl;

        public ApiRequestProvider(string url)
        {
            _apiUrl = url;
        }

        public T GetResponse()
        {
            HttpWebRequest request = WebRequest.CreateHttp(_apiUrl);
            WebResponse webResponse = request.GetResponse();

            string responseMessage;
            using (StreamReader sr = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8, true))
            {
                responseMessage = sr.ReadToEnd();
            }

            Type typeOfT = typeof(T);

            if (typeOfT.IsArray)
            {
                JArray array = JArray.Parse(responseMessage);
                return JsonConvert.DeserializeObject<T>(array.ToString());
            }
            else
            {
                JObject obj = JObject.Parse(responseMessage);
                return JsonConvert.DeserializeObject<T>(obj.ToString());
            }
        }
    }
}
