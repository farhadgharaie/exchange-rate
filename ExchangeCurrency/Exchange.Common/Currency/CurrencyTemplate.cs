using Exchange.Common.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exchange.Common.Currency
{
    public abstract class CurrencyTemplate : ICurrency
    {
        public abstract IEnumerable<CurrencyModel> Select();
        public IEnumerable<CurrencyModel> GetAll()
        {
            return Select();
        }
        public bool IsSymbolExist(string symbol)
        {
            var isSymbolExist= Select().Any(a => a.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase));
            return isSymbolExist;
        }

        public CurrencyModel Get(string symbol)
        {
            if (IsSymbolExist(symbol))
            {
                return Select().FirstOrDefault(currency => currency.Symbol.ToLower() == symbol.ToLower());
            }
            return null;
        }
    }

}
