using Exchange.Common.Currency;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Common.interfaces
{
    public interface ICurrency
    {
        IEnumerable<CurrencyModel> GetAll();
        bool IsSymbolExist(string symbol);
        CurrencyModel Get(string symbol);
        Task<Dictionary<string, double>> ConvertToAll(CurrencyModel fromCurrency);
        Task<double> ConvertTo(CurrencyModel fromCurrency, CurrencyModel toCurrency);
    }
    public interface IFiatCurrency : ICurrency
    {
    }
    public interface ICryptoCurrency : ICurrency
    {
    }

}
