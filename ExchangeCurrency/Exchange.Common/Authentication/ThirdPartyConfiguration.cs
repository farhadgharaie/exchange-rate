using Exchange.Common.Authentication.interfaces;

namespace Exchange.Common.Authentication
{
    public class ThirdPartyConfiguration : Authentication, IThirdPartyConfiguration
    {
        public int MaximumRetry { get; }
        public ThirdPartyConfiguration(string uRL, string aPIKey, int maximumRetry=3):base(uRL, aPIKey)
        {
            MaximumRetry = maximumRetry;
        }

        public int GetMaximumRetry()
        {
            return MaximumRetry;
        }
    }
}
