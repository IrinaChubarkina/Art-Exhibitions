namespace ExhibitionService.Data;

using Microsoft.EntityFrameworkCore;
using Models;

public class GalleryRepository : IGalleryRepository
{
    private readonly AppDbContext _context;

    public GalleryRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> SaveChanges(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken) >= 0;
    }

    public Task<List<Gallery>> GetAllGalleries(CancellationToken cancellationToken)
    {
        return _context.Galleries.ToListAsync(cancellationToken);
    }

    public async Task CreateGallery(Gallery gallery, CancellationToken cancellationToken)
    {
        if (gallery is null)
        {
            throw new ArgumentException("Parameter can not be null", nameof(gallery));
        }

        await _context.Galleries.AddAsync(gallery, cancellationToken);
    }

    public Task<bool> GalleryExists(int galleryId, CancellationToken cancellationToken)
    {
        return _context.Galleries.AnyAsync(x => x.Id == galleryId, cancellationToken);
    }

    public Task<bool> ExternalGalleryExists(int externalGalleryId, CancellationToken cancellationToken)
    {
        return _context.Galleries.AnyAsync(x => x.ExternalId == externalGalleryId, cancellationToken);
    }
}
