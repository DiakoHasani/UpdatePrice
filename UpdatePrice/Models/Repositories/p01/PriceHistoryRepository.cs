using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UpdatePrice.Models.Entities.p01;
using UpdatePrice.Models.XmlDocument;

namespace UpdatePrice.Models.Repositories.p01
{
    public class PriceHistoryRepository
    {
        private readonly ExchangeDBEntities exchangeDBEntities;
        decimal buyCommisionHdPay, sellCommisionHdPay;
        PriceHistory lastPriceHistory;
        public PriceHistoryRepository()
        {
            exchangeDBEntities = new ExchangeDBEntities();
        }

        public bool AddHdPay(decimal buyPrice, decimal sellPrice, int paymentServiceId)
        {
            try
            {
                lastPriceHistory = GetLast();

                buyCommisionHdPay = Convert.ToDecimal(ConfigurationManager.AppSettings["HDPayBuyPriceCommision"]);
                sellCommisionHdPay = Convert.ToDecimal(ConfigurationManager.AppSettings["HDPaySellPriceCommision"]);
                buyPrice += buyCommisionHdPay;
                sellPrice += sellCommisionHdPay;

                if (lastPriceHistory == null || (lastPriceHistory.BuyPrice != buyPrice || lastPriceHistory.SellPrice != sellPrice))
                {
                    exchangeDBEntities.PriceHistories.Add(new PriceHistory
                    {
                        BuyPrice = buyPrice,
                        SellPrice = sellPrice,
                        PaymentServiceId = paymentServiceId,
                        CreateDate = DateTime.Now
                    });
                    exchangeDBEntities.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                WriteXmlDocument.AddException(MethodBase.GetCurrentMethod().DeclaringType.FullName, ex);
                return false;
            }
        }

        public PriceHistory GetLast()
        {
            try
            {
                return exchangeDBEntities.PriceHistories.OrderByDescending(a => a.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                WriteXmlDocument.AddException(MethodBase.GetCurrentMethod().DeclaringType.FullName, ex);
                return null;
            }
        }
    }
}
