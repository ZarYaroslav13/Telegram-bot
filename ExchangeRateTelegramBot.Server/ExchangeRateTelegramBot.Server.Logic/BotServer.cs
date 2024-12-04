using Server.Logic.API.BaseAPI;
using Server.Logic.Data;
using Server.Logic.Updator;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace Server.Logic;

public class BotServer
{
    private readonly ITelegramBotClient _botClient;

    private BotUpdator _botUpdator;
    private readonly IBankExchangeAPI _bankAPI;
    private readonly UserDataManager _dataManager;

    public BotServer(ITelegramBotClient client, IBankExchangeAPI bankAPI, UserDataManager dataManager)
    {
        _botClient = client ?? throw new ArgumentNullException(nameof(client));
        _bankAPI = bankAPI ?? throw new ArgumentNullException(nameof(bankAPI));
        _dataManager = dataManager ?? throw new ArgumentNullException(nameof(dataManager));

        _botUpdator = new(client, _bankAPI, _dataManager);
    }

    public void Work()
    {
        _botClient.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync);

        Console.ReadLine();
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        await _botUpdator.Update(update, cancellationToken);
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}

