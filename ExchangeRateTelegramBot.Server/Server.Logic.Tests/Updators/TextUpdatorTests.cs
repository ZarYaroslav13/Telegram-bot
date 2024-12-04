using AutoMapper;
using FakeItEasy;
using Server.Logic.API.PivatBankAPI;
using Server.Logic.Currency;
using Server.Logic.Resources;
using Server.Logic.Tests.Data;
using Server.Logic.Updators;
using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Server.Logic.Tests.Updators;

[TestClass]
public class TextUpdatorTests
{
    private UserData _userData;
    private ITelegramBotClient _botClient;
    private TextUpdator _textUpdator;
    private Update _update;
    private readonly CancellationToken _cancellationToken;
    private readonly IMapper _mapper;
    private readonly CurrencyExchangeReader _currencyExchangeReader;

    public TextUpdatorTests()
    {
        _botClient = A.Fake<ITelegramBotClient>();
        _mapper = A.Fake<IMapper>();
        _currencyExchangeReader = A.Fake<CurrencyExchangeReader>(x => x.WithArgumentsForConstructor(new object[] { "url" }));

        A.CallTo(() => _currencyExchangeReader.ReadAsync<PrivatBankExchangeCurrencyDTO>(DateTime.Today)).Returns(TextUpdatorDataProvider.CurrencyDTOs);
        A.CallTo(() => 
            _mapper.Map<CurrencyExchange>(TextUpdatorDataProvider.CurrencyExchangeDTO)).
            Returns(TextUpdatorDataProvider.MyCurrencyExchange);

        _userData = new();
        _textUpdator = new(_botClient, _mapper, _currencyExchangeReader);
        _update = A.Fake<Update>();
        _cancellationToken = new();
    }

    [TestMethod]
    [DynamicData(nameof(TextUpdatorDataProvider.TouchedButtonAndTextWithMarkupResponseData), typeof(TextUpdatorDataProvider))]
    public async Task TextUpdator_ResponseToTouchMenuButtons_StringIReplyMarkup_Async(
        UserData userData,
        string messageButtonText,
        string expectedResponse,
        IReplyMarkup expectedMarkup)
    {
        Message message = new();
        message.Chat = A.Fake<Chat>();
        message.Text = messageButtonText;
        _update.Message = message;

        await _textUpdator.Update(_update, userData, _cancellationToken);

        Assert.AreEqual(expectedResponse, userData.Response);
        Assert.AreEqual(expectedMarkup, userData.Markup);
    }

    [TestMethod]
    [DynamicData(nameof(TextUpdatorDataProvider.EnteringDateAndTextWithMarkupResponseData), typeof(TextUpdatorDataProvider))]
    public async Task TextUpdator_EnteringData_StringIReplyMarkup_Async(
        string messageButtonText,
        string expectedResponse,
        IReplyMarkup expectedMarkup)
    {
        Message message = new();
        message.Chat = A.Fake<Chat>();
        message.Text = messageButtonText;
        _update.Message = message;

        _userData.MessageSent.Text = Messages.EnterDateByFormPattern +
            $"{CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern}";

        await _textUpdator.Update(_update, _userData, _cancellationToken);

        Assert.AreEqual(expectedResponse, _userData.Response);
        Assert.AreEqual(expectedMarkup, _userData.Markup);

        _userData.MessageSent.Text = Messages.InvalidFormatOfDate +
              $"{CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern}";

        await _textUpdator.Update(_update, _userData, _cancellationToken);

        Assert.AreEqual(expectedResponse, _userData.Response);
        Assert.AreEqual(expectedMarkup, _userData.Markup);
    }

    [TestMethod]
    [DynamicData(nameof(TextUpdatorDataProvider.UpdateExceptionData), typeof(TextUpdatorDataProvider))]
    public void TextUpdator_Update_Exception(Update update, UserData userData, CancellationToken cancellationToken)
    {
        Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _textUpdator.Update(update, userData, cancellationToken));
    }
}
