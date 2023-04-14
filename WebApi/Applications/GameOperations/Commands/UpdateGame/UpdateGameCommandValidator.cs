using FluentValidation;

namespace WebApi.Application.GameOperations.Commands.UpdateGame;

public class UpdateGameCommandValidator: AbstractValidator<UpdateGameCommand>
{
    public UpdateGameCommandValidator()
    {
        RuleFor(command => command.GameId).NotEmpty().GreaterThan(0);
        RuleFor(command => command.Model.GenreId).GreaterThan(0).When(x => x.Model.GenreId != default);
        RuleFor(command => command.Model.PublisherId).GreaterThan(0).When(x => x.Model.PublisherId != default);
        RuleFor(command => command.Model.ReleaseDate).LessThan(DateTime.Now.Date).When(x => x.Model.ReleaseDate != DateTime.Now.Date);
        RuleFor(command => command.Model.Price).GreaterThan(0).When(x => x.Model.Price != default);
        RuleFor(command => command.Model.Name).MinimumLength(2).When(x => x.Model.Name != string.Empty);
    }
}