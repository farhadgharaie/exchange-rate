using Exchange.Common.Currency;
using System.Collections.Generic;

namespace Exchange.Common.interfaces
{
    public interface ICurrency
    {
        IEnumerable<CurrencyModel> getAll();
        bool IsSymbolExist(string symbol);

    }
    public interface ICryptoCurrency : ICurrency
    {

    }
    public interface ITraditionalCurrency : ICurrency
    {

    }
}
