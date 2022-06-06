using Exchange.Common.interfaces;
using System;
using System.Collections.Generic;

namespace CryptoExchange.ACL
{
    
    public class CryptoExchange : ICryptoExchange
    {
        public Dictionary<string, double> ExchangeToTraditional(string cryptoSymbol, ICurrency traditionalCurrency)
        {
            var result = new Dictionary<string, double>();
            foreach (var currency in traditionalCurrency.getAll())
            {
                result.Add(currency.symbol, 1);
            }

            return result;
        }
    }
}
