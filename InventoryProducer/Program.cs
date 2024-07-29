var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddMassTransit(x =>
{
    var kafkaConfig = builder.Configuration.GetSection("Kafka").Get<KafkaConfig>();

    x.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });

    x.AddRider(rider =>
    {
        rider.AddProducer<InventoryItemCreated>(KafkaTopics.InventoryItemCreated);
        rider.UsingKafka((context, k) =>
        {
            k.Host(kafkaConfig.Host);
        });
    });
});

builder.Services.AddScoped(typeof(Publisher<>));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseAuthorization();
app.UseHttpsRedirection();
app.Run();

// docker run -d --name zookeeper -p 2181:2181 -e ALLOW_ANONYMOUS_LOGIN=yes bitnami/zookeeper:3.7.1
// docker run -d --name kafka -p 9092:9092 --link zookeeper:zookeeper -e KAFKA_CFG_ZOOKEEPER_CONNECT=zookeeper:2181 -e ALLOW_PLAINTEXT_LISTENER=yes -e KAFKA_CFG_LISTENERS=PLAINTEXT://:9092 -e KAFKA_CFG_ADVERTISED_LISTENERS=PLAINTEXT://localhost:9092 bitnami/kafka:3.7.1
// 