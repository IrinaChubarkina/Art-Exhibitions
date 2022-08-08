namespace ExhibitionService.Models;

using System.ComponentModel.DataAnnotations;

public class Gallery
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int ExternalId { get; set; }

    [Required]
    public string Name { get; set; }

    public ICollection<Exhibition> Exhibitions { get; set; } = new List<Exhibition>();
}