namespace CSharpCliTemplate.Core.Exceptions;

public class TodoItemNotFoundException : Exception
{
    public TodoItemNotFoundException()
    {
    }

    public TodoItemNotFoundException(string message)
        : base(message)
    {
    }

    public TodoItemNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public TodoItemNotFoundException(int id)
        : base($"Todo item with id '{id}' was not found.")
    {
        Id = id;
    }

    public int Id { get; }
}