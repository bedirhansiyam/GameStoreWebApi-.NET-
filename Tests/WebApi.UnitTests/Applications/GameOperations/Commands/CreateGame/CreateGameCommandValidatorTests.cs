using FluentAssertions;
using TestSetup;
using WebApi.Application.GameOperations.Commands.CreateGame;
using static WebApi.Application.GameOperations.Commands.CreateGame.CreateGameCommand;

namespace Applications.GameOperations.Commands.CreateGame;

public class CreateGameCommandValidatorTests:IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("Name", 10, 1, 0)]
    [InlineData("Name", 10, 0, 1)]
    [InlineData("Name", 0, 1, 1)]
    [InlineData("", 10, 1, 1)]
    [InlineData("N", 10, 1, 1)]
    public void WhenInvalidInputsAreGiven_CreateGameValidator_ShouldBeReturnErrors(string name, decimal price, int genreId, int publisherId)
    {
        CreateGameCommand command = new CreateGameCommand(null, null);
        command.Model = new CreateGameModel(){Name = name, Price = price, GenreId = genreId, PublisherId = publisherId};

        CreateGameCommandValidator validator = new CreateGameCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public  void WhenDateTimeEqualNowIsGiven_CreateGameValidator_ShouldBeReturnError()
    {
        CreateGameCommand command = new CreateGameCommand(null, null);
        command.Model = new CreateGameModel(){Name = "TestName", Price = 10, GenreId = 1, PublisherId = 1, ReleaseDate = DateTime.Now.Date};

        CreateGameCommandValidator validator = new CreateGameCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_CreateGameValidator_ShouldNotBeReturnError()
    {
        CreateGameCommand command = new CreateGameCommand(null, null);
        command.Model = new CreateGameModel(){Name = "TestName2", Price = 20, GenreId = 2, PublisherId = 2, ReleaseDate = DateTime.Now.Date.AddYears(-15)};

        CreateGameCommandValidator validator = new CreateGameCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}