using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ohms.Kopf.Desktop.Core.Contracts
{
    internal interface IScopes
    {
        string[] RequiredScopes { get; }

        void AddScope(string scope);
    }
}
