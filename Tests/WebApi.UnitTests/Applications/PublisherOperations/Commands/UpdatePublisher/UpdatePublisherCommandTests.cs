using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.PublisherOperations.Command.UpdatePublisher;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.PublisherOperations.Command.UpdatePublisher.UpdatePublisherCommand;

namespace Applications.PublisherOperations.Command.UpdatePublisher;

public class UpdatePublisherCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly GameStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdatePublisherCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }
        
    [Fact]
    public void WhenDoesNotExistPublisherIdIsGivenInUpdate_InvalidOperationException_ShouldBeReturn()
    {
        var publisher = new Publisher(){Name = "TestName5", NumberOfEmployees = 154, FoundationDate = new DateTime(2002,02,12)};
        _dbContext.Publishers.Add(publisher);
        _dbContext.SaveChanges();

        UpdatePublisherCommand command = new UpdatePublisherCommand(_dbContext, _mapper);
        command.PublisherId = 656;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The publisher doesn't exist.");
    }

    [Fact]
    public void WhenValidPublisherIdIsGiven_InvalidOperationException_ShouldNotBeReturn()
    {
        var publisher = new Publisher(){Name = "TestName6", NumberOfEmployees = 154, FoundationDate = new DateTime(2002,02,12)};
        _dbContext.Publishers.Add(publisher);
        _dbContext.SaveChanges();

        UpdatePublisherCommand command = new UpdatePublisherCommand(_dbContext, _mapper);
        command.PublisherId = publisher.Id;
        command.Model = new UpdatePublisherModel(){Name = "TestName8", NumberOfEmployees = 5454, FoundationDate = new DateTime(1993,02,12)};

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _dbContext.Publishers.SingleOrDefault(x => x.Id == publisher.Id);
        result.Should().NotBeNull();
    }
}