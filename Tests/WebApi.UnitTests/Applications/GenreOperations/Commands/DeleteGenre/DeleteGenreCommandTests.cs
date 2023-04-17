using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly GameStoreDbContext _dbContext;

    public DeleteGenreCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
    }

    [Fact]
    public void WhenInvalidGenreIdIsGivenInDelete_InvalidOperationException_ShouldBeReturn()
    {
        var genre = new Genre(){Name = "TestName7"};
        _dbContext.Genres.Add(genre);
        _dbContext.SaveChanges();

        DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
        command.GenreId = 111;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The genre doesn't exist.");
    }

    [Fact]
    public void WhenGenreHasGameIsGivenInDelete_InvalidOperationException_ShouldBeReturn()
    {
        var genre = new Genre(){Name = "TestName8"};
        _dbContext.Genres.Add(genre);
        _dbContext.SaveChanges();

        var game = new Game(){Name = "TestGame12", Price = 60, ReleaseDate = new DateTime(2021,10,07), GenreId = genre.Id, PublisherId = 1};
        _dbContext.Games.Add(game);
        _dbContext.SaveChanges();

        DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
        command.GenreId = genre.Id;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("There are/is game/s in this genre. To delete this genre, you must first delete the game/s.");
    }

    [Fact]
    public void WhenValidInputsAreGivenInDelete_InvalidOperationException_ShouldNotBeReturn()
    {
        var genre = new Genre(){Name = "TestName15"};
        _dbContext.Genres.Add(genre);
        _dbContext.SaveChanges();

        DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
        command.GenreId = genre.Id;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _dbContext.Genres.SingleOrDefault(x => x.Id == genre.Id);
        result.Should().BeNull();
    }
}