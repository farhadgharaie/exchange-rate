namespace Exchange.Common.CustomException
{
    public class ThirdPartyAPIServiceUnavailableException : ServiceUnavailableException
    {
        public ThirdPartyAPIServiceUnavailableException() : base("3rd party service is unavailable")
        {

        }
    }
}
