using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdatePrice.Models.Entities.p01;
using System.Configuration;
using UpdatePrice.Models.XmlDocument;
using System.Reflection;

namespace UpdatePrice.Models.Repositories.p01
{
    public class PaymentServiceRepository
    {
        private readonly ExchangeDBEntities exchangeDBEntities;
        private PaymentService paymentService;
        decimal commision = 0;

        public PaymentServiceRepository()
        {
            exchangeDBEntities = new ExchangeDBEntities();
        }

        public PaymentService GetByUnit(string unit)
        {
            try
            {
                return exchangeDBEntities.PaymentServices.Where(a => a.Unit == "USDT").FirstOrDefault();
            }
            catch (Exception ex)
            {
                WriteXmlDocument.AddException(MethodBase.GetCurrentMethod().DeclaringType.FullName, ex);
                return null;
            }
        }

        public bool EditPriceNobitex(decimal price)
        {
            try
            {
                price = price / 10;
                paymentService = exchangeDBEntities.PaymentServices.Where(a => a.Unit == "USDT").FirstOrDefault();
                paymentService.BuyPrice = (price + ((price * (0.5M)) / 100));
                paymentService.SellPrice = (price - ((price * (0.95M)) / 100));
                exchangeDBEntities.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                WriteXmlDocument.AddException(MethodBase.GetCurrentMethod().DeclaringType.FullName, ex);
                return false;
            }
        }

        public bool EditBuyPriceHdPay(decimal buyPrice)
        {
            try
            {
                commision = Convert.ToDecimal(ConfigurationManager.AppSettings["HDPayBuyPriceCommision"]);
                paymentService = exchangeDBEntities.PaymentServices.Where(a => a.Unit == "USDT").FirstOrDefault();
                paymentService.BuyPrice = buyPrice + (commision);
                exchangeDBEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                WriteXmlDocument.AddException(MethodBase.GetCurrentMethod().DeclaringType.FullName, ex);
                return false;
            }
        }
        public bool EditSellPriceHdPay(decimal sellPrice)
        {
            try
            {
                commision = Convert.ToDecimal(ConfigurationManager.AppSettings["HDPaySellPriceCommision"]);
                paymentService = exchangeDBEntities.PaymentServices.Where(a => a.Unit == "USDT").FirstOrDefault();
                paymentService.SellPrice = sellPrice + (commision);
                exchangeDBEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                WriteXmlDocument.AddException(MethodBase.GetCurrentMethod().DeclaringType.FullName, ex);
                return false;
            }
        }
    }
}
