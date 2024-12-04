using AutoMapper;
using Microsoft.Extensions.Logging;
using Server.Logic.API.BaseAPI;
using Server.Logic.Currency;

namespace Server.Logic.API.PivatBankAPI;

public class PrivatBankExchangeAPI : IBankExchangeAPI
{
    string IBankExchangeAPI.BankName => "Privat bank";

    private readonly PrivatBankExchangeAPISettings _settings;
    private readonly APIWebClient _webClient;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public PrivatBankExchangeAPI(PrivatBankExchangeAPISettings settings, IMapper mapper, APIWebClient webClient, ILogger<PrivatBankExchangeAPI> logger)
    {
        _settings = settings;
        _mapper = mapper;
        _webClient = webClient;
        _logger = logger;
    }

    public async Task<CurrencyExchange> GetCurrencyExchangeAsync(Currency.Currency currency, DateTime date)
    {
        ArgumentNullException.ThrowIfNull(nameof(currency));
        ArgumentNullException.ThrowIfNull(nameof(date));

        var exchanges = await GetAllExchangesAsync(date);

        return _mapper.Map<CurrencyExchange>(
                        exchanges.FirstOrDefault(
                            ce => ce.Currency == currency.Code));
    }

    private async Task<List<PrivatBankExchangeCurrencyDTO>> GetAllExchangesAsync(DateTime date)
    {
        string requestUrl = _settings.Url + "?json&date=" + date.ToString("dd.MM.yyyy");

        try
        {
            var responce = await _webClient.
                    GetAsync<PrivatBankExchangeCurrencyDTO>
                        (requestUrl);

            return responce;
        }
        catch (Exception e)
        {
            _logger.LogError($"Getting currency from API threw exception\n Date: {date.ToString("dd.MM.yyyy")} \nRequestUrl:{requestUrl} \nException: {e.Message}");
            return new List<PrivatBankExchangeCurrencyDTO>();
        }
    }
}
