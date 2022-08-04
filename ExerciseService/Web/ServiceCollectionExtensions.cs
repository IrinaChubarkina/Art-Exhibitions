namespace Web;

using TrainingProgramClient;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, TrainingClientConfiguration config)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddSingleton(config);
        services.AddHttpClient<ITrainingClient, TrainingClient>();
        
        return services;
    }
}
