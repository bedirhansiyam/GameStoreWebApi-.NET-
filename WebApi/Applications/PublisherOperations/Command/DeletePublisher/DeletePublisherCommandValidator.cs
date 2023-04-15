using FluentValidation;

namespace WebApi.Application.PublisherOperations.Command.DeletePublisher;

public class DeletePublisherCommandValidator: AbstractValidator<DeletePublisherCommand>
{
    public DeletePublisherCommandValidator()
    {
        RuleFor(command => command.PublisherId).NotEmpty().GreaterThan(0);
    }
}