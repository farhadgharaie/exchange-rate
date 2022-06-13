namespace Exchange.Common.CustomException
{
    public class NoResponseThirdPartyAPIServiceException : ServiceUnavailableException
    {
        public NoResponseThirdPartyAPIServiceException() : base("No response from thirdpart API")
        {

        }
    }
}
