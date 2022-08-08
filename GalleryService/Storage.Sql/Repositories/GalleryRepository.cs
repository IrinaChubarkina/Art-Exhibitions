namespace Storage.Sql.Repositories;

using Core.Models;
using Core.Repositories;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class GalleryRepository : IGalleryRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<GalleryRepository> _logger;

    public GalleryRepository(AppDbContext context, ILogger<GalleryRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> SaveChanges(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken) >= 0;
    }

    public Task<List<Gallery>> GetAllGalleries(CancellationToken cancellationToken)
    {
        return _context.Galleries.ToListAsync(cancellationToken);
    }

    public Task<Gallery?> GetGallery(int galleryId, CancellationToken cancellationToken)
    {
        return _context.Galleries.FirstOrDefaultAsync(x => x.Id == galleryId, cancellationToken);
    }

    public async Task CreateGallery(Gallery gallery, CancellationToken cancellationToken)
    {
        if (gallery is null)
        {
            _logger.LogError("Can not create gallery, parameter is missing");
            throw new ArgumentNullException(nameof(gallery));
        }

        await _context.Galleries.AddAsync(gallery, cancellationToken);    
    }
}
