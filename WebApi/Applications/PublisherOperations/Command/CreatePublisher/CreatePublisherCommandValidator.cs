using FluentValidation;

namespace WebApi.Application.PublisherOperations.Command.CreatePublisher;

public class CreatePublisherCommandValidator: AbstractValidator<CreatePublisherCommand>
{
    public CreatePublisherCommandValidator()
    {
        RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
        RuleFor(command => command.Model.NumberOfEmployees).NotEmpty().GreaterThan(0);
        RuleFor(command => command.Model.FoundationDate).NotEmpty().LessThan(DateTime.Now.Date);
    }
}