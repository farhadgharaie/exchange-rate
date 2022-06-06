using Exchange.Common.interfaces;
using System;
using System.Collections.Generic;

namespace Exchange.Service
{
    public class CryptoExchangeService
    {
        private readonly ICurrency _cryptoCurrencies;
        private readonly ICryptoExchange _cryptoExchange;
        public CryptoExchangeService(ICurrency cryptoCurrencies,
            ICryptoExchange cryptoExchange)
        {
            _cryptoCurrencies = cryptoCurrencies;
            _cryptoExchange = cryptoExchange;
        }
        
        public Dictionary<string, double> ToTraditional(string cryptoSymbol, ICurrency traditionalCurrency )
        {
            if (!_cryptoCurrencies.IsSymbolExist(cryptoSymbol))
            {
                throw new Exception("Symbol not found");
            }

            var result = _cryptoExchange.ExchangeToTraditional(cryptoSymbol, traditionalCurrency);
            return result;
        }
        private double USDQuote(string cryptoSymbol)
        {
            return 1;
        }
    }

    
}
