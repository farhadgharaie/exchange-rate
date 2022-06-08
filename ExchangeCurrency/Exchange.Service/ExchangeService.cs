﻿using Exchange.Common.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Service
{
    public class CryptoExchangeService
    {
        private readonly ICryptoCurrency _cryptoCurrencies;
        private readonly ICryptoToUSD _cryptoToUSD;
        private readonly IExchangeBaseOnUSD _exchangeBaseOnUSD;
        public CryptoExchangeService(ICryptoCurrency cryptoCurrencies,
                                     ICryptoToUSD cryptoToUsD,
                                     IExchangeBaseOnUSD exchangeBaseOnUSD
            )
        {
            _cryptoCurrencies = cryptoCurrencies;
            _cryptoToUSD = cryptoToUsD;
            _exchangeBaseOnUSD = exchangeBaseOnUSD;
        }

        public async Task<Dictionary<string, double>> ToTraditional(string cryptoSymbol, ITraditionalCurrency traditionalCurrency)
        {
            if (!_cryptoCurrencies.IsSymbolExist(cryptoSymbol))
            {
                throw new Exception("Symbol not found");
            }

            var uSD = await GetUSDQuote(cryptoSymbol);

            var result = await ExchangeFromUSDToTraditional(uSD, traditionalCurrency);

            return result;
        }

        private async Task<double> GetUSDQuote(string symbol)
        {
            var result = await _cryptoToUSD.GetUSDQuoteAsync(symbol);
            if (result == 0)
            {
                throw new Exception("No USD base quote provided");
            }
            return result;
        }
        private async Task<Dictionary<string, double>> ExchangeFromUSDToTraditional(double uSDrate, ITraditionalCurrency traditionalCurrency)
        {
            string[] exchangeTo = traditionalCurrency.getAll().Select(a => a.Symbol).ToArray();

            var exchangerate = await _exchangeBaseOnUSD.ExchangeBaseOnUSD(exchangeTo);
            var result = new Dictionary<string, double>();
            foreach (var rate in exchangerate)
            {
                result.Add(rate.Key, rate.Value * uSDrate);
            }
            return result;
        }

    }
}
