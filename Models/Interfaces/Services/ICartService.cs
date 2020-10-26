using HurtowniaReptiGood.Models.Entities;
using HurtowniaReptiGood.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Interfaces
{
    public interface ICartService
    {
        Task<int> CreateNewCartOrder(CustomerEntity loggedUser, ItemCartViewModel itemCart);
        Task<int> AddItemToExistCart(int orderId, ItemCartViewModel itemCart);
        Task<OrderDetailListViewModel> GetCartDetailList(int orderId);
        Task<ShippingAddressViewModel> GetShippingAddress(int orderId);
        Task<InvoiceAddressViewModel> GetInvoiceAddress(int orderId);
        Task RemoveItemFromCart(int orderDetailId);
        Task UpdateQuantityItemInCart(int orderDetailId, int quantity);
        Task SaveNewOrder(int orderId, double valueOrder, string orderMessage);
        Task CreatePdfAttachmentWithOrder(int orderId);
        Task SendMailWithAttachment(int orderId);
        Task DecreaseStockInWholesale(ItemCartViewModel itemCart);
        Task<int> GetCurrentStockInWholesale(int productId);
        Task IncreaseStockInWholesale(int orderDetailId);
        Task IncreaseStockInWholesale(int orderDetailId, int quantity);
    }
}
