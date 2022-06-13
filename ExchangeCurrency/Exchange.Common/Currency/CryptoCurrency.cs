using Exchange.Common.interfaces;
using System.Collections.Generic;

namespace Exchange.Common.Currency
{
    public class CryptoCurrency : CurrencyTemplate,ICryptoCurrency
    {
        public override IEnumerable<CurrencyModel> Select()
        {
            return new List<CurrencyModel>() {
               new CurrencyModel("Bitcoin", "BTC")
           };
        }
    }
}
