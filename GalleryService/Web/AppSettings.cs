namespace Web;

using ExhibitionClient;
using RabbitMq;
using Storage.Sql;

public sealed class AppSettings
{
    public SqlConfiguration SqlServer { get; init; } = null!;

    public ExhibitionClientConfiguration ExhibitionClient { get; init; } = null!;
    
    public RabbitMqConfiguration RabbitMq { get; init; } = null!;
}
