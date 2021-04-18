using ShopApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Data
{
    public interface IItem
    {
        bool SaveChanges();

        IEnumerable<Item> GetAllItems();
        Item GetItemById(int id);
        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Item item);
    }
}
