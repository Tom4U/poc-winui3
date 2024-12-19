using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Ohms.Kopf.Desktop.Core;
using Ohms.Kopf.Desktop.Core.Contracts;
using Ohms.Kopf.Desktop.Core.Services;

namespace Ohms.Kopf.Desktop.Features.Demo
{
    internal class DemoFeature : IFeature
    {
        public IRoute Route => new DemoRoute();

        public void RegisterServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<DemoViewModel>();
        }

        public void ServicesRegistered()
        {
            // Nothing to do
        }
    }
}