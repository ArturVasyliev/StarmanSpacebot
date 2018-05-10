using StarmanLibrary.Integrations.NASA;
using StarmanLibrary.Services;
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

namespace StarmanLibrary
{
    public class Starman : IDisposable
    {
        private readonly ITelegramBotClient _bot;
        private readonly ICommunicationService _communicationService;

        public Starman(ITelegramBotClient botClient, ICommunicationService communicationService)
        {
            _bot = botClient;
            _communicationService = communicationService;

            _bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            _bot.OnInlineQuery += BotOnInlineQueryReceived;
            _bot.OnInlineResultChosen += BotOnInlineResultChosenReceived;
            _bot.OnMessage += BotOnMessageReceived;
            _bot.OnMessageEdited += BotOnMessageEditedReceived;
            _bot.OnReceiveError += BotOnReceiveErrorReceived;
            _bot.OnReceiveGeneralError += BotOnReceiveGeneralErrorReceived;
            _bot.OnUpdate += BotOnUpdateReceived;
        }

        public void Dispose()
        {
            if (_bot != null)
            {
                _bot.OnCallbackQuery -= BotOnCallbackQueryReceived;
                _bot.OnInlineQuery -= BotOnInlineQueryReceived;
                _bot.OnInlineResultChosen -= BotOnInlineResultChosenReceived;
                _bot.OnMessage -= BotOnMessageReceived;
                _bot.OnMessageEdited -= BotOnMessageEditedReceived;
                _bot.OnReceiveError -= BotOnReceiveErrorReceived;
                _bot.OnReceiveGeneralError -= BotOnReceiveGeneralErrorReceived;
                _bot.OnUpdate -= BotOnUpdateReceived;
            }
        }

        public async Task<User> GetMe()
        {
            return await _bot.GetMeAsync();
        }

        public void Start()
        {
            _bot.StartReceiving();
        }

        public void Stop()
        {
            _bot.StopReceiving();
        }

        private IReplyMarkup mainKeyboard;
        private IReplyMarkup GetMainKeyboard()
        {
            return mainKeyboard ?? (mainKeyboard = new ReplyKeyboardMarkup(new KeyboardButton[][]
            {
                new [] {new KeyboardButton("Mars 🌕"), new KeyboardButton("Moon 🌑") },
                new [] {new KeyboardButton("ISS 🛰️"), new KeyboardButton("SpaceX 🚀") },
                new [] {new KeyboardButton("Astronauts 👨🏻‍🚀"), new KeyboardButton("Pics 🖼️") }
            }));
        }

        private IReplyMarkup spacexKeyboard;
        private IReplyMarkup GetSpacexKeyboard()
        {
            return spacexKeyboard ?? (spacexKeyboard = new ReplyKeyboardMarkup(new KeyboardButton[][]
            {
                new [] {new KeyboardButton("Company 🌕"), new KeyboardButton("Rockets 🚀") },
                new [] {new KeyboardButton("Launches 🛰️"), new KeyboardButton("⬅️Back") }
            }));
        }

        private IReplyMarkup picsKeyboard;
        private IReplyMarkup GetPicsKeyboard()
        {
            return picsKeyboard ?? (picsKeyboard = new InlineKeyboardMarkup(new[]
            {
                new [] { InlineKeyboardButton.WithCallbackData("Astro Pic Of The Day", "APOD") },
                new [] { InlineKeyboardButton.WithCallbackData("Earth Pic", "Earth Pic") }
            }));
        }

        public async void ProcessMessage(string text, long chatId)
        {
            await _bot.SendChatActionAsync(chatId, ChatAction.Typing);

            string responseText = string.Empty;
            IReplyMarkup replyKeyboard = null;

            switch (text)
            {
                case "/start":
                    responseText = _communicationService.GetHelloMessage();
                    replyKeyboard = GetMainKeyboard();
                    await _bot.SendTextMessageAsync(chatId, responseText, replyMarkup: replyKeyboard);
                    break;
                case "Mars 🌕":
                case "/mars":
                    responseText = _communicationService.GetMarsStatus();
                    replyKeyboard = GetMainKeyboard();
                    await _bot.SendTextMessageAsync(chatId, responseText, replyMarkup: replyKeyboard);
                    break;
                case "Moon 🌑":
                case "/moon":
                    responseText = _communicationService.GetMoonStatus();
                    replyKeyboard = GetMainKeyboard();
                    await _bot.SendTextMessageAsync(chatId, responseText, replyMarkup: replyKeyboard);
                    break;
                case "ISS 🛰️":
                case "/iss":
                    responseText = _communicationService.GetIssStatusText();
                    replyKeyboard = GetMainKeyboard();
                    await _bot.SendTextMessageAsync(chatId, responseText, replyMarkup: replyKeyboard);

                    var location = _communicationService.GetIssPosition();
                    await _bot.SendLocationAsync(chatId, (float)location[0], (float)location[1], replyMarkup: replyKeyboard);
                    break;
                case "SpaceX 🚀":
                    responseText = _communicationService.GetHomelandResponse();
                    replyKeyboard = GetSpacexKeyboard();
                    await _bot.SendTextMessageAsync(chatId, responseText, replyMarkup: replyKeyboard);
                    break;
                case "Company 🌕":
                    responseText = _communicationService.GetSpacexCompanyInfo();
                    replyKeyboard = GetSpacexKeyboard();
                    await _bot.SendTextMessageAsync(chatId, responseText, replyMarkup: replyKeyboard);
                    break;
                case "Rockets 🚀":
                    responseText = _communicationService.GetSpacexRocketsInfo();
                    replyKeyboard = GetSpacexKeyboard();
                    await _bot.SendTextMessageAsync(chatId, responseText, replyMarkup: replyKeyboard);
                    break;
                case "Launches 🛰️":
                    responseText = _communicationService.GetLaunchesInfo();
                    replyKeyboard = GetSpacexKeyboard();
                    await _bot.SendTextMessageAsync(chatId, responseText, replyMarkup: replyKeyboard);
                    break;
                case "⬅️Back":
                    responseText = _communicationService.GetBackResponse();
                    replyKeyboard = GetMainKeyboard();
                    await _bot.SendTextMessageAsync(chatId, responseText, replyMarkup: replyKeyboard);
                    break;
                case "Astronauts 👨🏻‍🚀":
                case "/astronauts":
                    responseText = _communicationService.GetHumansInSpace();
                    replyKeyboard = GetMainKeyboard();
                    await _bot.SendTextMessageAsync(chatId, responseText, replyMarkup: replyKeyboard);
                    break;
                case "Pics 🖼️":
                case "/pics":
                    responseText = _communicationService.GetSpacePics();
                    replyKeyboard = GetPicsKeyboard();
                    await _bot.SendTextMessageAsync(chatId, responseText, replyMarkup: replyKeyboard);
                    break;
                case "/help":
                    responseText = _communicationService.GetHelp();
                    replyKeyboard = GetMainKeyboard();
                    await _bot.SendTextMessageAsync(chatId, responseText, replyMarkup: replyKeyboard);
                    break;
                case "/settings":
                    responseText = _communicationService.GetSettings();
                    replyKeyboard = GetMainKeyboard();
                    await _bot.SendTextMessageAsync(chatId, responseText, replyMarkup: replyKeyboard);
                    break;
                default:
                    responseText = _communicationService.GetDefaultResponse();
                    await _bot.SendTextMessageAsync(chatId, responseText, replyMarkup: replyKeyboard);
                    break;
            }
        }

        // This method handles messages and main buttons
        public async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            Console.WriteLine("BotOnMessageReceived");
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.TextMessage) return;

            ProcessMessage(message.Text, message.Chat.Id);
        }

        // This method handles inline buttons
        public async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
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

                    await _bot.SendPhotoAsync(
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
                    await _bot.SendPhotoAsync(
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
                    await _bot.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id,
                            res.Description);
                    break;
                case "EarthPicLocation":
                    var earthPic2 = Nasa.GetTheLatestPicOfTheEarth();
                    await _bot.SendLocationAsync(
                            callbackQuery.Message.Chat.Id,
                            (float)earthPic2.Location.Latitude,
                            (float)earthPic2.Location.Longitude);
                    break;
            }
        }

        private async void BotOnInlineQueryReceived(object sender, InlineQueryEventArgs inlineQueryEventArgs)
        {
            Console.WriteLine("BotOnInlineQueryReceived");
            var inlineQuery = inlineQueryEventArgs.InlineQuery;
            
            // choose the command

            if (false == true)
            {
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

                await _bot.AnswerInlineQueryAsync(
                    inlineQueryEventArgs.InlineQuery.Id,
                    results,
                    isPersonal: true,
                    cacheTime: 0);
            }
        }

        private async void BotOnInlineResultChosenReceived(object sender, ChosenInlineResultEventArgs chosenInlineResultEventArgs)
        {
            Console.WriteLine("BotOnInlineResultChosenReceived");
            var chosenInlineResult = chosenInlineResultEventArgs.ChosenInlineResult;
        }

        private async void BotOnMessageEditedReceived(object sender, MessageEventArgs messageEventArgs)
        {
            Console.WriteLine("BotOnMessageEditedReceived");
            var message = messageEventArgs.Message;
        }

        private async void BotOnReceiveErrorReceived(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Console.WriteLine("BotOnReceiveErrorReceived");
            Console.WriteLine("Received error: {0} — {1}",
                receiveErrorEventArgs.ApiRequestException.ErrorCode,
                receiveErrorEventArgs.ApiRequestException.Message);
        }

        private async void BotOnReceiveGeneralErrorReceived(object sender, ReceiveGeneralErrorEventArgs receiveGeneralErrorEventArgs)
        {
            Console.WriteLine("BotOnReceiveGeneralErrorReceived");
            var receiveGeneralError = receiveGeneralErrorEventArgs.Exception;
        }

        private async void BotOnUpdateReceived(object sender, UpdateEventArgs updateEventArgs)
        {
            Console.WriteLine("BotOnUpdateReceived");
            var update = updateEventArgs.Update;
        }
    }
}
