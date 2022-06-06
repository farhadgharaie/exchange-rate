using System.Collections.Generic;

namespace Exchange.Common.Currency
{
    public class CryptoCurrency : Currency
    {
        private static List<CurrencyModel> currencies = new List<CurrencyModel>
        {
             new CurrencyModel("Bitcoin", "BTC")
        };
        public CryptoCurrency() : base(currencies)
        {

        }

    }
}
