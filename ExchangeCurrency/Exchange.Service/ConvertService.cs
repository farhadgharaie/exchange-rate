using System;
using System.Collections.Generic;

namespace Exchange.Service
{
    public class ConvertService
    {
        public Dictionary<string,double> ConvertCryptoToTraditional(string cryptoSymbol)
        {

            return new Dictionary<string, double>()
            {
                { "USD",1.1},
                { "EUR",1.2},
                { "BEL",1.3},
                { "GBP",1.4},
                { "AUD",1.5}
            };

        }
    }
}
