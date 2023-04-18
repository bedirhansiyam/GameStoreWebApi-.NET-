using FluentAssertions;
using TestSetup;
using WebApi.Application.OrderOperations.Commands.DeleteOrder;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.OrderOperations.Commands.DeleteOrder;

public class DeleteOrderCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly GameStoreDbContext _dbContext;

    public DeleteOrderCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
    }

    [Fact]
    public void WhenDoesNotExistOrderIdIsGivenInDelete_InvalidOperationException_ShouldBeReturn()
    {
        var order = new Order(){GameId = 1};
        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();

        DeleteOrderCommand command = new DeleteOrderCommand(_dbContext);
        command.OrderId = 512;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The order doesn't exist.");
    }

    [Fact]
    public void WhenValidOrderIdIsGivenInDelete_InvalidOperationException_ShouldNotBeReturn()
    {
        var order = new Order(){GameId = 1};
        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();

        DeleteOrderCommand command = new DeleteOrderCommand(_dbContext);
        command.OrderId = order.Id;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _dbContext.Orders.SingleOrDefault(x => x.Id == order.Id);
        result.Should().BeNull();
    }

}