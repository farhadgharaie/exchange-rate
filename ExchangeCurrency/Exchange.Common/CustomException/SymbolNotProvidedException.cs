using System;

namespace Exchange.Common.CustomException
{
    public class SymbolNotProvidedException : Exception
    {
        public SymbolNotProvidedException() : base("Symbol not provided")
        {

        }
    }
}
