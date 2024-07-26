using Microsoft.Extensions.Configuration;

namespace Snapp.Cli.Helpers;

public class AppSettings {
  public IConfiguration Config { get; set; }

  public AppSettings() {
    Config = AppSettingsHelper.InitialiseSettings();
  }

  public string? ServerUrl { get => Config.GetSection("ServerUrl").Value; }
  public string? ApiKey { get => Config.GetSection("ApiKey").Value; }
}

public class AppSettingsHelper {
  // private readonly AppSettings _config;
  //
  // public string? ApiKey { get => _config?.ApiKey; }
  // public string? ServerUrl { get => _config?.ServerUrl; }

  private static string SettingsFile {
    get => Path.Combine(
      Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
      "snapp-cli/config.json");
  }

  public static IConfiguration InitialiseSettings() {
    var config = new ConfigurationBuilder()
      .SetBasePath(AppContext.BaseDirectory)
      .AddJsonFile(SettingsFile, false, true)
      .Build();

    if (config is null) {
      throw new NullReferenceException("Missing settings file");
    }

    return config;
  }
}
