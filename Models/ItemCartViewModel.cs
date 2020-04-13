using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models
{
    public class ItemCartViewModel
    {
        public string ProductSymbol { get; set; }
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public double Price { get; set; }
    }
}
