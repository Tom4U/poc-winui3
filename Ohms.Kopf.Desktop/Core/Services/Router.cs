using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Ohms.Kopf.Desktop.Core.Contracts;

namespace Ohms.Kopf.Desktop.Core.Services
{
    internal class Router
    {
        private Frame frame;

        public List<IRoute> Routes { get; } = [];

        public void Back()
        {
            if (frame.CanGoBack) frame.GoBack();
        }

        public void Forward()
        {
            if (frame.CanGoForward) frame.GoForward();
        }

        public void Navigate<TPage>() where TPage : Page => frame.Navigate(typeof(TPage));

        public void Navigate(Type pageType) => frame.Navigate(pageType);

        public void Navigate<TPage>(TPage page) where TPage : Page => frame.Content = page;

        public void RegisterFrame(Frame frame)
        {
            this.frame = frame;

            this.frame.NavigationFailed += Frame_NavigationFailed;
        }

        private void Frame_NavigationFailed(object sender, Microsoft.UI.Xaml.Navigation.NavigationFailedEventArgs e)
        {
            Console.WriteLine("Navigation failed");
        }
    }
}