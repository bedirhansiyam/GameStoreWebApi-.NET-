using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GameOperations.Commands.UpdateGame;

public class UpdateGameCommand
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public int GameId { get; set; }
    public UpdateGameModel Model { get; set; }

    public UpdateGameCommand(IGameStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var game = _dbContext.Games.SingleOrDefault(x => x.Id == GameId);
        if(game is null)
            throw new InvalidOperationException("The game doesn't exist.");

        _mapper.Map(Model, game);
        _dbContext.SaveChanges();
    }

    public class UpdateGameModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int GenreId { get; set; }
        public int PublisherId { get; set; }
    }
}