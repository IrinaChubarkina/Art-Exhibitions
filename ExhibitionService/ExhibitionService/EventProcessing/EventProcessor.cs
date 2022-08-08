namespace ExhibitionService.EventProcessing;

using System.Text.Json;
using AsyncMessaging;
using AutoMapper;
using Data;
using Models;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;
    private readonly ILogger<EventProcessor> _logger;

    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper, ILogger<EventProcessor> logger)
    {
        _scopeFactory = scopeFactory;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task ProcessEvent(string message, CancellationToken cancellationToken)
    {
        var eventType = DetermineEvent(message);
        switch (eventType)
        {
            case EventType.GalleryPublished:
                await AddGallery(message, cancellationToken);
                break;
            default:
                break;
        }
    }

    private async Task AddGallery(string galleryPublishedMessage, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IGalleryRepository>();
        var galleryPublishedDto = JsonSerializer.Deserialize<GalleryPublishedDto>(galleryPublishedMessage);
        
        try
        {
            var gallery = _mapper.Map<Gallery>(galleryPublishedDto);
            var alreadyExists = await repository.ExternalGalleryExists(gallery.ExternalId, cancellationToken);
            
            if (alreadyExists)
            {
                _logger.LogInformation("Gallery already exists {gallery.ExternalId}", gallery.ExternalId);
            }
            else
            {
                await repository.CreateGallery(gallery, cancellationToken);
                await repository.SaveChanges(cancellationToken);
                _logger.LogInformation("Gallery added!");
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Could not add gallery to database: {ex.Message}", ex.Message);
            throw;
        }
    }
    
    private EventType DetermineEvent(string notificationMessage)
    {
        _logger.LogInformation("...Determining event");

        var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
        switch (eventType?.Event)
        {
            case "Gallery_Published":
                _logger.LogInformation("Gallery Published event detected");
                return EventType.GalleryPublished;

            default:
                _logger.LogInformation("Could not determine event type");
                return EventType.Undetermined;
        }
    }
}
