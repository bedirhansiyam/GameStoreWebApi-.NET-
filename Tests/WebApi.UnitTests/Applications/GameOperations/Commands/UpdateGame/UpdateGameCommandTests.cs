using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GameOperations.Commands.UpdateGame;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.GameOperations.Commands.UpdateGame.UpdateGameCommand;

namespace Applications.GameOperations.Commands.UpdateGame;

public class UpdateGameCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly GameStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateGameCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenDoesNotExistGameIdIsGivenUpdate_InvalidOperationException_ShouldBeReturn()
    {
        var game = new Game(){Name = "TestGame3", Price = 30, ReleaseDate = new DateTime(2021,10,07), GenreId = 3, PublisherId = 3};
        _dbContext.Games.Add(game);
        _dbContext.SaveChanges();

        UpdateGameCommand command = new UpdateGameCommand(_dbContext, _mapper);
        command.GameId = -1;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The game doesn't exist.");
    }

    [Fact]
    public void WhenValidGameIdIsGivenUpdate_InvalidOperationException_ShouldNotBeReturn()
    {
        var game = new Game(){Name = "TestGame4", Price = 40, ReleaseDate = new DateTime(2004,10,07), GenreId = 4, PublisherId = 3};
        _dbContext.Games.Add(game);
        _dbContext.SaveChanges();

        UpdateGameCommand command = new UpdateGameCommand(_dbContext, _mapper);
        command.GameId = game.Id;
        command.Model = new UpdateGameModel(){Name = "TestGame5", Price = 50, ReleaseDate = new DateTime(2005,10,07), GenreId = 4, PublisherId = 3};

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _dbContext.Games.SingleOrDefault(x => x.Id == game.Id);
        result.Should().NotBeNull();
    }
}