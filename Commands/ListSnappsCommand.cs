using Microsoft.Extensions.Options;
using Snapp.Cli.Helpers;
using Snapp.Cli.Services;
using Spectre.Console.Cli;

namespace Snapp.Cli.Commands;

public class ListSnappsCommand : AsyncCommand<ListSnappsCommand.Settings> {
  private readonly SnappService _snappService;

  public ListSnappsCommand(SnappService snappService) {
    _snappService = snappService;
  }

  public sealed class Settings : CommandSettings { }
  // public sealed class Settings : CommandSettings {
  //   public Settings(IOptions<AppSettings> settings) : base(settings) { }
  // }

  public override async Task<int> ExecuteAsync(CommandContext context, Settings settings) {
    Console.WriteLine($"Listing shit");
    return 1;
  }
}
