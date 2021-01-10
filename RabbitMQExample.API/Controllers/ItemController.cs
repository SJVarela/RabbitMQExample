using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQExample.API.Contracts.Services;
using RabbitMQExample.API.Models;

namespace RabbitMQExample.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly ILogger<ItemController> _logger;

        public ItemController(ILogger<ItemController> logger, IItemService itemService)
        {
            _logger = logger;
            _itemService = itemService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ItemDto), 200)]
        public IActionResult SaveItem([FromBody] ItemDto item)
        {
            var result = _itemService.SaveItem(item);
            return Ok(result);
        }
    }
}