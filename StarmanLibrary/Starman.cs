using StarmanLibrary.Integrations.BurningSoul;
using StarmanLibrary.Integrations.MAAS2;
using StarmanLibrary.Integrations.NASA;
using StarmanLibrary.Integrations.OpenNotify;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;

namespace StarmanLibrary
{
    public class Starman
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient(new Configuration().TelegramBotAPIKey);

        public Starman()
        {
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            Bot.OnInlineQuery += BotOnInlineQueryReceived;
            Bot.OnInlineResultChosen += BotOnInlineResultChosenReceived;
            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnMessageEdited += BotOnMessageEditedReceived;
            Bot.OnReceiveError += BotOnReceiveErrorReceived;
            Bot.OnReceiveGeneralError += BotOnReceiveGeneralErrorReceived;
            Bot.OnUpdate += BotOnUpdateReceived;
        }

        public async Task<User> GetMe()
        {
            return await Bot.GetMeAsync();
        }

        public void Start()
        {
            Bot.StartReceiving();
        }

        public void Stop()
        {
            Bot.StopReceiving();
        }

        private ReplyKeyboardMarkup GetMainKeyboard()
        {
            ReplyKeyboardMarkup mainKeyboard = new ReplyKeyboardMarkup(new KeyboardButton[][]
            {
                new [] {new KeyboardButton("Mars 🌕"), new KeyboardButton("Moon 🌑") },
                new [] {new KeyboardButton("ISS 🛰️"), new KeyboardButton("SpaceX 🚀") },
                new [] {new KeyboardButton("Astronauts 👨🏻‍🚀"), new KeyboardButton("Pics 🖼️") }
            });

            return mainKeyboard;
        }

        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            // Here we should handle buttons and messages
            Console.WriteLine("BotOnMessageReceived");
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.TextMessage) return;

            string messageText = "";

            switch (message.Text)
            {
                case "Mars 🌕":
                    var marsStatus = Maas2.GetCurrentMarsStatus();
                    messageText = $"{marsStatus.MaxTemp} {marsStatus.MinTemp} {marsStatus.Sol}";
                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        messageText,
                        replyMarkup: GetMainKeyboard());
                    break;
                case "Moon 🌑":
                    var moonStatus = BurningSoul.GetCurrentMoonStatus();
                    messageText = $"{moonStatus.Age} {moonStatus.DistanceFromCoreOfEarth} {moonStatus.Stage}";
                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        messageText,
                        replyMarkup: GetMainKeyboard());
                    break;
                case "ISS 🛰️":
                    var issStatus = OpenNotify.GetIssData();
                    messageText = $"{issStatus.Message} {issStatus.Position.Latitude} {issStatus.Position.Longitude}";
                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        messageText,
                        replyMarkup: GetMainKeyboard());
                    break;
                case "SpaceX 🚀":
                    break;
                case "Astronauts 👨🏻‍🚀":
                    var astronautsResult = OpenNotify.GetHumansInSpace();
                    messageText = $"{astronautsResult.Number} {astronautsResult.Message} {astronautsResult.People?[0].Name}";
                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        messageText,
                        replyMarkup: GetMainKeyboard());
                    break;
                case "Pics 🖼️":
                    var inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new [] { InlineKeyboardButton.WithCallbackData("Astro Pic Of The Day", "APOD") },
                        new [] { InlineKeyboardButton.WithCallbackData("Earth Pic", "Earth Pic") }
                    });

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "You can watch images from the space!!!",
                        replyMarkup: inlineKeyboard);
                    break;
                case "/start":
                    break;
                case "/astronauts":
                    break;
                case "/mars":
                    break;
                case "/pics":
                    break;
                case "/moon":
                    break;
                case "/iss":
                    break;
                case "/spacex":
                    await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                    await Task.Delay(1000); // simulate longer running task

                    var inlineKeyboard1 = new InlineKeyboardMarkup(new[]
                    {
                        new [] { InlineKeyboardButton.WithCallbackData("1.1"), InlineKeyboardButton.WithCallbackData("1.2"), },
                        new [] { InlineKeyboardButton.WithCallbackData("2.1"), InlineKeyboardButton.WithCallbackData("2.2"), }
                    });

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Choose",
                        replyMarkup: inlineKeyboard1);

                    break;
                case "":
                    break;
                default:
                    string usage = "";

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        usage,
                        replyMarkup: GetMainKeyboard());
                    break;
            }
        }

        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            // Here we should handle inline buttons
            Console.WriteLine("BotOnCallbackQueryReceived");
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;
            InlineKeyboardMarkup inlineKeyboard = null;

            switch (callbackQuery.Data)
            {
                case "APOD":
                    var re = Nasa.GetAstroPicOfTheDay(DateTime.Now);
                    if (re == null)
                    {
                        re = Nasa.GetAstroPicOfTheDay(DateTime.Now - TimeSpan.FromDays(1));
                    }
                    inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new [] { InlineKeyboardButton.WithCallbackData("Watch description", "APOD Description") }
                    });

                    await Bot.SendPhotoAsync(
                            callbackQuery.Message.Chat.Id,
                            new FileToSend(new Uri(re.Url)),
                            re.Title,
                            replyMarkup: inlineKeyboard);
                    break;
                case "Earth Pic":
                    var earthPic = Nasa.GetTheLatestPicOfTheEarth();
                    inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new [] { InlineKeyboardButton.WithCallbackData("Get location", "EarthPicLocation") }
                    });
                    await Bot.SendPhotoAsync(
                            callbackQuery.Message.Chat.Id,
                            new FileToSend(new Uri(earthPic.ImageUrl)),
                            earthPic.ImageTitle,
                            replyMarkup: inlineKeyboard);
                    break;
                case "APOD Description":
                    var res = Nasa.GetAstroPicOfTheDay(DateTime.Now);
                    if (res == null)
                    {
                        res = Nasa.GetAstroPicOfTheDay(DateTime.Now - TimeSpan.FromDays(1));
                    }
                    await Bot.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id,
                            res.Description);
                    break;
                case "EarthPicLocation":
                    var earthPic2 = Nasa.GetTheLatestPicOfTheEarth();
                    await Bot.SendLocationAsync(
                            callbackQuery.Message.Chat.Id,
                            (float)earthPic2.Location.Latitude,
                            (float)earthPic2.Location.Longitude);
                    break;
            }


            //await Bot.AnswerCallbackQueryAsync(
            //                callbackQuery.Id,
            //                $"Received {callbackQuery.Data}");
            //await Bot.SendTextMessageAsync(
            //    callbackQuery.Message.Chat.Id,
            //    $"Received {callbackQuery.Data}");
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
