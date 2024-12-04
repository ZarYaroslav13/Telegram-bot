namespace Server.Logic.Currency;

public static class CurrencyProvider
{
    public static Currency UAH { get; } = new(name: "Hryvnia", code: "UAH", sign: "\U000020B4");
    public static Currency USD { get; } = new(name: "Dollar", code: "USD", sign: "\U00000024");
    public static Currency EUR { get; } = new(name: "Euro", code: "EUR", sign: "\U000020AC");
    public static Currency GBP { get; } = new(name: "Pound sterling", code: "GBP", sign: "\U000000A3");
    public static Currency PLN { get; } = new(name: "Zloty", code: "PLN", sign: "zł");
    public static Currency SEK { get; } = new(name: "Swedish krona", code: "SEK", sign: "kr");
    public static Currency CHF { get; } = new(name: "Swiss franc", code: "CHF", sign: "\U000020A3");
    public static Currency XAU { get; } = new(name: "Gold", code: "XAU", sign: "Au");
    public static Currency CAD { get; } = new(name: "Canadian dollar", code: "CAD", sign: "\U00000024");


    public static List<Currency> Currencies { get; } = new()
    {
        UAH, USD, EUR, GBP, PLN, SEK, CHF, XAU, CAD
    };

    public static bool IsRegisteredCurrencyCode(string code)
    {
        return Currencies.Any(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
    }

    public static bool IsRegisteredCurrencySign(string sign)
    {
        return Currencies.Any(c => c.Sign == sign);
    }

    public static bool IsRegisteredCurrency(Currency currency)
    {
        return Currencies.Any(c => c.Equals(currency));
    }
}
