using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup;

public static class Genres
{
    public static void AddGenres(this GameStoreDbContext context)
    {
        context.Genres.AddRange(
            new Genre{Name = "Action"},
            new Genre{Name = "Sports"},
            new Genre{Name = "Adventure"},
            new Genre{Name = "Role-Playing"},
            new Genre{Name = "First-Person Shooter"},
            new Genre{Name = "Real-Time Strategy"});
    }
}