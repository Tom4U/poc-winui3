using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Ohms.Kopf.Desktop.Core.Contracts;

namespace Ohms.Kopf.Desktop.Core.Services
{
    internal class Authentication : IAuthentication
    {
        public async Task<AuthenticationResult> AuthenticateAsync() {
            var result = await AcquireTokenAsync();

            return result;
        }

        private static async Task<AuthenticationResult> AcquireTokenAsync()
        {
            var app = BuildClient();
            var scopes = DI.Get<IScopes>().RequiredScopes;
            var accounts = await app.GetAccountsAsync();
            AuthenticationResult result;

            if (accounts.Any())
                result = await app.AcquireTokenSilent(scopes, accounts.FirstOrDefault()).ExecuteAsync();
            else
                result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();

            return result;
        }

        private static IPublicClientApplication BuildClient()
        {
            var settings = DI.Get<ISettings>();

            var app = PublicClientApplicationBuilder.Create(settings.ClientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, settings.TenantId)
                .WithDefaultRedirectUri()
                .Build();

            app.UserTokenCache.SetBeforeAccess(next =>
            {
                string token = LoadToken();
                
                if (token != null)
                    next.TokenCache.DeserializeMsalV3(Convert.FromBase64String(token));
            });

            app.UserTokenCache.SetAfterAccess(next =>
            {
                if (next.HasStateChanged)
                {
                    string token = Convert.ToBase64String(next.TokenCache.SerializeMsalV3());
                    SaveToken(token);
                }
            });

            return app;
        }

        private static async void SaveToken(string token)
        {
            var settings = DI.Get<ISettings>();

            settings.AuthToken = token;

            await settings.SaveSettingsAsync();
        }

        private static string LoadToken()
        {
            var settings = DI.Get<ISettings>();

            return settings.AuthToken;
        }
    }
}
