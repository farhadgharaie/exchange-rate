using CryptoExchange.ACL.CoinMarketCap;
using CryptoExchange.ACL.CoinMarketCapModel;
using CryptoExchange.ACL.ExchangeRates;
using Exchange.Common.interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoExchange.ACL
{
    public class CryptoExchange : ICryptoExchange
    {
        public async Task<Dictionary<string, double>> GetExchangeToTraditionalAsync(string cryptoSymbol, ICurrency traditionalCurrency)
        {
            var uSD = await  GetUSDQuote(cryptoSymbol);
            
            var result= await ExchangeFromUSDToTraditional(uSD, traditionalCurrency);

            return result;
        }
        private async Task<double> GetUSDQuote(string symbol)
        {
            var result =await  new CoinMarketCapAPI().GetUSDQuoteAsync(symbol);

            return result;
        }
        private async Task<Dictionary<string, double>> ExchangeFromUSDToTraditional(double uSDrate,ICurrency traditionalCurrency)
        {
            string[] exchangeTo = traditionalCurrency.getAll().Select(a => a.symbol).ToArray();

            return await new ExchangeRatesAPI().Exchange(uSDrate, exchangeTo);
        }
    }
}
