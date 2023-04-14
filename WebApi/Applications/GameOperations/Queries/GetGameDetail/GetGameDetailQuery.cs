using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.GameOperations.Queries.GetGameDetail;

public class GetGameDetailQuery
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public int GameId { get; set; }
    public GameDetailViewModel Model { get; set; }

    public GetGameDetailQuery(IGameStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public GameDetailViewModel Handle()
    {
        var game = _dbContext.Games.Include(x => x.Genre).Include(x => x.Publisher).Where(x => x.Id == GameId).SingleOrDefault();
        if(game is null)
            throw new InvalidOperationException("The game doesn't exist.");

        GameDetailViewModel vm = _mapper.Map<GameDetailViewModel>(game);

        return vm;
    }

    public class GameDetailViewModel
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public decimal Price { get; set; }
        public string ReleaseDate { get; set; }
    }
}