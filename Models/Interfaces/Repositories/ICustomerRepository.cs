using HurtowniaReptiGood.Models.Entities;
using HurtowniaReptiGood.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Interfaces.Repositories
{
    public interface ICustomerRepository : IGenericRepository<CustomerEntity>
    {
    }
}
