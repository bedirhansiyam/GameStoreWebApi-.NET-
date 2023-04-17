using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GenGenreDetail;

namespace Applications.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidInputsAreGiven_GenreDetailValidator_ShouldBeReturn()
    {
        GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
        query.GenreId = -1;

        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_GenreDetailValidator_ShouldNotBeReturn()
    {
        GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
        query.GenreId = 1;

        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);
    }
}