using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;

namespace Applications.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly GameStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public CreateGenreCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        var genre = new Genre(){Name = "TestName"};
        _dbContext.Genres.Add(genre);
        _dbContext.SaveChanges();

        CreateGenreCommand command = new CreateGenreCommand(_dbContext, _mapper);
        command.Model = new CreateGenreModel(){Name = genre.Name};

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The genre already exist.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
    {
        CreateGenreCommand command = new CreateGenreCommand(_dbContext, _mapper);
        CreateGenreModel model = new CreateGenreModel(){Name = "TestName2"};
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _dbContext.Genres.SingleOrDefault(x => x.Name == model.Name);
        result.Should().NotBeNull();
    }
}