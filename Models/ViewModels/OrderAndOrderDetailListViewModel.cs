using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.ViewModels
{
    public class OrderAndOrderDetailListViewModel
    {
        public OrderViewModel Order { get; set; }
        public OrderDetailListViewModel OrderDetails { get; set; }
    }
}
