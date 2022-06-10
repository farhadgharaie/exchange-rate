using System;
using System.Collections.Generic;
using System.Text;

namespace Exchange.Common.Config
{
    public class APIConfiguration
    {
        public string URL { get;  }
        public string ApiKey { get; }
        public int MaximumRetries { get; }
        public APIConfiguration(string apiKey, string url,int maximumRetries=3)
        {
            ApiKey = apiKey;
            URL = url;
            MaximumRetries = maximumRetries;
        }
    }
}
