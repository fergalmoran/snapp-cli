using System.Text.Json;
using System.Text.Json.Serialization;
using Snapp.Cli.Helpers;

namespace Snapp.Cli.Services;

public class SnappApiResponse<T> {
  public int Count;
  public int Page;
  public int Limit;
  public string? SortDirection;
  public T? Data { get; set; }
}

public record Snapp(
  string Id,
  [property: JsonPropertyName("shortcode")]
  string Slug,
  [property: JsonPropertyName("created")]
  DateTime CreatedAt,
  [property: JsonPropertyName("original_url")]
  string Url,
  [property: JsonPropertyName("used")]
  int VisitCount);

public interface ISnappService {
  public Task<SnappApiResponse<List<Snapp>>?> GetSnaps();
}

public class SnappService : ISnappService {
  private readonly HttpClient _httpClient;
  private readonly AppSettings _settings;

  private readonly JsonSerializerOptions _jsonOptions = new() {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
  };

  public SnappService(HttpClient httpClient, AppSettings settings) {
    _httpClient = httpClient;
    _settings = settings;
    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_settings.ApiKey}");
    _httpClient.BaseAddress = new Uri(Flurl.Url.Combine(_settings.ServerUrl, "api", "/"));
  }

  public async Task<SnappApiResponse<List<Snapp>>?> GetSnaps() {
    var url = "snapps";
    var response = await _httpClient.GetAsync(url);
    var content = await response.Content.ReadAsStringAsync();

    return JsonSerializer.Deserialize<SnappApiResponse<List<Snapp>>>(content, _jsonOptions);
  }
}
