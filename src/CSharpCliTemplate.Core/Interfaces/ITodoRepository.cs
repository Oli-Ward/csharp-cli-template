using CSharpCliTemplate.Core.Models;

namespace CSharpCliTemplate.Core.Interfaces;

public interface ITodoRepository
{
    IReadOnlyCollection<TodoItem> GetAll();
    void SaveAll(IReadOnlyCollection<TodoItem> items);
}