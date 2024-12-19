using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ohms.Kopf.Desktop.Core.Contracts
{
    internal interface IAuthentication
    {
        Task<AuthenticationResult> AuthenticateAsync();
    }
}
