namespace Core.Repositories;

using Models;

public interface IGalleryRepository
{
    Task<bool> SaveChanges(CancellationToken cancellationToken);

    Task<List<Gallery>> GetAllGalleries(CancellationToken cancellationToken);

    Task<Gallery?> GetGallery(int galleryId, CancellationToken cancellationToken);

    Task CreateGallery(Gallery gallery, CancellationToken cancellationToken);
}
