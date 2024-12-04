using FakeItEasy;
using Server.Logic.Menu;
using Server.Logic.Tests.Data;
using Server.Logic.Updators;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Server.Logic.Tests.Updators;

[TestClass]
public class CallbackQueryUpdatorTests
{
    private UserData _userData = new();
    private ITelegramBotClient _botClient;
    private CallbackQueryUpdator _callbackQueryUpdator;
    private Update _update;
    private readonly CancellationToken _cancellationToken;

    public CallbackQueryUpdatorTests()
    {
        _botClient = A.Fake<ITelegramBotClient>();
        _userData = new();
        _callbackQueryUpdator = new(_botClient);
        _update = A.Fake<Update>();
        _cancellationToken = new();
    }

    [TestMethod]
    [DynamicData(nameof(CallbackQueryUpdatorDataProvider.TouchedCurrencyButtonData), typeof(CallbackQueryUpdatorDataProvider))]
    public async Task CallbackQueryUpdator_ResponseToTouchCurrencyMenuButtons_StringCurrency_Async(
        string messageButtonText,
        string expectedResponse,
        Logic.Currency.Currency expectedCurrency)
    {
        _update.CallbackQuery = new() { Data = messageButtonText };
        _update.CallbackQuery.Message = new() { Chat = A.Fake<Chat>() };

        await _callbackQueryUpdator.Update(_update, _userData, _cancellationToken);

        Assert.AreEqual(expectedResponse, _userData.Response);
        Assert.AreEqual(BotMenuProvider.MainMenu, _userData.Markup);
        Assert.AreEqual(expectedCurrency, _userData.ChoosenCurrency);
    }

    [TestMethod]
    public void CallbackQueryUpdator_Work_Exception()
    {
        Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _callbackQueryUpdator.Update(null, A.Dummy<UserData>(), _cancellationToken));
    }
}
