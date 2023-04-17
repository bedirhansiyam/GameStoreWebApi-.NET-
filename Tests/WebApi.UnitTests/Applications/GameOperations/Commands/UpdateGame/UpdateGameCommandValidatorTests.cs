using FluentAssertions;
using TestSetup;
using WebApi.Application.GameOperations.Commands.UpdateGame;
using static WebApi.Application.GameOperations.Commands.UpdateGame.UpdateGameCommand;

namespace Applications.GameOperations.Commands.UpdateGame;

public class UpdateGameCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(1, "Name", 10, 1, -1)]
    [InlineData(1, "Name", 10, -1, 1)]
    [InlineData(1, "Name", -10, 1, 1)]
    [InlineData(-1, "Name", 10, 1, 1)]
    [InlineData(1, "N", 10, 1, 1)]
    public void WhenInvalidInputsAreGiven_UpdateGameValidator_ShouldBeReturnErrors(int gameId, string name, decimal price, int genreId, int publisherId)
    {
        UpdateGameCommand command = new UpdateGameCommand(null, null);
        command.GameId = gameId;
        command.Model = new UpdateGameModel(){Name = name, Price = price, GenreId = genreId, PublisherId = publisherId};

        UpdateGameCommandValidator validator = new UpdateGameCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);

    }
    
    [Fact]
    public void WhenValidInputsAreGiven_UpdateGameValidator_ShouldNotBeReturnError()
    {
        UpdateGameCommand command = new UpdateGameCommand(null, null);
        command.GameId = 1;
        command.Model = new UpdateGameModel(){Name = "TestName6", Price = 15, GenreId = 2, PublisherId = 2, ReleaseDate = DateTime.Now.Date.AddYears(-20)};

        UpdateGameCommandValidator validator = new UpdateGameCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}