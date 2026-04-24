using CSharpCliTemplate.Core.Services;
using FluentAssertions;

namespace CSharpCliTemplate.UnitTests;

public class TodoServiceTests
{
    [Fact]
    public void Add_Should_Create_New_Todo_Item()
    {
        var service = new TodoService();

        var result = service.Add("Test task");

        result.Id.Should().Be(1);
        result.Title.Should().Be("Test task");
        result.IsComplete.Should().BeFalse();
    }

    [Fact]
    public void List_Should_Return_All_Items()
    {
        var service = new TodoService();

        service.Add("Task 1");
        service.Add("Task 2");

        var items = service.List();

        items.Should().HaveCount(2);
    }

    [Fact]
    public void Complete_Should_Mark_Item_As_Complete()
    {
        var service = new TodoService();

        var item = service.Add("Test task");

        var result = service.Complete(item.Id);

        result.IsComplete.Should().BeTrue();
    }

    [Fact]
    public void Complete_Should_Throw_When_Item_Not_Found()
    {
        var service = new TodoService();

        var act = () => service.Complete(999);

        act.Should().Throw<InvalidOperationException>();
    }
}