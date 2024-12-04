using AutoMapper;
using FakeItEasy;
using Server.Logic.Currency;
using Server.Logic.Updator;
using Server.Logic.Updators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Server.Logic.Tests.Data;

public class BotUpdatorDataProvider
{
    public static IEnumerable<object[]> ConstructorExcptionData { get; } = new List<object[]>
    {
        new object[]
        {
            A.Dummy<ITelegramBotClient>(), A.Dummy<IMapper>(), null
        },
        new object[]
        {
            A.Dummy<ITelegramBotClient>(), null, A.Fake<CurrencyExchangeReader>(x => x.WithArgumentsForConstructor(new object[]{ "url"}))
        },
        new object[]
        {
            A.Dummy<ITelegramBotClient>(), null, null
        },
        new object[]
        {
            null, A.Dummy<IMapper>(), null
        },
        new object[]
        {
            null, A.Dummy<IMapper>(), A.Fake<CurrencyExchangeReader>(x => x.WithArgumentsForConstructor(new object[]{ "url"}))
        },
        new object[]
        {
            null, null, A.Fake < CurrencyExchangeReader >(x => x.WithArgumentsForConstructor(new object[] { "url" }))
        },
        new object[]
        {
            null, null, null
        },
    };

    public static IEnumerable<object[]> UpdateUpdatorData { get; } = new List<object[]>
    {
        new object[]
        {
            new Update(){ Message = new(){ Chat = A.Dummy<Chat>()} }, 
            Telegram.Bot.Types.Enums.UpdateType.Message, 
            typeof(TextUpdator)
        },
        new object[]
        {
            new Update(){ CallbackQuery = A.Dummy<CallbackQuery>()},
            Telegram.Bot.Types.Enums.UpdateType.CallbackQuery,
            typeof(CallbackQueryUpdator)
        },
        new object[]
        {
            new Update(){ Poll = A.Dummy<Poll>() },
            Telegram.Bot.Types.Enums.UpdateType.Poll,
            typeof(TextUpdator)
        }
    };
}
