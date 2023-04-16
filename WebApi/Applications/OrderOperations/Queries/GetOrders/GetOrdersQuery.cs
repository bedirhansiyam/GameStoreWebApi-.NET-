using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.OrderOperations.Queries.GetOrders;

public class GetOrdersQuery
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetOrdersQuery(IGameStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<OrderViewModel> Handle()
    {
        var orderList = _dbContext.Orders.Include(x => x.Customer).Include(x => x.Game).OrderBy(x => x.Id).ToList();

        List<OrderViewModel> vm = _mapper.Map<List<OrderViewModel>>(orderList);

        return vm;
    }

    public class OrderViewModel
    {
        public string GameName { get; set; }
        public string CustomerName { get; set; }
        public string PurchasedDate { get; set; }
    }
}