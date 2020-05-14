using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace HurtowniaReptiGood.Models.DPDservice
{
    public class SoapWebRequestDPD
    {
        public static string SendSoap(string xmlRawData)
        {
            string url = "https://dpdinfoservices.dpd.com.pl/DPDInfoServicesObjEventsService/DPDInfoServicesObjEvents";

            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(xmlRawData);
            soapEnvelopeXml.GetElementsByTagName("waybill").Item(0).InnerText = "0000227801601U";

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            webRequest.Headers.Add("SOAPAction", "Vehicle");

            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
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
                if (e is WebException && ((WebException)e).Status
            == WebExceptionStatus.ProtocolError)
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

