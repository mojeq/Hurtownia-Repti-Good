using System;
using System.Net.Http;

namespace HurtowniaReptiGood.Models.Services
{
    public class SubiektAPI
    {
        public HttpClient InitAPI()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:35129/");

            return client;
        }
    }
}
