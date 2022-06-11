using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Common.interfaces
{
    public interface ICryptoExchange
    {
        Task<Dictionary<string, double>> GetExchangeToTraditionalAsync(string cryptoSymbol, ICurrency traditionalCurrency);
    }
}
