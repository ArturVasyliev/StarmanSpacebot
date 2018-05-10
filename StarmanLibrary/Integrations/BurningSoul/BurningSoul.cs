using System;
using System.Collections.Generic;
using System.Text;

namespace StarmanLibrary.Integrations.BurningSoul
{
    public static class BurningSoul
    {
        private static readonly string _baseUrl = new Configuration().BurningSoulBaseUrl;

        public static MoonStatus GetCurrentMoonStatus()
        {
            return GetMoonStatus(0);
        }

        public static MoonStatus GetMoonStatus(int timestamp)
        {
            string requestUrl = timestamp <= 0 ? _baseUrl : _baseUrl + $"/{timestamp}";
            var apiProvider = new ApiRequestProvider<MoonStatus>(requestUrl);
            return apiProvider.GetResponse();
        }
    }
}
