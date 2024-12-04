using AutoMapper;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Server.Logic.Tests.Data;

public class BotServerDataProvider
{
    public static IEnumerable<object[]> ConstructorExceptionsData { get; } = new List<object[]>
    {
        new object[]
        {
            A.Dummy<ITelegramBotClient>(), A.Dummy<IMapper>(), null
        },
        new object[]
        {
            A.Dummy<ITelegramBotClient>(), null, "somestring"
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
            null, A.Dummy<IMapper>(), "somestring"
        },
        new object[]
        {
            null, null, "somestring"
        },
        new object[]
        {
            null, null, null
        },
        new object[]
        {
            A.Dummy<ITelegramBotClient>(), A.Dummy<IMapper>(), String.Empty
        },
        new object[]
        {
            A.Dummy<ITelegramBotClient>(), A.Dummy<IMapper>(), "      "
        },
    };
}
