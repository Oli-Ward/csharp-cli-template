using CSharpCliTemplate.App.Commands;
using CSharpCliTemplate.App.Persistence;
using CSharpCliTemplate.Core.Exceptions;
using CSharpCliTemplate.Core.Interfaces;
using CSharpCliTemplate.Core.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<ITodoService, TodoService>();

services.AddSingleton<ICommand, AddCommand>();
services.AddSingleton<ICommand, ListCommand>();
services.AddSingleton<ICommand, CompleteCommand>();
services.AddSingleton<ITodoRepository, JsonTodoRepository>();
services.AddSingleton<ITodoService, TodoService>();


var provider = services.BuildServiceProvider();

var commands = provider.GetServices<ICommand>();
var commandName = args.FirstOrDefault();

var command = commands.FirstOrDefault(c => c.Name == commandName);

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