using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HurtowniaReptiGood.Models;
using HurtowniaReptiGood.Models.Entities;
using HurtowniaReptiGood.Models.ViewModels;

namespace HurtowniaReptiGood.Persistance
{
    public class Configuration : Profile
    {
        public Configuration()
        {
            CreateMap<ProductEntity, NewProductViewModel>()
                .ReverseMap();
            CreateMap<ProductEntity, ProductViewModel>()
                .ReverseMap();
            CreateMap<OrderEntity, OrderViewModel>()
                .ReverseMap();
            CreateMap<OrderDetailEntity, ItemCartViewModel>()
                .ReverseMap();
        }
    }
}
