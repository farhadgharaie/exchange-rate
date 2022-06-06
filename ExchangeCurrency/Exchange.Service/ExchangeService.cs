using System;
using System.Collections.Generic;
using System.Linq;

namespace Exchange.Service
{
    public class Currency
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
    }
    public class TraditionalCurrency : ICurrency
    {
        public IEnumerable<Currency> currencies = new List<Currency>
        {
            new Currency
            {
                Name="Dollar",
                Symbol= "USD"
            },
            new Currency
            {
                Name="Euro",
                Symbol= "EUR"
            },
            new Currency
            {
                Name="Brazilian real",
                Symbol= "BRL"
            },
            new Currency
            {
                Name="British pound sterling",
                Symbol= "GBP"
            },
            new Currency
            {
                Name="Australian dollar",
                Symbol= "AUD"
            }
        };
        public IEnumerable<Currency> getAll()
        {
            return currencies;
        }
    }
    public class CryptoCurrency : ICurrency
    {
        public IEnumerable<Currency> currencies = new List<Currency>
        {
             new Currency
            {
                Name="Bitcoin",
                Symbol= "BTC"
            }
        };
        public IEnumerable<Currency> getAll()
        {
            return currencies;
        }
    }
    public interface ICurrency
    {
        IEnumerable<Currency> getAll();
    }

    public class CryptoExchangeService : ExchangeService 
    {
        private readonly ICurrency _cryptoCurrencies;
        public CryptoExchangeService(ICurrency cryptoCurrencies, ICurrency convertTo):base (convertTo)
        {
            _cryptoCurrencies = cryptoCurrencies;
        }
        
        public Dictionary<string, double> ToTraditional(string cryptoSymbol)
        {
            if (!isExist(cryptoSymbol))
            {
                throw new Exception("Symbol not found");
            }
            var result = new Dictionary<string, double>();
            foreach (var currency in _convertTo.getAll())
            {
                result.Add(currency.Symbol, 1);
            }
            return result;

           
        }
        private bool isExist(string cryptoSymbol)
        {
            return _cryptoCurrencies.getAll().ToList().Exists(a => a.Symbol.Contains(cryptoSymbol, StringComparison.OrdinalIgnoreCase));
        }
    }

    public class ExchangeService
    {
        protected readonly ICurrency _convertTo;
        public ExchangeService(ICurrency convertTo)
        {
            _convertTo = convertTo;
        }
    }
}
