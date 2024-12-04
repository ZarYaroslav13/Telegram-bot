using FakeItEasy;
using Server.Logic.API.PivatBankAPI;
using Server.Logic.Currency;
using Server.Logic.Menu;
using Server.Logic.Menu.Buttons;
using Server.Logic.Resources;
using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Server.Logic.Tests.Data;

public class TextUpdatorDataProvider
{
    public static PrivatBankExchangeCurrencyDTO CurrencyExchangeDTO { get; } = new PrivatBankExchangeCurrencyDTO()
    {
        Currency = CurrencyProvider.USD.Code,
        PurchaseRateNB = 10.28f,
        SaleRateNB = 10.28f,
        SaleRate = 12.20f,
        PurchaseRate = 10.20f
    };

    public static CurrencyExchange MyCurrencyExchange { get; } = new()
    {
        Currency = CurrencyProvider.Currencies.First(x => x.Code == CurrencyExchangeDTO.GetCurrencyCode()),
        PurchaseRateNB = CurrencyExchangeDTO.PurchaseRateNB,
        PurchaseRate = CurrencyExchangeDTO.PurchaseRate,
        SaleRateNB = CurrencyExchangeDTO.SaleRateNB,
        SaleRate = CurrencyExchangeDTO.SaleRate
    };

    public static List<PrivatBankExchangeCurrencyDTO> CurrencyDTOs { get; } = new()
    {
        CurrencyExchangeDTO
    };


    
    public static IEnumerable<object[]> UpdateExceptionData { get; } = new List<object[]>
    {
        new object[]
        {
            A.Dummy<Update>(), new UserData(), null
        },
        new object[]
        {
            A.Dummy<Update>(), null, new CancellationToken()
        },
        new object[]
        {
            A.Dummy<Update>(), null, null
        },
        new object[]
        {
            null, new UserData(), null
        },
        new object[]
        {
            null, new UserData(), new CancellationToken()
        },
        new object[]
        {
            A.Dummy<Update>(), null, new CancellationToken()
        },
        new object[]
        {
            A.Dummy<Update>(), null, null
        }
    };

    public static IEnumerable<object[]> TouchedButtonAndTextWithMarkupResponseData { get; } = new List<object[]>
    {
        new object[] {
            A.Dummy<UserData>(),
            MainMenuButtonsProvider.ShowExchangeRate.Text,
            MyCurrencyExchange.ToString(DateTime.Today.Date),
            BotMenuProvider.MainMenu },
        new object[] {
            A.Dummy<UserData>(),
            MainMenuButtonsProvider.ChangeCurrency.Text,
            Messages.ChooseCurrency,
            BotMenuProvider.CurrencyMenu },
        new object[] {
            A.Dummy<UserData>(),
            MainMenuButtonsProvider.ChangeDate.Text,
            Messages.EnterDateInPattern + $"{CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern}",
            BotMenuProvider.DateMenu },
        new object[] {
            A.Dummy<UserData>(),
            MainMenuButtonsProvider.ShowCurrencyAndDate.Text,
                Messages.CurrentCurrencyAndDate +
                $"Currency: {Logic.Currency.CurrencyProvider.USD}\n" +
                $"Date: {DateTime.Today.ToShortDateString()}\n",
            BotMenuProvider.MainMenu },
        new object[] {
            A.Dummy<UserData>(),
            DateMenuButtonsProvider.Confirm.Text,
                Messages.DateNotchanged +
                $"{DateTime.Today.ToShortDateString()}",
            BotMenuProvider.MainMenu },
        new object[] {
            A.Dummy<UserData>(),
            String.Empty,
            Messages.UnknownMessageReceived,
            BotMenuProvider.MainMenu },
        new object[] {
            A.Dummy<UserData>(),
            "        ",
            Messages.UnknownMessageReceived,
            BotMenuProvider.MainMenu },
        new object[] {
            A.Dummy<UserData>(),
            _randomString,
            Messages.UnknownMessageReceived,
            BotMenuProvider.MainMenu },
        new object[] {
            A.Dummy<UserData>(),
            DateMenuButtonsProvider.DayBefore.Text,
            Messages.ChoosenDate + ": " + DateTime.Today.AddDays(-1).ToShortDateString(),
            BotMenuProvider.DateMenu },
        new object[] {
            new UserData(){ChoosenDate = DateTime.Today.AddDays(-1)},
            DateMenuButtonsProvider.DayAfter.Text,
            Messages.ChoosenDate + ": " + DateTime.Today.ToShortDateString(),
            BotMenuProvider.DateMenu },
        new object[] {
            new UserData(){ChoosenDate = DateTime.MinValue.AddDays(1)},
            DateMenuButtonsProvider.DayBefore.Text,
             Messages.CannotChangeDate +  DateTime.MinValue.AddDays(1).ToShortDateString(),
            BotMenuProvider.DateMenu },
        new object[] {
            A.Dummy<UserData>(),
            DateMenuButtonsProvider.DayAfter.Text,
             Messages.CannotChangeDate +  DateTime.Today.ToShortDateString(),
            BotMenuProvider.DateMenu },
        new object[] {
            A.Dummy<UserData>(),
            DateMenuButtonsProvider.Yesterday.Text,
            Messages.ChoosenDate + ": " + DateTime.Today.AddDays(-1).ToShortDateString(),
            BotMenuProvider.MainMenu },
    };

    public static IEnumerable<object[]> EnteringDateAndTextWithMarkupResponseData { get; } = new List<object[]>
    {
        new object[] {
            String.Empty,
            Messages.InvalidFormatOfDate +
              $"{CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern}",
            BotMenuProvider.DateMenu },
        new object[] {
            "        ",
            Messages.InvalidFormatOfDate +
              $"{CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern}",
            BotMenuProvider.DateMenu },
        new object[] {
            _randomString,
            Messages.InvalidFormatOfDate +
              $"{CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern}",
            BotMenuProvider.DateMenu },
        new object[] {
            _randomNumber,
            Messages.InvalidFormatOfDate +
              $"{CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern}",
            BotMenuProvider.DateMenu },
        new object[] {
            DateTime.Today.AddDays(1).ToString("MM.dd"),
            Messages.EnteredFutureData,
            BotMenuProvider.DateMenu },
        new object[] {
            DateTime.Today.AddDays(-1).ToString(),
            Messages.ChoosenDate + ": " +
            DateTime.Today.AddDays(-1).ToShortDateString(),
            BotMenuProvider.MainMenu },
        new object[] {
            DateTime.Today.ToString("MM.dd"),
            Messages.ChoosenDate + ": " +
            DateTime.Today.ToShortDateString(),
            BotMenuProvider.MainMenu },
        new object[] {
            DateTime.Today.ToString("MM.dd.yyyy"),
            Messages.ChoosenDate + ": " +
            DateTime.Today.ToShortDateString(),
            BotMenuProvider.MainMenu },
        new object[] {
            "-00.00.0000",
            Messages.InvalidFormatOfDate +
              $"{CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern}",
            BotMenuProvider.DateMenu },
        new object[] {
            "25.25.2024",
            Messages.InvalidFormatOfDate +
              $"{CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern}",
            BotMenuProvider.DateMenu },
        new object[] {
            "31.25.2024",
            Messages.InvalidFormatOfDate +
              $"{CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern}",
            BotMenuProvider.DateMenu },
        new object[] {
            "29.02.2023",
            Messages.InvalidFormatOfDate +
              $"{CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern}",
            BotMenuProvider.DateMenu },
        new object[] {
            "02.29.2023",
            Messages.InvalidFormatOfDate +
              $"{CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern}",
            BotMenuProvider.DateMenu }
    };

    private const string _randomString = "bhIUOgf45edtivf  7itgytr57e";
    private const string _randomNumber = "3";
}
