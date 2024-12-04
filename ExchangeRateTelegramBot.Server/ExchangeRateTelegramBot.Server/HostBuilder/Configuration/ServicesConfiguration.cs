using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Server.HostBuilder.Configuration;

internal class ServicesConfiguration
{
    public static void Configure(HostBuilderContext context, IServiceCollection services)
    {
        services.AddSingleton(services =>
        {
            var configuration = context.Configuration;

            return configuration.GetSection(BotServicesSetting.SettingsKey).Get<BotServicesSetting>();
        });
    }
}
