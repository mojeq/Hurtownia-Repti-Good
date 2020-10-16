using AutoMapper;
using HurtowniaReptiGood.Models.Entities;
using HurtowniaReptiGood.Models.Interfaces;
using HurtowniaReptiGood.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;
        private readonly MyContex _myContex;
        public AdminService(IMapper mapper, MyContex myContex)
        {
            _mapper = mapper;
            _myContex = myContex;
        }

        // adding new product to database
        public async Task AddNewProduct(NewProductViewModel newProduct)
        {
            ProductEntity newProductEntity = _mapper.Map<ProductEntity>(newProduct);

            await _myContex.Products.AddAsync(newProductEntity);

            await _myContex.SaveChangesAsync();
        }

        // get one product from database
        public async Task<ProductViewModel> GetProduct(int productId)
        {
            var productToEdit = await _myContex.Products
                .Where(c => c.ProductId == productId)
                .FirstOrDefaultAsync();

            var productViewModel = _mapper.Map<ProductViewModel>(productToEdit);

            return productViewModel;
        }

        // save edited product to database
        public async Task SaveChangesProduct(ProductViewModel productToChange)
        {
            var mapped = _mapper.Map<ProductEntity>(productToChange);

            _myContex.Products.Update(mapped);

            await _myContex.SaveChangesAsync();
        }

        // get list with all orders
        public async Task<OrderListViewModel> GetOrders()
        {
            var orders = await _myContex.Orders
                            .Where(c => c.StateOrder=="bought")
                            .ToListAsync();

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
            OrderDetailListViewModel orderDetails = new OrderDetailListViewModel();
            var orderDetailList = await _myContex.OrderDetails
                                        .Where(c => c.OrderId == orderId)
                                        .ToListAsync();

            orderDetails.OrderDetailList = _mapper.Map<List<OrderDetailViewModel>>(orderDetailList);

            return orderDetails;
        }

        // save changes in editable order
        public async Task SaveChangesOrder(Order orderToChange)
        {
            var correctedOrder = _mapper.Map<OrderEntity>(orderToChange);

            _myContex.Orders.Update(correctedOrder);

            await _myContex.SaveChangesAsync();
        }

        // get one order detail
        public async Task<OrderDetail> GetOrderDetail(int orderDetailId)
        {
            var order = await _myContex.OrderDetails
                .Where(x=>x.OrderDetailId==orderDetailId)
                .FirstOrDefaultAsync();

            var orderDetail = _mapper.Map<OrderDetail>(order);

            return orderDetail;
        }
    }
}
