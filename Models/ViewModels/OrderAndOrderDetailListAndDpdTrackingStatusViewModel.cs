using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.ViewModels
{
    public class OrderAndOrderDetailListAndDpdTrackingStatusViewModel
    {
        public OrderAndOrderDetailListViewModel OrderAndDetailsOrder { get; set; }
        public DpdTrackingStatusListViewModel DpdTrackingStatusList { get; set; }
    }
}
