using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace TestSetup;

public class CommonTestFixture
{
    public GameStoreDbContext Context { get; set; }
    public IMapper Mapper { get; set; }

    public CommonTestFixture()
    {
        var options = new DbContextOptionsBuilder<GameStoreDbContext>().UseInMemoryDatabase(databaseName: "GameStoreDb").Options;
        
        Context = new GameStoreDbContext(options);

        Context.Database.EnsureCreated();
        Context.AddCustomers();
        Context.AddGames();
        Context.AddGenres();
        Context.AddOrders();
        Context.AddPublishers();

        Mapper = new MapperConfiguration(cfg => {cfg.AddProfile<MappingProfile>();}).CreateMapper();
    }

}