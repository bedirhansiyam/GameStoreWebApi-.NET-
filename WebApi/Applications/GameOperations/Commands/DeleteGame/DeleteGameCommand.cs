using WebApi.DBOperations;

namespace WebApi.Application.GameOperations.Commands.DeleteGame;

public class DeleteGameCommand
{
    private readonly IGameStoreDbContext _dbContext;
    public int GameId { get; set; }

    public DeleteGameCommand(IGameStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var game = _dbContext.Games.SingleOrDefault(x => x.Id == GameId);
        if(game is null)
            throw new InvalidOperationException("The game doesn't exist.");

        _dbContext.Games.Remove(game);
        _dbContext.SaveChanges();
    }
}