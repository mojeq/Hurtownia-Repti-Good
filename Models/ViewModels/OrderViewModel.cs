using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models
{
    public class OrderViewModel : Order
    {
        public List<OrderDetailViewModel> OrderDetails { get; set; }
    }
}
