using ByteBank.Portal.Infra;
using ByteBank.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Controller
{
    public class ExchangeController : BaseController
    {
        private IExchangeService _exchangeService;

        public ExchangeController()
        {
            _exchangeService = new ExchangeTest();
        }
        public string MXN()
        {
            var result = _exchangeService.Calculate("MXN", "BRL", 1);
            
            var textPage = View();

            return textPage.Replace("BRL_VALUE", result.ToString());
        }

        public string USD()
        {
            return null;
        }

        public string Estimate(string originCoin, string destinationCoin, decimal value)
        {
            var result = _exchangeService.Calculate(originCoin, destinationCoin, value);
               var textPage = View();

            return textPage
                    .Replace("COIN_VALUE_ORIGIN", result.ToString())
                    .Replace("ORIGIN_COIN", result.ToString())
                    .Replace("DESTINATION_COIN", result.ToString())
                    .Replace("COIN_VALUE_DESTINATION", result.ToString());
        }
    }
}
