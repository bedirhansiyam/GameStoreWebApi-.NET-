using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandValidator: AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(command => command.Model.Name).MinimumLength(3).When(x => x.Model.Name != string.Empty);
        RuleFor(command => command.GenreId).NotEmpty().GreaterThan(0);
    }
}