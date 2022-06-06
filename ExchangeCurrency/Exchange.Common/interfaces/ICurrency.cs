using Exchange.Common.Currency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exchange.Common.interfaces
{
    public interface ICurrency
    {
        IEnumerable<CurrencyModel> getAll();
        bool IsSymbolExist(string symbol);

    }
}
