using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Text;

namespace StarmanLibrary.Integrations.MAAS2
{
    //https://api.maas2.jiinxt.com/(int)
    public static class Maas2
    {
        private static readonly string _baseUrl = new Configuration().Maas2BaseUrl;

        public static MarsStatus GetCurrentMarsStatus()
        {
            return GetMarsStatusBySol(0);
        }

        public static MarsStatus GetMarsStatusBySol(int sol)
        {
            string requestUrl = sol <= 0 ? _baseUrl : _baseUrl + $"/{sol}";
            var apiProvider = new ApiRequestProvider<MarsStatus>(requestUrl);
            return apiProvider.GetResponse();
        }
    }
}
