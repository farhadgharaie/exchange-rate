using System;
using System.Collections.Generic;
using System.Text;

namespace Exchange.Common.Authentication.interfaces
{
    public interface IAuthentication
    {
        string GetURL();
        string GetAPIKey();
    }
}
