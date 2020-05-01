using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Interfaces
{
    interface ICustomer
    {
        public int CustomerId { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public int ShippingAddressId { get; set; }
        public int InvoiceAddressId { get; set; }
    }
}
