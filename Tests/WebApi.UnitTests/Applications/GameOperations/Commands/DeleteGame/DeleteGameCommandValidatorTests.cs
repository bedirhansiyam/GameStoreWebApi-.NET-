using FluentAssertions;
using TestSetup;
using WebApi.Application.GameOperations.Commands.DeleteGame;

namespace Applications.GameOperations.Commands.DeleteGame;

public class DeleteGameCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidGameIdIsGiven_DeleteGameValidator_ShouldBeReturnError()
    {
        DeleteGameCommand command = new DeleteGameCommand(null);
        command.GameId = -1;

        DeleteGameCommandValidator validator = new DeleteGameCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidGameIdIsGiven_DeleteGameValidator_ShouldNotBeReturnError()
    {
        DeleteGameCommand command = new DeleteGameCommand(null);
        command.GameId = 1;

        DeleteGameCommandValidator validator = new DeleteGameCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}