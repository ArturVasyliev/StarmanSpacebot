namespace StarmanLibrary.Integrations.GoogleMaps
{
    //https://maps.googleapis.com/maps/api/geocode/json?latlng=40.714224,-73.961452&language=en
    //https://maps.googleapis.com/maps/api/geocode/json?latlng=-16.1134,-124.9037&language=en&key=AIzaSyAREz_hptzALuZAY5o7aTgQ6r68GhATrDA

    public class GoogleMaps
    {
        private static readonly string _baseUrl = new Configuration().GoogleMapsBaseUrl;
        private static readonly string _apiKey = new Configuration().GoogleMapsAPIKey;

        public static LocationResult GetAddressByLocation(double latitude, double longitude)
        {
            var apiProvider = new ApiRequestProvider<LocationResult>(_baseUrl +
                $"/json?latlng={latitude.ToString().Replace(',', '.')},{longitude.ToString().Replace(',', '.')}&language=en&key={_apiKey}");

            LocationResult result = apiProvider.GetResponse();
            result.SetCoordinates(latitude, longitude);
            return result;
        }
    }
}
