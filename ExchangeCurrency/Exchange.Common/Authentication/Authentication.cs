using Exchange.Common.Authentication.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exchange.Common.Authentication
{
    
    public  class Authentication : IAuthentication
    {
        public string URL { get; }
        public string APIKey { get; }
        public Authentication(string uRL, string aPIKey)
        {
            URL = uRL;
            APIKey = aPIKey;
        }
        public string GetAPIKey()
        {
            return APIKey;
        }

        public string GetURL()
        {
            return URL;
        }
    }
}
