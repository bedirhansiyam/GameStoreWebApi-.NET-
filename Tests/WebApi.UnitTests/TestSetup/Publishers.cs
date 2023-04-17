using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup;

public static class Publishers
{
    public static void AddPublishers(this GameStoreDbContext context)
    {
        Publisher publisher1 = new Publisher();
                publisher1.Name = "Ubisoft";
                publisher1.NumberOfEmployees = 20665;
                publisher1.FoundationDate = new DateTime(1986,03,28);
                publisher1.Games = context.Games.Where(x => x.PublisherId == publisher1.Id).ToList();
                context.Publishers.Add(publisher1);

                Publisher publisher2 = new Publisher();
                publisher2.Name = "Electronic Arts";
                publisher2.NumberOfEmployees = 12900;
                publisher2.FoundationDate = new DateTime(1982,05,27);
                publisher2.Games = context.Games.Where(x => x.PublisherId == publisher2.Id).ToList();
                context.Publishers.Add(publisher2);

                Publisher publisher3 = new Publisher();
                publisher3.Name = "CD Projekt RED";
                publisher3.NumberOfEmployees = 859;
                publisher3.FoundationDate = new DateTime(2002,02,12);
                publisher3.Games = context.Games.Where(x => x.PublisherId == publisher3.Id).ToList();
                context.Publishers.Add(publisher3);
    }
}