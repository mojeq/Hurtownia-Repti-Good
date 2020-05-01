using HurtowniaReptiGood.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models
{
    public class OrderViewModel : IOrder
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string StateOrder { get; set; }
        public string StatusOrder { get; set; }
        public DateTime DateOrder { get; set; }
        public double ValueOrder { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }
    }
}
