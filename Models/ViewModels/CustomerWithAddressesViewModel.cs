using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.ViewModels
{
    public class CustomerWithAddressesViewModel : Customer
    {
        public InvoiceAddressViewModel InvoiceAddress { get; set; }
        public ShippingAddressViewModel ShippingAddress { get; set; }
    }
}
