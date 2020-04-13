using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Entities
{
    public class ShippingAddressEntity
    {
        [Key]
        [Required]
        public int ShippingAddressId { get; set; }
        [Required]
        public string TypeAddress { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        string StreetNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
