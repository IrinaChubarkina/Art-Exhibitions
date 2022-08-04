namespace Web;

using Storage.Sql;
using TrainingProgramClient;

public sealed class AppSettings
{
    public SqlConfiguration SqlServer { get; init; } = null!;

    public TrainingClientConfiguration TrainingClient { get; init; } = null!;
}
