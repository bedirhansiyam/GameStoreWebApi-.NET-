using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommand
{
    private readonly IGameStoreDbContext _dbContext;
    public int GenreId { get; set; }

    public DeleteGenreCommand(IGameStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
        if(genre is null)
            throw new InvalidOperationException("The genre doesn't exist.");

        var games = _dbContext.Games.Where(x => x.GenreId == GenreId).Any();
        if(games)
            throw new InvalidOperationException("There are/is game/s in this genre. To delete this genre, you must first delete the game/s.");

        _dbContext.Genres.Remove(genre);
        _dbContext.SaveChanges();
    }
}