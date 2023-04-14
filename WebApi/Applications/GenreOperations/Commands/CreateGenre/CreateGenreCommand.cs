using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommand
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateGenreModel Model { get; set; }

    public CreateGenreCommand(IGameStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var genre = _dbContext.Genres.SingleOrDefault(x => x.Name == Model.Name);
        if(genre is not null)
            throw new InvalidOperationException("The genre already exist.");

        genre = _mapper.Map<Genre>(Model);
        _dbContext.Genres.Add(genre);
        _dbContext.SaveChanges();
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}