using ShopApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Data
{
    public class SqlItem : IItem
    {
        private readonly ItemContext _context;

        public SqlItem(ItemContext context)
        {
            _context = context;
        }

        public void CreateItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.Items.Add(item);

        }

        public void DeleteItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.Items.Remove(item);
        }

        public IEnumerable<Item> GetAllItems()
        {
            var items =_context.Items.ToList();
            return items;
        }

        public Item GetItemById(int id)
        {
            var WantedItem = _context.Items.FirstOrDefault(p => p.Id == id);
            return WantedItem;
        }

        public bool SaveChanges()
        {
           return (_context.SaveChanges()) >= 0;
        }

        public void UpdateItem(Item item)
        {
            //Nothing
        }
    }
}
