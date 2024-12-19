using Ohms.Kopf.Desktop.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ohms.Kopf.Desktop.Core.Services
{
    internal class Scopes : IScopes
    {
        public string[] RequiredScopes => ["User.Read"]; 

        public void AddScope(string scope)
        {
            RequiredScopes[RequiredScopes.Length + 1] = scope;
        }
    }
}
