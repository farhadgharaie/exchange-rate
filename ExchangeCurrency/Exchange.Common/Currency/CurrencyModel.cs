using System.Text;

namespace Exchange.Common.Currency
{
    public class CurrencyModel
    {
        public string Name { get; private set; }
        public string Symbol { get; private set; }
        public CurrencyModel(string name, string symbol)
        {
            Name = name;
            Symbol = symbol;
        }
    }
}
