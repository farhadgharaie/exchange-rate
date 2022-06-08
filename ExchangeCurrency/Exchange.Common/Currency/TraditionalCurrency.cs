using Exchange.Common.interfaces;
using System.Collections.Generic;

namespace Exchange.Common.Currency
{
    public class TraditionalCurrency : Currency, ITraditionalCurrency
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
}
