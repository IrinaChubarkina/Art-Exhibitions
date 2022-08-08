namespace Storage.Sql;

using Core.Repositories;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositories;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbServices(this IServiceCollection services, SqlConfiguration config)
    {
        services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(config.ConnectionString));

        services.AddScoped<IGalleryRepository, GalleryRepository>();

        return services;
    }
}
