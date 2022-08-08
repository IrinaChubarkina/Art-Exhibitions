namespace ExhibitionService.FacadeModels;

using System.ComponentModel.DataAnnotations;

public class CreateExhibitionRequest
{
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Artist { get; set; } = null!;
}
