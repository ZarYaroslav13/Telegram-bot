namespace Server.Logic.API.PivatBankAPI;

public class PrivatBankExchangeCurrencyDTO
{
    public string BaseCurrency { get; set; } = "";
    public string Currency { get; set; } = "";
    public float SaleRateNB { get; set; } = 0;
    public float PurchaseRateNB { get; set; } = 0;
    public float SaleRate { get; set; } = 0;
    public float PurchaseRate { get; set; } = 0;
}
