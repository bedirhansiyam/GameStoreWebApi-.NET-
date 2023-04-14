using FluentValidation;

namespace WebApi.Application.GameOperations.Queries.GetGameDetail;

public class GetGameDetailQueryValidator: AbstractValidator<GetGameDetailQuery>
{
    public GetGameDetailQueryValidator()
    {
        RuleFor(command => command.GameId).NotEmpty().GreaterThan(0);
    }
}