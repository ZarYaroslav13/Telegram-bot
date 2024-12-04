using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Logic.API;
using Server.Logic.API.BaseAPI;
using Server.Logic.API.PivatBankAPI;
using Server.Logic.Data;

namespace Server.HostBuilder;

public static class AddServicesConfigurationHostBuilderExtensions
{
    public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices((context, services) =>
        {
            services.AddSingleton(services =>
            {
                var configuration = context.Configuration;

                return configuration.GetSection(BotServicesSetting.SettingsKey).Get<BotServicesSetting>();
            });

            services.AddScoped<APIWebClient>();

            services.AddAutoMapper(typeof(PrivatBankExchangeMappingProfile));
            services.AddScoped<PrivatBankExchangeAPISettings>();
            services.AddScoped<IBankExchangeAPI, PrivatBankExchangeAPI>();

            services.AddMemoryCache();
            services.AddSingleton<UserDataManager>();
        });

        return hostBuilder;
    }
}
