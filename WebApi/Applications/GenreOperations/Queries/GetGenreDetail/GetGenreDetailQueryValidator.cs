using FluentValidation;

namespace WebApi.Application.GenreOperations.Queries.GenGenreDetail;

public class GetGenreDetailQueryValidator: AbstractValidator<GetGenreDetailQuery>
{
    public GetGenreDetailQueryValidator()
    {
        RuleFor(command => command.GenreId).NotEmpty().GreaterThan(0);
    }
}