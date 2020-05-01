using HurtowniaReptiGood.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.ViewModels
{
    public class NewProductViewModel : IProduct
    {   
        public int ProductId { get; set; }   
        public string ProductSymbol { get; set; }       
        public string ProductName { get; set; }   
        public double Price { get; set; }     
        public int Stock { get; set; }    
        public string Photo { get; set; }
        public string Manufacturer { get; set; }
    }
}
