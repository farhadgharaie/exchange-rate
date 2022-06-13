using Exchange.Common.interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Common.Currency
{ 
        public class FiatCurrency : CurrencyTemplate,IFiatCurrency
    {
        private readonly IConvertAll _convertAll;
        private readonly IConvertSingle _convertSingle;
        public FiatCurrency(IConvertSingle convertSingle, IConvertAll convertAll)
        {
            _convertSingle = convertSingle;
            _convertAll = convertAll;
        }

        public override Task<double> ConvertTo(CurrencyModel fromCurrency, CurrencyModel toCurrency)
        {
            return _convertSingle.ConvertTo(fromCurrency, toCurrency);
        }

        public override Task<Dictionary<string, double>> ConvertToAll(CurrencyModel fromCurrency)
        {
            return _convertAll.ConvertTo(fromCurrency, Select());
        }

        public override IEnumerable<CurrencyModel> Select()
        {
            return new List<CurrencyModel>() {
            new CurrencyModel("Dollar", "USD"),
            new CurrencyModel("Euro", "EUR"),
            new CurrencyModel("Brazilian real", "BRL"),
            new CurrencyModel("British pound sterling", "GBP"),
            new CurrencyModel("Australian dollar", "AUD")
        };
        }
        
    }
}
