using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;

namespace Applications.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly GameStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateGenreCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenDoesNotExistGenreIdIsGivenInUpdate_InvalidOperationException_ShouldBeReturn()
    {
        var genre = new Genre(){Name = "TestName20"};
        _dbContext.Genres.Add(genre);
        _dbContext.SaveChanges();

        UpdateGenreCommand command = new UpdateGenreCommand(_dbContext, _mapper);
        command.GenreId = 541;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The genre doesn't exist.");
    }

    [Fact]
    public void WhenValidGenreIdIsGiven_InvalidOperationException_ShouldNotBeReturn()
    {
        var genre = new Genre(){Name = "TestName20"};
        _dbContext.Genres.Add(genre);
        _dbContext.SaveChanges();

        UpdateGenreCommand command = new UpdateGenreCommand(_dbContext, _mapper);
        command.GenreId = genre.Id;
        command.Model = new UpdateGenreModel(){Name = "TestName21"};

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _dbContext.Genres.SingleOrDefault(x => x.Id == genre.Id);
        result.Should().NotBeNull();
    }
}