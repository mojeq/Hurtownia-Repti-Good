using HurtowniaReptiGood.Models.Entities;
using HurtowniaReptiGood.Models.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Services
{
    public class CustomerAccountService
    {
        private readonly AppService _appService;
        private readonly MyContex _myContex;
        public CustomerAccountService(
            MyContex myContex, 
            AppService appService)
        {
            _appService = appService;
            _myContex = myContex;
        }

        // get list with all orders from this customer from database
        public OrderListViewModel GetOrdersHistory(string userLogged)
        {
            CustomerEntity loggedUser = _appService.GetLoggedCustomer(userLogged);

            OrderListViewModel ordersHistory = new OrderListViewModel();
            ordersHistory.OrdersList = _myContex.Orders
                .Where(x => x.CustomerId == loggedUser.CustomerId)
                .Select(x => new OrderViewModel
                {
                    OrderId = x.OrderId,
                    CustomerId=x.CustomerId,
                    StateOrder=x.StateOrder,
                    StatusOrder=x.StatusOrder,
                    DateOrder=x.DateOrder,
                    ValueOrder=x.ValueOrder,
                    TrackingNumber = x.TrackingNumber,
                })
                .ToList();

            return ordersHistory;
        }

        // get one order from database
        public OrderViewModel GetOrder(int orderId)
        {
            OrderViewModel order = new OrderViewModel();
            order = _myContex.Orders
                .Where(x => x.OrderId == orderId)
                .Select(x => new OrderViewModel
                {
                    OrderId = x.OrderId,
                    CustomerId=x.CustomerId,
                    StateOrder=x.StateOrder,
                    StatusOrder=x.StatusOrder,
                    DateOrder=x.DateOrder,
                    ValueOrder=x.ValueOrder,
                    TrackingNumber = x.TrackingNumber,
                }).FirstOrDefault();

            return order;
        }
    }
}
