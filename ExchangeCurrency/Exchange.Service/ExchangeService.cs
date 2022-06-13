using Exchange.Common.interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Service
{
    public class CryptoExchangeService : ICryptoExchangeService
    {
        private readonly ICryptoCurrency _cryptoCurrencies;
        public CryptoExchangeService(ICryptoCurrency cryptoCurrencies)
        {
            _cryptoCurrencies = cryptoCurrencies;
        }
        
        public async Task<Dictionary<string, double>> ToFiat(string cryptoSymbol, ICurrency fiatCurrency)
        {
            var cryptoModel= _cryptoCurrencies.Get(cryptoSymbol);
            var usdModel = fiatCurrency.Get("usd");

            var amount = await _cryptoCurrencies.ConvertTo(cryptoModel, usdModel);
            var convertToFiat = await fiatCurrency.ConvertToAll(usdModel);

            var result = new Dictionary<string, double>();
            foreach (var rate in convertToFiat)
            {
                result.Add(rate.Key, Math.Round(rate.Value * amount, 2));
            }
            return result;
        }
    }
}
