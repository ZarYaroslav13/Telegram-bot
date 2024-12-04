using FakeItEasy;
using Server.Logic.Updators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Server.Logic.Tests.Data;
using Telegram.Bot;
using Server.Logic.Updator;
using AutoMapper;
using Server.Logic.Currency;

namespace Server.Logic.Tests.Updators;

[TestClass]
public class BotUpdatorTests
{
    private UserData _userData;
    private Update _update;
    private readonly CancellationToken _cancellationToken;
    private readonly ITelegramBotClient _botClient;
    private readonly BotUpdator _botUpdator;
    private readonly IMapper _mapper;

    public BotUpdatorTests()
    {
        _botClient = A.Fake<ITelegramBotClient>();
        _mapper = A.Dummy<IMapper>();

        _userData = new();
        _botUpdator = new(_botClient, _mapper);
        _update = A.Fake<Update>();
        _cancellationToken = new();
    }

    [TestMethod]
    [DynamicData(nameof(BotUpdatorDataProvider.ConstructorExcptionData), typeof(BotUpdatorDataProvider))]
    public void BotUpdator_Constuctor_Exception(ITelegramBotClient bot, IMapper mapper)
    {
        Assert.ThrowsException<ArgumentNullException>(() => new BotUpdator(bot, mapper));
    }

    [TestMethod]
    [DynamicData(nameof(BotUpdatorDataProvider.UpdateUpdatorData), typeof(BotUpdatorDataProvider))]
    public async Task BotUpdator_Update_CallUpdators(Update update, Telegram.Bot.Types.Enums.UpdateType updateType, Type updatorType)
    {
        _update.Message = A.Fake<Message>();

        await _botUpdator.Update(update, _userData, _cancellationToken);
        var resultUserData = _userData;
        await _botUpdator.GetUpdator(updateType).Update(update, _userData, _cancellationToken);
        var usedUpdator = _botUpdator.GetUpdator(updateType);

        Assert.AreEqual(updatorType, usedUpdator.GetType());
        Assert.AreEqual(_userData, resultUserData);
    }
}
