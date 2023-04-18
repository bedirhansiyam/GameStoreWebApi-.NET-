using FluentAssertions;
using TestSetup;
using WebApi.Application.PublisherOperations.Queries.GetPublisherDetail;

namespace Applications.PublisherOperations.Queries.GetPublisherDetail;

public class GetPublisherDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidInputsAreGiven_PublisherDetailValidator_ShouldBeReturnError()
    {
        GetPublisherDetailQuery query = new GetPublisherDetailQuery(null, null);
        query.PublisherId = -1;

        GetPublisherDetailQueryValidator validator = new GetPublisherDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_PublisherDetailValidator_ShouldNotBeReturnError()
    {
        GetPublisherDetailQuery query = new GetPublisherDetailQuery(null, null);
        query.PublisherId = 1;

        GetPublisherDetailQueryValidator validator = new GetPublisherDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);
    }
}