using Microsoft.Extensions.Caching.Memory;

namespace Server.Logic.Data;

public class UserDataManager
{
    private readonly IMemoryCache _memoryCache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;

    public UserDataManager(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        _cacheEntryOptions = new()
        {
            SlidingExpiration = TimeSpan.FromDays(1),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7)
        };
    }

    public async Task<UserData> GetAsync(long id)
    {
        ArgumentNullException.ThrowIfNull(id);

        return await _memoryCache.GetOrCreateAsync(id, (id) =>
        {
            return Task.FromResult(new UserData());
        });
    }

    public void Set(long id, UserData userData)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(userData);

        _memoryCache.Set(id, userData, _cacheEntryOptions);
    }
}
