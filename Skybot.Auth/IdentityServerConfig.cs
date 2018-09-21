using System.Collections.Generic;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace Skybot.Auth
{
    public static class IdentityServerConfig
    {
        public static IList<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("Texto.Api", "Texto App")
            };
        }

        public static IList<Client> GetApiClients(IConfiguration configuration)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "Texto.Func",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret(configuration["Texto.Func:ClientSecret"].Sha256())
                    },
                    AllowedScopes = { "Texto.Api" }
                }
            };
        }
    }
}
