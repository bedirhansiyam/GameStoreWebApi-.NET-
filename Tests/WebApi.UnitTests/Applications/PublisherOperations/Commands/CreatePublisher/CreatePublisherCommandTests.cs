using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.PublisherOperations.Command.CreatePublisher;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.PublisherOperations.Command.CreatePublisher.CreatePublisherCommand;

namespace Applications.PublisherOperations.Command.CreatePublisher;

public class CreatePublisherCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly GameStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreatePublisherCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistPublisherNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        var publisher = new Publisher(){Name = "TestName1", NumberOfEmployees = 1580, FoundationDate = new DateTime(2022,02,12)};
        _dbContext.Publishers.Add(publisher);
        _dbContext.SaveChanges();

        CreatePublisherCommand command = new CreatePublisherCommand(_dbContext, _mapper);
        command.Model = new CreatePublisherModel(){Name = publisher.Name, NumberOfEmployees = 3625, FoundationDate = new DateTime(2002,02,12)};

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The publisher already exist.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Publisher_ShouldBeCreated()
    {
        CreatePublisherCommand command = new CreatePublisherCommand(_dbContext, _mapper);
        command.Model = new CreatePublisherModel(){Name = "TestName2", NumberOfEmployees = 10000, FoundationDate = new DateTime(2012,08,15)};

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _dbContext.Publishers.SingleOrDefault(x => x.Name == command.Model.Name);
        result.Should().NotBeNull();
    }  
}