namespace InventoryConsumer.Config
{
    public class KafkaConfig
    { 
        public string Host { get; set; }
        public string Topic { get; set; }

        public string GroupId { get; set; }
    }
}
