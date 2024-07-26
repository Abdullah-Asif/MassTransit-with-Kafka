using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace InventoryConsumer
{
    
    public class InventoryItemConsumer : IConsumer<InventoryItemCreated>
    {
        private readonly ILogger<InventoryItemConsumer> _logger;

        public InventoryItemConsumer(ILogger<InventoryItemConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<InventoryItemCreated> context)
        {
            var message = context.Message;
            _logger.LogInformation($"Received message: ItemId={message.ItemId}, Name={message.Name}, Quantity={message.Quantity}");

            // Process the message
            await Task.CompletedTask;
        }
    }
}