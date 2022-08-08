namespace ExhibitionService.EventProcessing;

public interface IEventProcessor
{
    Task ProcessEvent(string message, CancellationToken cancellationToken);
}
