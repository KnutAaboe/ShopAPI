using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopApi.Model
{
    public class ItemContext : DbContext
    {
        public ItemContext(DbContextOptions<ItemContext> opt) : base(opt)
        {

        }

        public DbSet<Item> Items { get; set; }
    }
}
