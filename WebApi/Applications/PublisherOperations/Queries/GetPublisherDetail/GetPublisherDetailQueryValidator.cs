using FluentValidation;

namespace WebApi.Application.PublisherOperations.Queries.GetPublisherDetail;

public class GetPublisherDetailQueryValidator: AbstractValidator<GetPublisherDetailQuery>
{
    public GetPublisherDetailQueryValidator()
    {
        RuleFor(command => command.PublisherId).NotEmpty().GreaterThan(0);
    }
}