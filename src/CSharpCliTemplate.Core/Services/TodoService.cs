using CSharpCliTemplate.Core.Interfaces;
using CSharpCliTemplate.Core.Models;

namespace CSharpCliTemplate.Core.Services;

public sealed class TodoService : ITodoService
{
    private readonly List<TodoItem> _items = [];

    public TodoItem Add(string title)
    {
        var item = new TodoItem(_items.Count + 1, title);
        _items.Add(item);
        return item;
    }

    public IReadOnlyCollection<TodoItem> List() => _items.AsReadOnly();

    public TodoItem Complete(int id)
    {
        var item = _items.Single(x => x.Id == id);
        var completed = item with { IsComplete = true };

        _items.Remove(item);
        _items.Add(completed);

        return completed;
    }
}