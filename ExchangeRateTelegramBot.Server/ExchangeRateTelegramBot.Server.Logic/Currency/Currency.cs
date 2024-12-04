namespace Server.Logic.Currency;

public class Currency
{
    public string Name { get; } = "";
    public string Code { get; } = "";
    public string Sign { get; } = "";

    public Currency(string name, string code, string sign)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Sign = sign ?? throw new ArgumentNullException(nameof(sign));
    }

    public Currency(Currency currency)
    {
        ArgumentNullException.ThrowIfNull(currency);

        Name = currency.Name ?? throw new ArgumentNullException(nameof(Name));
        Code = currency.Code ?? throw new ArgumentNullException(nameof(Code));
        Sign = currency.Sign ?? throw new ArgumentNullException(nameof(Sign));
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Currency))
        {
            return false;
        }

        var currency = (Currency)obj;

        return Name.Equals(currency.Name, StringComparison.OrdinalIgnoreCase) &&
                Code.Equals(currency.Code, StringComparison.OrdinalIgnoreCase) &&
                Sign.Equals(currency.Sign, StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString()
    {
        return Name + " " + Code + " " + Sign;
    }
}
