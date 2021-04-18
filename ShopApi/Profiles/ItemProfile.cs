using AutoMapper;
using ShopApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.NewFolder
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            //Source to target
            CreateMap<Item, ItemReadDTO>();
            CreateMap<ItemCreateDTO, Item>();
            CreateMap<ItemUpdateDTO, Item>();
            CreateMap<Item, ItemUpdateDTO>();
        }
    }
}
