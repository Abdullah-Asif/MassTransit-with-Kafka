//using Confluent.Kafka;

//namespace InventoryConsumer;

//public class ConsumerService : BackgroundService
//{
//    private readonly IConsumer<Ignore, string> _consumer;
//    private readonly IConfiguration _configuration;
//    private readonly ILogger<ConsumerService> _logger;

//    public ConsumerService(IConfiguration configuration, ILogger<ConsumerService> logger)
//    {
//        _configuration = configuration;
//        _logger = logger;
//        _consumer = new ConsumerBuilder<Ignore, string>(new ConsumerConfig
//        {
//            BootstrapServers = configuration["Kafka:BootstrapServers"],
//            GroupId = configuration["Kafka:GroupId"]
//        }).Build();
//    }

//    protected override Task ExecuteAsync(CancellationToken stoppingToken)
//    {
//        var topic = _configuration["Kafka:Topic"];
//        _consumer.Subscribe(topic);

//        return Task.Run(() =>
//        {
//            while (!stoppingToken.IsCancellationRequested)
//            {
//                var consumeResult = _consumer.Consume(stoppingToken);

//                _logger.LogInformation($"Consumed message '{consumeResult.Message.Value}' at: '{consumeResult.TopicPartitionOffset}'");

//                Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
//            }
//            _consumer.Close();
//        });
//    }
//}