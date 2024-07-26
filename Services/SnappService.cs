using System.Text.Json;
using Snapp.Cli.Helpers;

namespace Snapp.Cli.Services;

public class SnappApiResponse<T> {
  public int Count;
  public int Page;
  public int Limit;
  public string? SortDirection;
  public T? Results { get; set; }
}

public record Snapp(string Id, string Title, string Description);

public interface ISnappService {
  public Task<SnappApiResponse<List<Snapp>>?> GetSnaps();
}

public class SnappService : ISnappService {
  private readonly HttpClient _httpClient;
  private readonly AppSettingsHelper _settingsHelper;

  public SnappService(HttpClient httpClient, AppSettingsHelper settingsHelper) {
    _httpClient = httpClient;
    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {settingsHelper.ApiKey}");
    _settingsHelper = settingsHelper;
  }

  public async Task<SnappApiResponse<List<Snapp>>?> GetSnaps() {
    var url = Flurl.Url.Combine(_settingsHelper.ServerUrl, "/snapps");
    var response = await _httpClient.GetAsync(url);
    var content = await response.Content.ReadAsStringAsync();

    return JsonSerializer.Deserialize<SnappApiResponse<List<Snapp>>>(content);
  }
}
