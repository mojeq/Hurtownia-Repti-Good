using AutoMapper;
using HurtowniaReptiGood.Models.Entities;
using HurtowniaReptiGood.Models.ViewModels;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Services
{
    public class AdminService
    {
        private readonly IMapper _mapper;
        private readonly MyContex _myContex;
        public AdminService(IMapper mapper, MyContex myContex)
        {
            _mapper = mapper;
            _myContex = myContex;
        }

        // adding new product to database
        public void AddNewProduct(NewProductViewModel newProduct)
        {
            ProductEntity newProductEntity = _mapper.Map<ProductEntity>(newProduct);

            _myContex.Products.Add(newProductEntity);
            _myContex.SaveChanges();
        }

        // get one product from database
        public ProductViewModel GetProduct(int productId)
        {
            var productToEdit = _myContex.Products
                .Where(c => c.ProductId == productId)            
                .FirstOrDefault();

            var productViewModel = _mapper.Map<ProductViewModel>(productToEdit);

            return productViewModel;
        }

        // save edited product to database
        public void SaveChangesProduct(ProductViewModel productToChange)
        {
            var mapped = _mapper.Map<ProductEntity>(productToChange);

            _myContex.Products.Update(mapped);
            _myContex.SaveChanges();
        }

        // get list with all orders
        public OrderListViewModel GetOrders()
        {
            var orders = _myContex.Orders
                .Where(c => c.StateOrder=="bought")
                .ToList();

            var mapped = _mapper.Map<List<OrderViewModel>>(orders);

            OrderListViewModel orderList = new OrderListViewModel()
            {
                OrdersList = mapped,
            };

            return orderList;
        }

        // get content of one order
        public OrderDetailListViewModel GetOrderDetails(int orderId)
        {
            OrderDetailListViewModel orderDetails = new OrderDetailListViewModel();
            var orderDetailList = _myContex.OrderDetails
                .Where(c => c.OrderId == orderId)
                .ToList();

            orderDetails.OrderDetailList = _mapper.Map<List<OrderDetailViewModel>>(orderDetailList);

            return orderDetails;
        }

        // save changes in editable order
        public void SaveChangesOrder(Order orderToChange)
        {
            var correctedOrder = _mapper.Map<OrderEntity>(orderToChange);

            _myContex.Orders.Update(correctedOrder);
            _myContex.SaveChanges();
        }

        // get one order detail
        public OrderDetail GetOrderDetail(int orderDetailId)
        {
            var order = _myContex.OrderDetails
                .Where(x=>x.OrderDetailId==orderDetailId)
                .FirstOrDefault();

            var orderDetail = _mapper.Map<OrderDetail>(order);

            return orderDetail;
        }
    }
}
