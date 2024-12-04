using Server.Logic.Currency;

namespace Server.Logic.API.BaseAPI;

public interface IBankExchangeAPI
{
    public string BankName { get; }

    public Task<CurrencyExchange> GetCurrencyExchangeAsync(Currency.Currency currency, DateTime date);
}
