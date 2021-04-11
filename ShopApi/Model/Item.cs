using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public string Location { get; set; }
        public string Secret { get; set; } //Need to be hidden from app, but admin app could expose it
    }
}
