using HurtowniaReptiGood.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Interfaces
{
    public interface IAdminService
    {
        Task AddNewProduct(NewProductViewModel newProduct);
        Task<ProductViewModel> GetProduct(int productId);
        Task SaveChangesProduct(ProductViewModel productToChange);
        Task DeleteProduct(int productId);
        Task<OrderListViewModel> GetOrders();
        Task<OrderDetailListViewModel> GetOrderDetails(int orderId);
        Task SaveChangesOrder(Order orderToChange);
        Task<OrderDetail> GetOrderDetail(int orderDetailId);
        Task SaveFile(IFormFile file);
        Task DeleteFile(int productId);
    }
}
