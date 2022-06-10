using Exchange.Common.interfaces;
using System.Threading.Tasks;

namespace ExchangeCurrency.Specs.Fakes
{
    public class FakeCryptotoUSD : ICryptoToUSD
    {
        public Task<double> GetUSDQuoteAsync(string cryptoSymbol)
        {
            return Task.FromResult(1.0);
        }
    }
}
