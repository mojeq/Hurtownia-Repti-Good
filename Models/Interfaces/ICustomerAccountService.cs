using HurtowniaReptiGood.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Interfaces
{
    public interface ICustomerAccountService
    {
        Task<OrderListViewModel> GetOrdersHistory(string userLogged);
        Task<OrderViewModel> GetOrder(int orderId);
    }
}
