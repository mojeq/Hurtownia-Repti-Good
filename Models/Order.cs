using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string StateOrder { get; set; }
        public string StatusOrder { get; set; }
        public DateTime DateOrder { get; set; }
        public double ValueOrder { get; set; }
    }
}
