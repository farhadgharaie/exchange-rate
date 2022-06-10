using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Common.interfaces
{
    public interface ICryptoToUSD
    {
        Task<double> GetUSDQuoteAsync(string cryptoSymbol);
    }
    public interface IExchangeBaseOnUSD
    {
        Task<Dictionary<string, double>> ExchangeBaseOnUSD(string[] exchangeTo);
    }
}
