using Server.Logic.Menu.Buttons;
using Telegram.Bot.Types.ReplyMarkups;

namespace Server.Logic.Menu;

public static class BotMenuProvider
{
    public static ReplyKeyboardMarkup MainMenu = new(MainMenuButtonsProvider.RowsButtons)
    {
        ResizeKeyboard = true
    };

    public static ReplyKeyboardMarkup DateMenu = new(DateMenuButtonsProvider.RowsButtons)
    {
        ResizeKeyboard = true
    };

    public static InlineKeyboardMarkup CurrencyMenu = new(CurrencyMenuButtonsProvider.RowsButtons);
}


