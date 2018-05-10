using System;
using System.Collections.Generic;
using System.Text;

namespace StarmanLibrary.Integrations.SpaceX
{
    //https://api.spacexdata.com/v2/info
    //
    //https://api.spacexdata.com/v2/rockets
    //https://api.spacexdata.com/v2/rockets/falcon1
    //https://api.spacexdata.com/v2/rockets/falcon9
    //https://api.spacexdata.com/v2/rockets/falconheavy
    //
    //https://api.spacexdata.com/v2/capsules
    //https://api.spacexdata.com/v2/capsules/dragon1
    //https://api.spacexdata.com/v2/capsules/dragon2
    //https://api.spacexdata.com/v2/capsules/crewdragon
    //
    //https://api.spacexdata.com/v2/launchpads
    //https://api.spacexdata.com/v2/launchpads/ksc_lc_39a
    //
    //https://github.com/r-spacex/SpaceX-API/wiki/Launches
    //
    //

    public static class SpaceX
    {
        private static readonly string _baseUrl = new Configuration().SpacexBaseUrl;

        public static Company GetCompanyInfo()
        {
            var apiProvider = new ApiRequestProvider<Company>(_baseUrl + $"/info");
            return apiProvider.GetResponse();
        }

        public static Rocket[] GetRockets()
        {
            var apiProvider = new ApiRequestProvider<Rocket[]>(_baseUrl + $"/rockets");
            return apiProvider.GetResponse();
        }

        public static Rocket GetRocketById(string rocketId)
        {
            var apiProvider = new ApiRequestProvider<Rocket>(_baseUrl + $"/rockets/{rocketId}");
            return apiProvider.GetResponse();
        }

        public static Launch GetLatestLaunch()
        {
            var apiProvider = new ApiRequestProvider<Launch>(_baseUrl + $"/launches/latest");
            return apiProvider.GetResponse();
        }

        public static Launch[] GetAllPastLaunches()
        {
            var apiProvider = new ApiRequestProvider<Launch[]>(_baseUrl + $"/launches");
            return apiProvider.GetResponse();
        }

        public static Launch[] GetAllUpcomingLaunches()
        {
            var apiProvider = new ApiRequestProvider<Launch[]>(_baseUrl + $"/launches/upcoming");
            return apiProvider.GetResponse();
        }
    }
}
