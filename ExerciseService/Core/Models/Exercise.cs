namespace Core.Models;

using System.ComponentModel.DataAnnotations;

public class Exercise
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
  
    [Required]
    public TimeSpan Duration { get; set; }
}
