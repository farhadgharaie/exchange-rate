using System;

namespace Exchange.Common.CustomException
{
    public class ServiceUnavailableException : Exception
    {
        public ServiceUnavailableException() : base("3rd part service is unavailable")
        {

        }
    }
}
