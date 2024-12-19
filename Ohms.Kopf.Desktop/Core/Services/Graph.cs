using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Ohms.Kopf.Desktop.Core.Contracts;
using Ohms.Kopf.Desktop.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ohms.Kopf.Desktop.Core.Services
{
    internal class Graph : IGraph
    {
        private GraphServiceClient client;

        public Task<User> ActiveUser
        {
            get
            {
                try
                {
                    var client = GetClient();
                    var user = client.Me.GetAsync();

                    return user;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading active user details: {ex}");
                    return null;
                }
            }
        }

        public GraphServiceClient GetClient()
        {
            client ??= new GraphServiceClient(new GraphAuthProvider());

            return client;
        }
    }

    internal class GraphAuthProvider : IAuthenticationProvider
    {
        public async Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object> additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
        {
            var auth = DI.Get<IAuthentication>();
            var authResult = await auth.AuthenticateAsync();
            var token = authResult.AccessToken;

            request.Headers.Add("Authorization", $"Bearer {token}");
        }
    }
}
