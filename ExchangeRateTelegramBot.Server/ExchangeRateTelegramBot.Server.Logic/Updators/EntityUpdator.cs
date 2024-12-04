using Telegram.Bot;
using Telegram.Bot.Types;

namespace Server.Logic.Updators;

public abstract class EntityUpdator
{
    protected readonly ITelegramBotClient _botClient;
    protected UserData _userData = new();

    public EntityUpdator(ITelegramBotClient botClient)
    {
        _botClient = botClient ?? throw new ArgumentNullException(nameof(botClient));
    }

    public abstract long GetChatId(Update update);

    public async Task Update(Update update, UserData userData, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(update);
        ArgumentNullException.ThrowIfNull(cancellationToken);

        _userData = userData ?? throw new ArgumentNullException(nameof(userData));

        await WorkAsync(update, cancellationToken);
    }

    protected abstract Task WorkAsync(Update update, CancellationToken cancellationToken);

    protected async Task SendMessageAsync(long chatId, CancellationToken cancellationToken)
    {
        try
        {
            _userData.MessageSent = await _botClient.SendTextMessageAsync(
                   chatId: chatId,
                   text: _userData.Response,
                   replyMarkup: _userData.Markup,
                   cancellationToken: cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            _userData.MessageSent.Text = "Exception!!: " + e.Message;
        }
    }
}
