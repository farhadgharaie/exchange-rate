using Exchange.Common.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exchange.Common.Currency
{
    public class Currency : ICurrency
    {
        public IEnumerable<CurrencyModel> _currencies { get; set; }
        public Currency(IEnumerable<CurrencyModel> currencies)
        {
            _currencies = currencies;
        }
        public IEnumerable<CurrencyModel> getAll()
        {
            return _currencies;
        }

        public bool IsSymbolExist(string symbol)
        {
            return _currencies.Any(a => a.Symbol.Contains(symbol, StringComparison.OrdinalIgnoreCase));
        }
    }
}
