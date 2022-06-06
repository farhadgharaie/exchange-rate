using System;
using System.Collections.Generic;
using System.Text;

namespace Exchange.Common.interfaces
{
    public interface ICryptoExchange
    {
        Dictionary<string, double> ExchangeToTraditional(string cryptoSymbol, ICurrency traditionalCurrency);
    }
}
