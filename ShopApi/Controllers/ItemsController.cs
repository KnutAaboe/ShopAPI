using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.Model;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ShopContext _context;

        public ItemsController(ShopContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems()
        {
            return await _context.Items.Select(x => ItemToDTO(x)).ToListAsync(); //_context.Items.ToListAsync();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return ItemToDTO(item); //return item

        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, ItemDTO itemDTO) //Update
        {
            //if (id != item.Id)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(item).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ItemExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();

            if (id != itemDTO.Id)
            {
                return BadRequest();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound(); 
            }

            item.Name = itemDTO.Name;
            item.Amount = itemDTO.Amount;
            item.Location = itemDTO.Location;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ItemExists(id))
            {
                return NotFound();
            }

            return NoContent();


        }

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemDTO>> PostItem(ItemDTO itemDTO) //Create 
        {
            //_context.Items.Add(item);
            //await _context.SaveChangesAsync();

            ////return CreatedAtAction("GetItem", new { id = item.Id }, item);
            //return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item); //nameof - To avoid hard-coding the action name 

            var item = new Item
            {
                Name = itemDTO.Name,
                Amount = itemDTO.Amount,
                Location = itemDTO.Location,
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetItem),
                new { Id = item.Id },
                ItemToDTO(item));

        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }

        private static ItemDTO ItemToDTO(Item Item) =>
    new ItemDTO
    {
        Id = Item.Id,
        Name = Item.Name,
        Amount = Item.Amount,
        Location = Item.Location
    };

    }
}