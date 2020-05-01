using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HurtowniaReptiGood.Models.Interfaces;

namespace HurtowniaReptiGood.Models.Entities
{
    public class ProductEntity : IProduct
    {
        [Key]
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string ProductSymbol { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public string Photo { get; set; }
        [Required]
        public string Manufacturer { get; set; }
    }
}
