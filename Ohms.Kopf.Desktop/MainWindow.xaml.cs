using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Ohms.Kopf.Desktop.Core;
using Ohms.Kopf.Desktop.Core.Contracts;
using Ohms.Kopf.Desktop.Core.Models;
using Ohms.Kopf.Desktop.Core.Pages;
using Ohms.Kopf.Desktop.Core.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Ohms.Kopf.Desktop
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private readonly Router router;
        private readonly MainViewModel viewModel;

        private bool configured = false;

        public MainWindow()
        {
            InitializeComponent();

            Activated += MainWindow_Activated;

            router = DI.Get<Router>();
            router.RegisterFrame(ActivePage);

            viewModel = DI.Get<MainViewModel>();
            MainGrid.DataContext = viewModel;
        }

        private void HideHeader() => Header.Visibility = Visibility.Collapsed;

        private void HideMenu() => Menu.Visibility = Visibility.Collapsed;

        private async void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
        {
            if (configured) return;

            var settings = DI.Get<ISettings>();

            await settings.LoadSettingsAsync();

            if (settings.NeedsSetup())
            {
                HideHeader();
                router.Navigate<SetupPage>();
            }
            else
            {
                await viewModel.LoadUserDetailsCommand.ExecuteAsync(null);

                ShowHeader();
                router.Navigate<StartPage>();

                if (UserNameBlock.Visibility == Visibility.Collapsed)
                    HideMenu();
                else
                    ShowMenu();
            }

            configured = true;
        }

        private void ShowHeader() => Header.Visibility = Visibility.Visible;

        private void ShowMenu() => Menu.Visibility = Visibility.Visible;
    }
}