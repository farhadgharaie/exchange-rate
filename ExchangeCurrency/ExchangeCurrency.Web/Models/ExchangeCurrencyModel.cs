using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeCurrency.Web.Models
{
    public class ExchangeCurrencyModel
    {
        public string Symbol { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public Dictionary<string,double> Exchange { get; set; }
    }
}
