using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models
{
    public class ShippingAddressViewModel : Address
    {
        public int AddressId { get; set; }
        public string Email { get; set; }
    }
}

