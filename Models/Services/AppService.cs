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
using Microsoft.EntityFrameworkCore;
using HurtowniaReptiGood.Models.Interfaces;

namespace HurtowniaReptiGood.Models
{
    public class AppService : IAppService
    {
        private readonly IMapper _mapper;
        private readonly MyContex _myContex;    
        public AppService(IMapper mapper, MyContex myContex)
        {
            _mapper = mapper;
            _myContex = myContex;
        }

        // get current logged user
        public async Task<CustomerEntity> GetLoggedCustomer(string userLogged)
        {
            CustomerEntity loggedUser = await _myContex.Customers.FirstOrDefaultAsync(a => a.UserName == userLogged);

            return loggedUser;
        }

        // get list with all products from database
        public async Task<ProductsListViewModel> GetAllProducts()
        {
            var productsList = new ProductsListViewModel();

            var products = await _myContex.Products.ToListAsync();

            productsList.Products = _mapper.Map<List<ProductViewModel>>(products);

            return productsList;
        }

        // get products from manufcturer category
        public async Task<ProductsListViewModel> GetProductsFromCategory(string manufacturer)
        {
            var productsList = new ProductsListViewModel();

            var products = await _myContex.Products
                                .Where(x => x.Manufacturer == manufacturer)
                                .ToListAsync();

            productsList.Products = _mapper.Map<List<ProductViewModel>>(products);

            return productsList;
        }
    }
}
