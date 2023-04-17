using FluentAssertions;
using TestSetup;
using WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.CustomerOperations.Commands.DeleteCustomer.DeleteCustomerCommand;

namespace Applications.CustomerOperations.Commands.DeleteCustomer;

public class DeleteCustomerCommandTest: IClassFixture<CommonTestFixture>
{
    private readonly GameStoreDbContext _dbContext;
    
    public DeleteCustomerCommandTest(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
    }

    [Fact]
    public void WhenDoesNotExistCustomerIdIsGivenInDelete_InvalidOperationException_ShouldBeReturn()
    {
        var customer = new Customer(){Name = "TestName5", Surname = "TestSurname5", Email = "Test5@mail.com", Password = "123456789"};
        _dbContext.Customers.Add(customer);
        _dbContext.SaveChanges();

        DeleteCustomerCommand command = new DeleteCustomerCommand(_dbContext);
        command.CustomerId = 55;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The customer doesn't exist.");
    }

    [Fact]
    public void WhenInvalidPasswordIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        var customer = new Customer(){Name = "TestName6", Surname = "TestSurname6", Email = "Test6@mail.com", Password = "123456789"};
        _dbContext.Customers.Add(customer);
        _dbContext.SaveChanges();

        DeleteCustomerCommand command = new DeleteCustomerCommand(_dbContext);
        command.CustomerId = customer.Id;
        command.Model = new DeleteCustomerModel(){Password = "987654321"};

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Wrong password!");
    }

    [Fact]
    public void WhenValidInputsAreGiven_InvalidOperationException_ShouldNotBeReturn()
    {
        var customer = new Customer(){Name = "TestName7", Surname = "TestSurname7", Email = "Test7@mail.com", Password = "123456789"};
        _dbContext.Customers.Add(customer);
        _dbContext.SaveChanges();

        DeleteCustomerCommand command = new DeleteCustomerCommand(_dbContext);
        command.CustomerId = customer.Id;
        command.Model = new DeleteCustomerModel(){Password = customer.Password};

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _dbContext.Customers.SingleOrDefault(x => x.Id == customer.Id);
        result.Should().BeNull();
    }
}