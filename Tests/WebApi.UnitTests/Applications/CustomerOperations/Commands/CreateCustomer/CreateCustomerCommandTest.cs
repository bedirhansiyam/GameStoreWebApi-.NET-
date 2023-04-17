using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.CustomerOperations.Commands.CreateCustomer.CreateCustomerCommand;

namespace Applications.CustomerOperations.Commands.CreateCustomer;

public class CreateCustomerCommandTest: IClassFixture<CommonTestFixture>
{
    private readonly GameStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateCustomerCommandTest(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyGivenCustomerEmailIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        var customer = new Customer(){Name = "TestName", Surname = "TestSurname", Email = "Test@mail.com", Password = "123456789"};
        _dbContext.Customers.Add(customer);
        _dbContext.SaveChanges();

        CreateCustomerCommand command = new CreateCustomerCommand(_dbContext, _mapper);
        command.Model = new CreateCustomerModel(){Name = "TestName2", Surname = "TestSurname2", Email = customer.Email, Password = "987654321"};

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Email already exist.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_InvalidOperationException_ShouldNotBeReturn()
    {
        CreateCustomerCommand command = new CreateCustomerCommand(_dbContext, _mapper);
        CreateCustomerModel model = new CreateCustomerModel(){Name = "TestName3", Surname = "TestSurname3", Email = "Test3@mail.com", Password = "123456789"};
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _dbContext.Customers.SingleOrDefault(x => x.Email == model.Email);
        result.Should().NotBeNull();
    }
}