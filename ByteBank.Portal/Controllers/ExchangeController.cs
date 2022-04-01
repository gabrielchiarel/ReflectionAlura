using ByteBank.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Controllers
{
    public class ExchangeController
    {
        private IExchangeService _exchangeService;

        public ExchangeController()
        {
            _exchangeService = new ExchangeTest();
        }
        public string GetMXN()
        {
            var result = _exchangeService.Calculate("MXN", "BRL", 1);
            var resourceName = "ByteBank.Portal.View.Cambio.MXN.html";
            var assembly = Assembly.GetExecutingAssembly();
            
            var streamResource = assembly.GetManifestResourceStream(resourceName);

            var streamReader = new StreamReader(streamResource);
            var textPage = streamReader.ReadToEnd();

            return textPage.Replace("BRL_VALUE", result.ToString());
        }

        public string GetUSD()
        {
            var result = _exchangeService.Calculate("USD", "BRL", 1);
            var resourceName = "ByteBank.Portal.View.Cambio.USD.html";
            var assembly = Assembly.GetExecutingAssembly();

            var streamResource = assembly.GetManifestResourceStream(resourceName);

            var streamReader = new StreamReader(streamResource);
            var textPage = streamReader.ReadToEnd();

            return textPage.Replace("BRL_VALUE", result.ToString());
        }
    }
}
