using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Common.interfaces
{
    public interface ICryptoExchangeService
    {
        Task<Dictionary<string, double>> ToFiat(string cryptoSymbol, IFiatCurrency fiatCurrency);
    }
}
