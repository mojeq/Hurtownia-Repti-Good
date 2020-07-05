using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Entities
{
    public class OrderEntity
    {
        [Key]
        [Required]
        public int OrderId { get; set; }
        [Required]
        public string StateOrder { get; set; }
        [Required]
        public string StatusOrder { get; set; }
        [Required]
        public DateTime DateOrder { get; set; }
        public string TrackingNumber { get; set; }
        [Required]
        public double ValueOrder { get; set; }
        public string OrderMessage { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }
        public  ICollection<OrderDetailEntity> OrderDetails { get; set; }        
    }
}
