using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup;

public static class Customers
{
    public static void AddCustomers(this GameStoreDbContext context)
    {
        context.Customers.AddRange(
            new Customer
            {
                Name = "Bedirhan",
                Surname = "Siyam",
                Email = "bedirhan@mail.com",
                Password = "123456789"
            },
            new Customer
            {
                Name = "Arda",
                Surname = "Güler",
                Email = "arda@mail.com",
                Password = "123456789"
            },
            new Customer
            {
                Name = "Enner",
                Surname = "Valencia",
                Email = "enner@mail.com",
                Password = "123456789"
            });
    }
}