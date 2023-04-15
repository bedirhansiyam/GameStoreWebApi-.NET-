using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations;

public interface IGameStoreDbContext
{
    DbSet<Game> Games { get; set;} 
    DbSet<Genre> Genres { get; set;} 
    DbSet<Publisher> Publishers { get; set;}
    DbSet<Customer> Customers { get; set;}

    int SaveChanges();
}