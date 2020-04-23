using HurtowniaReptiGood.Models.Entities;
using HurtowniaReptiGood.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Services
{
    public class CartService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MyContex _myContex;
        public CartService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            MyContex myContex)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _myContex = myContex;
        }
        public int CreateNewCartOrder(CustomerEntity loggedUser, ItemCartViewModel itemCart)
        {
            OrderEntity order = new OrderEntity()
            {
                CustomerId = loggedUser.CustomerId,
                StateOrder = "cart",
                DateOrder = DateTime.Now,
                StatusOrder = "W realizacji",
            };
            _myContex.Orders.Add(order);
            _myContex.SaveChanges();

            int orderId = _myContex.Orders.OrderByDescending(s => s.OrderId)
                                 .Where(o => o.CustomerId == loggedUser.CustomerId)
                                 .Where(o => o.StateOrder == "cart")
                                 .FirstOrDefault().OrderId;

            // create object with order details and save in database
            OrderDetailEntity orderDetail = new OrderDetailEntity()
            {
                ProductId = itemCart.ProductId,
                ProductSymbol = itemCart.ProductSymbol,
                ProductName = itemCart.ProductName,
                OrderId = orderId,
                Quantity = itemCart.Quantity,
                Price = itemCart.Price,
            };
            _myContex.OrderDetails.Add(orderDetail);
            _myContex.SaveChanges();

            return orderId;
        }
        public int AddItemToExistCart(CustomerEntity loggedUser, ItemCartViewModel itemCart)
        {
            // finding last order ID
            int orderId = _myContex.Orders.OrderByDescending(s => s.OrderId)
                                             .Where(o => o.CustomerId == loggedUser.CustomerId)
                                             .Where(o => o.StateOrder == "cart")
                                             .FirstOrDefault().OrderId;

            //checking it is this item in cart if yes increase quantity if no, add new position OrderDetail
            OrderDetailEntity orderDetailExist = _myContex.OrderDetails.Where(a => a.OrderId == orderId)
                                                                  .Where(a => a.ProductSymbol == itemCart.ProductSymbol)
                                                                  .FirstOrDefault();
            if (orderDetailExist == null)
            {
                // create object with order details and save in database
                OrderDetailEntity orderDetail = new OrderDetailEntity()
                {
                    ProductId= itemCart.ProductId,
                    ProductSymbol = itemCart.ProductSymbol,
                    ProductName = itemCart.ProductName,
                    OrderId = orderId,
                    Quantity = itemCart.Quantity,
                    Price = itemCart.Price,
                };
                _myContex.OrderDetails.Add(orderDetail);
                _myContex.SaveChanges();
            }
            else
            {
                orderDetailExist.Quantity += itemCart.Quantity;
                _myContex.SaveChanges();
            }

            //receive items in cart
            var itemsInCart = _myContex.OrderDetails.Where(c => c.OrderId == orderId).ToList();
            return orderId;
        }

        public OrderDetailListViewModel GetCartDetailList(int orderId)
        {
            OrderDetailListViewModel cartDetails = new OrderDetailListViewModel();
            cartDetails.OrderDetailList = _myContex.OrderDetails
                .Where(c => c.OrderId == orderId)
                .Select(x => new OrderDetailViewModel
                {
                    OrderDetailId=x.OrderDetailId,
                    OrderId=x.OrderId,
                    ProductName=x.ProductName,
                    ProductSymbol=x.ProductSymbol,
                    Price=x.Price,
                    Quantity=x.Quantity                
                }).ToList();

            return cartDetails;
        }

        public ShippingAddressViewModel GetShippingAddress(int orderId)
        {
            var customerId = _myContex.Orders.Find(orderId).CustomerId;
            int shippingAddressId = _myContex.Customers.Find(customerId).ShippingAddressId;
            var shippingAddress = _myContex.ShippingAddresses
                .Where(c => c.ShippingAddressId == shippingAddressId)
                .Select(x => new ShippingAddressViewModel
                {                    
                    Street = x.Street,
                    StreetNumber = x.StreetNumber,
                    ZipCode = x.ZipCode,
                    City = x.City,
                    Phone = x.Phone,
                    CompanyName = x.CompanyName,
                    CustomerName = x.CustomerName,
                    CustomerSurname = x.CustomerSurname,
                    Email = x.Email,
                }).FirstOrDefault();

            return shippingAddress;
        }

        public InvoiceAddressViewModel GetInvoiceAddress(int orderId)
        {
            var customerId = _myContex.Orders.Find(orderId).CustomerId;
            int invoiceAddressId = _myContex.Customers.Find(customerId).InvoiceAddressId;
            var invoiceAddress = _myContex.InvoiceAddresses
                .Where(c => c.InvoiceAddressId == invoiceAddressId)
                .Select(x => new InvoiceAddressViewModel
                {
                    Street = x.Street,
                    StreetNumber = x.StreetNumber,
                    ZipCode = x.ZipCode,
                    City = x.City,
                    CompanyName = x.CompanyName,
                    CustomerName = x.CustomerName,
                    CustomerSurname = x.CustomerSurname,
                    Phone=x.Phone,
                    NIP=x.NIP                    
                }).FirstOrDefault();

            return invoiceAddress;
        }

        public void RemoveItemFromCart(int orderDetailId)
        {
            var orderDetailToRemove = _myContex.OrderDetails.Find(orderDetailId);
            _myContex.OrderDetails.Remove(orderDetailToRemove);
            _myContex.SaveChanges();
        }

        public void UpdateQuantityItemInCart(int orderDetailId, int quantity)
        {           
            var orderDetailExist = _myContex.OrderDetails.Find(orderDetailId);
            orderDetailExist.Quantity = quantity;
            _myContex.SaveChanges();
        }        
    }
}
