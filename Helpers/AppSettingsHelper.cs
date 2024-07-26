using Microsoft.Extensions.Configuration;

namespace Snapp.Cli.Helpers;

public class AppSettings {
  public string? ApiKey { get; set; }
  public string? ServerUrl { get; set; }
}

public class AppSettingsHelper {
  private readonly AppSettings _config;

  public string? ApiKey { get => _config.ApiKey; }
  public string? ServerUrl { get => _config.ServerUrl; }

  private static string SettingsFile {
    get => Path.Combine(
      Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
      "snapp-cli/config.json");
  }

  public AppSettingsHelper() {
    _config = _initialiseSettings();
  }

  private AppSettings _initialiseSettings() {
    var config = new ConfigurationBuilder()
      .SetBasePath(AppContext.BaseDirectory)
      .AddJsonFile(SettingsFile, false, true)
      .Build();

    if (config is null) {
      throw new NullReferenceException("Missing settings file");
    }

    var c = config.Get<AppSettings>();
    if (c is null) {
      throw new NullReferenceException("Missing settings");
    }

    return c;
  }
}
