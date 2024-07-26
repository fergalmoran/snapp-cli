using System.ComponentModel;
using Microsoft.Extensions.Options;
using Snapp.Cli.Helpers;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Snapp.Cli.Commands;

public class DefaultCommandSettings : CommandSettings {
  public readonly AppSettings Config;

  protected DefaultCommandSettings(AppSettings settings) {
    Config = settings;
  }

  [Description("Path to search. Defaults to current directory.")]
  [CommandArgument(0, "[-s|--server]")]
  public string? ServerUrl { get; set; }

  [Description("API Key for server.")]
  [CommandArgument(0, "[-k|--api-key]")]
  public string? ApiKey { get; set; }

  public static string AskServerIfMissing(string? current) =>
    !string.IsNullOrWhiteSpace(current)
      ? current
      : AnsiConsole.Prompt(
        new TextPrompt<string>("Enter your server URL:")
          .PromptStyle("green")
          .Validate(serverAddress => !string.IsNullOrWhiteSpace(serverAddress)
            ? ValidationResult.Success()
            : ValidationResult.Error("Server URL cannot be empty.")));

  public static string AskApiKeyIfMissing(string? current) =>
    !string.IsNullOrWhiteSpace(current)
      ? current
      : AnsiConsole.Prompt(
        new TextPrompt<string>("Enter your API Key:")
          .PromptStyle("green")
          .Validate(serverAddress => !string.IsNullOrWhiteSpace(serverAddress)
            ? ValidationResult.Success()
            : ValidationResult.Error("API Key cannot be empty.")));
}
