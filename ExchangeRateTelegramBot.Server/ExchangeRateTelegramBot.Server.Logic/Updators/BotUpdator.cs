using Server.Logic.API.BaseAPI;
using Server.Logic.Data;
using Server.Logic.Updators;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Server.Logic.Updator;

public class BotUpdator
{
    public Dictionary<Telegram.Bot.Types.Enums.UpdateType, EntityUpdator> _updators;

    private readonly ITelegramBotClient _botClient;
    private readonly IBankExchangeAPI _bankExchangeAPI;
    private readonly UserDataManager _dataManager;

    public BotUpdator(ITelegramBotClient botClient, IBankExchangeAPI bankExchangeAPI, UserDataManager dataManager)
    {
        _botClient = botClient ?? throw new ArgumentNullException(nameof(botClient));
        _bankExchangeAPI = bankExchangeAPI ?? throw new ArgumentNullException(nameof(bankExchangeAPI));
        _dataManager = dataManager ?? throw new ArgumentNullException(nameof(dataManager));

        _updators = new();

        Initialization();
    }

    public async Task Update(Update update, CancellationToken cancellationToken)
    {
        if (update == null)
            return;

        EntityUpdator updator = GetUpdator(update.Type);

        long chatId = updator.GetChatId(update);

        UserData userData = await _dataManager.GetAsync(chatId);

        await updator.Update(update, userData, cancellationToken);

        _dataManager.Set(chatId, userData);
    }

    public EntityUpdator GetUpdator(Telegram.Bot.Types.Enums.UpdateType updateType)
    {
        if (_updators.Keys.Any(u => u == updateType))
        {
            return _updators[updateType];
        }

        return _updators[Telegram.Bot.Types.Enums.UpdateType.Message];
    }

    private void Initialization()
    {
        _updators.Add(Telegram.Bot.Types.Enums.UpdateType.Message, new TextUpdator(_botClient, _bankExchangeAPI));
        _updators.Add(Telegram.Bot.Types.Enums.UpdateType.CallbackQuery, new CallbackQueryUpdator(_botClient));
    }
}
