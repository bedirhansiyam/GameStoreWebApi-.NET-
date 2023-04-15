using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.PublisherOperations.Queries.GetPublisherDetail;

public class GetPublisherDetailQuery
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public int PublisherId { get; set; }
    public PublisherDetailViewModel Model { get; set; }

    public GetPublisherDetailQuery(IGameStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public PublisherDetailViewModel Handle()
    {
        var publisher = _dbContext.Publishers.Include(x => x.Games).FirstOrDefault(x => x.Id == PublisherId);
        if(publisher is null)
            throw new InvalidOperationException("The publisher doesn't exist.");

        var vm = _mapper.Map<PublisherDetailViewModel>(publisher);

        return vm;
    }

    public class PublisherDetailViewModel
    {
        public string Name { get; set; }
        public int NumberOfEmployees { get; set; }
        public string FoundationDate { get; set; }
        public List<string> Games { get; set; }
    }
}