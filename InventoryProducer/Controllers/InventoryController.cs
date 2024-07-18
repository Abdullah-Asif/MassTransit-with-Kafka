using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace InventoryProducer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController(IConfiguration configuration, ProducerService producerService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Inventory inventory)
    {
        var topic = configuration["Kafka:Topic"];
        await producerService.ProduceAsync(topic , JsonSerializer.Serialize(inventory));
        return Ok("Inventory sent to Kafka");
    }
    
}