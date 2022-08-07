namespace Web;

using ExhibitionClient;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, AppSettings config)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddSingleton(config.ExhibitionClient);
        services.AddHttpClient<IExhibitionClient, ExhibitionClient.ExhibitionClient>();
        
        return services;
    }
}
