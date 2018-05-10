using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StarmanLibrary;
using StarmanLibrary.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace StarmanTests
{
    [TestClass]
    public class StarmanTest
    {
        private void CallProcessMessage(string text, Mock<ITelegramBotClient> botClient, Mock<ICommunicationService> communicationService)
        {
            var starman = new Starman(botClient.Object, communicationService.Object);
            starman.ProcessMessage(text, 0);
        }

        private Mock<ITelegramBotClient> SetupTextBotClient()
        {
            var botClient = new Mock<ITelegramBotClient>();
            botClient.Setup(bot => bot.SendChatActionAsync(It.IsAny<ChatId>(),
                It.IsAny<ChatAction>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask).Verifiable();
            botClient.Setup(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), It.IsAny<string>(),
                It.IsAny<ParseMode>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(),
                It.IsAny<IReplyMarkup>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(new Message())).Verifiable();
            return botClient;
        }

        private string responseText = "responseText";

        private Mock<ICommunicationService> SetupCommunicationService()
        {
            var communicationService = new Mock<ICommunicationService>();
            communicationService.Setup(x => x.GetIssPosition()).Returns(new double[2] { 12, 21 });
            communicationService.Setup(x => x.GetHelloMessage()).Returns(responseText);
            communicationService.Setup(x => x.GetMarsStatus()).Returns(responseText);
            communicationService.Setup(x => x.GetMoonStatus()).Returns(responseText);
            communicationService.Setup(x => x.GetIssStatusText()).Returns(responseText);
            communicationService.Setup(x => x.GetHomelandResponse()).Returns(responseText);
            communicationService.Setup(x => x.GetSpacexCompanyInfo()).Returns(responseText);
            communicationService.Setup(x => x.GetSpacexRocketsInfo()).Returns(responseText);
            communicationService.Setup(x => x.GetLaunchesInfo()).Returns(responseText);
            communicationService.Setup(x => x.GetBackResponse()).Returns(responseText);
            communicationService.Setup(x => x.GetHumansInSpace()).Returns(responseText);
            communicationService.Setup(x => x.GetSpacePics()).Returns(responseText);
            communicationService.Setup(x => x.GetHelp()).Returns(responseText);
            communicationService.Setup(x => x.GetSettings()).Returns(responseText);
            communicationService.Setup(x => x.GetDefaultResponse()).Returns(responseText);
            
            return communicationService;
        }

        private void RunProcessMessageTest(string text, 
            out Mock<ICommunicationService> communicationService, out Mock<ITelegramBotClient> botClient)
        {
            communicationService = SetupCommunicationService();
            botClient = SetupTextBotClient();

            CallProcessMessage(text, botClient, communicationService);
        }

        private void RunProcessMessageTestForIss(string text, 
            out Mock<ICommunicationService> communicationService, out Mock<ITelegramBotClient> botClient)
        {
            communicationService = SetupCommunicationService();
            botClient = SetupTextBotClient();
            botClient.Setup(bot => bot.SendLocationAsync(It.IsAny<ChatId>(),
                It.IsAny<float>(), It.IsAny<float>(), It.IsAny<int>(),
                It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>())).Returns(Task.FromResult(new Message())).Verifiable();

            CallProcessMessage(text, botClient, communicationService);
        }

        [TestMethod]
        public void Starman_ProcessMessage_StartMessage_ClientCalled()
        {
            RunProcessMessageTest("/start", out Mock<ICommunicationService> communicationService, 
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_StartMessage_ServiceCalled()
        {
            RunProcessMessageTest("/start", out Mock<ICommunicationService> communicationService, 
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetHelloMessage());
        }

        [TestMethod]
        public void Starman_ProcessMessage_MarsEmojiMessage_ClientCalled()
        {
            RunProcessMessageTest("Mars 🌕", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_MarsEmojiMessage_ServiceCalled()
        {
            RunProcessMessageTest("Mars 🌕", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetMarsStatus());
        }

        [TestMethod]
        public void Starman_ProcessMessage_MarsMessage_ClientCalled()
        {
            RunProcessMessageTest("/mars", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_MarsMessage_ServiceCalled()
        {
            RunProcessMessageTest("/mars", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetMarsStatus());
        }

        [TestMethod]
        public void Starman_ProcessMessage_MoonEmojiMessage_ClientCalled()
        {
            RunProcessMessageTest("Moon 🌑", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_MoonEmojiMessage_ServiceCalled()
        {
            RunProcessMessageTest("Moon 🌑", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetMoonStatus());
        }

        [TestMethod]
        public void Starman_ProcessMessage_MoonMessage_ClientCalled()
        {
            RunProcessMessageTest("/moon", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_MoonMessage_ServiceCalled()
        {
            RunProcessMessageTest("/moon", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetMoonStatus());
        }

        [TestMethod]
        public void Starman_ProcessMessage_IssEmojiMessage_ClientCalled()
        {
            RunProcessMessageTestForIss("ISS 🛰️", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
            botClient.Verify(x => x.SendLocationAsync(It.IsAny<ChatId>(), 12, 21,
                It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(), It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_IssEmojiMessage_ServiceCalled()
        {
            RunProcessMessageTestForIss("ISS 🛰️", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetIssStatusText());
        }

        [TestMethod]
        public void Starman_ProcessMessage_IssMessage_ClientCalled()
        {
            RunProcessMessageTestForIss("/iss", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
            botClient.Verify(x => x.SendLocationAsync(It.IsAny<ChatId>(), 12, 21,
                It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(), It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_IssMessage_ServiceCalled()
        {
            RunProcessMessageTestForIss("/iss", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetIssStatusText());
        }

        [TestMethod]
        public void Starman_ProcessMessage_SpaceXEmojiMessage_ClientCalled()
        {
            RunProcessMessageTest("SpaceX 🚀", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_SpaceXEmojiMessage_ServiceCalled()
        {
            RunProcessMessageTest("SpaceX 🚀", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetHomelandResponse());
        }

        [TestMethod]
        public void Starman_ProcessMessage_CompanyEmojiMessage_ClientCalled()
        {
            RunProcessMessageTest("Company 🌕", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_CompanyEmojiMessage_ServiceCalled()
        {
            RunProcessMessageTest("Company 🌕", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetSpacexCompanyInfo());
        }

        [TestMethod]
        public void Starman_ProcessMessage_CompanyMessage_ClientCalled()
        {
            RunProcessMessageTest("/company", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_CompanyMessage_ServiceCalled()
        {
            RunProcessMessageTest("/company", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetSpacexCompanyInfo());
        }

        [TestMethod]
        public void Starman_ProcessMessage_RocketsEmojiMessage_ClientCalled()
        {
            RunProcessMessageTest("Rockets 🚀", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_RocketsEmojiMessage_ServiceCalled()
        {
            RunProcessMessageTest("Rockets 🚀", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetSpacexRocketsInfo());
        }

        [TestMethod]
        public void Starman_ProcessMessage_RocketsMessage_ClientCalled()
        {
            RunProcessMessageTest("/rockets", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_RocketsMessage_ServiceCalled()
        {
            RunProcessMessageTest("/rockets", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetSpacexRocketsInfo());
        }

        [TestMethod]
        public void Starman_ProcessMessage_LaunchesEmojiMessage_ClientCalled()
        {
            RunProcessMessageTest("Launches 🛰️", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_LaunchesEmojiMessage_ServiceCalled()
        {
            RunProcessMessageTest("Launches 🛰️", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetLaunchesInfo());
        }

        [TestMethod]
        public void Starman_ProcessMessage_LaunchesMessage_ClientCalled()
        {
            RunProcessMessageTest("/launches", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_BackMessage_ServiceCalled()
        {
            RunProcessMessageTest("⬅️Back", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetBackResponse());
        }

        [TestMethod]
        public void Starman_ProcessMessage_BackMessage_ClientCalled()
        {
            RunProcessMessageTest("⬅️Back", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_LaunchesMessage_ServiceCalled()
        {
            RunProcessMessageTest("/launches", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetLaunchesInfo());
        }

        [TestMethod]
        public void Starman_ProcessMessage_AstronautsEmojiMessage_ClientCalled()
        {
            RunProcessMessageTest("Astronauts 👨🏻‍🚀", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_AstronautsEmojiMessage_ServiceCalled()
        {
            RunProcessMessageTest("Astronauts 👨🏻‍🚀", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetHumansInSpace());
        }

        [TestMethod]
        public void Starman_ProcessMessage_AstronautsMessage_ClientCalled()
        {
            RunProcessMessageTest("/astronauts", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_AstronautsMessage_ServiceCalled()
        {
            RunProcessMessageTest("/astronauts", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetHumansInSpace());
        }

        [TestMethod]
        public void Starman_ProcessMessage_PicsEmojiMessage_ClientCalled()
        {
            RunProcessMessageTest("Pics 🖼️", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_PicsEmojiMessage_ServiceCalled()
        {
            RunProcessMessageTest("Pics 🖼️", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetSpacePics());
        }

        [TestMethod]
        public void Starman_ProcessMessage_PicsMessage_ClientCalled()
        {
            RunProcessMessageTest("/pics", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_PicsMessage_ServiceCalled()
        {
            RunProcessMessageTest("/pics", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetSpacePics());
        }

        [TestMethod]
        public void Starman_ProcessMessage_HelpMessage_ClientCalled()
        {
            RunProcessMessageTest("/help", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_HelpMessage_ServiceCalled()
        {
            RunProcessMessageTest("/help", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetHelp());
        }

        [TestMethod]
        public void Starman_ProcessMessage_SettingsMessage_ClientCalled()
        {
            RunProcessMessageTest("/settings", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_SettingsMessage_ServiceCalled()
        {
            RunProcessMessageTest("/settings", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetSettings());
        }

        [TestMethod]
        public void Starman_ProcessMessage_DefaultMessage_ClientCalled()
        {
            RunProcessMessageTest(null, out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.Verify(x => x.SendTextMessageAsync(It.IsAny<ChatId>(), responseText, It.IsAny<ParseMode>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Starman_ProcessMessage_DefaultMessage_ServiceCalled()
        {
            RunProcessMessageTest(null, out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetDefaultResponse());
        }
    }
}
