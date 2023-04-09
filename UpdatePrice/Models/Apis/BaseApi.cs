using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UpdatePrice.Models.XmlDocument;

namespace UpdatePrice.Models.Apis
{
    public abstract class BaseApi
    {
        protected const string NobitexUrl = "https://api.nobitex.ir/";
        protected const string HdPayUrl = "https://hdpay.ir/";

        protected async Task<HttpResponseMessage> Post(string url, Dictionary<string, string> parameters)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(parameters);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    return await client.PostAsync(url, data);
                }
            }
            catch (Exception ex)
            {
                WriteXmlDocument.AddException(MethodBase.GetCurrentMethod().DeclaringType.FullName, ex);
                return null;
            }
        }

        protected async Task<HttpResponseMessage> Get(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    return await client.GetAsync(url);
                }
            }
            catch (Exception ex)
            {
                WriteXmlDocument.AddException(MethodBase.GetCurrentMethod().DeclaringType.FullName, ex);
                return null;
            }
        }
    }
}
