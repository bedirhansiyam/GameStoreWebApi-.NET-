using FluentValidation;

namespace WebApi.Application.GameOperations.Commands.DeleteGame;

public class DeleteGameCommandValidator: AbstractValidator<DeleteGameCommand>
{
    public DeleteGameCommandValidator()
    {
        RuleFor(command => command.GameId).NotEmpty().GreaterThan(0);
    }
}