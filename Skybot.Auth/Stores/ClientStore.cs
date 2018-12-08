using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Configuration;
using Skybot.Auth.Models;

namespace Skybot.Auth.Stores
{
    public class ClientStore : IClientStore
    {
        private readonly IConfiguration _configuration;
        public ClientStore(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return Task.Run(() => GetApiClients()[clientId]);
        }

        private IDictionary<string, Client> GetApiClients()
        {
            var clientsConfig = _configuration.GetSection("ApiClients").Get<ApiClient[]>();
            var clients = new Dictionary<string, Client>();

            foreach (var client in clientsConfig)
            {
                clients.Add(client.Id, new Client
                {
                    ClientId = client.Id,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret(client.Secret.Sha256())
                    },
                    AllowedScopes = client.Scopes
                });
            }

            return clients;
        }
    }
}
