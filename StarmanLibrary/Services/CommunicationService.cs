﻿using StarmanLibrary.Integrations.BurningSoul;
using StarmanLibrary.Integrations.MAAS2;
using StarmanLibrary.Integrations.OpenNotify;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarmanLibrary.Services
{
    public class CommunicationService : ICommunicationService
    {
        public string GetHelloMessage()
        {
            return "Hello, I am Starman!!! I am somewhere and I can do something!";
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

        public string GetDefaultResponse()
        {
            return "Damn, you are so far away from me that I can't understand you. Try again, please.";
        }

        public string GetHelp()
        {
            return "Damn, you are so far away from me that I can't understand you. Try again, please.";
        }

        public string GetSettings()
        {
            return "Damn, you are so far away from me that I can't understand you. Try again, please.";
        }
    }
}
