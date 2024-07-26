public class Publisher<T> where T : class
{
    private readonly ITopicProducer<T> _producer;

    public Publisher(ITopicProducer<T> producer)
    {
        _producer = producer;
    }

    public async Task PublishToTopic(T message)
    {
        await _producer.Produce(message);
    }

}
