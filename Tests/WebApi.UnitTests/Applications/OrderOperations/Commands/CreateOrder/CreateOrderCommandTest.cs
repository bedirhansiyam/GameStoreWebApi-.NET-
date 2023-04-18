using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.OrderOperations.Commands.CreateOrder.CreateOrderCommand;

namespace Applications.OrderOperations.Commands.CreateOrder;

public class CreateOrderCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly GameStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateOrderCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenGameIdIsNotExist_InvalidOperationException_ShouldBeReturn()
    {
        CreateOrderCommand command = new CreateOrderCommand(_dbContext, _mapper);
        command.Model = new CreateOrderModel(){GameId = 1};

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Game not found!");
    }
}