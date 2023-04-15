using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.PublisherOperations.Command.UpdatePublisher;

public class UpdatePublisherCommand
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public int PublisherId { get; set; }
    public UpdatePublisherModel Model { get; set; }

    public UpdatePublisherCommand(IGameStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var publisher = _dbContext.Publishers.SingleOrDefault(x => x.Id == PublisherId);
        if(publisher is null)
            throw new InvalidOperationException("The publisher doesn't exist.");

        _mapper.Map(Model, publisher);
        _dbContext.SaveChanges();
    }

    public class UpdatePublisherModel
    {
        public string Name { get; set; }
        public int NumberOfEmployees { get; set; }
        public DateTime FoundationDate { get; set; }
    }
}