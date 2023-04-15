using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using WebApi.DBOperations;
using static WebApi.Application.CustomerOperations.Commands.CreateCustomer.CreateCustomerCommand;
using static WebApi.Application.CustomerOperations.Commands.DeleteCustomer.DeleteCustomerCommand;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]s")]
public class CustomerController: ControllerBase
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public CustomerController(IGameStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _configuration = configuration;
    }

    [HttpPost]
    public IActionResult AddCustomer([FromBody] CreateCustomerModel newCustomer)
    {
        CreateCustomerCommand command = new CreateCustomerCommand(_dbContext, _mapper);
        command.Model = newCustomer;

        CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
        validator.ValidateAndThrow(command);
        
        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteCustomer(int id, [FromBody] DeleteCustomerModel password)
    {
        DeleteCustomerCommand command = new DeleteCustomerCommand(_dbContext);
        command.CustomerId = id;
        command.Model = password;

        DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        
        return Ok();
    }
}