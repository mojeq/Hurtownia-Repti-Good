using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.ViewModels
{
    public class InvoiceAddressViewModel : Address
    {
        public int AddressId { get; set; }
        public string NIP { get; set; }
    }
}
