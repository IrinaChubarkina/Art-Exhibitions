using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

using AutoMapper;
using Core.EventMessaging;
using Core.Models;
using Core.Repositories;
using ExhibitionClient;
using FacadeModels;

[ApiController]
[Route("api/galleries")]
public class GalleryController : ControllerBase
{
    private readonly IGalleryRepository _galleryRepository;
    private readonly IMapper _mapper;
    private readonly IExhibitionClient _exhibitionClient;
    private readonly IMessageBusClient _messageBusClient;
    private readonly ILogger<GalleryController> _logger;

    public GalleryController(
        IGalleryRepository galleryRepository,
        IMapper mapper,
        IExhibitionClient exhibitionClient,
        IMessageBusClient messageBusClient,
        ILogger<GalleryController> logger)
    {
        _galleryRepository = galleryRepository;
        _mapper = mapper;
        _exhibitionClient = exhibitionClient;
        _messageBusClient = messageBusClient;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetGalleries(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all galleries...");
        var galleries = await _galleryRepository.GetAllGalleries(cancellationToken);
        
        return Ok(_mapper.Map<IEnumerable<GalleryResponse>>(galleries));
    }

    [HttpGet("{galleryId}")]
    public async Task<IActionResult> GetGallery(int galleryId, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting the gallery {galleryId}...",
            galleryId);

        var gallery = await _galleryRepository.GetGallery(galleryId, cancellationToken);

        if (gallery != null)
        {
            return Ok(_mapper.Map<GalleryResponse>(gallery));
        }

        _logger.LogInformation(
            "Client requested non-existing gallery {galleryId}",
            galleryId);
        return NotFound("Gallery not found");
    }

    [HttpPost]
    public async Task<IActionResult> CreateGallery(CreateGalleryRequest request, CancellationToken cancellationToken)
    {
        var gallery = _mapper.Map<Gallery>(request);
        await _galleryRepository.CreateGallery(gallery, cancellationToken);
        await _galleryRepository.SaveChanges(cancellationToken);

        var createdGallery = _mapper.Map<GalleryResponse>(gallery);

        // Send sync message
        try
        {
            await _exhibitionClient.SendGalleryToExhibition(createdGallery);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Could not send sync request to Training Service: {ex.Message}",
                ex.Message);
        }

        // Send async message

        try
        {
            var galleryPublishedDto = _mapper.Map<GalleryPublishedDto>(createdGallery);
            galleryPublishedDto.Event = "Platform_Published";
            _messageBusClient.PublishNewGallery(galleryPublishedDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Could not send async request to Training Service: {ex.Message}",
                ex.Message);
        }
        return CreatedAtAction(
            nameof(GetGallery),
            new { galleryId = createdGallery.Id },
            createdGallery);
    }
}
