using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace Ohms.Kopf.Desktop.Core.Contracts
{
    internal interface IRoute
    {
        int Order { get; }
        Type Page { get; }
        string Title { get; set; }
    }
}