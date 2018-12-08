using System.Collections.Generic;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Skybot.Auth.Models;
using ApiResource = IdentityServer4.Models.ApiResource;

namespace Skybot.Auth
{
    public static class IdentityServerConfig
    {
        public static IList<ApiResource> GetApiResources(IConfigurationSection configurationSection)
        {
            Models.ApiResource[] apiSettings = configurationSection.Get<Models.ApiResource[]>();
            List<ApiResource> apiResources = new List<ApiResource>();
            foreach(var api in apiSettings)
            {
                apiResources.Add(new ApiResource(api.Name, api.DisplayName));
            }

            return apiResources;
        }

        public static IList<Client> GetApiClients(IConfigurationSection configurationSection)
        {
            var clientsSettings = configurationSection.Get<ApiClient[]>();
            var clients = new List<Client>();

            foreach(var clientSetting in clientsSettings)
            {
                clients.Add(new Client
                {
                    ClientId = clientSetting.Id,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret(clientSetting.Secret.Sha256())
                    },
                    AllowedScopes = clientSetting.Scopes
                });
            }

            return clients;
        }
    }
}
