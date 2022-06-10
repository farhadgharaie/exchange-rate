using System;
using System.Collections.Generic;
using System.Text;

namespace Exchange.Common.CustomException
{
    public  class SymbolNotFoundException : Exception
    {
        public SymbolNotFoundException() : base("Symbol not found")
        {

        }
    }
}
