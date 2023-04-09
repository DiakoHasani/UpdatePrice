using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UpdatePrice.Models.ViewModels;
using UpdatePrice.Models.XmlDocument;

namespace UpdatePrice.Models.Apis
{
    public class HdPayApi : BaseApi
    {
        public async Task<HDPayPriceViewModel> GetBuyPriceTether()
        {
            try
            {
                var response = await Get(HdPayUrl + "ajax/exchange/update-rate?c1=1&c2=12");
                if (response == null)
                {
                    return new HDPayPriceViewModel
                    {
                        Message = "response is null in get buy price hdpay"
                    };
                }

                if (!response.IsSuccessStatusCode)
                {
                    WriteXmlDocument.AddError(MethodBase.GetCurrentMethod().DeclaringType.FullName, "GetBuyPriceTether response is false", await response.Content.ReadAsStringAsync());
                    return new HDPayPriceViewModel
                    {
                        Message = "error call api in get buy price hdpay"
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var amountSplit = result.Split('|');
                if (amountSplit.Length > 1)
                {
                    return new HDPayPriceViewModel
                    {
                        Price = Convert.ToDecimal(amountSplit[1]),
                        Message = "success call api hdpay and get buy price",
                        Result = true
                    };
                }
                else
                {
                    return new HDPayPriceViewModel
                    {
                        Message = "error call api in get buy price hdpay"
                    };
                }
            }
            catch (Exception ex)
            {
                WriteXmlDocument.AddException(MethodBase.GetCurrentMethod().DeclaringType.FullName, ex);
                return new HDPayPriceViewModel
                {
                    Message = ex.Message
                };
            }
        }

        public async Task<HDPayPriceViewModel> GetSellPriceTether()
        {
            try
            {
                var response = await Get(HdPayUrl + "ajax/exchange/update-rate?c1=12&c2=1");
                if (response == null)
                {
                    return new HDPayPriceViewModel
                    {
                        Message = "response is null in get sell price hdpay"
                    };
                }

                if (!response.IsSuccessStatusCode)
                {
                    WriteXmlDocument.AddError(MethodBase.GetCurrentMethod().DeclaringType.FullName, "GetSellPriceTether response is false", await response.Content.ReadAsStringAsync());
                    return new HDPayPriceViewModel
                    {
                        Message = "error call api in get sell price hdpay"
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var amountSplit = result.Split('|');
                if (amountSplit.Length > 1)
                {
                    return new HDPayPriceViewModel
                    {
                        Price = Convert.ToDecimal(amountSplit[1]),
                        Message = "success call api hdpay and get sell price",
                        Result = true
                    };
                }
                else
                {
                    return new HDPayPriceViewModel
                    {
                        Message = "error call api in get sell price hdpay"
                    };
                }
            }
            catch (Exception ex)
            {
                WriteXmlDocument.AddException(MethodBase.GetCurrentMethod().DeclaringType.FullName, ex);
                return new HDPayPriceViewModel
                {
                    Message = ex.Message
                };
            }
        }
    }
}
