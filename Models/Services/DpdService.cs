using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using HurtowniaReptiGood.Models.Entities;
using System.Net;
using System.IO;
using HurtowniaReptiGood.Models.ViewModels;
using HurtowniaReptiGood.Models.Interfaces;
using HurtowniaReptiGood.Models.Repositories;
using HurtowniaReptiGood.Models.Interfaces.Repositories;

namespace HurtowniaReptiGood.Models.Services
{
    public class DpdService : IDpdService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly MyContext _myContex;
        public DpdService(IOrderRepository orderRepository, MyContext myContex)
        {
            _orderRepository = orderRepository;
            _myContex = myContex;
        }

        // achive tracking status from Webservice DPD
        public async Task<DpdTrackingStatusListViewModel> GetTrackingStatusFromDPDWebservice(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            string xmlRequest = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:even=""http://events.dpdinfoservices.dpd.com.pl/"">
            <soapenv:Header/> 
            <soapenv:Body>  
            <even:getEventsForWaybillV1>    
             <waybill></waybill>     
              <eventsSelectType>ALL</eventsSelectType>      
               <language>PL</language>       
                <authDataV1>           
                    <channel></channel>         
                     <login></login>          
                      <password></password>             
                      </authDataV1>             
                   </even:getEventsForWaybillV1>              
                 </soapenv:Body>
               </soapenv:Envelope>";

            DpdTrackingStatusListViewModel lastTrackingStatus = await DeserializeXmlResponse(await SendSoap(xmlRequest, order.TrackingNumber));

            return lastTrackingStatus;
        }

        // get data from xml response
        public async Task<DpdTrackingStatusListViewModel> DeserializeXmlResponse(string responseXml)
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(responseXml);

            List<DpdTrackingStatusViewModel> statusList = new List<DpdTrackingStatusViewModel>();

            XmlNodeList eventsList = xmlDoc.SelectNodes("//eventsList");

            foreach (XmlNode nodeElement in eventsList)
            {
                if (nodeElement.HasChildNodes)
                {
                    DpdTrackingStatusViewModel status = new DpdTrackingStatusViewModel()
                    {
                        Event = nodeElement["description"].InnerText,
                        EventDateTime = nodeElement["eventTime"].InnerText,
                        EventPlace = nodeElement["depotName"].InnerText,
                        TrackingNumber = nodeElement["waybill"].InnerText,
                    };
                    statusList.Add(status);
                }
            }
            DpdTrackingStatusListViewModel trackingStatusList = new DpdTrackingStatusListViewModel();

            trackingStatusList.TrackingList = statusList;

            if (!trackingStatusList.TrackingList.Any())
            {
                var emptyRow = new DpdTrackingStatusViewModel()
                {
                    TrackingNumber = "brak danych",
                    Event = "brak danych",
                    EventDateTime = "brak danych",
                    EventPlace = "brak danych"
                };
                trackingStatusList.TrackingList.Add(emptyRow);
            }

            return trackingStatusList;
        }     

            //trackingStatusList.TrackingList.Add(status);
            //foreach(XmlNode item in nodeElement.ChildNodes)
            //{
            //    DpdTrackingStatusViewModel status = new DpdTrackingStatusViewModel();

            //    if (item.Name == "description") status.Event = item.InnerText;
            //    if (item.Name == "eventTime") status.EventDateTime = item.InnerText;
            //    if (item.Name == "depotName") status.EventPlace = item.InnerText;
            //    if (item.Name == "waybill") status.TrackingNumber = item.InnerText;

            //    trackingStatusList.TrackingList.Add(status);
            //string s = item.Attributes["//description"].InnerText;
            //if (item.Name == "description")
            //{
            //    list.Add(item.InnerText);
            //}
            // }                   
                       
            //List<string> list2 = new List<string>();
            //list2 = list;

            //DpdTrackingStatusViewModel lastTrackingStatus;
            //if (xmlDoc.GetElementsByTagName("description").Item(0)==null)
            //{
            //    lastTrackingStatus = new DpdTrackingStatusViewModel()
            //    {
            //        Event = "brak",
            //        EventDateTime = "brak",
            //        EventPlace = "brak",
            //        TrackingNumber = "brak"
            //    };
            //}
            //else
            //{
            //    lastTrackingStatus = new DpdTrackingStatusViewModel()
            //    {
            //        Event = xmlDoc.GetElementsByTagName("description").Item(0).InnerText,
            //        EventDateTime = xmlDoc.GetElementsByTagName("eventTime").Item(0).InnerText,
            //        EventPlace = xmlDoc.GetElementsByTagName("depotName").Item(0).InnerText,
            //        TrackingNumber = xmlDoc.GetElementsByTagName("waybill").Item(0).InnerText
            //    };
            //}

            //return lastTrackingStatus;
        

        // sending soap request to webservice DPD
        public async Task<string> SendSoap(string xmlRequest, string trackingNumber)
        {
            // get DPD config
            DpdConfigEntity dpdConfig = _myContex.DpdConfigs.First();

            // SOAP url DPD
            string url = "https://dpdinfoservices.dpd.com.pl/DPDInfoServicesObjEventsService/DPDInfoServicesObjEvents";

            XmlDocument soapXml = new XmlDocument();
            soapXml.LoadXml(xmlRequest);

            // add config to XML
            soapXml.GetElementsByTagName("waybill").Item(0).InnerText = trackingNumber;
            soapXml.GetElementsByTagName("login").Item(0).InnerText = dpdConfig.Login;
            soapXml.GetElementsByTagName("password").Item(0).InnerText = dpdConfig.Password;
            soapXml.GetElementsByTagName("channel").Item(0).InnerText = dpdConfig.Channel;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            webRequest.Headers.Add("SOAPAction", "Vehicle");

            using (Stream stream = webRequest.GetRequestStream())
            {
                soapXml.Save(stream);
            }

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            string soapResult;

            WebResponse webResponse = null;

            try
            {
                webResponse = webRequest.EndGetResponse(asyncResult);

                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }

                return soapResult;
            }
            catch (Exception e)
            {
                if (e is WebException && ((WebException)e).Status == WebExceptionStatus.ProtocolError)
                {
                    WebResponse errResp = ((WebException)e).Response;

                    using (Stream respStream = errResp.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(respStream);
                        string text = reader.ReadToEnd();
                        return text;
                    }
                }
            }
            finally
            {
                webResponse.Close();
            }
            return "";
        }
    }
}
