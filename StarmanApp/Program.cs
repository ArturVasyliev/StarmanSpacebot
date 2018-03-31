using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;
using StarmanLibrary.Integrations.MAAS2;
using StarmanLibrary;

namespace StarmanApp
{
    class Program
    {
        public static readonly TelegramBotClient Bot = new TelegramBotClient(new Configuration().TelegramBotAPIKey);

        static void Main(string[] args)
        {
            //var me = Bot.GetMeAsync().Result;

            //Bot.OnMessage += BotOnMessageReceived;
            //Bot.StartReceiving();
            //Console.WriteLine($"Start listening for @{me.Username}");
            //Console.ReadLine();
            //Bot.StopReceiving();

            var r = Maas2.GetCurrentMarsStatus();
            var r1 = Maas2.GetMarsStatusBySol(1);
            var r2 = Maas2.GetMarsStatusBySol(190);
            var r3 = Maas2.GetMarsStatusBySol(200);
            var r4 = Maas2.GetMarsStatusBySol(2000000);
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

        static string GetBotAPIKey()
        {
            string configuration;

            using(StreamReader sr = new StreamReader("starman.json"))
            {
                configuration = sr.ReadToEnd();
            }

            JObject obj = JObject.Parse(configuration);
            return (string)obj["ApiKey"];
        }
    }
}
