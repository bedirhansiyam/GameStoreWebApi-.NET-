using FluentAssertions;
using TestSetup;
using WebApi.Application.GameOperations.Queries.GetGameDetail;

namespace Applications.GameOperations.Queries.GetGameDetail;

public class GetGameDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidGameIdIsGiven_GameDetailValidator_ShouldBeReturnError()
    {
        GetGameDetailQuery query = new GetGameDetailQuery(null, null);
        query.GameId = -1;

        GetGameDetailQueryValidator validator = new GetGameDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidGameIdIsGiven_GameDetailValidator_ShouldNotBeReturnError()
    {
        GetGameDetailQuery query = new GetGameDetailQuery(null, null);
        query.GameId = 1;

        GetGameDetailQueryValidator validator = new GetGameDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);
    }
}