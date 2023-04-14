using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenres;

public class GetGenresQuery
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGenresQuery(IGameStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<GenreViewModel> Handle()
    {
        var genres = _dbContext.Genres.OrderBy(x => x.Id);
        List<GenreViewModel> vm = _mapper.Map<List<GenreViewModel>>(genres);

        return vm;
    }
    public class GenreViewModel
    {
        public string Name { get; set; }
    }
}