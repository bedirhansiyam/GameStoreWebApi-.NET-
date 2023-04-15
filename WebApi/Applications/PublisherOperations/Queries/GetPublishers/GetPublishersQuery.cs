using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.PublisherOperations.Queries.GetPublishers;

public class GetPublishersQuery
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public PublisherViewModel Model { get; set; }

    public GetPublishersQuery(IGameStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<PublisherViewModel> Handle()
    {
        var publishers = _dbContext.Publishers.Include(x => x.Games).OrderBy(x => x.Id).ToList();
        var games = _dbContext.Games.Where(x => x.Id == x.PublisherId).ToList();

        List<PublisherViewModel> vm = _mapper.Map<List<PublisherViewModel>>(publishers);

        return vm;
    }

    public class PublisherViewModel
    {
        public string Name { get; set; }
        public int NumberOfEmployees { get; set; }
        public string FoundationDate { get; set; }
        public List<string> Games { get; set; }
    }
}