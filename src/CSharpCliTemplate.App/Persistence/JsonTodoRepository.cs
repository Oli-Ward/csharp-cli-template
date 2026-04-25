using System.Text.Json;
using CSharpCliTemplate.App.Config;
using CSharpCliTemplate.Core.Interfaces;
using CSharpCliTemplate.Core.Models;
using Microsoft.Extensions.Options;

namespace CSharpCliTemplate.App.Persistence;

public sealed class JsonTodoRepository(IOptions<StorageOptions> options) : ITodoRepository
{
    private readonly string _filePath = options.Value.TodoFilePath;
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
    };

    public IReadOnlyCollection<TodoItem> GetAll()
    {
        if (!File.Exists(_filePath))
        {
            return [];
        }

        var json = File.ReadAllText(_filePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return [];
        }

        return JsonSerializer.Deserialize<List<TodoItem>>(json, _options) ?? [];
    }

    public void SaveAll(IReadOnlyCollection<TodoItem> items)
    {
        var json = JsonSerializer.Serialize(items, _options);

        File.WriteAllText(_filePath, json);
    }
}