using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoExchange.ACL.ExchangeRates.ExchnageRatesModel
{
    public class ExchangeRateAPIResponse
    {
        public bool success { get; set; }
        public int timestamp { get; set; }
        public string @base { get; set; }
        public string date { get; set; }
        public Dictionary<string, double> rates { get; set; }
    }
}
