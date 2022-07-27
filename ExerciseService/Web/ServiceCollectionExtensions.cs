namespace Web;

using TrainingProgramClient;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        var trainingClientConfiguration = configuration.GetSection("TrainingService").Get<TrainingClientConfiguration>();
        services.AddSingleton(trainingClientConfiguration);
        services.AddHttpClient<ITrainingClient, TrainingClient>();
        
        return services;
    }
}
