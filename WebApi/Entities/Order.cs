using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Order
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int GameId { get; set; }
    public Game Game { get; set; }
    public DateTime PurchasedDate { get; set; }
}