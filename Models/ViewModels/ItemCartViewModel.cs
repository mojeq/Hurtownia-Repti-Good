using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models
{
    public class ItemCartViewModel : Product
    {
        public int Quantity { get; set; }
        public double Value { get; set; }
    }
}
