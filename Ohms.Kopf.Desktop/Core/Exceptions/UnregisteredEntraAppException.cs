using System;

namespace Ohms.Kopf.Desktop.Core.Exceptions
{
    internal class UnregisteredEntraAppException : Exception
    {
        public UnregisteredEntraAppException() : base("Application (client) and/or Directory (tenant) ID not set.") { }
    }
}
