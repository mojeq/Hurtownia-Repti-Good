using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models
{
    public class AddressViewModel
    {
        public int AddressId { get; set; }
        public string TypeAddress { get; set; }
        public string Street { get; set; }
        string StreetNumber { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string NIP { get; set; }
    }
}

