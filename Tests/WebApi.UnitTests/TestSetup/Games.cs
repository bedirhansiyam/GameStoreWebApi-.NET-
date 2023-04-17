using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup;

public static class Games
{
    public static void AddGames(this GameStoreDbContext context)
    {
        context.Games.AddRange(
            new Game{Name = "Far Cry 6", Price = 60, ReleaseDate = new DateTime(2021,10,07), GenreId = 5, PublisherId = 1},
            new Game{Name = "The Witcher 3: Wild Hunt", Price = 60, ReleaseDate = new DateTime(2015,05,19), GenreId = 4, PublisherId = 3},
            new Game{Name = "Fifa 23", Price = 60, ReleaseDate = new DateTime(2022,09,30), GenreId = 2, PublisherId = 2},
            new Game{Name = "Assassin's Creed Valhalla", Price = 60, ReleaseDate = new DateTime(2020,11,10), GenreId = 1, PublisherId = 1});
    }
}