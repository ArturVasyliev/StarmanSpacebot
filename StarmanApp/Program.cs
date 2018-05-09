using StarmanLibrary;
using StarmanLibrary.Services;
using System;
using Telegram.Bot;

namespace StarmanApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var botClient = new TelegramBotClient(new Configuration().TelegramBotAPIKey);
            var communicationService = new CommunicationService();

            Starman starman = new Starman(botClient, communicationService);

            var me = starman.GetMe().Result;


            starman.Start();
            Console.WriteLine($"Start listening for @{me.Username}");


            var stopWord = Console.ReadLine();
            while(stopWord.ToLower() != "shutdown")
            {
                stopWord = Console.ReadLine();
            }

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
