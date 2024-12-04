namespace Server.Logic.Currency;

public class CurrencyExchange
{
    public static Currency BaseCurrency { get; set; } = CurrencyProvider.UAH;
    public Currency Currency { get; set; }
    public float SaleRateNB { get; set; } = 0;
    public float PurchaseRateNB { get; set; } = 0;
    public float SaleRate { get; set; } = 0;
    public float PurchaseRate { get; set; } = 0;

    public string ToString(string bankName, DateTime date)
    {
        return $"Currency: {Currency.Name} {Currency.Code}\n" +
        $"Date: {date.ToShortDateString()}\n" +
        "Buy/Sell in National bank: \n" +
        $"Buy: {PurchaseRateNB.ToString("0.00") + BaseCurrency.Sign} \n" +
        $"Sell: {SaleRateNB.ToString("0.00") + BaseCurrency.Sign}\n" +
        $"Buy/Sell in exchange rate provider({bankName}):\n" +
        $"Buy: {PurchaseRate.ToString("0.00") + BaseCurrency.Sign}\n" +
        $"Sell: {SaleRate.ToString("0.00") + BaseCurrency.Sign}";
    }
}
