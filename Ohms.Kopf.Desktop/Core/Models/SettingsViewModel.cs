using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ohms.Kopf.Desktop.Core.Contracts;
using Ohms.Kopf.Desktop.Core.Pages;
using Ohms.Kopf.Desktop.Core.Services;

namespace Ohms.Kopf.Desktop.Core.Models
{
    internal partial class SettingsViewModel : ViewModel
    {
        private readonly Router navigator;
        private readonly ISettings settings;
        private string clientId;
        private string tenantId;

        public RelayCommand CancelCommand { get; private set; }

        public string ClientId
        { get { return clientId; } set { SetProperty(ref clientId, value, nameof(ClientId)); } }

        public AsyncRelayCommand StoreSettingsCommand { get; private set; }

        public string TenantId
        { get { return tenantId; } set { SetProperty(ref tenantId, value, nameof(TenantId)); } }

        public SettingsViewModel()
        {
            settings = DI.Get<ISettings>();
            navigator = DI.Get<Router>();

            StoreSettingsCommand = new AsyncRelayCommand(StoreSettings);
            CancelCommand = new RelayCommand(Cancel);

            ClientId = settings.ClientId;
            TenantId = settings.TenantId;
        }

        private void Cancel()
        {
            navigator.Navigate<StartPage>();
        }

        private async Task StoreSettings()
        {
            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(tenantId))
                return;

            settings.ClientId = clientId;
            settings.TenantId = tenantId;

            await settings.SaveSettingsAsync();

            navigator.Navigate<StartPage>();
        }
    }
}