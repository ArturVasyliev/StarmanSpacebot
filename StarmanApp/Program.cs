using StarmanLibrary;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace StarmanApp
{
    class Program
    {
        public static readonly TelegramBotClient Bot = new TelegramBotClient(new Configuration().TelegramBotAPIKey);

        static void Main(string[] args)
        {
            var me = Bot.GetMeAsync().Result;

            Bot.OnMessage += BotOnMessageReceived;
            Bot.StartReceiving();
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            Bot.StopReceiving();

            //var r = Maas2.GetCurrentMarsStatus();
            //var r1 = Maas2.GetMarsStatusBySol(1);
            //var r2 = OpenNotify.GetHumansInSpace();
            //var r3 = OpenNotify.GetIssData();
            //var r4 = GoogleMaps.GetAddressByLocation(r3.Position.Latitude, r3.Position.Longitude);
            //var r5 = Nasa.GetAstroPicOfTheDay(DateTime.Now);
            //var r6 = Nasa.GetAstroPicOfTheDay(new DateTime(2017, 11, 2));
            //var r7 = Nasa.GetTheLatestPicOfTheEarth();

        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.TextMessage) return;

            switch (message.Text.Split(' ')[0])
            {
                // send inline keyboard
                case "/inline":
                    await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                    await Task.Delay(500); // simulate longer running task

                    var inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new [] // first row
                        {
                            InlineKeyboardButton.WithCallbackData("1.1"),
                            InlineKeyboardButton.WithCallbackData("1.2"),
                        },
                        new [] // second row
                        {
                            InlineKeyboardButton.WithCallbackData("2.1"),
                            InlineKeyboardButton.WithCallbackData("2.2"),
                        }
                    });

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Choose",
                        replyMarkup: inlineKeyboard);
                    break;

                // send custom keyboard
                case "/keyboard":
                    ReplyKeyboardMarkup ReplyKeyboard = new ReplyKeyboardMarkup(new KeyboardButton[]
                    {
                        new KeyboardButton("button1"),
                        new KeyboardButton("button2")
                    });

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Choose",
                        replyMarkup: ReplyKeyboard);
                    break;

                default:
                    const string usage = @"
Usage:
/inline   - send inline keyboard
/keyboard - send custom keyboard
/photo    - send a photo
/request  - request location or contact";

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        usage,
                        replyMarkup: new ReplyKeyboardRemove());
                    break;
            }
        }
    }
}
