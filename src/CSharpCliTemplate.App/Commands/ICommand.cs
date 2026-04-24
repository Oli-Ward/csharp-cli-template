namespace CSharpCliTemplate.App.Commands;

public interface ICommand
{
    string Name { get; }
    void Execute(string[] args);
}