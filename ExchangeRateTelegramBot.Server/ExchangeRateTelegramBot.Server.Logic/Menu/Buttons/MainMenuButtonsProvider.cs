using Server.Logic.Resources;
using Telegram.Bot.Types.ReplyMarkups;

namespace Server.Logic.Menu.Buttons;

public static class MainMenuButtonsProvider
{
    public static readonly KeyboardButton ShowExchangeRate = new(Messages.ShowExchangeRateButton);
    public static readonly KeyboardButton ChangeCurrency = new(Messages.ChangeCurrencyButton);
    public static readonly KeyboardButton ChangeDate = new(Messages.ChangeDateButton);
    public static readonly KeyboardButton ShowCurrencyAndDate = new(Messages.ShowCurrencyAndDateButton);

    public static List<KeyboardButton> Buttons = new()
    {
        ShowExchangeRate,
        ChangeCurrency,
        ChangeDate,
        ShowCurrencyAndDate
    };

    public static List<KeyboardButton[]> RowsButtons = new()
    {
        new[]{ ShowExchangeRate },
        new[]{ ChangeCurrency },
        new[]{ ChangeDate },
        new[]{ ShowCurrencyAndDate },
    };
}