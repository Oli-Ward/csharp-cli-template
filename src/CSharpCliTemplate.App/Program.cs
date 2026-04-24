using CSharpCliTemplate.Core.Interfaces;
using CSharpCliTemplate.Core.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<ITodoService, TodoService>();

var provider = services.BuildServiceProvider();
var todoService = provider.GetRequiredService<ITodoService>();

var command = args.FirstOrDefault();

switch (command)
{
    case "add":
        var title = string.Join(' ', args.Skip(1));
        var item = todoService.Add(title);
        Console.WriteLine($"Added #{item.Id}: {item.Title}");
        break;

    case "list":
        foreach (var todo in todoService.List())
        {
            var status = todo.IsComplete ? "done" : "todo";
            Console.WriteLine($"#{todo.Id} [{status}] {todo.Title}");
        }
        break;

    case "complete":
        if (!int.TryParse(args.ElementAtOrDefault(1), out var id))
        {
            Console.WriteLine("Please provide a valid id");
            return;
        }

        var completed = todoService.Complete(id);
        Console.WriteLine($"Completed #{completed.Id}: {completed.Title}");
        break;

    default:
        Console.WriteLine("Usage:");
        Console.WriteLine("  add <title>");
        Console.WriteLine("  list");
        break;
}