using Server.Logic.Currency;
using Server.Logic.Resources;

namespace Server.Logic.Tests.Data;

public class CallbackQueryUpdatorDataProvider
{
    public static IEnumerable<object[]> TouchedCurrencyButtonData { get; } = new List<object[]>
    {
        new object[]
        {
            CurrencyProvider.USD.Code,
            Messages.YouSelected + CurrencyProvider.USD.ToString(),
            CurrencyProvider.USD
        },
        new object[]
        {
            CurrencyProvider.CHF.Code,
            Messages.YouSelected + CurrencyProvider.CHF.ToString(),
            CurrencyProvider.CHF
        },
        new object[]
        {
            string.Empty,
            Messages.UnknownCurrency,
            CurrencyProvider.USD
        },
        new object[]
        {
            "    ",
            Messages.UnknownCurrency,
            CurrencyProvider.USD
        },
    };
}
