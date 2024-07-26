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
        rider.AddProducer<DemoEvent>(kafkaConfig.Topic);

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