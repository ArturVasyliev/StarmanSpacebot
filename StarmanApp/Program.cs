using StarmanLibrary;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;

namespace StarmanApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Starman starman = new Starman();

            var me = starman.GetMe().Result;


            starman.Start();
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            starman.Stop();

            //var r = Maas2.GetCurrentMarsStatus();
            //var r1 = Maas2.GetMarsStatusBySol(1);
            //var r2 = OpenNotify.GetHumansInSpace();
            //var r3 = OpenNotify.GetIssData();
            //var r4 = GoogleMaps.GetAddressByLocation(r3.Position.Latitude, r3.Position.Longitude);
            //var r5 = Nasa.GetAstroPicOfTheDay(DateTime.Now);
            //var r6 = Nasa.GetAstroPicOfTheDay(new DateTime(2017, 11, 2));
            //var r7 = Nasa.GetTheLatestPicOfTheEarth();

        }
    }
}
