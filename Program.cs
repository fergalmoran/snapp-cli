using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Spectre.Console.Cli;
using Snapp.Cli.Commands;
using Snapp.Cli.Helpers;
using Snapp.Cli.Services;

var registrations = new ServiceCollection();
registrations.AddSingleton<SnappService>();
registrations.AddHttpClient<ISnappService, SnappService>();
registrations.AddScoped<AppSettings>(sp => sp.GetRequiredService<IOptions<AppSettings>>().Value);
registrations.AddSingleton<AppSettingsHelper>();
registrations.AddOptions();


var registrar = new TypeRegistrar(registrations);

var app = new CommandApp<DebugCommand>(registrar);


app.Configure(config => {
  config.CaseSensitivity(CaseSensitivity.None);
  config.SetApplicationName("Snapp CLI");
  config.ValidateExamples();

#if DEBUG
  config.PropagateExceptions();
  config.ValidateExamples();
#endif

  config.AddCommand<DebugCommand>("debug");
  config.AddCommand<ListSnappsCommand>("list");
  config.AddCommand<AddSnappsCommand>("add");
});

return await app
  .RunAsync(args)
  .ConfigureAwait(false);
