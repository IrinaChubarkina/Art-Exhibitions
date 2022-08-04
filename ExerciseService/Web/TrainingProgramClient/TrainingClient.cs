namespace Web.TrainingProgramClient;

using System.Text;
using System.Text.Json;
using Web.FacadeModels;

public class TrainingClient : ITrainingClient
{
    private readonly HttpClient _httpClient;
    private readonly TrainingClientConfiguration _config;
    private readonly ILogger<TrainingClient> _logger;

    public TrainingClient(
        HttpClient httpClient,
        TrainingClientConfiguration config,
        ILogger<TrainingClient> logger)
    {
        _httpClient = httpClient;
        _config = config;
        _logger = logger;
    }

    public async Task SendExerciseToTraining(ExerciseResponse exercise)
    {
        var httpContent = new StringContent(
            JsonSerializer.Serialize(exercise),
            Encoding.UTF8,
            "application/json");

        var url = _config.ApiBaseUri + "/api/tr/exercises";
        var response = await _httpClient.PostAsync(url, httpContent);

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Sync POST to Training Program Service was OK!");
        }
        else
        {
            _logger.LogInformation("Sync POST to Training Program Service has failed!");
        }
    }
}
