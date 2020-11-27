using AutoMapper;
using HurtowniaReptiGood.Models.Entities;
using HurtowniaReptiGood.Models.Interfaces;
using HurtowniaReptiGood.Models.Interfaces.Repositories;
using HurtowniaReptiGood.Models.Repositories;
using HurtowniaReptiGood.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        private readonly IProductRepository _productRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public AdminService(IProductRepository productRepository, IOrderDetailRepository orderDetailRepository, IOrderRepository orderRepository, IMapper mapper)
        {
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
    }
}