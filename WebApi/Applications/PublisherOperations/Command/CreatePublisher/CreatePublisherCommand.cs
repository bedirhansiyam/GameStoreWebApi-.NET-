using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.PublisherOperations.Command.CreatePublisher;

public class CreatePublisherCommand
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreatePublisherModel Model { get; set; }

    public CreatePublisherCommand(IGameStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var publisher = _dbContext.Publishers.SingleOrDefault(x => x.Name == Model.Name);
        if(publisher is not null)
            throw new InvalidOperationException("The publisher already exist.");

        publisher = _mapper.Map<Publisher>(Model);
        _dbContext.Publishers.Add(publisher);
        _dbContext.SaveChanges();
    }

    public class CreatePublisherModel
    {
        public string Name { get; set; }
        public int NumberOfEmployees { get; set; }
        public DateTime FoundationDate { get; set; }
    }
}