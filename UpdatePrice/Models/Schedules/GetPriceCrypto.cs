using DNTScheduler;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UpdatePrice.Models.Apis;
using UpdatePrice.Models.Enums;
using UpdatePrice.Models.Repositories.p01;
using UpdatePrice.Models.Services;
using UpdatePrice.Models.ViewModels;
using UpdatePrice.Models.XmlDocument;

namespace UpdatePrice.Models.Schedules
{
    public class GetPriceCrypto : ScheduledTaskTemplate
    {
        private IGetPrice _getPrice;
        private readonly NobitexApi nobitexApi;
        private readonly HdPayApi hdPayApi;
        private ResultNobitexViewModel resultNobitex;
        private HDPayPriceViewModel hdPayPriceBuy, hdPayPriceSell;
        private bool hdPayPriceBuyResult = false, hdPayPriceSellResult = false;
        private readonly PaymentServiceRepository paymentServiceRepository;
        private readonly PriceHistoryRepository priceHistoryRepository;
        DateTime dateTimeNow;
        MessagesViewModel updateBuyPriceResult, updateSellPriceResult, addPriceHistory;
        private readonly AvvalMoneyApi avvalMoneyApi;
        decimal sellCommision, buyCommision = 0;

        public GetPriceCrypto(IGetPrice getPrice)
        {
            _getPrice = getPrice;
            nobitexApi = new NobitexApi();
            hdPayApi = new HdPayApi();
            paymentServiceRepository = new PaymentServiceRepository();
            priceHistoryRepository = new PriceHistoryRepository();
            avvalMoneyApi = new AvvalMoneyApi();
        }

        public override string Name
        {
            get { return "GetPriceCrypto"; }
        }

        public override int Order
        {
            get { return 1; }
        }

        public override bool RunAt(DateTime utcNow)
        {
            if (this.IsShuttingDown || this.Pause)
                return false;

            dateTimeNow = utcNow.AddHours(3.5);

            return (dateTimeNow.Minute == 10 && dateTimeNow.Second == 0) || (dateTimeNow.Minute == 20 && dateTimeNow.Second == 0) || (dateTimeNow.Minute == 30 && dateTimeNow.Second == 0) || (dateTimeNow.Minute == 40 && dateTimeNow.Second == 0) || (dateTimeNow.Minute == 50 && dateTimeNow.Second == 0) || (dateTimeNow.Minute == 0 && dateTimeNow.Second == 0);
            //return dateTimeNow.Second == 0 || dateTimeNow.Second == 30;
        }

        public override async void Run()
        {
            if (IsShuttingDown || Pause)
                return;

            //در اینجا تایمر متوقف می شود
            this.Pause = true;

            try
            {
                await CallHdPayApi();
            }
            catch (Exception ex)
            {
                WriteXmlDocument.AddException(MethodBase.GetCurrentMethod().DeclaringType.FullName, ex);
                _getPrice.ShowMessage($"Error in central try catch. Exception is: {ex.Message}");
            }
            finally
            {
                _getPrice.ShowMessage("-----------------------------------------");
                //در اینجا تایمر شروع به کار می کند
                this.Pause = false;
            }
        }

        private async Task CallHdPayApi()
        {
            _getPrice.ShowMessage($"call api for get buy price");
            hdPayPriceBuy = await hdPayApi.GetBuyPriceTether();
            _getPrice.ShowMessage(hdPayPriceBuy.Message);
            if (hdPayPriceBuy.Result)
            {
                _getPrice.ShowMessage($"call api for update buy price");
                buyCommision = Convert.ToDecimal(ConfigurationManager.AppSettings["HDPayBuyPriceCommision"]);
                updateBuyPriceResult = await avvalMoneyApi.UpdatePrice(hdPayPriceBuy.Price + (buyCommision), BuyAndSellTypeEnum.Buy);
                if (updateBuyPriceResult.Result)
                {
                    hdPayPriceBuyResult = true;
                }
                else
                {
                    hdPayPriceBuyResult = false;
                }
                _getPrice.ShowMessage(updateBuyPriceResult.Messages.FirstOrDefault());
            }

            _getPrice.ShowMessage($"call api for get sell price");
            hdPayPriceSell = await hdPayApi.GetSellPriceTether();
            _getPrice.ShowMessage(hdPayPriceSell.Message);
            if (hdPayPriceSell.Result)
            {
                _getPrice.ShowMessage($"call api for update sell price");
                sellCommision = Convert.ToDecimal(ConfigurationManager.AppSettings["HDPaySellPriceCommision"]);
                updateSellPriceResult = await avvalMoneyApi.UpdatePrice(hdPayPriceSell.Price + (sellCommision), BuyAndSellTypeEnum.Sell);
                if (updateSellPriceResult.Result)
                {
                    hdPayPriceSellResult = true;
                }
                else
                {
                    hdPayPriceSellResult = false;
                }
                _getPrice.ShowMessage(updateSellPriceResult.Messages.FirstOrDefault());
            }

            if (hdPayPriceBuyResult && hdPayPriceSellResult)
            {
                _getPrice.ShowMessage($"call api for priceHistory");
                addPriceHistory = await avvalMoneyApi.AddPriceHistory(hdPayPriceBuy.Price+(buyCommision), hdPayPriceSell.Price+(sellCommision), 2);

                _getPrice.ShowMessage(addPriceHistory.Messages.FirstOrDefault());
            }

        }

        private async Task CallNubitexApi()
        {
            _getPrice.ShowMessage($"call api for get price");
            resultNobitex = await nobitexApi.GetPrice(CurrencyTypeEnum.Tether);
            if (resultNobitex.ResultApi)
            {
                _getPrice.ShowMessage(resultNobitex.Message ?? "");
                if (resultNobitex.Status == "ok")
                {
                    if (!resultNobitex.Stats.USDT_RLS.IsClosed)
                    {
                        if (paymentServiceRepository.EditPriceNobitex(Convert.ToDecimal(resultNobitex.Stats.USDT_RLS.Latest)))
                        {
                            _getPrice.ShowMessage("updated Price in database");
                        }
                        else
                        {
                            _getPrice.ShowMessage("error update Price in database");
                        }
                    }
                    else
                    {
                        _getPrice.ShowMessage($"rls tether is closed");
                    }
                }
                else
                {
                    _getPrice.ShowMessage($"error tether: resultNobitex.Status in not ok");
                }
            }
            else
            {
                _getPrice.ShowMessage($"call api tether is error exception: {resultNobitex.Message ?? ""}");
            }
        }
    }
}
