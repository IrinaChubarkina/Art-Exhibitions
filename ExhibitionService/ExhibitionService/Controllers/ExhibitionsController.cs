using Microsoft.AspNetCore.Mvc;

namespace ExhibitionService.Controllers;

using AutoMapper;
using Data;
using FacadeModels;
using Models;

[ApiController]
[Route("api/e/galleries/{galleryId}/[controller]")]
public class ExhibitionsController : ControllerBase
{
    private readonly IExhibitionRepository _exhibitionRepository;
    private readonly IGalleryRepository _galleryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ExhibitionsController> _logger;

    public ExhibitionsController(
        IExhibitionRepository exhibitionRepository,
        IGalleryRepository galleryRepository,
        IMapper mapper,
        ILogger<ExhibitionsController> logger)
    {
        _exhibitionRepository = exhibitionRepository;
        _galleryRepository = galleryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetExhibitionsForGallery(int galleryId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("...Getting Exhibitions for Gallery {galleryId}", galleryId);
        
        if (!await _galleryRepository.GalleryExists(galleryId, cancellationToken))
        {
            _logger.LogInformation("Gallery {galleryId} does not exist", galleryId);
            return NotFound();
        }

        var exhibitions = await _exhibitionRepository.GetExhibitionsForGallery(galleryId, cancellationToken);

        return Ok(_mapper.Map<List<ExhibitionResponse>>(exhibitions));
    }
    
    [HttpGet("{exhibitionId}")]
    public async Task<IActionResult> GetExhibitionForGallery(int galleryId, int exhibitionId, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "...Getting Exhibition {exhibitionId} for Gallery {galleryId}",
            galleryId,
            exhibitionId);
        
        if (!await _galleryRepository.GalleryExists(galleryId, cancellationToken))
        {
            _logger.LogInformation("Gallery {galleryId} does not exist", galleryId);
            return NotFound();
        }

        var exhibition = await _exhibitionRepository.GetExhibition(galleryId, exhibitionId, cancellationToken);

        if (exhibition is null)
        {
            _logger.LogInformation("Exhibition {exhibitionId} does not exist", exhibitionId);
            return NotFound();
        }

        return Ok(_mapper.Map<ExhibitionResponse>(exhibition));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateExhibitionForGallery(int galleryId, CreateExhibitionRequest createExhibitionRequest, CancellationToken cancellationToken)
    {
        _logger.LogInformation("...Creating Exhibition for Gallery {galleryId}", galleryId);

        if (!await _galleryRepository.GalleryExists(galleryId, cancellationToken))
        {
            _logger.LogInformation("...Gallery {galleryId} does not exist", galleryId);
            return NotFound();
        }

        var exhibition = _mapper.Map<Exhibition>(createExhibitionRequest);
        await _exhibitionRepository.CreateExhibition(galleryId, exhibition, cancellationToken);
        await _exhibitionRepository.SaveChanges(cancellationToken);
        
        var createdExhibition = _mapper.Map<ExhibitionResponse>(exhibition);

        return CreatedAtAction(
            nameof(GetExhibitionForGallery),
            new { exhibitionId = createdExhibition.Id },
            createdExhibition);
    }
}
