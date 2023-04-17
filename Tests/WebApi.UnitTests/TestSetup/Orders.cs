using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup;

public static class Orders
{
    public static void AddOrders(this GameStoreDbContext context)
    {
        context.Orders.AddRange(
            new Order
            {
                GameId = 1,
                CustomerId = 2,                
            },
            new Order
            {
                GameId = 3,
                CustomerId = 1,                
            });
    }
}