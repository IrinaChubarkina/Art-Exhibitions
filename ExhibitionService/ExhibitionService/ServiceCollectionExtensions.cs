namespace ExhibitionService;

using Data;
using Microsoft.EntityFrameworkCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        services.AddScoped<IExhibitionRepository, ExhibitionRepository>();
        services.AddScoped<IGalleryRepository, GalleryRepository>();
        
        services.AddDbContext<AppDbContext>(opt =>
            opt.UseInMemoryDatabase("InMemory"));
        
        return services;
    }
}
