using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Entities
{
    public class OrderDetailEntity
    {
        [Key]
        [Required]
        public int OrderDetailId { get; set; }
        [Required]
        public string ProductSymbol { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double Value { get; set; }
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }     
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; }
    }
}
