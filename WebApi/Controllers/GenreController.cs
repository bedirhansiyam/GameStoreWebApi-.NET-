using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GenGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DBOperations;
using static WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using static WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]s")]
public class GenreController: ControllerBase
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GenreController(IGameStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetGenres()
    {
        GetGenresQuery query = new GetGenresQuery(_dbContext, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("id")]
    public IActionResult GenGenreDetail(int id)
    {
        GetGenreDetailQuery query = new GetGenreDetailQuery(_dbContext, _mapper);
        query.GenreId = id;

        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var result = query.Handle();
        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
    {
        CreateGenreCommand command = new CreateGenreCommand(_dbContext, _mapper);
        command.Model = newGenre;

        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

     [HttpDelete("id")]
     public IActionResult DeleteGenre(int id)
     {
        DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
        command.GenreId = id;

        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
     }

     [HttpPut("id")]
     public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updatedGenre)
     {
        UpdateGenreCommand command = new UpdateGenreCommand(_dbContext, _mapper);
        command.GenreId = id;
        command.Model = updatedGenre;

        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
     }
}