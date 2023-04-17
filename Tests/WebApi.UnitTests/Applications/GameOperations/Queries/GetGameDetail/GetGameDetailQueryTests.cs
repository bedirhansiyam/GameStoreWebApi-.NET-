using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GameOperations.Queries.GetGameDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.GameOperations.Queries.GetGameDetail;

public class GetGameDetailQueryTests: IClassFixture<CommonTestFixture>
{
    private readonly GameStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGameDetailQueryTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenGameIdIsNotMatchAnyGame_InvalidOperationException_ShouldBeReturn()
    {
        var game = new Game(){Name = "TestGame6", Price = 60, ReleaseDate = new DateTime(2016,10,07), GenreId = 2, PublisherId = 1};
        _dbContext.Games.Add(game);
        _dbContext.SaveChanges();

        GetGameDetailQuery query = new GetGameDetailQuery(_dbContext, _mapper);
        query.GameId = 150;

        FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The game doesn't exist.");
    }

    [Fact]
    public void WhenGivenGameIdIsMatchAnyGame_InvalidOperationException_ShouldNotBeReturn()
    {
        var game = new Game(){Name = "TestGame7", Price = 60, ReleaseDate = new DateTime(2017,10,07), GenreId = 2, PublisherId = 2};
        _dbContext.Games.Add(game);
        _dbContext.SaveChanges();

        GetGameDetailQuery query = new GetGameDetailQuery(_dbContext, _mapper);
        query.GameId = game.Id;

        FluentActions.Invoking(() => query.Handle()).Invoke();
        var result = _dbContext.Games.SingleOrDefault(x => x.Id == game.Id);
        result.Should().NotBeNull();
    }
}