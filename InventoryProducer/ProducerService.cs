using Confluent.Kafka;

namespace InventoryProducer;

public class ProducerService
{
    private readonly IConfiguration _configuration;
    private readonly IProducer<Null, string> _producer;
    
    public ProducerService(IConfiguration configuration)
    {
        _configuration = configuration;
        _producer = new ProducerBuilder<Null, string>(new ProducerConfig
        {
            BootstrapServers = _configuration["Kafka:BootstrapServers"]
        }).Build();
    }
    
    public async Task ProduceAsync(string topic, string message)
    {
        await _producer.ProduceAsync(topic, new Message<Null, string>
        {
            Value = message
        });

        Console.WriteLine("Message delivered");
    }
}