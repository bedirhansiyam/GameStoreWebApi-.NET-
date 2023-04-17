using FluentAssertions;
using TestSetup;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using static WebApi.Application.CustomerOperations.Commands.CreateCustomer.CreateCustomerCommand;

namespace Applications.CustomerOperations.Commands.CreateCustomer;

public class CreateCustomerCommandValidatorTest: IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("","Surname","Email@mail.com","123456789")]
    [InlineData("Name","","Email@mail.com","123456789")]
    [InlineData("Name","Surname","","123456789")]
    [InlineData("Name","Surname","Email@mail.com","")]
    [InlineData("Name","Surname","Email@mail.com","1234")]
    [InlineData("N","Surname","Email@mail.com","123456789")]
    [InlineData("Name","S","Email@mail.com","123456789")]
    [InlineData("Name","Surname","Email","123456789")]
    public void WhenInvalidInputsAreGiven_CreateCustomerValidator_ShouldBeReturnErrors(string name, string surname, string email, string password)
    {
        CreateCustomerCommand command = new CreateCustomerCommand(null, null);
        command.Model = new CreateCustomerModel(){Name = name, Surname = surname, Email = email, Password = password};

        CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0); 
    }

    [Fact]
    public void WhenValidInputsAreGiven_CreateCustomerValidator_ShouldNotBeReturnError()
    {
        CreateCustomerCommand command = new CreateCustomerCommand(null, null);
        command.Model = new CreateCustomerModel(){Name = "TestName4", Surname = "TestSurname4", Email = "Test4@mail.com", Password = "123456789"};

        CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}