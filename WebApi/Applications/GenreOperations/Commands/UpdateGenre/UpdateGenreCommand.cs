using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommand
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public UpdateGenreModel Model { get; set; }
    public int GenreId { get; set; }

    public UpdateGenreCommand(IGameStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
        if(genre is null)
            throw new InvalidOperationException("The genre doesn't exist.");

        _mapper.Map(Model, genre);
        _dbContext.SaveChanges();
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
    }
}