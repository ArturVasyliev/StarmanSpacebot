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

        private void RunProcessMessageTest(string text, 
            out Mock<ICommunicationService> communicationService, out Mock<ITelegramBotClient> botClient)
        {
            botClient = SetupTextBotClient();
            communicationService = new Mock<ICommunicationService>();

            CallProcessMessage(text, botClient, communicationService);
        }

        private void RunProcessMessageTestForIss(string text, 
            out Mock<ICommunicationService> communicationService, out Mock<ITelegramBotClient> botClient)
        {
            botClient = SetupTextBotClient();
            botClient.Setup(bot => bot.SendLocationAsync(It.IsAny<ChatId>(),
                It.IsAny<float>(), It.IsAny<float>(), It.IsAny<int>(),
                It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                It.IsAny<CancellationToken>())).Returns(Task.FromResult(new Message())).Verifiable();
            communicationService = new Mock<ICommunicationService>();
            communicationService.Setup(x => x.GetIssPosition()).Returns(new double[2] { 0, 0 });

            CallProcessMessage(text, botClient, communicationService);
        }

        [TestMethod]
        public void Starman_ProcessMessage_StartMessage_ClientCalled()
        {
            RunProcessMessageTest("/start", out Mock<ICommunicationService> communicationService, 
                out Mock<ITelegramBotClient> botClient);
            botClient.VerifyAll();
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
            botClient.VerifyAll();
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
            botClient.VerifyAll();
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
            botClient.VerifyAll();
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
            botClient.VerifyAll();
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
            botClient.VerifyAll();
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
            RunProcessMessageTest("/iss", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.VerifyAll();
        }

        [TestMethod]
        public void Starman_ProcessMessage_IssMessage_ServiceCalled()
        {
            RunProcessMessageTest("/iss", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            communicationService.Verify(x => x.GetIssStatusText());
        }

        [TestMethod]
        public void Starman_ProcessMessage_AstronautsEmojiMessage_ClientCalled()
        {
            RunProcessMessageTest("Astronauts 👨🏻‍🚀", out Mock<ICommunicationService> communicationService,
                out Mock<ITelegramBotClient> botClient);
            botClient.VerifyAll();
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
            botClient.VerifyAll();
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
            botClient.VerifyAll();
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
            botClient.VerifyAll();
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
            botClient.VerifyAll();
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
            botClient.VerifyAll();
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
            botClient.VerifyAll();
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
