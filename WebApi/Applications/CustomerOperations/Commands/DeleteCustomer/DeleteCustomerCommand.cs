using WebApi.DBOperations;

namespace WebApi.Application.CustomerOperations.Commands.DeleteCustomer;

public class DeleteCustomerCommand
{
    private readonly IGameStoreDbContext _dbContext;
    public int CustomerId { get; set; }
    public DeleteCustomerModel Model { get; set; }

    public DeleteCustomerCommand(IGameStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var customer = _dbContext.Customers.SingleOrDefault(x => x.Id == CustomerId);
        if(customer is null)
            throw new InvalidOperationException("The customer doesn't exist.");

        if(customer.Password == Model.Password)
            _dbContext.Customers.Remove(customer);
        else
            throw new InvalidOperationException("Wrong password!");

        _dbContext.SaveChanges();
    }

    public class DeleteCustomerModel
    {
        public string Password { get; set; }
    }
}