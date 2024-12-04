using AutoMapper;
using Server.Logic.Currency;

namespace Server.Logic.API.PivatBankAPI;

public class PrivatBankExchangeMappingProfile : Profile
{
    public PrivatBankExchangeMappingProfile()
    {
        CreateMap<PrivatBankExchangeCurrencyDTO, CurrencyExchange>().
            ForMember(
                ce => ce.Currency,
                opt => opt.MapFrom(
                    pbecDTO => CurrencyProvider.Currencies.
                            FirstOrDefault(c => c.Code == pbecDTO.Currency)));
    }
}
