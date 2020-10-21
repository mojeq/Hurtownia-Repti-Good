using AutoMapper;
using HurtowniaReptiGood.Models.Entities;
using HurtowniaReptiGood.Models.Interfaces;
using HurtowniaReptiGood.Models.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Services
{
    public class CustomerAccountService : ICustomerAccountService
    {
        private readonly IMapper _mapper;
        private readonly AppService _appService;
        private readonly MyContext _myContex;
        public CustomerAccountService(IMapper mapper, MyContext myContex, AppService appService)
        {
            _mapper = mapper;
            _appService = appService;
            _myContex = myContex;
        }

        // get list with all orders from this customer from database
        public async Task<OrderListViewModel> GetOrdersHistory(string userLogged)
        {
            CustomerEntity loggedUser = await _appService.GetLoggedCustomer(userLogged);            

            var orderList = await _myContex.Orders
                                .Where(x => x.CustomerId == loggedUser.CustomerId) 
                                .ToListAsync();

            var mapped = _mapper.Map<List<OrderViewModel>>(orderList);

            OrderListViewModel ordersHistory = new OrderListViewModel()
            {
                OrdersList = mapped,
            };

            return ordersHistory;
        }

        // get one order from database
        public async Task<OrderViewModel> GetOrder(int orderId)
        { 
            var orderEntity = await _myContex.Orders
                                    .Where(x => x.OrderId == orderId)
                                    .FirstOrDefaultAsync();

            var order = _mapper.Map<OrderViewModel>(orderEntity);

            return order;
        }
    }
}
