namespace CrypApi.Services;

public class CrypApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CrypApiService> _logger;   
    public CrypApiService(HttpClient httpClient, ILogger<CrypApiService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<string> GetCrptoDataAsync()
    {
        var response = await _httpClient.GetAsync("https://api.coincap.io/v2/assets");

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        _logger.LogInformation("Getting all cryto data processing");
        return content;
    }
}
