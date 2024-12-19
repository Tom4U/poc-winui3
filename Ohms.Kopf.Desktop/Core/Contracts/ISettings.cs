using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Ohms.Kopf.Desktop.Core.Contracts
{
    internal interface ISettings
    {
        string ClientId { get; set; }

        string TenantId { get; set; }

        string AuthToken { get; set; }

        bool NeedsSetup();

        Task SaveSettingsAsync();

        Task LoadSettingsAsync();
    }
}
