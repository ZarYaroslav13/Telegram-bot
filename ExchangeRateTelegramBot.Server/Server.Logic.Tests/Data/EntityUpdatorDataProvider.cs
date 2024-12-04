using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Server.Logic.Tests.Data;

public class EntityUpdatorDataProvider
{
    public static IEnumerable<object[]> EntityUpdatorConstructExceptionData { get; } = new List<object[]>
    {
        new object[]{ A.Dummy<ITelegramBotClient>(), null },
        new object[]{ null, new UserData() },
        new object[]{ null, null },
    };

    public static IEnumerable<object[]> EntityUpdatorUpdateExceptionsData { get; } = new List<object[]>
    {
        new object[]{ A.Dummy<Update>(), A.Dummy<UserData>(), null},
        new object[]{ A.Dummy<Update>(), null, A.Dummy<CancellationToken>()},
        new object[]{ null, A.Dummy<UserData>(), A.Dummy<CancellationToken>() },
        new object[]{ A.Dummy<Update>(), null, null},
        new object[]{ null, A.Dummy<UserData>(), null},
        new object[]{ null, null, A.Dummy<CancellationToken>()},
        new object[]{ null, null, null}
    };
}
