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
            return "Hello, I'm Starman 😃 I am ... Actually, I don't know who I am, but I know that I am in space now and..";
        }

        public string GetMarsStatus()
        {
            var marsStatus = Maas2.GetCurrentMarsStatus();
            return $"{marsStatus.MaxTemp} {marsStatus.MinTemp} {marsStatus.Sol}";
        }

        public string GetMoonStatus()
        {
            var moonStatus = BurningSoul.GetCurrentMoonStatus();
            return $"{moonStatus.Age} {moonStatus.DistanceFromCoreOfEarth} {moonStatus.Stage}";
        }

        public string GetIssStatusText()
        {
            return "Internatioal Space Station is over this place right now:";
        }

        public double[] GetIssPosition()
        {
            var issStatus = OpenNotify.GetIssData();
            return new double[] {issStatus.Position.Latitude, issStatus.Position.Longitude};
        }

        public string GetHumansInSpace()
        {
            var astronautsResult = OpenNotify.GetHumansInSpace();
            // 0, 1, more than 2
            StringBuilder sb = new StringBuilder($"There are {astronautsResult.Number} people in space right now.");
            foreach (var name in astronautsResult.People)
            {
                sb.Append($"\n- {name.Name}");
            }

            return sb.ToString();
        }

        public string GetSpacePics()
        {
            return "You can watch images from the space!!!";
        }

        public string[] GetPicOfTheDayInfo()
        {
            var picture = Nasa.GetAstroPicOfTheDay(DateTime.Now);
            if (picture == null)
            {
                picture = Nasa.GetAstroPicOfTheDay(DateTime.Now - TimeSpan.FromDays(1));
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
            return "Available commands: \n /mars \n /help";
        }

        public string GetSettings()
        {
            return "Why are you trying to do something with me 😠 I am who I am - Starman who rides Tesla Model X in space 😝";
        }

        public string GetSpacexCompanyInfo()
        {
            var companyInfo = SpaceX.GetCompanyInfo();

            return $"{companyInfo.Name} \n {companyInfo.Summary} \n {companyInfo.Valuation} \n {companyInfo.CEO} \n {companyInfo.CreationYear} \n {companyInfo.CTO} \n {companyInfo.EmployeesAmount} \n {companyInfo.Founder} \n {companyInfo.Headquarters} \n";
        }

        public string GetSpacexRocketsInfo()
        {
            var rockets = SpaceX.GetRockets();

            return $"{rockets[0].Description}";
        }

        public string GetLaunchesInfo()
        {
            var launch = SpaceX.GetLatestLaunch();

            return $"{launch.LaunchDate}";
        }
    }
}
