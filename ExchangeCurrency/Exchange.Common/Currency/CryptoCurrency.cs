using Exchange.Common.interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Common.Currency
{
    public class CryptoCurrency : CurrencyTemplate,ICryptoCurrency
    {
        private readonly IConvertSingle _convertSingle;
        private readonly IConvertAll _convertAll;
        public CryptoCurrency(IConvertSingle convertSingle,IConvertAll convertAll)
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
               new CurrencyModel("Bitcoin", "BTC")
           };
        }
    }
}
