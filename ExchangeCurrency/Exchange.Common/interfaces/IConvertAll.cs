using Exchange.Common.Currency;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Common.interfaces
{
    public interface IConvertAll
    {
        Task<Dictionary<string, double>> ConvertTo(CurrencyModel fromCurrency, IEnumerable<CurrencyModel> convertTo);
    }

}
