using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Publisher
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public int NumberOfEmployees { get; set; }
    public DateTime FoundationDate  { get; set; }
    public List<Game> Games { get; set; }
}