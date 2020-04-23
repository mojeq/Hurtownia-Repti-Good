using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.ViewModels
{
    public class CartAndAddressesViewModel
    {
        public ShippingAddressViewModel ShippingAddress { get; set; }
        public InvoiceAddressViewModel InvoiceAddress { get; set; }
        public OrderDetailListViewModel CartToView { get; set; }
    }
}
