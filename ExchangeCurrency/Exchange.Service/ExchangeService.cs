using Exchange.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exchange.Service
{
    public class CurrencyModel
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
    }
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
    public class TraditionalCurrency : Currency
    {
        private static List<CurrencyModel> currencies = new List<CurrencyModel>
        {
            new CurrencyModel
            {
                Name="Dollar",
                Symbol= "USD"
            },
            new CurrencyModel
            {
                Name="Euro",
                Symbol= "EUR"
            },
            new CurrencyModel
            {
                Name="Brazilian real",
                Symbol= "BRL"
            },
            new CurrencyModel
            {
                Name="British pound sterling",
                Symbol= "GBP"
            },
            new CurrencyModel
            {
                Name="Australian dollar",
                Symbol= "AUD"
            }
        };
        public TraditionalCurrency() : base(currencies)
        {

        }
    }
    public class CryptoCurrency : Currency
    {
        private static List<CurrencyModel> currencies = new List<CurrencyModel>
        {
             new CurrencyModel
            {
                Name="Bitcoin",
                Symbol= "BTC"
            }
        };
        public CryptoCurrency():base(currencies)
        {

        }
       
    }
    public interface ICurrency
    {
        IEnumerable<CurrencyModel> getAll();
        bool IsSymbolExist(string symbol);

    }

    public class CryptoExchangeService
    {
        private readonly ICurrency _cryptoCurrencies;
        public CryptoExchangeService(ICurrency cryptoCurrencies)
        {
            _cryptoCurrencies = cryptoCurrencies;
        }
        
        public Dictionary<string, double> ToTraditional(string cryptoSymbol, ICurrency traditionalCurrencies)
        {
            if (!_cryptoCurrencies.IsSymbolExist(cryptoSymbol))
            {
                throw new Exception("Symbol not found");
            }
            var result = new Dictionary<string, double>();
            foreach (var currency in traditionalCurrencies.getAll())
            {
                result.Add(currency.Symbol, 1);
            }
            return result;
        }
    }

    
}
