using Exchange.Common.Currency;
using Exchange.Common.interfaces;
using System.Threading.Tasks;

namespace ExchangeCurrency.Specs.Fakes
{
    public class FakeCryptoConvertTo : IConvertSingle
    {
        public Task<double> ConvertTo(CurrencyModel fromCurrency, CurrencyModel toCurrency)
        {
            return Task.FromResult(1.0);
        }
    }
}
