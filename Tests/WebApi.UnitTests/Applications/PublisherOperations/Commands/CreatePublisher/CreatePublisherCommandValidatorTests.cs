using FluentAssertions;
using TestSetup;
using WebApi.Application.PublisherOperations.Command.CreatePublisher;
using static WebApi.Application.PublisherOperations.Command.CreatePublisher.CreatePublisherCommand;

namespace Applications.PublisherOperations.Command.CreatePublisher;

public class CreatePublisherCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("Name", 0)]
    [InlineData("N", 1000)]
    public void WhenInvalidInputsAreGiven_CreatePublisherValidator_ShouldBeReturnErrors(string name, int numberOfEmployees)
    {
        CreatePublisherCommand command = new CreatePublisherCommand(null, null);
        command.Model = new CreatePublisherModel(){Name = name, NumberOfEmployees = numberOfEmployees};

        CreatePublisherCommandValidator validator = new CreatePublisherCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenDateTimeEqualNowIsGiven_CreatePublisherValidator_ShouldBeReturnError()
    {
        CreatePublisherCommand command = new CreatePublisherCommand(null, null);
        command.Model = new CreatePublisherModel(){Name = "TestName4", NumberOfEmployees = 5412, FoundationDate = DateTime.Now.Date};

        CreatePublisherCommandValidator validator = new CreatePublisherCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_CreatePublisherValidator_ShouldNotBeReturnError()
    {
        CreatePublisherCommand command = new CreatePublisherCommand(null, null);
        command.Model = new CreatePublisherModel(){Name = "TestName3", NumberOfEmployees = 1500, FoundationDate = new DateTime(1993,02,12)};

        CreatePublisherCommandValidator validator = new CreatePublisherCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}