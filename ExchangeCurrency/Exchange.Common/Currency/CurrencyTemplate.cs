using Exchange.Common.CustomException;
using Exchange.Common.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public abstract Task<Dictionary<string, double>> ConvertToAll(CurrencyModel fromCurrency);
       
        public abstract Task<double> ConvertTo(CurrencyModel fromCurrency, CurrencyModel toCurrency);

        public CurrencyModel Get(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new SymbolNotProvidedException();
            }
            else if (!IsSymbolExist(symbol))
            {
                throw new SymbolNotFoundException();
            }
            return Select().FirstOrDefault(currency => currency.Symbol.ToLower() == symbol.ToLower());
        }

    }

}
