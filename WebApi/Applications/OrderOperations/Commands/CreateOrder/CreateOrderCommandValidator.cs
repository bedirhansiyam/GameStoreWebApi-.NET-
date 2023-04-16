using FluentValidation;

namespace WebApi.Application.OrderOperations.Commands.CreateOrder;

public class CreateOrderCommandValidator: AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(command => command.Model.GameId).NotEmpty().GreaterThan(0);
    }
}