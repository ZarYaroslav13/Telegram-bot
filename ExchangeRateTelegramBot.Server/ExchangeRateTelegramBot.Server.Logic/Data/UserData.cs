using Server.Logic.Currency;
using Server.Logic.Menu;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Server.Logic;

public class UserData
{
    public Currency.Currency ChoosenCurrency { get; set; } = CurrencyProvider.USD;

    public DateTime ChoosenDate { get; set; } = DateTime.Today;

    public Message MessageSent { get; set; } = new();

    public IReplyMarkup Markup { get; set; } = BotMenuProvider.MainMenu;

    public string Response { get; set; } = "";
}
