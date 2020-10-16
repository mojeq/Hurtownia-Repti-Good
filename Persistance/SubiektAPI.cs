using HurtowniaReptiGood.Models.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Services
{
    public class SubiektAPI : ISubiektAPI
    {
        public async Task<HttpClient> InitAPI()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:35129/");

            return client;
        }
    }
}
