using Ohms.Kopf.Desktop.Core.Contracts;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Storage;
using System;

namespace Ohms.Kopf.Desktop.Core.Services
{
    [DataContract(Namespace = "http://Ohms.Kopf.Desktop.Core.Contracts")]
    internal class Settings : ISettings
    {
        private const string STORAGE_KEY = "app.settings";

        [DataMember]
        public string ClientId { get; set; } = string.Empty;

        [DataMember]
        public string TenantId { get; set; } = string.Empty;

        [DataMember]
        public string AuthToken { get; set; } = string.Empty;

        public bool NeedsSetup()
        {
            return string.IsNullOrWhiteSpace(ClientId) || string.IsNullOrWhiteSpace(TenantId);
        }

        public async Task SaveSettingsAsync()
        {
            var memoryStream = new MemoryStream();

            DataContractSerializer serializer = new(GetType());
            serializer.WriteObject(memoryStream, this);

            await memoryStream.FlushAsync();

            memoryStream.Seek(0, SeekOrigin.Begin);

            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(STORAGE_KEY, CreationCollisionOption.ReplaceExisting);
            
            using Stream fileStream = await file.OpenStreamForWriteAsync();

            await memoryStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
        }

        public async Task LoadSettingsAsync()
        {
            try
            {
                var ms = new MemoryStream();
                DataContractSerializer serializer = new(GetType());

                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(STORAGE_KEY);

                using IInputStream inStream = await file.OpenSequentialReadAsync();
                var result = (Settings)serializer.ReadObject(inStream.AsStreamForRead());

                AuthToken = result.AuthToken;
                ClientId = result.ClientId;
                TenantId = result.TenantId;
            }
            catch (FileNotFoundException)
            {
                
            }
        }
    }
}
