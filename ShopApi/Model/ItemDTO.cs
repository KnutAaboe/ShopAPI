using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Model
{
    //Use this class to verify that you can post and get the secret field.
    public class ItemDTO 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public string Location { get; set; }
    }
}
