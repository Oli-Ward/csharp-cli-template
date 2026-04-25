using CSharpCliTemplate.Core.Interfaces;
using CSharpCliTemplate.Core.Models;

namespace CSharpCliTemplate.UnitTests.Fakes;

public sealed class InMemoryTodoRepository : ITodoRepository
{
    private List<TodoItem> _items = [];

    public IReadOnlyCollection<TodoItem> GetAll()
    {
        return _items.AsReadOnly();
    }

    public void SaveAll(IReadOnlyCollection<TodoItem> items)
    {
        _items = [.. items];
    }
}