using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Ohms.Kopf.Desktop.Core
{
    /// <summary>
    /// Facade for <see cref="Microsoft.Extensions.DependencyInjection.Ioc.Default"/>
    /// </summary>
    /// <typeparam name="T">Type of the service(s) to use.</typeparam>
    internal static class DI
    {
        public static T Get<T>()
        {
            return Ioc.Default.GetService<T>();
        }

        public static IEnumerable<T> GetAll<T>()
        {
            return Ioc.Default.GetServices<T>();
        }

        public static void CreateServices(ServiceCollection services) {
            Ioc.Default.ConfigureServices(services.BuildServiceProvider());
        }
    }
}
