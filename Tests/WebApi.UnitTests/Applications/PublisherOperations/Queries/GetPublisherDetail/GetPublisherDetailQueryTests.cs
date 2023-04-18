using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.PublisherOperations.Queries.GetPublisherDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.PublisherOperations.Queries.GetPublisherDetail;

public class GetPublisherDetailQueryTests: IClassFixture<CommonTestFixture>
{
    private readonly GameStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPublisherDetailQueryTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenPublisherIdIsNotMatchAnyPublisher_InvalidOperationExcepiton_ShouldBeReturn()
    {
        var publisher = new Publisher(){Name = "TestName9", NumberOfEmployees = 78, FoundationDate = new DateTime(1907,02,12)};
        _dbContext.Publishers.Add(publisher);
        _dbContext.SaveChanges();

        GetPublisherDetailQuery query = new GetPublisherDetailQuery(_dbContext, _mapper);
        query.PublisherId = 485;

        FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The publisher doesn't exist.");
    }

    [Fact]
    public void WhenGivenPublisherIdIsMatchAnyPublisher_InvalidOperationExcepiton_ShouldNotBeReturn()
    {
        var publisher = new Publisher(){Name = "TestName10", NumberOfEmployees = 778, FoundationDate = new DateTime(1987,02,12)};
        _dbContext.Publishers.Add(publisher);
        _dbContext.SaveChanges();

        GetPublisherDetailQuery query = new GetPublisherDetailQuery(_dbContext, _mapper);
        query.PublisherId = publisher.Id;

        FluentActions.Invoking(() => query.Handle()).Invoke();

        var result = _dbContext.Publishers.SingleOrDefault(x => x.Id == publisher.Id);
        result.Should().NotBeNull();
    }
}