using HurtowniaReptiGood.Models.Entities;
using HurtowniaReptiGood.Models.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Repositories
{
    public class CustomerRepository : GenericRepository<CustomerEntity>, ICustomerRepository
    {
        public CustomerRepository(MyContext myContext) : base(myContext) { }
    }
}
