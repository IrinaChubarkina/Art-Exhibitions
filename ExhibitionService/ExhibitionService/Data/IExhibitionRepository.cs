namespace ExhibitionService.Data;

using Models;

public interface IExhibitionRepository
{
    Task<bool> SaveChanges(CancellationToken cancellationToken);
    Task<List<Exhibition>> GetExhibitionsForGallery(int galleryId, CancellationToken cancellationToken);
    Task<Exhibition?> GetExhibition(int galleryId, int exhibitionId, CancellationToken cancellationToken);
    Task CreateExhibition(int galleryId, Exhibition exhibition, CancellationToken cancellationToken);
}
