using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Model
{
    public class ItemReadDTO
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }
    }
}
