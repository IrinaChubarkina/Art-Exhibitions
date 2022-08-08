namespace ExhibitionService.Models;

using System.ComponentModel.DataAnnotations;

public class Exhibition
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Artist { get; set; }

    [Required]
    public int GalleryId { get; set; }

    public Gallery Gallery {get; set;}
}
