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
                new ApiResource("Texto.Api", "Texto App"),
                new ApiResource("Skybot.Api", "Skybot App"),
                new ApiResource("Skybot.HomeAutomation", "Skybot Home Automation")
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
                        new Secret(configuration["ApiClients:Texto.Func:ClientSecret"].Sha256())
                    },
                    AllowedScopes = { "Texto.Api" }
                },
                new Client
                {
                    ClientId = "Skybot.Func",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret(configuration["ApiClients:Skybot.Func:ClientSecret"].Sha256())
                    },
                    AllowedScopes = { "Skybot.Api" }
                },
                new Client
                {
                    ClientId = "Skybot.HomeAutomation",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("SomeSecret".Sha256())
                    },
                    AllowedScopes = { "Skybot.HomeAutomation" }
                }
            };
        }
    }
}
