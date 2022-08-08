namespace ExhibitionService.Controllers;

using AutoMapper;
using Data;
using FacadeModels;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/e/galleries")]
public class GalleriesController : ControllerBase
{
    private readonly IGalleryRepository _galleryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GalleriesController> _logger;
    
    public GalleriesController(
        IGalleryRepository galleryRepository,
        IMapper mapper,
        ILogger<GalleriesController> logger)
    {
        _galleryRepository = galleryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetGalleries(CancellationToken cancellationToken)
    {
        _logger.LogInformation("... Getting Galleries (Exhibition service)...");
        var galleries = await _galleryRepository.GetAllGalleries(cancellationToken);
        
        return Ok(_mapper.Map<List<GalleryResponse>>(galleries));
    }

    [HttpPost]
    public async Task<IActionResult> TestInboundConnection()
    {
        _logger.LogInformation("... --> Inbound POST # Exhibition Service");
        return Ok("Inbound test from Galleries controller");
    }
}
