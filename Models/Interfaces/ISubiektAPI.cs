using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Interfaces
{
    public interface ISubiektAPI
    {
        Task<HttpClient> InitAPI();
    }
}
