using System;

namespace Exchange.Common.CustomException
{
    public  class SymbolNotFoundException : Exception
    {
        public SymbolNotFoundException() : base("Symbol not found")
        {

        }
    }
}
