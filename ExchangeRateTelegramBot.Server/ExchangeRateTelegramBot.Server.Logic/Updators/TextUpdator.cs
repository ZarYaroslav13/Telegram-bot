using Server.Logic.API.BaseAPI;
using Server.Logic.Menu;
using Server.Logic.Menu.Buttons;
using Server.Logic.Resources;
using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Server.Logic.Updators;

public class TextUpdator : EntityUpdator
{
    private IBankExchangeAPI _bankAPI;

    public TextUpdator(ITelegramBotClient botClient, IBankExchangeAPI bankAPI) : base(botClient)
    {
        _bankAPI = bankAPI ?? throw new ArgumentNullException(nameof(bankAPI));
    }

    public override long GetChatId(Update update)
    {
        return update.Message.Chat.Id;
    }

    protected override async Task WorkAsync(Update update, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(update);

        if (update.Message is not { } message)
            return;

        if (message.Text is not { } messageText)
            return;

        long chatId = GetChatId(update);

        Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

        switch (messageText)
        {
            #region Main buttons cases
            case var _ when messageText == MainMenuButtonsProvider.ShowExchangeRate.Text:
                {
                    await ShowExchangeRateChoosenCurrencyAsync();
                    break;
                }
            case var _ when messageText == MainMenuButtonsProvider.ChangeCurrency.Text:
                {
                    ChooseCurrency();
                    break;
                }
            case var _ when messageText == MainMenuButtonsProvider.ShowCurrencyAndDate.Text:
                {
                    ShowCurrencyAndDate();
                    break;
                }
            case var _ when messageText == MainMenuButtonsProvider.ChangeDate.Text:
                {
                    ChangeDate();
                    break;
                }
            #endregion
            #region Date buttons cases
            case var _ when messageText == DateMenuButtonsProvider.DayBefore.Text:
                {
                    SetChoosenDateDayBefore(_userData.ChoosenDate);
                    break;
                }
            case var _ when messageText == DateMenuButtonsProvider.DayAfter.Text:
                {
                    SetChoosenDateDayAfter(_userData.ChoosenDate);
                    break;
                }
            case var _ when messageText == DateMenuButtonsProvider.Yesterday.Text:
                {
                    SetYestardayAsChoosenDate();
                    break;
                }
            case var _ when messageText == DateMenuButtonsProvider.Confirm.Text:
                {
                    LeaveCurrentDate();
                    break;
                }
            #endregion
            default:
                {
                    if (IsDateEntered())
                    {
                        DateEntered(messageText, chatId);
                        break;
                    }

                    UnknownMessageReceived();
                    break;
                }

        }

        await SendMessageAsync(chatId, cancellationToken);
    }

    private async Task ShowExchangeRateChoosenCurrencyAsync()
    {
        var currencyExchange = await _bankAPI.GetCurrencyExchangeAsync(_userData.ChoosenCurrency, _userData.ChoosenDate);

        _userData.Markup = BotMenuProvider.MainMenu;

        if (currencyExchange == null)
        {
            _userData.Response = $"Sorry, my exchange rate provider({_bankAPI.BankName}) have not information about this currency on choosen date";

            return;
        }

        _userData.Response = currencyExchange.ToString(_bankAPI.BankName, _userData.ChoosenDate);
    }

    private void ChooseCurrency()
    {
        _userData.Response = Messages.ChooseCurrency;
        _userData.Markup = BotMenuProvider.CurrencyMenu;
    }

    #region Date actions

    private void ChangeDate()
    {
        _userData.Response = Messages.EnterDateInPattern + $"{CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern}";
        _userData.Markup = BotMenuProvider.DateMenu;
    }

    private void SetYestardayAsChoosenDate()
    {
        SetChoosenDateDayBefore(DateTime.Now);
        _userData.Markup = BotMenuProvider.MainMenu;
    }

    private void SetChoosenDateDayAfter(DateTime dateTime)
    {
        _userData.Markup = BotMenuProvider.DateMenu;

        if (dateTime.Date == DateTime.Now.Date)
        {
            _userData.Response = Messages.CannotChangeDate +
                                 _userData.ChoosenDate.ToShortDateString();
            return;
        }

        _userData.ChoosenDate = dateTime.AddDays(1);

        _userData.Response = Messages.ChoosenDate + ": " + _userData.ChoosenDate.ToShortDateString();
    }

    private void SetChoosenDateDayBefore(DateTime dateTime)
    {
        _userData.Markup = BotMenuProvider.DateMenu;

        if (dateTime.Date == DateTime.MinValue.AddDays(1).Date)
        {
            _userData.Response = Messages.CannotChangeDate +
                                 _userData.ChoosenDate.ToShortDateString();
            return;
        }

        _userData.ChoosenDate = dateTime.AddDays(-1);

        _userData.Response = Messages.ChoosenDate + ": " + _userData.ChoosenDate.ToShortDateString();
    }

    private void ShowCurrencyAndDate()
    {
        _userData.Response = Messages.CurrentCurrencyAndDate +
                        $"Currency: {_userData.ChoosenCurrency}\n" +
                        $"Date: {_userData.ChoosenDate.ToShortDateString()}\n";
    }

    private void LeaveCurrentDate()
    {
        _userData.Response = Messages.DateNotchanged + $"{_userData.ChoosenDate.ToShortDateString()}";
        _userData.Markup = BotMenuProvider.MainMenu;
    }

    private void DateEntered(string messageText, long chatId)
    {
        if (DateTime.TryParse(messageText, out var date))
        {
            if (date > DateTime.Today)
            {
                DateFromFuture();

                return;
            }

            _userData.ChoosenDate = date;

            _userData.Response = Messages.ChoosenDate + ": " + _userData.ChoosenDate.ToShortDateString();
            _userData.Markup = BotMenuProvider.MainMenu;

            Console.WriteLine($"Choosen date {_userData.ChoosenDate} in chat {chatId}");
            return;
        }

        _userData.Response = Messages.InvalidFormatOfDate +
              $"{CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern}";
        _userData.Markup = BotMenuProvider.DateMenu;
    }

    private void DateFromFuture()
    {
        _userData.Response = Messages.EnteredFutureData;
        _userData.Markup = BotMenuProvider.DateMenu;
    }

    #endregion

    private void UnknownMessageReceived()
    {
        _userData.Response = Messages.UnknownMessageReceived;
    }

    private bool IsDateEntered()
    {
        return _userData.MessageSent.Text == Messages.EnterDateByFormPattern +
            $"{CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern}" ||
              _userData.MessageSent.Text == Messages.InvalidFormatOfDate +
              $"{CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern}";
    }
}
