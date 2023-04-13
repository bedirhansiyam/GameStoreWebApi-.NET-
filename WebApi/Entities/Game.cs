using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Game
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    public Genre Genre { get; set; }
    public int GenreId { get; set; }
    public Publisher Publisher { get; set; }
    public int PublisherId { get; set; }

}