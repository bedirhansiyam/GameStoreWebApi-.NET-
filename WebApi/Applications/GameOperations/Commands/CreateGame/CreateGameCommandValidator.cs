using FluentValidation;

namespace WebApi.Application.GameOperations.Commands.CreateGame;

public class CreateGameCommandValidator: AbstractValidator<CreateGameCommand>
{
    public CreateGameCommandValidator()
    {
        RuleFor(command => command.Model.GenreId).NotEmpty().GreaterThan(0);
        RuleFor(command => command.Model.PublisherId).NotEmpty().GreaterThan(0);
        RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(2);
        RuleFor(command => command.Model.Price).NotEmpty().GreaterThan(0);
        RuleFor(command => command.Model.ReleaseDate).NotEmpty().LessThan(DateTime.Now.Date);
    }
}