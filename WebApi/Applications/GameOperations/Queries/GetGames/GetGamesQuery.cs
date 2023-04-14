using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.GameOperations.Queries.GetGames;

public class GetGamesQuery
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public GameViewModel Model { get; set; }

    public GetGamesQuery(IGameStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<GameViewModel> Handle()
    {
        var games = _dbContext.Games.Include(x => x.Genre).Include(x => x.Publisher).OrderBy(x => x.Id).ToList();

        List<GameViewModel> vm = _mapper.Map<List<GameViewModel>>(games);

        return vm;
    }

    public class GameViewModel
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public decimal Price { get; set; }
        public string ReleaseDate { get; set; }
    }
}