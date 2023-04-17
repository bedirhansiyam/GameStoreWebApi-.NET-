using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GameOperations.Commands.CreateGame;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.GameOperations.Commands.CreateGame.CreateGameCommand;

namespace Applications.GameOperations.Commands.CreateGame;

public class CreateGameCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly GameStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateGameCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyGivenGameNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        var game = new Game(){Name = "TestGame", Price = 60, ReleaseDate = new DateTime(2021,10,07), GenreId = 5, PublisherId = 1};
        _dbContext.Games.Add(game);
        _dbContext.SaveChanges();

        CreateGameCommand command = new CreateGameCommand(_dbContext, _mapper);
        command.Model = new CreateGameModel(){Name = game.Name, Price = 70, ReleaseDate = new DateTime(2020,10,07), GenreId = 2, PublisherId = 3};

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The game already exist.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Game_ShouldBeCreated()
    {
        CreateGameCommand command = new CreateGameCommand(_dbContext, _mapper);
        CreateGameModel model = new CreateGameModel(){Name = "TestGame1", Price = 30, ReleaseDate = new DateTime(2001,10,07), GenreId = 1, PublisherId = 1};
         command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _dbContext.Games.SingleOrDefault(x => x.Name == model.Name);
        result.Should().NotBeNull();
    }
}