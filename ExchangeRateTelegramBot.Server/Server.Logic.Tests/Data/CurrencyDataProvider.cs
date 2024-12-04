using Server.Logic.Currency;

namespace Server.Logic.Tests.Data;

public class CurrencyDataProvider
{
    public static IEnumerable<object[]> CurrencyConstructorExceeptionData { get; } = new List<object[]>
    {
        new object[]{ null, null, null },
        new object[]{ CurrencyProvider.USD.Name, null, null },
        new object[]{ null, CurrencyProvider.USD.Code, null },
        new object[]{ null, null, CurrencyProvider.USD.Sign },
        new object[]{ CurrencyProvider.USD.Name, CurrencyProvider.USD.Code, null },
        new object[]{ null, CurrencyProvider.USD.Code, CurrencyProvider.USD.Sign },
        new object[]{ CurrencyProvider.USD.Name, null, CurrencyProvider.USD.Sign },
    };

    public static IEnumerable<object[]> CurrencyCodeData { get; } = new List<object[]>
    {
        new object[] { String.Empty, false },
        new object[] { _emptyCurrency.Code , false},
        new object[] { CurrencyProvider.USD.Sign , false},
        new object[] { " ", false},
        new object[] { "12"  , false},

        new object[] { CurrencyProvider.USD.Code.ToLower(), true},
        new object[] { CurrencyProvider.USD.Code , true}
    };

    public static IEnumerable<object[]> CurrencySignData { get; } = new List<object[]>
    {
        new object[] { String.Empty, false },
        new object[] { " ", false },
        new object[] { CurrencyProvider.USD.Code, false },
        new object[] { _emptyCurrency.Sign, false },
        new object[] { CurrencyProvider.PLN.Sign.ToUpper(), false },
        new object[] { CurrencyProvider.USD.Sign, true },
        new object[] { "\u0024", true },
        new object[] { CurrencyProvider.PLN.Sign, true }
    };

    public static IEnumerable<object[]> RegisteredCurrencies { get; } = new List<object[]>
    {
        new object[] { _emptyCurrency, false },
        new object[] { new Logic.Currency.Currency(CurrencyProvider.USD.Name, CurrencyProvider.USD.Code, ""), false},
        new object[] { new Logic.Currency.Currency(CurrencyProvider.USD.Name, "", CurrencyProvider.USD.Sign), false},
        new object[] { new Logic.Currency.Currency("", CurrencyProvider.USD.Code, CurrencyProvider.USD.Sign), false},
        new object[] { new Logic.Currency.Currency(CurrencyProvider.USD.Name, CurrencyProvider.USD.Code, CurrencyProvider.USD.Sign), true},
        new object[] { new Logic.Currency.Currency(CurrencyProvider.EUR.Name, CurrencyProvider.USD.Code, CurrencyProvider.USD.Sign), false},
        new object[] { new Logic.Currency.Currency(CurrencyProvider.USD.Name, CurrencyProvider.EUR.Code, CurrencyProvider.USD.Sign), false},
        new object[] { new Logic.Currency.Currency(CurrencyProvider.USD.Name, CurrencyProvider.USD.Code, CurrencyProvider.EUR.Sign), false},
        new object[] { new Logic.Currency.Currency("Dollar", "USD", "\U00000024"), true},
        new object[] { CurrencyProvider.USD , true},
        new object[] { CurrencyProvider.PLN , true},
    };

    public static IEnumerable<object[]> EqualsCurrencies { get; } = new List<object[]>
    {
        new object[]{ CurrencyProvider.EUR, null, false },
        new object[]{ CurrencyProvider.EUR, new object(), false },
        new object[]{ CurrencyProvider.USD, CurrencyProvider.EUR, false },
        new object[]{ CurrencyProvider.USD, _emptyCurrency, false },
        new object[]{ CurrencyProvider.USD,
            new Logic.Currency.Currency(CurrencyProvider.USD.Name, CurrencyProvider.USD.Code, ""), false},
        new object[]{ CurrencyProvider.USD,
            new Logic.Currency.Currency(CurrencyProvider.EUR.Name, CurrencyProvider.USD.Code, CurrencyProvider.USD.Sign), false},
        new object[]{ CurrencyProvider.USD,
            new Logic.Currency.Currency(CurrencyProvider.USD.Name, CurrencyProvider.EUR.Code, CurrencyProvider.USD.Sign), false},
        new object[]{ CurrencyProvider.USD,
            CurrencyProvider.USD,
            true },
         new object[]{ CurrencyProvider.USD,
            new Logic.Currency.Currency(CurrencyProvider.USD.Name, CurrencyProvider.USD.Code, CurrencyProvider.USD.Sign), true}
    };

    public static IEnumerable<object[]> ToStringCurrencies { get; } = new List<object[]>
    {
        new object[]{ CurrencyProvider.USD,
                        CurrencyProvider.USD.Name + " " +
                        CurrencyProvider.USD.Code + " " +
                        CurrencyProvider.USD.Sign},
        new object[]{ CurrencyProvider.EUR,
                        CurrencyProvider.EUR.Name + " " +
                        CurrencyProvider.EUR.Code + " " +
                        CurrencyProvider.EUR.Sign},
        new object[]{ _emptyCurrency, "  " }
    };

    private static Logic.Currency.Currency _emptyCurrency => new Logic.Currency.Currency("","","");
}
