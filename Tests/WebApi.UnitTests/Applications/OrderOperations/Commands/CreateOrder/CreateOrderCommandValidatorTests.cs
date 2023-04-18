using FluentAssertions;
using TestSetup;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using static WebApi.Application.OrderOperations.Commands.CreateOrder.CreateOrderCommand;

namespace Applications.OrderOperations.Commands.CreateOrder;

public class CreateOrderCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidInputsAreGiven_CreateOrderValidator_ShouldBeReturnError()
    {
        CreateOrderCommand command = new CreateOrderCommand(null, null);
        command.Model = new CreateOrderModel(){GameId = -1};

        CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    } 

    [Fact]
    public void WhenValidInputsAreGiven_CreateOrderValidator_ShouldNotBeReturnError()
    {
        CreateOrderCommand command = new CreateOrderCommand(null, null);
        command.Model = new CreateOrderModel(){GameId = 1};

        CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}