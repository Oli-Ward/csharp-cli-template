using CSharpCliTemplate.Core.Exceptions;
using CSharpCliTemplate.Core.Services;
using CSharpCliTemplate.UnitTests.Fakes;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;

namespace CSharpCliTemplate.UnitTests;

public class TodoServiceTests
{


    private static TodoService CreateService()
    {
        return new TodoService(new InMemoryTodoRepository(), NullLogger<TodoService>.Instance);
    }

    [Fact]
    public void Add_Should_Create_New_Todo_Item()
    {
        var service = CreateService();

        var result = service.Add("Test task");

        result.Id.Should().Be(1);
        result.Title.Should().Be("Test task");
        result.IsComplete.Should().BeFalse();
    }

    [Fact]
    public void List_Should_Return_All_Items()
    {
        var service = CreateService();

        service.Add("Task 1");
        service.Add("Task 2");

        var items = service.List();

        items.Should().HaveCount(2);
    }

    [Fact]
    public void Complete_Should_Mark_Item_As_Complete()
    {
        var service = CreateService();

        var item = service.Add("Test task");

        var result = service.Complete(item.Id);

        result.IsComplete.Should().BeTrue();
    }

    [Fact]
    public void Complete_Should_Throw_When_Item_Not_Found()
    {
        System.Diagnostics.Debugger.Launch();

        var service = CreateService();

        var act = () => service.Complete(999);

        act.Should().Throw<TodoItemNotFoundException>();
    }
}