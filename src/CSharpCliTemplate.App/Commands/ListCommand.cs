using CSharpCliTemplate.Core.Interfaces;

namespace CSharpCliTemplate.App.Commands;

public sealed class ListCommand(ITodoService todoService) : ICommand
{
    public string Name => "list";

    public void Execute(string[] args)
    {
        var items = todoService.List();

        foreach (var todo in items)
        {
            var status = todo.IsComplete ? "done" : "todo";
            Console.WriteLine($"#{todo.Id} [{status}] {todo.Title}");
        }
    }
}