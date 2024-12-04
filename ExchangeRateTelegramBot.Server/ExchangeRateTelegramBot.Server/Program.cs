using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.HostBuilder;
using Server.Logic;
using Server.Logic.API.BaseAPI;
using Server.Logic.Data;
using Telegram.Bot;

namespace Server;

public class Program
{
    public static IServiceProvider ServiceProvider { get; private set; } = default!;

    private static IHost _host;

    public static void Main(string[] args)
    {
        _host = CreateHostBuilder(args).
            Build();

        ServiceProvider = _host.Services;

        var botClient = new TelegramBotClient($"{ServiceProvider.GetRequiredService<BotServicesSetting>().API}");
        var bankAPI = ServiceProvider.GetRequiredService<IBankExchangeAPI>();
        var userDataManager = ServiceProvider.GetRequiredService<UserDataManager>();

        new BotServer(botClient, bankAPI, userDataManager).Work();
    }

    private static IHostBuilder CreateHostBuilder(string[] args = null)
    {
        return Host.CreateDefaultBuilder(args)
           .Configure()
           .AddServices();
    }
}
