using FluentAssertions;
using TestSetup;
using WebApi.Application.OrderOperations.Queries.GetOrderDetail;

namespace Applications.OrderOperations.Queries.GetOrderDetail;

public class GetOrderDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidInputsAreGiven_OrderDetailValidator_ShouldBeReturnError()
    {
        GetOrderDetailQuery query = new GetOrderDetailQuery(null, null);
        query.OrderId = -1;

        GetOrderDetailQueryValidator validator = new GetOrderDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_OrderDetailValidator_ShouldNotBeReturnError()
    {
        GetOrderDetailQuery query = new GetOrderDetailQuery(null, null);
        query.OrderId = 1;

        GetOrderDetailQueryValidator validator = new GetOrderDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);
    }
}