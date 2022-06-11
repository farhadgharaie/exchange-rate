using System;
using System.Collections.Generic;
using System.Text;

namespace Exchange.Common.Config
{
    public class APIConfig
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public int MaximumRetry { get; set; }
    }
}
