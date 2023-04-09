using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UpdatePrice.Models.Enums;
using UpdatePrice.Models.ViewModels;
using UpdatePrice.Models.XmlDocument;

namespace UpdatePrice.Models.Apis
{
    public class AvvalMoneyApi
    {
        public async Task<MessagesViewModel> UpdatePrice(decimal price, BuyAndSellTypeEnum type)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var parameters = "{\"Price\":" + price + ",\"Type\":" + (int)type + "}";
                    var data = new StringContent(parameters, Encoding.UTF8, "application/json");

                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    var response = await client.PostAsync("https://panel.avvalmoney.com/api/Price/UpdateTetherPrice", data);
                    if (response.IsSuccessStatusCode)
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<MessagesViewModel>(await response.Content.ReadAsStringAsync());
                    }
                    WriteXmlDocument.AddError(MethodBase.GetCurrentMethod().DeclaringType.FullName,"Update Price Response is false",await response.Content.ReadAsStringAsync());
                    return new MessagesViewModel
                    {
                        Result = false,
                        Messages = new List<string> { $"error in call api UpdatePrice(AvvalMoney) error:" + await response.Content.ReadAsStringAsync() }
                    };
                }
            }
            catch (Exception ex)
            {
                WriteXmlDocument.AddException(MethodBase.GetCurrentMethod().DeclaringType.FullName,ex);
                return new MessagesViewModel
                {
                    Result = false,
                    Messages = new List<string> { $"error in call api UpdatePrice(AvvalMoney) error: {(ex.Message.Length > 300 ? ex.Message.Substring(0, 300) : ex.Message)}" }
                };
            }
        }

        public async Task<MessagesViewModel> AddPriceHistory(decimal buyPrice, decimal sellPrice, int paymentServiceId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var parameters = "{\"BuyPrice\":" + buyPrice + ",\"SellPrice\":" + sellPrice + ",\"PaymentServiceId\":" + paymentServiceId + "}";
                    var data = new StringContent(parameters, Encoding.UTF8, "application/json");

                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    var response = await client.PostAsync("https://panel.avvalmoney.com/api/Price/AddPriceHistory", data);
                    if (response.IsSuccessStatusCode)
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<MessagesViewModel>(await response.Content.ReadAsStringAsync());
                    }

                    WriteXmlDocument.AddError(MethodBase.GetCurrentMethod().DeclaringType.FullName, "AddPriceHistory response is false", await response.Content.ReadAsStringAsync());
                    return new MessagesViewModel
                    {
                        Result = false,
                        Messages = new List<string> { $"error in call api AddPriceHistory(AvvalMoney) error:" + await response.Content.ReadAsStringAsync() }
                    };
                }
            }
            catch (Exception ex)
            {
                WriteXmlDocument.AddException(MethodBase.GetCurrentMethod().DeclaringType.FullName, ex);
                return new MessagesViewModel
                {
                    Result = false,
                    Messages = new List<string> { $"error in call api AddPriceHistory(AvvalMoney) error: {(ex.Message.Length > 300 ? ex.Message.Substring(0, 300) : ex.Message)}" }
                };
            }
        }
    }
}
