using System.Text.Json;

namespace CSharpCliTemplate.App.Persistence;

public sealed class JsonTodoRepository : ITodoRepository
{
    private const string FilePath = "todos.json";

    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
    };

    public IReadOnlyCollection<TodoItem> GetAll()
    {
        if (!File.Exists(FilePath))
        {
            return [];
        }

        var json = File.ReadAllText(FilePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return [];
        }

        return JsonSerializer.Deserialize<List<TodoItem>>(json, _options) ?? [];
    }

    public void SaveAll(IReadOnlyCollection<TodoItem> items)
    {
        var json = JsonSerializer.Serialize(items, _options);

        File.WriteAllText(FilePath, json);
    }
}