using WebApi.DBOperations;

namespace WebApi.Application.PublisherOperations.Command.DeletePublisher;

public class DeletePublisherCommand
{
    private readonly IGameStoreDbContext _dbContext;
    public int PublisherId { get; set; }

    public DeletePublisherCommand(IGameStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var publisher = _dbContext.Publishers.SingleOrDefault(x => x.Id == PublisherId);
        if(publisher is null)
            throw new InvalidOperationException("The publisher doesn't exist.");

        _dbContext.Publishers.Remove(publisher);
        _dbContext.SaveChanges();
    }
}