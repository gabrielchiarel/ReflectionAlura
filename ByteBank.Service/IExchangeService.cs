using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Service
{
    public interface IExchangeService
    {
        decimal Calculate(string originCoin, string destinationCoin, decimal value);
    }
}
