using Exchange.Common.Currency;
using System.Threading.Tasks;

namespace Exchange.Common.interfaces
{
    public interface IConvertSingle
    {
        Task<double> ConvertTo(CurrencyModel fromCurrency, CurrencyModel toCurrency);
    }

}
