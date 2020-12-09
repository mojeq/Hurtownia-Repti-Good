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
            CreateMap<CustomerEntity, CustomerWithAddressesViewModel>()
                .ReverseMap();
            CreateMap<InvoiceAddressEntity, InvoiceAddressViewModel>()
                .ForMember(dest => dest.AddressId,
                    opt => opt.MapFrom(src => src.InvoiceAddressId))
                .ReverseMap();
            CreateMap<ShippingAddressEntity, ShippingAddressViewModel>()
                .ForMember(dest => dest.AddressId,
                    opt => opt.MapFrom(src => src.ShippingAddressId))
                .ReverseMap();
            CreateMap<CustomerEntity, CustomerViewModel>()
                .ReverseMap();
            CreateMap<ProductEntity, NewProductViewModel>()
                .ReverseMap();
            CreateMap<ProductEntity, ProductViewModel>()
                .ReverseMap();
            CreateMap<OrderViewModel, OrderEntity>();
            CreateMap<OrderEntity, OrderViewModel>()
                .ForMember(dest => dest.Customer,
                    opt => opt.MapFrom(src => src.Customer.CompanyName));                
            CreateMap<OrderEntity, ShippingAddressViewModel>()
                .ForMember(dest => dest.AddressId,
                    opt => opt.MapFrom(src => src.Customer.ShippingAddress.ShippingAddressId))
                .ForMember(dest => dest.City,
                    opt => opt.MapFrom(src => src.Customer.ShippingAddress.City))
                .ForMember(dest => dest.CompanyName,
                    opt => opt.MapFrom(src => src.Customer.ShippingAddress.CompanyName))
                .ForMember(dest => dest.CustomerName,
                    opt => opt.MapFrom(src => src.Customer.ShippingAddress.CustomerName))
                .ForMember(dest => dest.CustomerSurname,
                    opt => opt.MapFrom(src => src.Customer.ShippingAddress.CustomerSurname))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Customer.ShippingAddress.Email))
                .ForMember(dest => dest.Phone,
                    opt => opt.MapFrom(src => src.Customer.ShippingAddress.Phone))
                .ForMember(dest => dest.Street,
                    opt => opt.MapFrom(src => src.Customer.ShippingAddress.Street))
                .ForMember(dest => dest.ZipCode,
                    opt => opt.MapFrom(src => src.Customer.ShippingAddress.ZipCode))
                .ReverseMap();
                        CreateMap<OrderEntity, InvoiceAddressViewModel>()
                .ForMember(dest => dest.AddressId,
                    opt => opt.MapFrom(src => src.Customer.InvoiceAddress.InvoiceAddressId))
                .ForMember(dest => dest.City,
                    opt => opt.MapFrom(src => src.Customer.InvoiceAddress.City))
                .ForMember(dest => dest.CompanyName,
                    opt => opt.MapFrom(src => src.Customer.InvoiceAddress.CompanyName))
                .ForMember(dest => dest.CustomerName,
                    opt => opt.MapFrom(src => src.Customer.InvoiceAddress.CustomerName))
                .ForMember(dest => dest.CustomerSurname,
                    opt => opt.MapFrom(src => src.Customer.InvoiceAddress.CustomerSurname))
                .ForMember(dest => dest.NIP,
                    opt => opt.MapFrom(src => src.Customer.InvoiceAddress.NIP))
                .ForMember(dest => dest.Phone,
                    opt => opt.MapFrom(src => src.Customer.InvoiceAddress.Phone))
                .ForMember(dest => dest.Street,
                    opt => opt.MapFrom(src => src.Customer.InvoiceAddress.Street))
                .ForMember(dest => dest.ZipCode,
                    opt => opt.MapFrom(src => src.Customer.InvoiceAddress.ZipCode))
                .ReverseMap();
            CreateMap<OrderDetailEntity, ItemCartViewModel>()
                .ReverseMap();
            CreateMap<OrderDetailEntity, OrderDetailViewModel>()
                .ReverseMap();
        }
    }
}
