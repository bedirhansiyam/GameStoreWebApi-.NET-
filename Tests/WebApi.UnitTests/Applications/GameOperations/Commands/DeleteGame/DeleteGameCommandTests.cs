using FluentAssertions;
using TestSetup;
using WebApi.Application.GameOperations.Commands.DeleteGame;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.GameOperations.Commands.DeleteGame;

public class DeleteGameCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly GameStoreDbContext _dbContext;

    public DeleteGameCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
    }

    [Fact]
    public void WhenInvalidGameIdIsGivenInDelete_InvalidOperationException_ShouldBeReturn()
    {
        var game = new Game(){Name = "TestGame3", Price = 30, ReleaseDate = new DateTime(2021,10,07), GenreId = 3, PublisherId = 3};
        _dbContext.Games.Add(game);
        _dbContext.SaveChanges();

        DeleteGameCommand command = new DeleteGameCommand(_dbContext);
        command.GameId = -1;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The game doesn't exist.");
    }

    [Fact]
    public void WhenValidGameIdIsGivenInDelete_InvalidOperationException_ShouldNotBeReturn()
    {
        var game = new Game(){Name = "TestGame3", Price = 30, ReleaseDate = new DateTime(2021,10,07), GenreId = 3, PublisherId = 3};
        _dbContext.Games.Add(game);
        _dbContext.SaveChanges();

        DeleteGameCommand command = new DeleteGameCommand(_dbContext);
        command.GameId = game.Id;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _dbContext.Games.SingleOrDefault(x => x.Id == game.Id);
        result.Should().BeNull();
    }
}