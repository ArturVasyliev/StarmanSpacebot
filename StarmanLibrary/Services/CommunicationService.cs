using StarmanLibrary.Integrations.BurningSoul;
using StarmanLibrary.Integrations.MAAS2;
using StarmanLibrary.Integrations.NASA;
using StarmanLibrary.Integrations.OpenNotify;
using StarmanLibrary.Integrations.SpaceX;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarmanLibrary.Services
{
    public class CommunicationService : ICommunicationService
    {
        public string GetHelloMessage()
        {
            return "Hello, I'm Starman 😃 I am ... Actually, I don't know who I am, but I know that I ride Tesla car in space so I can help you to get more information from space";
        }

        public string GetMarsStatus()
        {
            try
            {
                var marsStatus = Maas2.GetCurrentMarsStatus();
                return $"It's {marsStatus.Sol} sol right now on Mars.\n" +
                $"Sunerise time is {marsStatus.SunriseTime} and sunset time is {marsStatus.SunsetTime}.\n" +
                $"Today's forecast: from {marsStatus.MinTemp} to {marsStatus.MaxTemp} {marsStatus.TempUnitOfMeasure}, {marsStatus.Opacity}.\n" +
                $"I can barely see it but it seems to be a beautiful place!";
            }
            catch
            {
                return GetDefaultResponse();
            }
        }

        public string GetMoonStatus()
        {
            try
            {
                var moonStatus = BurningSoul.GetCurrentMoonStatus();
                return $"Currently the Moon is {moonStatus.Stage} and {(int)Math.Floor(moonStatus.Illumination)}% of its visible surface is illuminated.\n" +
                    $"🌕 is located at the distance {moonStatus.DistanceFromSun} km. from the Sun and {moonStatus.DistanceFromCoreOfEarth} km. from the center of the Earth.";
            }
            catch
            {
                return GetDefaultResponse();
            }
        }

        public string GetIssStatusText()
        {
            return "International Space Station is over this place right now:";
        }

        public double[] GetIssPosition()
        {
            try
            {
                var issStatus = OpenNotify.GetIssData();
                return new double[] { issStatus.Position.Latitude, issStatus.Position.Longitude };
            }
            catch
            {
                return new double[] { 0, 0 };
            }
        }

        public string GetHumansInSpace()
        {
            try
            {
                var astronautsResult = OpenNotify.GetHumansInSpace();
                // 0, 1, more than 2
                StringBuilder sb = new StringBuilder($"There are {astronautsResult.Number} people in space right now:");
                foreach (var name in astronautsResult.People)
                {
                    sb.Append($"\n- {name.Name}");
                }

                return sb.ToString();
            }
            catch
            {
                return GetDefaultResponse();
            }
        }

        public string GetSpacePics()
        {
            return "You can watch images related to space here:";
        }

        public string[] GetPicOfTheDayInfo()
        {
            AstroPicOfTheDay picture = null;

            try
            {
                picture = Nasa.GetAstroPicOfTheDay(DateTime.Now);
            }
            catch
            {
            }

            try
            {
                if (picture == null)
                {
                    picture = Nasa.GetAstroPicOfTheDay(DateTime.Now - TimeSpan.FromDays(1));
                }
            }
            catch
            {
                return new string[] { "none", "none", "none" };
            }

            return new string[] { picture.Title, picture.Url, picture.Description };
        }

        public string[] GetPicOfTheEarthInfo()
        {
            var picture = Nasa.GetTheLatestPicOfTheEarth();

            return new string[] { picture.ImageTitle, picture.ImageUrl };
        }

        public double[] GetLocationOfTheEarth()
        {
            var picture = Nasa.GetTheLatestPicOfTheEarth();
            
            return new double[] { picture.Location.Latitude, picture.Location.Longitude };
        }

        public string GetDefaultResponse()
        {
            return "Damn, you are so far away from me that I can't hear you! Try again, please.";
        }

        public string GetHomelandResponse()
        {
            return "This is my homeland, I miss it so much 😭";
        }

        public string GetBackResponse()
        {
            return "Here we go 🙂";
        }

        public string GetHelp()
        {
            return "I am sorry, but I am in space alone!\nI am the only one who needs help!!!";
        }

        public string GetSettings()
        {
            return "Why are you trying to do something with me 😠 I am who I am - Starman who rides Tesla Model X in space 😝";
        }

        public string GetSpacexCompanyInfo()
        {
            try
            {
                var companyInfo = SpaceX.GetCompanyInfo();

                return $"{companyInfo.Summary}\nCEO: {companyInfo.CEO}\nCTO: {companyInfo.CTO}\nCompany's valuation: {companyInfo.Valuation}$\n{companyInfo.EmployeesAmount} employees work here!\nHeadquarters: {companyInfo.Headquarters.State}, {companyInfo.Headquarters.City}, {companyInfo.Headquarters.Address}\n";
            }
            catch
            {
                return GetDefaultResponse();
            }
        }

        public string GetSpacexRocketsInfo()
        {
            var rockets = SpaceX.GetRockets();
            StringBuilder sb = new StringBuilder();

            foreach (var rocket in rockets)
            {
                sb.AppendLine(rocket.Name.ToUpper() + ":");
                sb.AppendLine(rocket.Description);
                sb.AppendLine("Mass: " + rocket.Mass.Kilograms + "kg.");
                sb.AppendLine("Height: " + rocket.Height.Meters + "meters.");
                sb.AppendLine("Diameter: " + rocket.Diameter.Meters + "meters.");
                sb.AppendLine("It costs " + rocket.CostPerLaunch + "$ per launch.");
                sb.AppendLine("Success rate percent is " + rocket.SuccessRatePercent + "%");
                sb.AppendLine();
                sb.AppendLine();
            }

            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();
        }

        public string GetLaunchesInfo()
        {
            var latestLaunch = SpaceX.GetLatestLaunch();
            var upcomingLaunch = SpaceX.GetAllUpcomingLaunches()[0];

            return $"The last ({latestLaunch.FlightNumber}) launch happened {latestLaunch?.LaunchDate.ToShortDateString()} on {latestLaunch?.Rocket.Name}.\n{latestLaunch?.Description}\n\n" +
                $"The next ({upcomingLaunch.FlightNumber}) launch happens {upcomingLaunch?.LaunchDate.ToShortDateString()} on {latestLaunch?.Rocket.Name}.";
        }
    }
}
