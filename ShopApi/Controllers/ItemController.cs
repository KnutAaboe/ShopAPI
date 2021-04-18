using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Model;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {

        private readonly IItem _context;
        private readonly IMapper _mapper;

        public ItemController(IItem context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        //GET api/items (async?)
        [HttpGet]
        public ActionResult<IEnumerator<ItemReadDTO>> GetAllItems()
        {
            var items = _context.GetAllItems();
            return Ok(_mapper.Map<IEnumerable<ItemReadDTO>>(items));
        }

        //GET api/items/{id}
        [HttpGet("{id}", Name = "GetItemById")]
        public ActionResult<IEnumerator<ItemReadDTO>> GetItemById(int id)
        {
            var item = _context.GetItemById(id);
            if (item != null)
            {
                return Ok(_mapper.Map<ItemReadDTO>(item));
            }
            return NotFound();

            
        }

        //POST api/items
        [HttpPost]
        public ActionResult<ItemReadDTO> CreateItem(ItemCreateDTO itemCreateDTO)
        {
            //Validation check on itemCreateDTO

            var itemModel = _mapper.Map<Item>(itemCreateDTO);
            _context.CreateItem(itemModel);
            _context.SaveChanges();

            var itemReadDTO = _mapper.Map<ItemReadDTO>(itemModel);
            
            //CreatedAtRoute - Name of route | Route data | Content value (body)
            return CreatedAtRoute(nameof(GetItemById), new { Id = itemReadDTO }, itemReadDTO);
        }

        //PUT api/item/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateItem(int id, ItemUpdateDTO itemUpdateDTO)
        {
            var itemModel = _context.GetItemById(id);
            if (itemModel == null)
            {
                return NotFound();
            }

            _mapper.Map(itemUpdateDTO, itemModel);
            _context.UpdateItem(itemModel);
            _context.SaveChanges();

            return NoContent();
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialItemUpdate(int id, JsonPatchDocument<ItemUpdateDTO> patchDoc)
        {
            var itemModel = _context.GetItemById(id);
            if (itemModel == null)
            {
                return NotFound();
            }

            var itemToPatch = _mapper.Map<ItemUpdateDTO>(itemModel);
            patchDoc.ApplyTo(itemToPatch, ModelState);
            if (!TryValidateModel(itemToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(itemToPatch, itemModel);
            _context.UpdateItem(itemModel);
            _context.SaveChanges();

            return NoContent();

        }

        //Delete api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(int id)
        {
            var itemModel = _context.GetItemById(id);
            if (itemModel == null)
            {
                return NotFound();
            }

            _context.DeleteItem(itemModel);
            _context.SaveChanges();

            return NoContent();
        }

    }
}

//private readonly ShopContext _context;

//        public ItemsController(ShopContext context)
//        {
//            _context = context;
//        }
//// GET: api/Items
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
//        {
//            var items = await _context.Items.ToListAsync();
//            return Ok(items);
//        }

//        // GET: api/Items/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Item>> GetItem(int id)
//        {
//            var item = await _context.Items.FindAsync(id);

//            if (item == null)
//            {
//                return NotFound();
//            }

//            return Ok(item); //return item

//        }

//        // PUT: api/Items/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutItem(int id, Item item) //Update
//        {
//            if (id != item.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(item).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ItemExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();


//        }

//        // POST: api/Items
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Item>> PostItem(Item item) //Create 
//        {
//            _context.Items.Add(item);
//            await _context.SaveChangesAsync();
//            var newItem = CreatedAtAction(nameof(GetItem), new { id = item.Id }, item); //nameof - To avoid hard-coding the action name
//            return Ok(newItem);

//        }

//        // DELETE: api/Items/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteItem(int id)
//        {
//            var item = await _context.Items.FindAsync(id);
            
//            if (item == null){
//                return NotFound();
//            }

//            _context.Items.Remove(item);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool ItemExists(int id)
//        {
//            return _context.Items.Any(e => e.Id == id);
//        }