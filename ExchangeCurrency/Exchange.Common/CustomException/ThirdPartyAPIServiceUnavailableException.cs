namespace Exchange.Common.CustomException
{
    public class ThirdPartyAPIServiceUnavailableException : ServiceUnavailableException
    {
        public ThirdPartyAPIServiceUnavailableException() : base("3rd part service is unavailable")
        {

        }
    }
}
