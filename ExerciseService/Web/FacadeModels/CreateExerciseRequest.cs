namespace Web.FacadeModels;

using System.ComponentModel.DataAnnotations;

public class CreateExerciseRequest
{
    [Required(AllowEmptyStrings = false)]
    public string Name { get; init; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Description { get; init; } = null!;
    
    public TimeSpan Duration { get; set; }
}
