using System;
using Microsoft.Extensions.DependencyInjection;
using Snapp.Cli.Commands;
using Snapp.Cli.Helpers;
using Snapp.Cli.Services;
using Spectre.Console.Cli;

var registrations = new ServiceCollection();
registrations.AddSingleton<SnappService>();
registrations.AddSingleton<AppSettingsHelper>();
registrations.AddHttpClient<ISnappService, SnappService>();

var registrar = new Snapp.Cli.Helpers.TypeRegistrar(registrations);

var app = new CommandApp<DebugCommand>(registrar);


app.Configure(config => {
#if DEBUG
  config.PropagateExceptions();
  config.ValidateExamples();
#endif
  config.AddCommand<DebugCommand>("debug");
  config.AddCommand<ListSnappsCommand>("list");
});

return await app
  .RunAsync(args)
  .ConfigureAwait(false);
