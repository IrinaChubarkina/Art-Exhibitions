namespace Web.ExhibitionClient;

using System.Text;
using System.Text.Json;
using Web.FacadeModels;

public class ExhibitionClient : IExhibitionClient
{
    private readonly HttpClient _httpClient;
    private readonly ExhibitionClientConfiguration _config;
    private readonly ILogger<ExhibitionClient> _logger;

    public ExhibitionClient(
        HttpClient httpClient,
        ExhibitionClientConfiguration config,
        ILogger<ExhibitionClient> logger)
    {
        _httpClient = httpClient;
        _config = config;
        _logger = logger;
    }

    public async Task SendGalleryToExhibition(GalleryResponse gallery)
    {
        var httpContent = new StringContent(
            JsonSerializer.Serialize(gallery),
            Encoding.UTF8,
            "application/json");

        var url = _config.ApiBaseUri + "/api/e/galleries";
        var response = await _httpClient.PostAsync(url, httpContent);

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Sync POST to Exhibition Service was OK!");
        }
        else
        {
            _logger.LogInformation("Sync POST to Exhibition Service has failed!");
        }
    }
}
