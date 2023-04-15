using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations;

public class GameStoreDbContext : DbContext, IGameStoreDbContext
{
    public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options){}
    public DbSet<Game> Games { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Customer> Customers { get; set; }

    public int SaveChanges()
    {
        return base.SaveChanges();
    }
}