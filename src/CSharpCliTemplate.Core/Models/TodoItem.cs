namespace CSharpCliTemplate.Core.Models;

public sealed record TodoItem(int Id, string Title, bool IsComplete = false);