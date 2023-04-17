using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using static WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;

namespace Applications.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidInputsAreGiven_CreateGenreValidator_ShouldBeReturnErrors()
    {
        CreateGenreCommand command = new CreateGenreCommand(null, null);
        command.Model = new CreateGenreModel(){Name = "Na"};

        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_CreateGenreValidator_ShouldNotBeReturn()
    {
        CreateGenreCommand command = new CreateGenreCommand(null, null);
        command.Model = new CreateGenreModel(){Name = "TestName6"};

        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}