using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using static WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;

namespace Applications.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(1, "Na")]
    [InlineData(-1, "Name")]
    public void WhenInvalidInputsAreGiven_UpdateGenreValidator_ShouldBeReturnErrors(int genreId, string name)
    {   
        UpdateGenreCommand command = new UpdateGenreCommand(null, null);
        command.GenreId = genreId;
        command.Model = new UpdateGenreModel(){Name = name};

        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);
        
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_UpdateGenreValidator_ShouldNotBeReturnError()
    {
        UpdateGenreCommand command = new UpdateGenreCommand(null, null);
        command.GenreId = 1;
        command.Model = new UpdateGenreModel(){Name = "name"};

        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);
        
        result.Errors.Count.Should().Be(0);
    }
}
