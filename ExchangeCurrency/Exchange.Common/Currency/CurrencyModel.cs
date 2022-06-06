using System.Text;

namespace Exchange.Common.Currency
{
    public class CurrencyModel
    {
        protected string _name { get; private set; }
        public string symbol { get; private set; }
        public CurrencyModel(string name, string symbol)
        {
            _name = name;
            this.symbol = symbol;
        }
    }
}
