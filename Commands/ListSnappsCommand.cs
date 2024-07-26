using System.Diagnostics;
using Microsoft.Extensions.Options;
using Snapp.Cli.Helpers;
using Snapp.Cli.Services;
using Spectre.Console.Cli;

namespace Snapp.Cli.Commands;

public class ListSnappsCommand(SnappService snappService) :
  AsyncCommand<ListSnappsCommand.Settings> {
  public sealed class Settings(AppSettings settings) :
    DefaultCommandSettings(settings);

  public override async Task<int> ExecuteAsync(CommandContext context, Settings settings) {
    var server = DefaultCommandSettings.AskServerIfMissing(
      settings.ServerUrl ??
      settings.Config.ServerUrl);
    var apiKey = DefaultCommandSettings.AskApiKeyIfMissing(
      settings.ApiKey ??
      settings.Config.ApiKey);

    Log.Debug(
      $"Executing list snapps command with server address: {server} and API key: {apiKey}");
    var snaps = await snappService.GetSnaps();
    if (snaps?.Data == null) {
      Log.Debug("No snaps found");
      return -1;
    }

    foreach (var snap in snaps.Data) {
      Log.Debug($"{snap.Url}: {snap.Slug} ({snap.VisitCount})");
    }

    return snaps.Data.Count;
  }
}
