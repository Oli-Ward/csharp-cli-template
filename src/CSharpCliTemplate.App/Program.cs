using CSharpCliTemplate.App.Commands;
using CSharpCliTemplate.App.Config;
using CSharpCliTemplate.App.Persistence;
using CSharpCliTemplate.Core.Exceptions;
using CSharpCliTemplate.Core.Interfaces;
using CSharpCliTemplate.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var services = new ServiceCollection()
    .AddLogging(loggingBuilder =>
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddSerilog();
    });

services.AddSingleton<ICommand, AddCommand>();
services.AddSingleton<ICommand, ListCommand>();
services.AddSingleton<ICommand, CompleteCommand>();
services.AddSingleton<ITodoRepository, JsonTodoRepository>();
services.AddSingleton<ITodoService, TodoService>();


var provider = services.BuildServiceProvider();

var commands = provider.GetServices<ICommand>();
var commandName = args.FirstOrDefault();

var command = commands.FirstOrDefault(c => c.Name == commandName);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

services.Configure<StorageOptions>(
    configuration.GetSection("Storage")
);

try
{
    if (command is null)
    {
        Console.WriteLine("Available commands: add, list, complete");
        return;
    }

    command.Execute(args);
}
catch (TodoItemNotFoundException ex)
{
    Console.WriteLine(ex.Message);
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}