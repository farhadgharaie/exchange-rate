using System;
using System.Collections.Generic;
using System.Text;

namespace Exchange.Common.Config
{
    public class ThirdPartAPIConfig
    {
        public string URL { get;  }
        public string ApiKey { get; }
        public int MaximumRetries { get; }
        public ThirdPartAPIConfig(string apiKey, string url,int maximumRetries=3)
        {
            ApiKey = apiKey;
            URL = url;
            MaximumRetries = maximumRetries;
        }
    }
}
