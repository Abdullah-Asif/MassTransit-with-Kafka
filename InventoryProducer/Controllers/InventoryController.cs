using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace InventoryProducer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Publisher<DemoEvent> _publisher;

        public InventoryController(IConfiguration configuration, Publisher<DemoEvent> publisher)
        {
            _configuration = configuration;
            _publisher = publisher;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InventoryItemCreated inventory)
        {
            await _publisher.PublishToTopic(inventory);
            return Ok("Inventory sent to Kafka");
        }
    }
}