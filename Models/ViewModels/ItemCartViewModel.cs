using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models
{
    public class ItemCartViewModel
    {
        public int ProductId { get; set; }
        public string ProductSymbol { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Value { get; set; }
    }
}
