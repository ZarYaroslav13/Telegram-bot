using Server.Logic.Currency;
using Telegram.Bot.Types.ReplyMarkups;

namespace Server.Logic.Menu.Buttons;

public class CurrencyMenuButtonsProvider
{
    public static readonly InlineKeyboardButton USD = new(CurrencyProvider.USD.ToString()) { CallbackData = CurrencyProvider.USD.Code };
    public static readonly InlineKeyboardButton EUR = new(CurrencyProvider.EUR.ToString()) { CallbackData = CurrencyProvider.EUR.Code };
    public static readonly InlineKeyboardButton GBP = new(CurrencyProvider.GBP.ToString()) { CallbackData = CurrencyProvider.GBP.Code };
    public static readonly InlineKeyboardButton PLZ = new(CurrencyProvider.PLN.ToString()) { CallbackData = CurrencyProvider.PLN.Code };
    public static readonly InlineKeyboardButton SEK = new(CurrencyProvider.SEK.ToString()) { CallbackData = CurrencyProvider.SEK.Code };
    public static readonly InlineKeyboardButton CHF = new(CurrencyProvider.CHF.ToString()) { CallbackData = CurrencyProvider.CHF.Code };
    public static readonly InlineKeyboardButton XAU = new(CurrencyProvider.XAU.ToString()) { CallbackData = CurrencyProvider.XAU.Code };
    public static readonly InlineKeyboardButton CAD = new(CurrencyProvider.CAD.ToString()) { CallbackData = CurrencyProvider.CAD.Code };

    public static List<InlineKeyboardButton[]> RowsButtons = new()
    {
        new[]{ USD, EUR },
        new[]{ GBP, PLZ },
        new[]{ SEK, CHF },
        new[]{ XAU, CAD }
    };
}

