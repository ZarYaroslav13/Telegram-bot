using Server.Logic.Currency;
using Server.Logic.Menu;
using Server.Logic.Resources;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Server.Logic.Updators;

public class CallbackQueryUpdator : EntityUpdator
{
    public CallbackQueryUpdator(ITelegramBotClient botClient) : base(botClient)
    {
    }

    public override long GetChatId(Update update)
    {
        return update.CallbackQuery.Message.Chat.Id;
    }

    protected override async Task WorkAsync(Update update, CancellationToken cancellationToken)
    {
        if (update.CallbackQuery is not { } callbackQuery)
            return;

        if (callbackQuery.Data is not { } callbackQueryText)
            return;

        string consoleMessage = $"Unindetified currency in chat {callbackQuery.Message.Chat.Id}.";
        _userData.Response = Messages.UnknownCurrency;

        if (CurrencyProvider.IsRegisteredCurrencyCode(callbackQueryText))
        {
            _userData.ChoosenCurrency = CurrencyProvider.Currencies.First(c => c.Code == callbackQueryText);

            await _botClient.DeleteMessageAsync(GetChatId(update), callbackQuery.Message.MessageId);

            _userData.Response = Messages.YouSelected + _userData.ChoosenCurrency.ToString();
            _userData.Markup = BotMenuProvider.MainMenu;
            consoleMessage = $"Choosen a '{_userData.ChoosenCurrency.Code}' currency in chat {callbackQuery.Message.Chat.Id}.";
        }

        Console.WriteLine(consoleMessage);
        await SendMessageAsync(GetChatId(update), cancellationToken);
    }
}
