using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models
{
    public class CustomerViewModel
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
