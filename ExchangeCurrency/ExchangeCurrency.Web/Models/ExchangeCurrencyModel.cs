using System;
using System.Collections.Generic;

namespace ExchangeCurrency.Web.Models
{
    public class ExchangeCurrencyModel
    {
        public string Symbol { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public Dictionary<string,double> Exchange { get; set; }
    }
}
