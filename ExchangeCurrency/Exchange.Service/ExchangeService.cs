using Exchange.Common.CustomException;
using Exchange.Common.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Service
{
   
    public class CryptoExchangeService : ICryptoExchangeService
    {
        private readonly ICurrency _cryptoCurrencies;
        private readonly ICryptoToUSD _cryptoToUSD;
        private readonly IExchangeBaseOnUSD _exchangeBaseOnUSD;
        public CryptoExchangeService(ICurrency cryptoCurrencies,
                                     ICryptoToUSD cryptoToUsD,
                                     IExchangeBaseOnUSD exchangeBaseOnUSD
            )
        {
            _cryptoCurrencies = cryptoCurrencies;
            _cryptoToUSD = cryptoToUsD;
            _exchangeBaseOnUSD = exchangeBaseOnUSD;
        }

        public async Task<Dictionary<string, double>> ToFiat(string cryptoSymbol, ICurrency fiatCurrency)
        {
            InputValidation(cryptoSymbol);

            var uSD = await GetUSDQuote(cryptoSymbol);

            var result = await ExchangeFromUSDToFiat(uSD, fiatCurrency);

            return result;
        }

        private void InputValidation(string cryptoSymbol)
        {
            if (string.IsNullOrEmpty(cryptoSymbol))
            {
                throw new SymbolNotProvidedException();
            }
            if (!_cryptoCurrencies.IsSymbolExist(cryptoSymbol))
            {
                throw new SymbolNotFoundException();
            }
        }

        private async Task<double> GetUSDQuote(string symbol)
        {
            var result = await _cryptoToUSD.GetUSDQuoteAsync(symbol);
            if (result <= 0)
            {
                throw new NoUSDBaseQuoteProvidedException();
            }
            return result;
        }
        private async Task<Dictionary<string, double>> ExchangeFromUSDToFiat(double uSDrate, ICurrency fiatCurrency)
        {
            string[] exchangeTo = fiatCurrency.GetAll().Select(a => a.Symbol).ToArray();

            var exchangerate = await _exchangeBaseOnUSD.ExchangeBaseOnUSD(exchangeTo);
            var result = new Dictionary<string, double>();
            foreach (var rate in exchangerate)
            {
                result.Add(rate.Key,Math.Round( rate.Value * uSDrate,2));
            }
            return result;
        }

    }
}
