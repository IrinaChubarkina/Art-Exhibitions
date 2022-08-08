namespace RabbitMq;

using System.Text;
using System.Text.Json;
using Core.EventMessaging;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

public class MessageBusClient : IMessageBusClient
{
    private readonly RabbitMqConfiguration _config;
    private readonly ILogger<MessageBusClient> _logger;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageBusClient(RabbitMqConfiguration config, ILogger<MessageBusClient> logger)
    {
        _config = config;
        _logger = logger;

        var factory = new ConnectionFactory
                      {
                          HostName = config.Host,
                          Port = int.Parse(config.Port)
                      };
        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _connection.ConnectionShutdown += RabbitMq_ConnectionShutDown;
            _logger.LogInformation("Connected to Message Bus!");
        }
        catch (Exception e)
        {
            _logger.LogError("Could not connect to the message bus {e.Message}", e.Message);
        }
    }

    public void PublishNewGallery(GalleryPublishedDto gallery)
    {
        var message = JsonSerializer.Serialize(gallery);

        if (_connection.IsOpen)
        {
            _logger.LogInformation("RabbitMQ connection open, sending message...");
            SendMessage(message);
        }
        else
        {
            _logger.LogInformation("RabbitMQ connection closed, can not send the message");
        }
    }

    private void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(
            exchange: "trigger",
            routingKey: string.Empty,
            body: body);
        
        _logger.LogInformation("Message sent {message}", message);
    }

    private void Dispose()
    {
        _logger.LogInformation("Message bus disposed");

        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }

    private void RabbitMq_ConnectionShutDown(object? sender, ShutdownEventArgs e)
    {
        _logger.LogInformation("Connection shut down");
    }
}
