using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models
{
    public class EmailAttachmentViewModel
    {
        public string Email { get; set; }
        public double OrderValue { get; set; }
        public List<OrderDetailViewModel> OrderList { get; set; }
    }
}
