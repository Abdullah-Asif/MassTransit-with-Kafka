var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddMassTransit(x =>
{
    var kafkaConfig = builder.Configuration.GetSection("Kafka").Get<KafkaConfig>();

    if (kafkaConfig == null)
    {
        throw new InvalidOperationException("Kafka configuration is missing.");
    }

    x.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });

    x.AddRider(rider =>
    {
        rider.AddConsumer<InventoryItemConsumer>();

        rider.UsingKafka((context, k) =>
        {
            k.Host(kafkaConfig.Host);
            k.TopicEndpoint<InventoryItemCreated>(kafkaConfig.Topic, nameof(InventoryItemConsumer), e =>
            {
                e.ConfigureConsumer<InventoryItemConsumer>(context);
            });
        });
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();