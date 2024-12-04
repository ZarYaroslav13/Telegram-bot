using AutoMapper;
using FakeItEasy;
using Server.Logic.Currency;
using Server.Logic.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Server.Logic.Tests;

[TestClass]
public class BotServerTests
{
    [TestMethod]
    [DynamicData(nameof(BotServerDataProvider.ConstructorExceptionsData), typeof(BotServerDataProvider))]
    public void BotServer_Constructor_Exception(ITelegramBotClient botClient, IMapper mapper, string Url)
    {
        Assert.ThrowsException<ArgumentNullException>(() => new BotServer(botClient, mapper, Url));
    }
}
