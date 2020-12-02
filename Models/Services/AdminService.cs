using AutoMapper;
using HurtowniaReptiGood.Models.Entities;
using HurtowniaReptiGood.Models.Interfaces;
using HurtowniaReptiGood.Models.Interfaces.Repositories;
using HurtowniaReptiGood.Models.Repositories;
using HurtowniaReptiGood.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Services
{
    public class AdminService : IAdminService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public AdminService(ICustomerRepository customerRepository, IProductRepository productRepository, IOrderDetailRepository orderDetailRepository, IOrderRepository orderRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        // adding new product to database
        public async Task AddNewProduct(NewProductViewModel newProduct)
        {
            ProductEntity newProductEntity = _mapper.Map<ProductEntity>(newProduct);

            await _productRepository.AddAsync(newProductEntity);
        }

        // get one product from database
        public async Task<ProductViewModel> GetProduct(int productId)
        {
            var productToEdit = await _productRepository.GetByIdAsync(productId);

            var productViewModel = _mapper.Map<ProductViewModel>(productToEdit);

            return productViewModel;
        }

        // save edited product to database
        public async Task SaveChangesProduct(ProductViewModel productToChange)
        {
            var mapped = _mapper.Map<ProductEntity>(productToChange);

            await _productRepository.Update(mapped);
        }

        // remove product from database
        public async Task DeleteProduct(int productId)
        {
            await _productRepository.DeleteByIdAsync(productId);
        }

        // get list with all orders
        public async Task<OrderListViewModel> GetOrders()
        {
            var orders = await _orderRepository.GetAsync(predicate: x => x.StateOrder == "bought");

            var mapped = _mapper.Map<List<OrderViewModel>>(orders);

            OrderListViewModel orderList = new OrderListViewModel()
            {
                OrdersList = mapped,
            };

            return orderList;
        }

        // get content of one order
        public async Task<OrderDetailListViewModel> GetOrderDetails(int orderId)
        {
            var orderDetailList = await _orderDetailRepository.GetAsync(predicate: x => x.OrderId == orderId);

            OrderDetailListViewModel orderDetails = new OrderDetailListViewModel();

            orderDetails.OrderDetailList = _mapper.Map<List<OrderDetailViewModel>>(orderDetailList);

            return orderDetails;
        }

        // save changes in editable order
        public async Task SaveChangesOrder(Order orderToChange)
        {
            var correctedOrder = _mapper.Map<OrderEntity>(orderToChange);

            await _orderRepository.Update(correctedOrder);
        }

        // get one order detail
        public async Task<OrderDetail> GetOrderDetail(int orderDetailId)
        {
            var order = await _orderDetailRepository.GetAsync(predicate: x => x.OrderDetailId == orderDetailId);

            var orderDetail = _mapper.Map<OrderDetail>(order);

            return orderDetail;
        }

        public async Task SaveFile(IFormFile file)
        {
            if (file != null)
            {
                string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Media/img", file.FileName);

                using (var stream = new FileStream(SavePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
        }

        public async Task DeleteFile(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            try
            {
                File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.Photo));
            }
            catch (Exception)
            {
                throw new Exception("Nie znaleziono pliku"); ;
            }
        }

        public async Task SaveProductsStockSubiektFile(IFormFile formFile)
        {
            if (formFile != null)
            {
                string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/SubiektFiles", formFile.FileName);

                using (var stream = new FileStream(SavePath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }
            }
        }

        public async Task UpdateProductsStockFromSubiektFile()
        {
            List<ProductAPI> productsListFromSubiektFile = new List<ProductAPI>();

            string ReadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/SubiektFiles", "ListaSubiekt.json");

            productsListFromSubiektFile = JsonConvert.DeserializeObject<List<ProductAPI>>(File.ReadAllText(ReadPath));

            var productListToUpdate = await _productRepository.GetAllAsync();

            var resultList = from product1 in productsListFromSubiektFile
                             join product2 in productListToUpdate
                             on product1.ProductSymbol equals product2.ProductSymbol
                             select new ProductEntity
                             {
                                 IdGroupSubiekt = product2.IdGroupSubiekt,
                                 IdSubiekt = product2.IdSubiekt,
                                 ProductId = product2.ProductId,
                                 ProductSymbol = product2.ProductSymbol,
                                 ProductName = product2.ProductName,
                                 Manufacturer = product2.Manufacturer,
                                 Price = product2.Price,
                                 Photo = product2.Photo,
                                 Stock = product1.Stock
                             };

            await _productRepository.UpdateRange(resultList);
        }

        public async Task<CustomersListViewModel> GetCustomers()
        {
            var customers = await _customerRepository.GetAllAsync();

            var mapped = _mapper.Map<List<CustomerViewModel>>(customers);

            CustomersListViewModel customersList = new CustomersListViewModel()
            {
                CustomersList = mapped
            };

            return customersList;
        }

        public async Task<CustomerWithAddressesViewModel> GetCustomer(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId,
                include: source => source
                .Include(x => x.InvoiceAddress)
                .Include(c => c.ShippingAddress));

            var mapped = _mapper.Map<CustomerWithAddressesViewModel>(customer);

            return mapped;
        }
    }
}