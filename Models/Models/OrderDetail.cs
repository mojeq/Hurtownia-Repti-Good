﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductSymbol { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int CurrentStockInWholesale { get; set; }
        public double Price { get; set; }
        public double Value { get; set; }
    }
}
