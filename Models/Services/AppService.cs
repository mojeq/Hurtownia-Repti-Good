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
using HurtowniaReptiGood.Models.Repositories;
using HurtowniaReptiGood.Models.Interfaces.Repositories;

namespace HurtowniaReptiGood.Models
{
    public class AppService : IAppService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;   
        public AppService(ICustomerRepository customerRepository, IProductRepository productRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        // get current logged user
        public async Task<CustomerEntity> GetLoggedCustomer(string userLogged)
        {
            var loggedUser = await _customerRepository.GetByFieldAsync(predicate: a => a.UserName == userLogged);

            return loggedUser;
        }

        // get list with all products from database
        public async Task<ProductsListViewModel> GetAllProducts()
        {
            var productsList = new ProductsListViewModel();

            var products = await _productRepository.GetAllAsync();

            productsList.Products = _mapper.Map<List<ProductViewModel>>(products);

            return productsList;
        }

        // get products from manufcturer category
        public async Task<ProductsListViewModel> GetProductsFromCategory(string manufacturer)
        {
            var productsList = new ProductsListViewModel();

            var products = await _productRepository.GetAsync(predicate: x => x.Manufacturer == manufacturer);

            productsList.Products = _mapper.Map<List<ProductViewModel>>(products);

            return productsList;
        }
    }
}
