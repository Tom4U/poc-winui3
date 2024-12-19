using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Ohms.Kopf.Desktop.Core.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Ohms.Kopf.Desktop.Core.Controls
{
    public sealed partial class MainMenu : UserControl
    {
        public static readonly DependencyProperty ButtonStyleProperty = DependencyProperty.Register(nameof(ButtonStyle), typeof(Style), typeof(MainMenu), new PropertyMetadata(null));

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(MainMenu), new PropertyMetadata(null));

        public Style ButtonStyle
        {
            get => (Style)GetValue(ButtonStyleProperty);
            set { SetValue(ButtonStyleProperty, value); }
        }

        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set { SetValue(OrientationProperty, value); }
        }

        public MainMenu()
        {
            InitializeComponent();

            DataContext = this;

            Loaded += UserControlRoot_Loaded;
        }

        private void CreateNavigation()
        {
            var router = DI.Get<Router>();

            Menu.Children.Clear();

            var ordered = router.Routes.OrderBy(next => next.Order);

            ordered.ToList().ForEach(next =>
            {
                var button = new Button
                {
                    Style = ButtonStyle,
                    Content = next.Title,
                    Margin = new Thickness(4)
                };

                button.Click += (sender, e) => router.Navigate((next.Page));

                Menu.Children.Add(button);
            });
        }

        private void UserControlRoot_Loaded(object sender, RoutedEventArgs e)
        {
            CreateNavigation();
        }
    }
}