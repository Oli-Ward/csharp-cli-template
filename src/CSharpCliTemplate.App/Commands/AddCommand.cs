using CSharpCliTemplate.Core.Interfaces;

namespace CSharpCliTemplate.App.Commands;

public sealed class AddCommand(ITodoService todoService) : ICommand
{
    public string Name => "add";

    public void Execute(string[] args)
    {
        var title = string.Join(' ', args.Skip(1));

        var item = todoService.Add(title);

        Console.WriteLine($"Added #{item.Id}: {item.Title}");
    }
}