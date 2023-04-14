using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries.GenGenreDetail;

public class GetGenreDetailQuery
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public int GenreId { get; set; }

    public GetGenreDetailQuery(IGameStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public GenreDetailViewModel Handle()
    {
        var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
        if(genre is null)
            throw new InvalidOperationException("The genre doesn't exist.");

        GenreDetailViewModel vm = _mapper.Map<GenreDetailViewModel>(genre);
        return vm;
    }

    public class GenreDetailViewModel
    {
        public string Name { get; set; }
    }
}