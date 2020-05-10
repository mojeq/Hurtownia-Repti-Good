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
        private readonly MyContex _myContex;
        public AdminService(MyContex myContex)
        {
            _myContex = myContex;
        }
        public void AddNewProduct(NewProductViewModel newProduct)
        {
            ProductEntity newProductEntity = new ProductEntity
            {
                ProductName = newProduct.ProductName,
                ProductSymbol = newProduct.ProductSymbol,
                Stock = newProduct.Stock,
                Price = newProduct.Price,
                Photo = newProduct.Photo,
                Manufacturer = newProduct.Manufacturer,
            };
            _myContex.Products.Add(newProductEntity);
            _myContex.SaveChanges();
        }

        public ProductViewModel GetProduct(int productId)
        {
            ProductViewModel productToEdit = _myContex.Products
                .Where(c=>c.ProductId==productId)
                .Select(x => new ProductViewModel
                {
                    ProductId=x.ProductId,
                    ProductSymbol=x.ProductSymbol,
                    ProductName=x.ProductName,
                    Manufacturer=x.Manufacturer,
                    Price=x.Price,
                    Stock=x.Stock,
                    Photo=x.Photo
                }).FirstOrDefault();

            return productToEdit;
        }

        public void SaveChangesProduct(ProductViewModel productToChange)
        {
            var product = _myContex.Products.Find(productToChange.ProductId);
            product.ProductName = productToChange.ProductName;
            product.ProductSymbol = productToChange.ProductSymbol;
            product.Manufacturer = productToChange.Manufacturer;
            product.Price = productToChange.Price;
            product.Stock = productToChange.Stock;
            product.Photo = productToChange.Photo;
            _myContex.SaveChanges();
        }

        public OrderListViewModel GetOrders()
        {
            OrderListViewModel orders = new OrderListViewModel();
            orders.OrdersList = _myContex.Orders.Select(x => new OrderViewModel
            {
                OrderId=x.OrderId,
                CustomerId=x.CustomerId,
                DateOrder=x.DateOrder,
                StateOrder=x.StateOrder,
                StatusOrder=x.StatusOrder,
                ValueOrder=x.ValueOrder
            }).ToList();

            return orders;
        }

        public OrderDetailListViewModel GetOrderDetails(int orderId)
        {
            OrderDetailListViewModel orderDetails = new OrderDetailListViewModel();
            orderDetails.OrderDetailList = _myContex.OrderDetails
                .Where(c => c.OrderId == orderId)
                .Select(x => new OrderDetailViewModel
                {
                    OrderDetailId=x.OrderDetailId,
                    OrderId=x.OrderId,
                    ProductId=x.ProductId,
                    ProductName=x.ProductName,
                    ProductSymbol=x.ProductSymbol,
                    Price=x.Price,
                    Quantity=x.Quantity,
                    Value=x.Value
                }).ToList();

            return orderDetails;
        }
    }
}
