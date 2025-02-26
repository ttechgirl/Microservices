using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CatalogService.Models;
using CatalogService.Interfaces;


namespace CatalogService.Controllers
{
    [ApiController]
    [Route("catalog/items")]
    //[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class ItemsController : ControllerBase
    {
        private readonly ILogger<ItemsController> _logger;
        private readonly IItemsRepository _itemsRepository;

        public ItemsController(ILogger<ItemsController> logger, IItemsRepository itemsRepository)
        {
            _logger = logger;
            _itemsRepository = itemsRepository;
        }

        [HttpGet("get-items")]
        public async Task<ActionResult<IEnumerable<ItemsDto>>> GetItems()
        {
            var items = await _itemsRepository.GetAllAsync(); //map the entity to dto 
            if (items.Count <= 0)
                return Array.Empty<ItemsDto>();

            return items.Select(item => item.DtoMap()).ToList();
        }

        [HttpGet("get-item/{id}")]
        public async Task<ActionResult<ItemsDto>> GetItemById(Guid id)
        {
            var item = (await _itemsRepository.GetAsync(id))
                       .DtoMap(); //map the entity to dto 
            if (item == null)
                return NotFound();

            return item;
        }

        [HttpPost("create-item")]
        public async Task<ActionResult<ItemsDto>> CreateItem(CreateItemsRequest request)
        {
            var item = await _itemsRepository.CreateAsync(request);
            if (item == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }

        [HttpPut("update-item")]
        public async Task<ActionResult<ItemsDto>> UpdateItem(UpdateItemsRequest request)
        {
            var existingItem = await GetItemById(request.Id);
            if (existingItem == null)
                return NotFound();

            return await _itemsRepository.UpdateAsync(request) == null ? BadRequest()
            : NoContent();
        }

        [HttpDelete("remove-item/{id}")]
        public async Task<ActionResult> RemoveItem(Guid id)
        {
            var existingItem = await GetItemById(id);
            if (existingItem == null)
                return NotFound();

            await _itemsRepository.RemoveAsync(id);
            return NoContent();
        }

        // private string GetDebuggerDisplay()
        // {
        //     return ToString();
        // }
    }
}