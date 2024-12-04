using Newtonsoft.Json;

namespace Server.Logic.API;

public class APIWebClient
{
    private readonly HttpClient _httpClient;

    public APIWebClient()
    {
        _httpClient = new();
    }

    public async Task<string> GetAsync(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentNullException(nameof(url));
        }

        var responce = await _httpClient.GetAsync(url);
        responce.EnsureSuccessStatusCode();
        return await responce.Content.ReadAsStringAsync();
    }

    public async Task<List<T>> GetAsync<T>(string url) where T : class
    {
        var responce = await GetAsync(url);

        int from = responce.IndexOf('[');
        int to = responce.IndexOf(']') + 1;

        responce = responce.Substring(from, to - from);
        return JsonConvert.DeserializeObject<List<T>>(responce);
    }
}
