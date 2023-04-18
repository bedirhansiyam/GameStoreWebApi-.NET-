using FluentAssertions;
using TestSetup;
using WebApi.Application.PublisherOperations.Command.DeletePublisher;

namespace Applications.PublisherOperations.Command.DeletePublisher;

public class DeletePublisherCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidInputsAreGiven_DeletePublisherValidator_ShouldBeReturnError()
    {
        DeletePublisherCommand command = new DeletePublisherCommand(null);
        command.PublisherId = -1;

        DeletePublisherCommandValidator validator = new DeletePublisherCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_DeletePublisherValidator_ShouldNotBeReturnError()
    {
        DeletePublisherCommand command = new DeletePublisherCommand(null);
        command.PublisherId = 1;

        DeletePublisherCommandValidator validator = new DeletePublisherCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}