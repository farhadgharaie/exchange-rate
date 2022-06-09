using System;

namespace Exchange.Common.CustomException
{
    public class NoUSDBaseQuoteProvidedException : ServiceUnavailableException
    {
        public NoUSDBaseQuoteProvidedException() : base("No USD base quote provided")
        {

        }
    }
}
