using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GameOperations.Commands.CreateGame;
using WebApi.Application.GameOperations.Commands.DeleteGame;
using WebApi.Application.GameOperations.Commands.UpdateGame;
using WebApi.Application.GameOperations.Queries.GetGameDetail;
using WebApi.Application.GameOperations.Queries.GetGames;
using WebApi.DBOperations;
using static WebApi.Application.GameOperations.Commands.CreateGame.CreateGameCommand;
using static WebApi.Application.GameOperations.Commands.UpdateGame.UpdateGameCommand;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]s")]
public class GameController: ControllerBase
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GameController(IGameStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetGames()
    {
        GetGamesQuery query = new GetGamesQuery(_dbContext, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("id")]
    public IActionResult GetGameDetail(int id)
    {
        GetGameDetailQuery query = new GetGameDetailQuery(_dbContext, _mapper);
        query.GameId = id;

        GetGameDetailQueryValidator validator = new GetGameDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var result = query.Handle();
        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddGame([FromBody] CreateGameModel newGame)
    {
        CreateGameCommand command = new CreateGameCommand(_dbContext, _mapper);
        command.Model = newGame;

        CreateGameCommandValidator validator = new CreateGameCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteGame(int id)
    {
        DeleteGameCommand command = new DeleteGameCommand(_dbContext);
        command.GameId = id;

        DeleteGameCommandValidator validator = new DeleteGameCommandValidator();
        validator.ValidateAndThrow(command);
        
        command.Handle();

        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateGame(int id, [FromBody] UpdateGameModel updatedGame)
    {
        UpdateGameCommand command = new UpdateGameCommand(_dbContext, _mapper);
        command.GameId = id;
        command.Model = updatedGame;

        UpdateGameCommandValidator validator = new UpdateGameCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }
}