using CSharpCliTemplate.Core.Interfaces;

namespace CSharpCliTemplate.App.Commands;

public sealed class CompleteCommand(ITodoService todoService) : ICommand
{
    public string Name => "complete";

    public void Execute(string[] args)
    {
        if (!int.TryParse(args.ElementAtOrDefault(1), out var id))
        {
            Console.WriteLine("Please provide a valid id");
            return;
        }

        var item = todoService.Complete(id);

        Console.WriteLine($"Completed #{item.Id}: {item.Title}");
    }
}