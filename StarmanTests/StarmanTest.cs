using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StarmanLibrary;
using StarmanLibrary.Services;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace StarmanTests
{
    [TestClass]
    public class StarmanTest
    {
        [TestMethod]
        public void Starman_ProcessMessage_NullMessage_NothingHappens()
        {
            var botClient = new Mock<ITelegramBotClient>();
            botClient.Setup(bot => bot.SendChatActionAsync(It.IsAny<long>(),
                It.IsAny<ChatAction>(), It.IsAny<CancellationToken>())).Verifiable();
            var communicationService = new Mock<ICommunicationService>();
            
            var starman = new Starman(botClient.Object, communicationService.Object);
            starman.ProcessMessage(null, 0);

            botClient.VerifyAll();
        }
    }
}
