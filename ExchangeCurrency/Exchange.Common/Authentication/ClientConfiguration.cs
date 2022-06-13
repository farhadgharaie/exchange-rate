using Exchange.Common.Authentication.interfaces;

namespace Exchange.Common.Authentication
{
    public class ClientConfiguration : Authentication
    {
        public ClientConfiguration(string uRL, string aPIKey) : base(uRL, aPIKey)
        {
        }
    }
}
