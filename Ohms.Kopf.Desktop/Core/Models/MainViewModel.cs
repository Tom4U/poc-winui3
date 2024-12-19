using System;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ohms.Kopf.Desktop.Core.Contracts;
using Ohms.Kopf.Desktop.Core.Services;

namespace Ohms.Kopf.Desktop.Core.Models
{
    internal partial class MainViewModel : ViewModel
    {
        private string userName;

        public AsyncRelayCommand LoadUserDetailsCommand { get; private set; }

        public string UserName
        { get { return userName; } set { SetProperty(ref userName, value, nameof(UserName)); } }

        public MainViewModel()
        {
            LoadUserDetailsCommand = new AsyncRelayCommand(LoadUserDetails);
        }

        private async Task LoadUserDetails()
        {
            var graph = DI.Get<IGraph>();
            var user = await graph.ActiveUser;

            UserName = user.DisplayName;
        }
    }
}