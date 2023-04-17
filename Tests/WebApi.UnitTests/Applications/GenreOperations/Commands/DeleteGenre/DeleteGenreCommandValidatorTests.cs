using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;

namespace Applications.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidInputsAreGiven_DeleteGenreValidator_ShouldBeReturnErrors()
    {
        DeleteGenreCommand command = new DeleteGenreCommand(null);
        command.GenreId = -1;

        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_DeleteGenreValidator_ShouldNotBeReturnErrors()
    {
        DeleteGenreCommand command = new DeleteGenreCommand(null);
        command.GenreId = 1;

        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}