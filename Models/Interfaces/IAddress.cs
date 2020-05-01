using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Interfaces
{
    interface IAddress
    {
        public string CompanyName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }   
        public string Street { get; set; }       
        public string StreetNumber { get; set; }    
        public string City { get; set; }      
        public string ZipCode { get; set; }     
        public string Phone { get; set; }
    }
}
