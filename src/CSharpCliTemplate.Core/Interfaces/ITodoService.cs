using CSharpCliTemplate.Core.Models;

namespace CSharpCliTemplate.Core.Interfaces;

public interface ITodoService
{
    TodoItem Add(string title);
    IReadOnlyCollection<TodoItem> List();
    TodoItem Complete(int id);
}