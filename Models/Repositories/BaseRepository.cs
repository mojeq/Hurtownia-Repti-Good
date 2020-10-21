using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Repositories
{
    public class BaseRepository
    {
        protected readonly MyContext _context;
        public BaseRepository(MyContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
