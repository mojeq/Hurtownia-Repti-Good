using HurtowniaReptiGood.Models.Entities;
using HurtowniaReptiGood.Models.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Repositories
{
    public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
    {
        public ProductRepository(MyContext myContext) : base(myContext) { }
    }
}
