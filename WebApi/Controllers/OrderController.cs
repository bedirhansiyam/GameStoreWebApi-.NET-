using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.Application.OrderOperations.Commands.DeleteOrder;
using WebApi.Application.OrderOperations.Queries.GetOrderDetail;
using WebApi.Application.OrderOperations.Queries.GetOrders;
using WebApi.DBOperations;
using static WebApi.Application.OrderOperations.Commands.CreateOrder.CreateOrderCommand;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]s")]
public class OrderController: ControllerBase
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public OrderController(IGameStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetOrders()
    {
        GetOrdersQuery query = new GetOrdersQuery(_dbContext, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("id")]
    public IActionResult GetOrderDetail(int id)
    {
        GetOrderDetailQuery query = new GetOrderDetailQuery(_dbContext, _mapper);
        query.OrderId = id;

        GetOrderDetailQueryValidator validator = new GetOrderDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddOrder([FromBody] CreateOrderModel newOrder)
    {
        CreateOrderCommand command = new CreateOrderCommand(_dbContext, _mapper);
        command.Model = newOrder;

        CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteOrder(int id)
    {
        DeleteOrderCommand command = new DeleteOrderCommand(_dbContext);
        command.OrderId = id;

        DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }
}