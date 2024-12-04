using Server.Logic.Resources;
using Telegram.Bot.Types.ReplyMarkups;

namespace Server.Logic.Menu.Buttons;

public class DateMenuButtonsProvider
{
    public static readonly KeyboardButton DayBefore = new(Messages.DayBeforeButton);
    public static readonly KeyboardButton DayAfter = new(Messages.DayAfterButton);
    public static readonly KeyboardButton Yesterday = new(Messages.YestardayDateButton);
    public static readonly KeyboardButton Confirm = new(Messages.Confirm);

    public static List<KeyboardButton> Buttons = new()
    {
        DayBefore,
        DayAfter,
        Yesterday,
        Confirm
    };

    public static List<KeyboardButton[]> RowsButtons = new()
    {
        new[]{ DayBefore, DayAfter},
        new[]{ Yesterday },
        new[]{ Confirm }
    };
}