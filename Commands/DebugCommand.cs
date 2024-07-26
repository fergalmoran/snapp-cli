using System.ComponentModel;
using Spectre.Console.Cli;

namespace Snapp.Cli.Commands;

public class DebugCommand : AsyncCommand<DebugCommand.Settings> {
  public class Settings : CommandSettings {
    [CommandArgument(1, "<ECHO-TEXT>")]
    [Description("Gimme some text to echo.")]
    public string? EchoText { get; set; }
  }

  public override async Task<int> ExecuteAsync(CommandContext context, Settings settings) {
    Console.WriteLine($"Echoing: {settings.EchoText}");
    return await Task.FromResult(3);
  }
}
