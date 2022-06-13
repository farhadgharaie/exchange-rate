using Exchange.Common.Currency;
using Exchange.Common.interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeCurrency.Specs.Fakes
{
    public class FakeFiatConvertAll : IConvertAll
    {
        public Task<Dictionary<string, double>> ConvertTo(CurrencyModel fromCurrency, IEnumerable<CurrencyModel> convertTo)
        {
            return Task.FromResult(new Dictionary<string, double>()
            {
                { "USD", 1 },
                { "AUD", 1 },
                { "EUR", 1 },
                { "GBP", 1 },
                { "BRL", 1 },
            });
        }
    }
}
