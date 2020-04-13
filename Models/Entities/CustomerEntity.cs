using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Entities
{
    public class CustomerEntity
    {
        [Key]
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int ShippingAddressId { get; set; }
        public virtual ShippingAddressEntity ShippingAddress { get; set; }
        public virtual InvoiceAddressEntity InvoiceAddres { get; set; }
        public virtual ICollection<OrderEntity> OrderList { get; set; }
        //public OrderEntity Order { get; set; }
    }
}
