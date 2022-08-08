namespace ExhibitionService.Data;

using Microsoft.EntityFrameworkCore;
using Models;

public class ExhibitionRepository : IExhibitionRepository
{
    private readonly AppDbContext _context;

    public ExhibitionRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<bool> SaveChanges(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken) >= 0;
    }

    public Task<List<Exhibition>> GetExhibitionsForGallery(int galleryId, CancellationToken cancellationToken)
    {
        return _context.Exhibitions
            .Where(x => x.GalleryId == galleryId)
            .OrderBy(x => x.Artist)
            .ToListAsync(cancellationToken);
    }

    public Task<Exhibition?> GetExhibition(int galleryId, int exhibitionId, CancellationToken cancellationToken)
    {
        return _context.Exhibitions
            .Where(x => x.GalleryId == galleryId && x.Id == exhibitionId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateExhibition(int galleryId, Exhibition exhibition, CancellationToken cancellationToken)
    {
        if (exhibition == null)
        {
            throw new ArgumentNullException(nameof(exhibition));
        }

        exhibition.GalleryId = galleryId;
        await _context.Exhibitions.AddAsync(exhibition, cancellationToken);
    }
}
