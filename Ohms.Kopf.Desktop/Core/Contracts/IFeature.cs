using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Ohms.Kopf.Desktop.Core.Contracts
{
    internal interface IFeature
    {
        IRoute Route { get; }

        void RegisterServices(ServiceCollection serviceCollection);

        void ServicesRegistered();
    }
}