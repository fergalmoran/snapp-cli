using Microsoft.Extensions.Options;
using Snapp.Cli.Helpers;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Snapp.Cli.Commands;

public class AddSnappsCommand : Command<AddSnappsCommand.Settings> {
  public sealed class Settings : DefaultCommandSettings {
    public Settings(IOptions<AppSettings> settings) : base(settings) { }
  }

  public override int Execute(CommandContext context, Settings settings) {
    if (string.IsNullOrEmpty(settings.ServerUrl)) {
      settings.ServerUrl = AnsiConsole.Prompt(
        new TextPrompt<string>("Snapp server address?")
          .PromptStyle("green"));
    }

    if (string.IsNullOrEmpty(settings.ApiKey)) {
      settings.ApiKey = AnsiConsole.Prompt(
        new TextPrompt<string>("Snapp server API Key?")
          .PromptStyle("green"));
    }

    Console.WriteLine(
      $"Executing list snapps command with server address: {settings.ServerUrl} and API key: {settings.ApiKey}");
    // var snaps = await service.GetSnaps();
    // foreach (var snap in snaps.Results) {
    //   Console.WriteLine($"{snap.Id}: {snap.Title}");
    // }
    return -1;
  }
}
