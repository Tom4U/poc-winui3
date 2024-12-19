using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ohms.Kopf.Desktop.Core.Contracts;

namespace Ohms.Kopf.Desktop.Features.Demo
{
    internal class DemoRoute : IRoute
    {
        public int Order => 1;

        public Type Page => typeof(DemoPage);

        public string Title { get; set; } = "Demo Feature";
    }
}