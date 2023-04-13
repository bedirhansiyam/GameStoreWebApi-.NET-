using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using(var context = new GameStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<GameStoreDbContext>>()))
        {
            if(context.Genres.Any())
            {
                return;
            }
            else
            {
                context.Genres.AddRange(
                    new Genre{Name = "Action"},
                    new Genre{Name = "Sports"},
                    new Genre{Name = "Adventure"},
                    new Genre{Name = "Role-Playing"},
                    new Genre{Name = "First-Person Shooter"},
                    new Genre{Name = "Real-Time Strategy"});
            }
            context.SaveChanges();

            if(context.Games.Any())
            {
                return;
            }
            else
            {
                context.Games.AddRange(
                    new Game{Name = "Far Cry 6", Price = 60, ReleaseDate = new DateTime(2021,10,07), GenreId = 5, PublisherId = 1},
                    new Game{Name = "The Witcher 3: Wild Hunt", Price = 60, ReleaseDate = new DateTime(2015,05,19), GenreId = 4, PublisherId = 3},
                    new Game{Name = "Fifa 23", Price = 60, ReleaseDate = new DateTime(2022,09,30), GenreId = 2, PublisherId = 2},
                    new Game{Name = "Assassin's Creed Valhalla", Price = 60, ReleaseDate = new DateTime(2020,11,10), GenreId = 1, PublisherId = 1});
            }
            context.SaveChanges();

            if(context.Publishers.Any())
            {
                return;
            }
            else
            {
                Publisher publisher1 = new Publisher();
                publisher1.Name = "Ubisoft";
                publisher1.NumberOfEmployees = 20665;
                publisher1.FoundationDate = new DateTime(1986,03,28);
                publisher1.Games = context.Games.Where(x => x.PublisherId == publisher1.Id).ToList();
                context.Publishers.Add(publisher1);
                context.SaveChanges();

                Publisher publisher2 = new Publisher();
                publisher2.Name = "Electronic Arts";
                publisher2.NumberOfEmployees = 12900;
                publisher2.FoundationDate = new DateTime(1982,05,27);
                publisher2.Games = context.Games.Where(x => x.PublisherId == publisher2.Id).ToList();
                context.Publishers.Add(publisher2);
                context.SaveChanges();

                Publisher publisher3 = new Publisher();
                publisher3.Name = "CD Projekt RED";
                publisher3.NumberOfEmployees = 859;
                publisher3.FoundationDate = new DateTime(2002,02,12);
                publisher3.Games = context.Games.Where(x => x.PublisherId == publisher3.Id).ToList();
                context.Publishers.Add(publisher3);
                context.SaveChanges();
            }
        }
    }
}