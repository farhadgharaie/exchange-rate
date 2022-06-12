namespace Exchange.Common.Authentication.interfaces
{
    public interface IThirdPartyConfiguration : IAuthentication
    {
        int GetMaximumRetry();
    }
}
