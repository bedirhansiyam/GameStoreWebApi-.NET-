using FluentAssertions;
using TestSetup;
using WebApi.Application.PublisherOperations.Command.UpdatePublisher;
using static WebApi.Application.PublisherOperations.Command.UpdatePublisher.UpdatePublisherCommand;

namespace Applications.PublisherOperations.Command.UpdatePublisher;

public class UpdatePublisherCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(-1, "Name", 152)]
    [InlineData(1, "Na", 152)]
    [InlineData(1, "Name", -152)]
    public void WhenInvalidInputsAreGiven_UpdatePublisherValidator_ShouldBeReturnErros(int publisherId, string name, int numberOfEmployees)
    {
        UpdatePublisherCommand command = new UpdatePublisherCommand(null, null);
        command.PublisherId = publisherId;
        command.Model = new UpdatePublisherModel(){Name = name, NumberOfEmployees = numberOfEmployees};

        UpdatePublisherCommandValidator validator = new UpdatePublisherCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenDateTimeEqualNowIsGiven_UpdatePublisherValidator_ShouldBeReturnError()
    {
        UpdatePublisherCommand command = new UpdatePublisherCommand(null, null);
        command.Model = new UpdatePublisherModel(){Name = "TestName6", NumberOfEmployees = 5412, FoundationDate = DateTime.Now.Date};

        UpdatePublisherCommandValidator validator = new UpdatePublisherCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_UpdatePublisherValidator_ShouldNotBeReturnError()
    {
        UpdatePublisherCommand command = new UpdatePublisherCommand(null, null);
        command.PublisherId = 1;
        command.Model = new UpdatePublisherModel(){Name = "TestName7", NumberOfEmployees = 52, FoundationDate = DateTime.Now.Date.AddYears(-14)};

        UpdatePublisherCommandValidator validator = new UpdatePublisherCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}