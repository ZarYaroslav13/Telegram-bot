using FakeItEasy;
using Server.Logic.Tests.Data;
using Server.Logic.Updators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Server.Logic.Tests.Updators;

[TestClass]
public class EntityUpdatorTests
{
    private UserData _userData;
    private ITelegramBotClient _botClient;
    private Update _update;

    public EntityUpdatorTests()
    {
        _botClient = A.Fake<ITelegramBotClient>();

        _userData = new();
        _update = A.Fake<Update>();
    }

    [TestMethod]
    public void EntityUpdator_Constructor_Exception()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new CallbackQueryUpdator(null));
    }

    [TestMethod]
    [DynamicData(nameof(EntityUpdatorDataProvider.EntityUpdatorUpdateExceptionsData), typeof(EntityUpdatorDataProvider))]
    public void EntityUpdator_Update_Exception(Update update, UserData userdata, CancellationToken cancellationToken)
    {
        Assert.ThrowsExceptionAsync<ArgumentNullException>(() => new CallbackQueryUpdator(_botClient).Update(update, userdata, cancellationToken));
    }
}
