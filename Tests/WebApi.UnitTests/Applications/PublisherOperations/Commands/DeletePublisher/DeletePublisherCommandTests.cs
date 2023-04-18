using FluentAssertions;
using TestSetup;
using WebApi.Application.PublisherOperations.Command.DeletePublisher;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.PublisherOperations.Command.DeletePublisher;

public class DeletePublisherCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly GameStoreDbContext _dbContext;

    public DeletePublisherCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
    }

    [Fact]
    public void WhenInvalidPublisherIdIsGivenInDelete_InvalidOperationException_ShouldBeReturn()
    {
        var publisher = new Publisher(){Name = "TestName8", NumberOfEmployees = 158, FoundationDate = new DateTime(2021,02,12)};
        _dbContext.Publishers.Add(publisher);
        _dbContext.SaveChanges();

        DeletePublisherCommand command = new DeletePublisherCommand(_dbContext);
        command.PublisherId = 154;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The publisher doesn't exist.");
    }

    [Fact]
    public void WhenValidPublisherIdIsGivenInDelete_InvalidOperationException_ShouldNotBeReturn()
    {
        var publisher = new Publisher(){Name = "TestName9", NumberOfEmployees = 18, FoundationDate = new DateTime(2020,02,12)};
        _dbContext.Publishers.Add(publisher);
        _dbContext.SaveChanges();

        DeletePublisherCommand command = new DeletePublisherCommand(_dbContext);
        command.PublisherId = publisher.Id;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _dbContext.Publishers.SingleOrDefault(x => x.Id == publisher.Id);
        result.Should().BeNull();
    }
}