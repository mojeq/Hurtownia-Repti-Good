using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HurtowniaReptiGood.Models.Entities;
using HurtowniaReptiGood.Models;
using AutoMapper;

namespace HurtowniaReptiGood.Models
{
    public class AppService
    {
        private readonly IMapper _mapper;
        private readonly MyContex _myContex;    
        public AppService(IMapper mapper, MyContex myContex)
        {
            _mapper = mapper;
            _myContex = myContex;
        }

        // get current logged user
        public CustomerEntity GetLoggedCustomer(string userLogged)
        {
            CustomerEntity loggedUser = _myContex.Customers.FirstOrDefault(a => a.UserName == userLogged);

            return loggedUser;
        }

        // get list with all products from database
        public ProductsListViewModel GetAllProducts()
        {
            var productsList = new ProductsListViewModel();

            var products = _myContex.Products
                .ToList();

            productsList.Products = _mapper.Map<List<ProductViewModel>>(products);

            return productsList;
        }

        // get products from manufcturer category
        public ProductsListViewModel GetProductsFromCategory(string manufacturer)
        {
            var productsList = new ProductsListViewModel();

            var products = _myContex.Products
                .Where(x => x.Manufacturer == manufacturer)
                .ToList();

            productsList.Products = _mapper.Map<List<ProductViewModel>>(products);

            return productsList;
        }
    }
}
