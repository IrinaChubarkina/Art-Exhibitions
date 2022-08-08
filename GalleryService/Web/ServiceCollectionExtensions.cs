namespace Web;

using ExhibitionClient;
using RabbitMq;
using Storage.Sql;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, AppSettings config)
    {
        services.AddDbServices(config.SqlServer);
        services.AddRabbitMq(config.RabbitMq);

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddSingleton(config.ExhibitionClient);
        services.AddHttpClient<IExhibitionClient, ExhibitionClient.ExhibitionClient>();
        
        return services;
    }
}
