namespace Core.Models;

using System.ComponentModel.DataAnnotations;

public class Gallery
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Address { get; set; }
  
    [Required]
    public string ContactEmail { get; set; }
}
