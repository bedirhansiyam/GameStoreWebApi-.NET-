using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GenGenreDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryTests: IClassFixture<CommonTestFixture>
{
    private readonly GameStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGenreDetailQueryTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenGenreIdIsNotMatchAnyGenre_InvalidOperationException_ShouldBeReturn()
    {
        var genre = new Genre(){Name = "TestName30"};
        _dbContext.Genres.Add(genre);
        _dbContext.SaveChanges();

        GetGenreDetailQuery query = new GetGenreDetailQuery(_dbContext, _mapper);
        query.GenreId = 45;

        FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The genre doesn't exist.");
    }

    [Fact]
    public void WhenGivenGenreIdIsMatchAnyGenre_InvalidOperationException_ShouldNotBeReturn()
    {
        var genre = new Genre(){Name = "TestName30"};
        _dbContext.Genres.Add(genre);
        _dbContext.SaveChanges();

        GetGenreDetailQuery query = new GetGenreDetailQuery(_dbContext, _mapper);
        query.GenreId = genre.Id;

        FluentActions.Invoking(() => query.Handle()).Invoke();

        var result = _dbContext.Genres.SingleOrDefault(x => x.Id == genre.Id);
        result.Should().NotBeNull();
    }
}