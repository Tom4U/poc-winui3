using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Ohms.Kopf.Desktop.Core.Contracts;
using Ohms.Kopf.Desktop.Core.Pages;

namespace Ohms.Kopf.Desktop.Core.Routes
{
    internal sealed class SettingsRoute : IRoute
    {
        public int Order => 999;

        public Type Page { get; } = typeof(SettingsPage);

        public string Title { get; set; } = "Einstellungen";
    }
}