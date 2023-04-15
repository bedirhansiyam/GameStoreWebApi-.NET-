using FluentValidation;

namespace WebApi.Application.PublisherOperations.Command.UpdatePublisher;

public class UpdatePublisherCommandValidator: AbstractValidator<UpdatePublisherCommand>
{
    public UpdatePublisherCommandValidator()
    {
        RuleFor(command => command.PublisherId).NotEmpty().GreaterThan(0);
        RuleFor(command => command.Model.Name).MinimumLength(4).When(x => x.Model.Name != string.Empty);
        RuleFor(command => command.Model.NumberOfEmployees).GreaterThan(0).When(x => x.Model.NumberOfEmployees != default);
        RuleFor(command => command.Model.FoundationDate).LessThan(DateTime.Now.Date).When(x => x.Model.FoundationDate != default);
    }
}