using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.PublisherOperations.Command.CreatePublisher;
using WebApi.Application.PublisherOperations.Command.DeletePublisher;
using WebApi.Application.PublisherOperations.Command.UpdatePublisher;
using WebApi.Application.PublisherOperations.Queries.GetPublisherDetail;
using WebApi.Application.PublisherOperations.Queries.GetPublishers;
using WebApi.DBOperations;
using static WebApi.Application.PublisherOperations.Command.CreatePublisher.CreatePublisherCommand;
using static WebApi.Application.PublisherOperations.Command.UpdatePublisher.UpdatePublisherCommand;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]s")]
public class PublisherController: ControllerBase
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public PublisherController(IGameStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetPublishers()
    {
        GetPublishersQuery query = new GetPublishersQuery(_dbContext, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("id")]
    public IActionResult GetPublisherDetail(int id)
    {
        GetPublisherDetailQuery query = new GetPublisherDetailQuery(_dbContext, _mapper);
        query.PublisherId = id;

        GetPublisherDetailQueryValidator validator = new GetPublisherDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var result = query.Handle();
        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddPublisher([FromBody] CreatePublisherModel newPublisher)
    {
        CreatePublisherCommand command = new CreatePublisherCommand(_dbContext, _mapper);
        command.Model = newPublisher;

        CreatePublisherCommandValidator validator = new CreatePublisherCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeletePublisher(int id)
    {
        DeletePublisherCommand command = new DeletePublisherCommand(_dbContext);
        command.PublisherId = id;

        DeletePublisherCommandValidator validator = new DeletePublisherCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdatePublisher(int id, [FromBody] UpdatePublisherModel updatedPublisher)
    {
        UpdatePublisherCommand command = new UpdatePublisherCommand(_dbContext, _mapper);
        command.PublisherId = id;
        command.Model = updatedPublisher;

        UpdatePublisherCommandValidator validator = new UpdatePublisherCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }
}