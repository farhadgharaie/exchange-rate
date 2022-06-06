using Exchange.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exchange.Service
{
    public class CurrencyModel
    {
        protected string _name { get; private set; }
        public string symbol { get; private set; }
        public CurrencyModel(string name,string symbol)
        {
            _name = name;
            this.symbol = symbol;

        }
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
            return _currencies.Any(a => a.symbol.Contains(symbol, StringComparison.OrdinalIgnoreCase));
        }
    }
    public class TraditionalCurrency : Currency
    {
        private static List<CurrencyModel> currencies = new List<CurrencyModel>
        {
            new CurrencyModel("Dollar","USD"),
            new CurrencyModel("Euro","EUR"),
            new CurrencyModel("Brazilian real","BRL"),
            new CurrencyModel("British pound sterling","GBP"),
            new CurrencyModel("Australian dollar","AUD")
           
        };
        public TraditionalCurrency() : base(currencies)
        {

        }
    }
    public class CryptoCurrency : Currency
    {
        private static List<CurrencyModel> currencies = new List<CurrencyModel>
        {
             new CurrencyModel("Bitcoin", "BTC")
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
                result.Add(currency.symbol, 1);
            }
            return result;
        }
    }

    
}
