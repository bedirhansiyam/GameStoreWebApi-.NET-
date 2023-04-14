using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GameOperations.Commands.CreateGame;

public class CreateGameCommand
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateGameModel Model { get; set; }

    public CreateGameCommand(IGameStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var game = _dbContext.Games.SingleOrDefault(x => x.Name == Model.Name);
        if(game is not null)
            throw new InvalidOperationException("The game already exist.");

        game = _mapper.Map<Game>(Model);

        _dbContext.Games.Add(game);
        _dbContext.SaveChanges();
    }

    public class CreateGameModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int GenreId { get; set; }
        public int PublisherId { get; set; }
    }
}