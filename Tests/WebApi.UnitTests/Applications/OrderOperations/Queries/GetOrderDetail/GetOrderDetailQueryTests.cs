using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.OrderOperations.Queries.GetOrderDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.OrderOperations.Queries.GetOrderDetail;

public class GetOrderDetailQueryTests: IClassFixture<CommonTestFixture>
{
    private readonly GameStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetOrderDetailQueryTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenOrderIdIsNotMatchAnyOrder_InvalidOperationException_ShouldBeReturn()
    {
        var order = new Order(){GameId = 1};
        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();

        GetOrderDetailQuery query = new GetOrderDetailQuery(_dbContext, _mapper);
        query.OrderId = 148;

        FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The order doesn't exist.");
    }

    [Fact]
    public void WhenGivenOrderIdIsMatchAnyOrder_InvalidOperationException_ShouldNotBeReturn()
    {
        var order = new Order(){GameId = 1};
        order.CustomerId = 1;
        order.PurchasedDate = new DateTime(2022,02,12);
        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();

        GetOrderDetailQuery query = new GetOrderDetailQuery(_dbContext, _mapper);
        query.OrderId = order.Id;

        FluentActions.Invoking(() => query.Handle()).Invoke();

        var result = _dbContext.Orders.SingleOrDefault(x => x.Id == order.Id);
        result.Should().NotBeNull();
    }
}