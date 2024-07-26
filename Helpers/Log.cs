using Spectre.Console;

namespace Snapp.Cli.Helpers;

public static class Log {
  private static readonly object _lock = new();

  public static void Debug(string message) {
#if DEBUG
    lock (_lock) {
      Console.ForegroundColor = ConsoleColor.DarkGray;
      Console.WriteLine($"{message}");
      Console.ResetColor();
    }
#endif
  }
}
