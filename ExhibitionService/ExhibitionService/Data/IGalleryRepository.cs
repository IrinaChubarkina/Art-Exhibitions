namespace ExhibitionService.Data;

using Models;

public interface IGalleryRepository
{
    Task<bool> SaveChanges(CancellationToken cancellationToken);
    Task<List<Gallery>> GetAllGalleries(CancellationToken cancellationToken);
    Task CreateGallery(Gallery gallery, CancellationToken cancellationToken);
    Task<bool> GalleryExists(int galleryId, CancellationToken cancellationToken);
    Task<bool> ExternalGalleryExists(int externalGalleryId, CancellationToken cancellationToken);
}
