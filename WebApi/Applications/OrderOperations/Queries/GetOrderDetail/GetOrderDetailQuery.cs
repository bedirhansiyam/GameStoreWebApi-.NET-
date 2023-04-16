using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.OrderOperations.Queries.GetOrderDetail;

public class GetOrderDetailQuery
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public OrderDetailViewModel Model { get; set; }
    public int OrderId { get; set; }

    public GetOrderDetailQuery(IGameStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public OrderDetailViewModel Handle()
    {
        var order = _dbContext.Orders.Include(x => x.Customer).Include(x => x.Game).Where(x => x.Id == OrderId).SingleOrDefault();
        if(order is null)
            throw new InvalidOperationException("The order doesn't exist.");

        OrderDetailViewModel vm = _mapper.Map<OrderDetailViewModel>(order);

        return vm;
    }

    public class OrderDetailViewModel
    {
        public string GameName { get; set; }
        public string CustomerName { get; set; }
        public string PurchasedDate { get; set; }
    }
}