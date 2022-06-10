using Exchange.Common.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeCurrency.Specs.Fakes
{
    public class FakeExchangeBaseOnUSD : IExchangeBaseOnUSD
    {
        public Task<Dictionary<string, double>> ExchangeBaseOnUSD(string[] exchangeTo)
        {
            return Task.FromResult(new Dictionary<string, double>()
            {
                { "USD", 1 },
                { "AUD", 1 },
                { "EUR", 1 },
                { "GBP", 1 },
                { "BRL", 1 },
            });
        }
    }
}
