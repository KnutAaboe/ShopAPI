using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Model
{
    public class ItemCreateDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int Amount { get; set; }

    }
}
