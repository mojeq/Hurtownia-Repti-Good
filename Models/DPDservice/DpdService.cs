using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using HurtowniaReptiGood.Models.DPDservice;

namespace HurtowniaReptiGood.Models.Services
{
    public class DpdService
    {
        public static string GetTrackingStatusFromDPDWebservice(string trackingNumber)
        {
            string trackingStatus;
            string request = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:even=""http://events.dpdinfoservices.dpd.com.pl/"">
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

            var responseXml = SoapWebRequestDPD.SendSoap(request);
            trackingStatus = DeserializeXmlResponse(responseXml);

            return trackingStatus;
        }

        static string DeserializeXmlResponse(string responseXml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseXml);
            string statusWayBill = xmlDoc.GetElementsByTagName("description").Item(0).InnerText;
            string eventTime = xmlDoc.GetElementsByTagName("eventTime").Item(0).InnerText;
            string trackingStatus = statusWayBill + " " + eventTime;

            return trackingStatus;
        }
    }
}
