using HurtowniaReptiGood.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Interfaces
{
    public interface IDpdService
    {
        Task<DpdTrackingStatusListViewModel> GetTrackingStatusFromDPDWebservice(int orderId);
        Task<DpdTrackingStatusListViewModel> DeserializeXmlResponse(string responseXml);
        Task<string> SendSoap(string xmlRequest, string trackingNumber);
    }
}
