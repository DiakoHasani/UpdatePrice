using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UpdatePrice.Models.Enums;
using UpdatePrice.Models.Helpers;
using UpdatePrice.Models.ViewModels;
using UpdatePrice.Models.XmlDocument;

namespace UpdatePrice.Models.Apis
{
    public class NobitexApi : BaseApi
    {
        private ResultNobitexViewModel resultNobitex;
        public async Task<ResultNobitexViewModel> GetPrice(CurrencyTypeEnum currency)
        {
            resultNobitex = new ResultNobitexViewModel();
            try
            {
                var parameters = new Dictionary<string, string> {
                    { "srcCurrency", currency.GetDescription() },
                    { "dstCurrency", "rls" }
                };

                var response = await Post(NobitexUrl + "market/stats", parameters);

                if (response.IsSuccessStatusCode)
                {
                    resultNobitex = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultNobitexViewModel>(await response.Content.ReadAsStringAsync());
                    resultNobitex.Message = $"{currency.GetDescription()}: call success api in nobitex";
                }
                WriteXmlDocument.AddError(MethodBase.GetCurrentMethod().DeclaringType.FullName, "GetPrice Nobitext response is false", await response.Content.ReadAsStringAsync());
                resultNobitex.ResultApi = response.IsSuccessStatusCode;
            }
            catch(Exception ex)
            {
                WriteXmlDocument.AddException(MethodBase.GetCurrentMethod().DeclaringType.FullName, ex);
                resultNobitex.Message = $"{currency.GetDescription()}: {ex.Message}";
            }
            return resultNobitex;
        }
    }
}
