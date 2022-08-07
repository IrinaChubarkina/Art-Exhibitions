namespace Web.FacadeModels;

using System.ComponentModel.DataAnnotations;

public class CreateGalleryRequest
{
    [Required(AllowEmptyStrings = false)]
    public string Name { get; init; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Address { get; set; }
    
    [Required(AllowEmptyStrings = false)]
    [EmailAddress]
    public string ContactEmail { get; set; }
}
