using System;
using System.Collections.Generic;
using System.Text;

namespace StarmanLibrary.Integrations.NASA
{
    public static class Nasa
    {
        private static readonly string _baseUrlAPOD = new Configuration().NasaBaseUrlAPOD;
        private static readonly string _baseUrlEPIC = new Configuration().NasaBaseUrlEPIC;
        private static readonly string _apiKey = new Configuration().NasaAPIKey;
        
        public static AstroPicOfTheDay GetAstroPicOfTheDay(DateTime date)
        {
            var apiProvider = new ApiRequestProvider<AstroPicOfTheDay>(_baseUrlAPOD + $"?api_key={_apiKey}&date={date.ToString("yyyy-MM-dd")}");
            return apiProvider.GetResponse();
        }

        public static EarthPicture GetTheLatestPicOfTheEarth()
        {
            var apiProvider = new ApiRequestProvider<EarthPicture[]>(_baseUrlEPIC + "/api/natural");

            var res = apiProvider.GetResponse();

            EarthPicture result = res[res.Length - 1];
            result.ImageUrl = $"{_baseUrlEPIC}/archive/natural/{result.Date.Year}/{string.Format("{0:D2}", result.Date.Month)}/{string.Format("{0:D2}", result.Date.Day)}/png/{result.ImageTitle}.png";

            return result;
        }
    }
}
