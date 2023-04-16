using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.Commands.CreateOrder;

public class CreateOrderCommand
{
    private readonly IGameStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateOrderModel Model { get; set; }

    public CreateOrderCommand(IGameStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var game = _dbContext.Games.SingleOrDefault(x => x.Id == Model.GameId);
        if(game is null)
            throw new InvalidOperationException("Game not found!");

        var order = _mapper.Map<Order>(Model);
        order.PurchasedDate = DateTime.Now.Date;
        order.CustomerId = LoginCustomer.loginCustomer.Id;

        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();
    }

    public class CreateOrderModel
    {
        public int GameId { get; set; }
    }
}