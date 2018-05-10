namespace StarmanLibrary.Integrations.OpenNotify
{
    //http://api.open-notify.org/astros.json
    //http://api.open-notify.org/iss-now.json

    public static class OpenNotify
    {
        private static readonly string _baseUrl = new Configuration().OpenNotifyBaseUrl;

        public static HumansInSpace GetHumansInSpace()
        {
            var apiProvider = new ApiRequestProvider<HumansInSpace>(_baseUrl + $"/astros.json");
            return apiProvider.GetResponse();
        }

        public static ISSData GetIssData()
        {
            var apiProvider = new ApiRequestProvider<ISSData>(_baseUrl + $"/iss-now.json");
            return apiProvider.GetResponse();
        }
    }
}
