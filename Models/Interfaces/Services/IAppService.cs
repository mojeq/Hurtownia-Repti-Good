using HurtowniaReptiGood.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Interfaces
{
    public interface IAppService
    {
        Task<CustomerEntity> GetLoggedCustomer(string userLogged);
        Task<ProductsListViewModel> GetAllProducts();
        Task<ProductsListViewModel> GetProductsFromCategory(string manufacturer);

    }
}
