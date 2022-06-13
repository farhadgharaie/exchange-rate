using Exchange.Common.Currency;
using System.Collections.Generic;

namespace Exchange.Common.interfaces
{
    public interface ICurrency
    {
        IEnumerable<CurrencyModel> GetAll();
        bool IsSymbolExist(string symbol);
        CurrencyModel Get(string symbol);
    }
    public interface IFiatCurrency : ICurrency
    {
    }
    public interface ICryptoCurrency : ICurrency
    {
    }

}
