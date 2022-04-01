using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Service
{
    public class ExchangeTest : IExchangeService
    {
        private readonly Random _rdm = new();
        public decimal Calculate(string originCoin, string destinationCoin, decimal value) =>
            value * (decimal)_rdm.NextDouble();
        
    }
}
