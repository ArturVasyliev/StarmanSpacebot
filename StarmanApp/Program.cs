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
        public static readonly TelegramBotClient Bot = new TelegramBotClient(new Configuration().TelegramBotAPIKey);

        static void Main(string[] args)
        {
            var me = Bot.GetMeAsync().Result;

            
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            Bot.OnInlineQuery += BotOnInlineQueryReceived;
            Bot.OnInlineResultChosen += BotOnInlineResultChosenReceived;
            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnMessageEdited += BotOnMessageEditedReceived;
            Bot.OnReceiveError += BotOnReceiveErrorReceived;
            Bot.OnReceiveGeneralError += BotOnReceiveGeneralErrorReceived;
            Bot.OnUpdate += BotOnUpdateReceived;
            
            

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
            // Here we should handle buttons and messages
            Console.WriteLine("BotOnMessageReceived");
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
                    const string usage = @"Usage:
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

        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            // Here we should handle inline buttons
            Console.WriteLine("BotOnCallbackQueryReceived");
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;

            await Bot.AnswerCallbackQueryAsync(
                callbackQuery.Id,
                $"Received {callbackQuery.Data}");

            await Bot.SendTextMessageAsync(
                callbackQuery.Message.Chat.Id,
                $"Received {callbackQuery.Data}");
        }

        private static async void BotOnInlineQueryReceived(object sender, InlineQueryEventArgs inlineQueryEventArgs)
        {
            Console.WriteLine("BotOnInlineQueryReceived");
            var inlineQuery = inlineQueryEventArgs.InlineQuery;


            //id: "1",
            //        latitude: 40.7058316f,
            //        longitude: -74.2581888f,
            //        title: "New York"


            //    Latitude: 40.7058316f,
            //                Longitude: -74.2581888f   // message if result is selected

            InlineQueryResultLocation[] results = new InlineQueryResultLocation[] {
                new InlineQueryResultLocation()   // displayed result
                    {
                        Id = "1",
                        Latitude = 40.7058316f,
                        Longitude = -74.2581888f,
                        Title = "New York",
                        InputMessageContent = new InputLocationMessageContent()
                        {
                            Latitude = 40.7058316f,
                            Longitude = -74.2581888f
                        }
                    },

                new InlineQueryResultLocation()
                    {
                        Id = "2",
                        Latitude = 13.1449577f,
                        Longitude =  52.507629f,
                        Title =  "Berlin", // displayed result
                        InputMessageContent = new InputLocationMessageContent()
                        {
                            Latitude = 13.1449577f,
                            Longitude = 52.507629f   // message if result is selected
                        }
                    }
            };

            await Bot.AnswerInlineQueryAsync(
                inlineQueryEventArgs.InlineQuery.Id,
                results,
                isPersonal: true,
                cacheTime: 0);
        }

        private static async void BotOnInlineResultChosenReceived(object sender, ChosenInlineResultEventArgs chosenInlineResultEventArgs)
        {
            Console.WriteLine("BotOnInlineResultChosenReceived");
            var chosenInlineResult = chosenInlineResultEventArgs.ChosenInlineResult;
        }

        private static async void BotOnMessageEditedReceived(object sender, MessageEventArgs messageEventArgs)
        {
            Console.WriteLine("BotOnMessageEditedReceived");
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.TextMessage) return;

            switch (message.Text.Split(' ')[0])
            {
            }
        }

        private static async void BotOnReceiveErrorReceived(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Console.WriteLine("BotOnReceiveErrorReceived");
            Console.WriteLine("Received error: {0} — {1}",
                receiveErrorEventArgs.ApiRequestException.ErrorCode,
                receiveErrorEventArgs.ApiRequestException.Message);
            var receiveError = receiveErrorEventArgs.ApiRequestException;
        }

        private static async void BotOnReceiveGeneralErrorReceived(object sender, ReceiveGeneralErrorEventArgs receiveGeneralErrorEventArgs)
        {
            Console.WriteLine("BotOnReceiveGeneralErrorReceived");
            var receiveGeneralError = receiveGeneralErrorEventArgs.Exception;
        }

        private static async void BotOnUpdateReceived(object sender, UpdateEventArgs updateEventArgs)
        {
            Console.WriteLine("BotOnUpdateReceived");
            var update = updateEventArgs.Update;
        }
    }
}
