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

    public class SpaceX
    {
        private static readonly string _baseUrl = new Configuration().SpacexBaseUrl;

        public static string GetCompanyInfo()
        {
            var apiProvider = new ApiRequestProvider<string>(_baseUrl + $"/info");
            return apiProvider.GetResponse();
        }

        public static string GetRockets()
        {
            var apiProvider = new ApiRequestProvider<string>(_baseUrl + $"/rockets");
            return apiProvider.GetResponse();
        }

        public static string GetRocketById(string rocketId)
        {
            var apiProvider = new ApiRequestProvider<string>(_baseUrl + $"/rockets/{rocketId}");
            return apiProvider.GetResponse();
        }

        public static string GetCapsules()
        {
            var apiProvider = new ApiRequestProvider<string>(_baseUrl + $"/capsules");
            return apiProvider.GetResponse();
        }

        public static string GetCapsuleById(string capsuleId)
        {
            var apiProvider = new ApiRequestProvider<string>(_baseUrl + $"/capsules/{capsuleId}");
            return apiProvider.GetResponse();
        }

        public static string GetLaunchpads()
        {
            var apiProvider = new ApiRequestProvider<string>(_baseUrl + $"/launchpads");
            return apiProvider.GetResponse();
        }

        public static string GetLaunchpadById(string launchpadId)
        {
            var apiProvider = new ApiRequestProvider<string>(_baseUrl + $"/launchpads/k{launchpadId}");
            return apiProvider.GetResponse();
        }

        public static string GetLatestLaunch()
        {
            var apiProvider = new ApiRequestProvider<string>(_baseUrl + $"/launches/latest");
            return apiProvider.GetResponse();
        }

        public static string GetAllPastLaunches()
        {
            var apiProvider = new ApiRequestProvider<string>(_baseUrl + $"/launches");
            return apiProvider.GetResponse();
        }

        public static string GetAllUpcomingLaunches()
        {
            var apiProvider = new ApiRequestProvider<string>(_baseUrl + $"/launches/upcoming");
            return apiProvider.GetResponse();
        }
    }
}
