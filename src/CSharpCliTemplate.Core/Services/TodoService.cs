using CSharpCliTemplate.Core.Exceptions;
using CSharpCliTemplate.Core.Interfaces;
using CSharpCliTemplate.Core.Models;

namespace CSharpCliTemplate.Core.Services;

public sealed class TodoService(ITodoRepository todoRepository) : ITodoService
{
    public TodoItem Add(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Todo title cannot be empty.", nameof(title));
        }

        var items = todoRepository.GetAll().ToList();

        var nextId = items.Count == 0 ? 1 : items.Max(x => x.Id) + 1;
        var item = new TodoItem(nextId, title.Trim());

        items.Add(item);
        todoRepository.SaveAll(items);

        return item;
    }

    public IReadOnlyCollection<TodoItem> List()
    {
        return todoRepository.GetAll();
    }

    public TodoItem Complete(int id)
    {
        var items = todoRepository.GetAll().ToList();
        var item = items.SingleOrDefault(x => x.Id == id);

        if (item is null)
        {
            throw new TodoItemNotFoundException(id);
        }

        var completed = item with { IsComplete = true };

        items.Remove(item);
        items.Add(completed);

        todoRepository.SaveAll(items);

        return completed;
    }
}