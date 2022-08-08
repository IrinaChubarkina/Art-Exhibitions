namespace ExhibitionService.AsyncMessaging;

using System.Text;
using EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class MessageBusSubscriber : BackgroundService
{
    private readonly IConfiguration _config;
    private readonly IEventProcessor _eventProcessor;
    private IConnection _connection;
    private IModel _channel;
    private string _queueName;
    private readonly ILogger<MessageBusSubscriber> _logger;

    public MessageBusSubscriber(
        IConfiguration config,
        IEventProcessor eventProcessor,
        ILogger<MessageBusSubscriber> logger)
    {
        _config = config;
        _eventProcessor = eventProcessor;
        _logger = logger;

        InitializeRabbitMq();
    }

    private void InitializeRabbitMq()
    {
        var factory = new ConnectionFactory
                      {
                          HostName = _config["RabbitMQHost"],
                          Port = int.Parse(_config["RabbitMQPort"])
                      };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        
        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
        _queueName = _channel.QueueDeclare().QueueName;

        _channel.QueueBind(queue: _queueName,
            exchange: "trigger",
            routingKey: string.Empty);

        _logger.LogInformation("...Listening on the Message Bus...");

        _connection.ConnectionShutdown += RabbitMq_ConnectionShutDown;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (ModuleHandle, ea) =>
        {
            _logger.LogInformation("Event received!");

            var body = ea.Body;
            var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

            await _eventProcessor.ProcessEvent(notificationMessage, stoppingToken);
        };

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        return Task.CompletedTask;
    }

    private void RabbitMq_ConnectionShutDown(object? sender, ShutdownEventArgs e)
    {
        _logger.LogInformation("Connection shut down");
    }

    public override void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
        
        base.Dispose();
    }
}
