using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Ohms.Kopf.Desktop.Core.Contracts;
using Ohms.Kopf.Desktop.Core.Models;
using Ohms.Kopf.Desktop.Core.Routes;
using Ohms.Kopf.Desktop.Core.Services;

namespace Ohms.Kopf.Desktop.Core
{
    internal class Startup
    {
        private bool graphAdded = false;
        private bool settingsAdded = false;

        private static List<IFeature> RegisteredFeatures { get; } = [];

        private ServiceCollection ServiceCollection { get; set; }

        private Startup()
        { }

        public static Startup Configure()
        {
            var instance = new Startup
            {
                ServiceCollection = new ServiceCollection()
            };

            instance.RegisterRequiredServices();
            instance.RegisterCoreViewModels();

            return instance;
        }

        public Startup ActivateFeature<TImplemenation>() where TImplemenation : class, IFeature
        {
            var feature = Activator.CreateInstance<TImplemenation>();

            RegisterFeature(feature);

            return this;
        }

        public void Run()
        {
            DI.CreateServices(ServiceCollection);

            RegisterRoutes();

            RegisteredFeatures.ForEach(next => next.ServicesRegistered());
        }

        public Startup WithAuthentication<TImplementation>() where TImplementation : class, IAuthentication
        {
            ServiceCollection.AddSingleton<IAuthentication, TImplementation>();

            return this;
        }

        public Startup WithDefaults()
        {
            WithGraph<Graph>();
            WithSettings<Settings>();
            WithScopes<Scopes>();
            WithAuthentication<Authentication>();

            return this;
        }

        public Startup WithGraph<TImplementation>() where TImplementation : class, IGraph
        {
            ServiceCollection.AddSingleton<IGraph, TImplementation>();

            graphAdded = true;

            return this;
        }

        public Startup WithScopes<TImplementation>() where TImplementation : class, IScopes
        {
            ServiceCollection.AddSingleton<IScopes, TImplementation>();

            return this;
        }

        public Startup WithSettings<TImplementation>() where TImplementation : class, ISettings
        {
            ServiceCollection.AddSingleton<ISettings, TImplementation>();

            settingsAdded = true;

            return this;
        }

        private static void RegisterRoutes()
        {
            var router = DI.Get<Router>();
            var route = new SettingsRoute();

            router.Routes.Add(route);

            Debug.Assert(router.Routes.Count > 0);

            RegisteredFeatures.ForEach(next => router.Routes.Add(next.Route));
        }

        private void RegisterCoreViewModels()
        {
            var requiredNamespace = typeof(ViewModel).Namespace;
            var requiredSuffix = nameof(ViewModel);
            var assembly = Assembly.GetExecutingAssembly();

            var models = assembly.GetTypes()
                .Where(next => next.Namespace != null && next.Namespace == requiredNamespace)
                .Where(next => next.Name != requiredSuffix && next.Name.EndsWith(requiredSuffix));

            foreach (var model in models)
            {
                ServiceCollection.AddSingleton(model);
            }
        }

        private void RegisterFeature(IFeature feature)
        {
            if (ServiceCollection == null)
                throw new InvalidOperationException("Service Collection must be added before registering features.");

            if (!graphAdded)
                throw new InvalidOperationException("Feature registration requires registered graph");

            if (!settingsAdded)
                throw new InvalidOperationException("Feature registration requires registered settings");

            feature.RegisterServices(ServiceCollection);

            RegisteredFeatures.Add(feature);
        }

        private void RegisterRequiredServices()
        {
            ServiceCollection.AddSingleton<Router>();
        }
    }
}