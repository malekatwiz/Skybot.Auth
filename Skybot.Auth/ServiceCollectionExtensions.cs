using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.File;
using Skybot.Auth.Models;
using Skybot.Auth.Stores;

namespace Skybot.Auth
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureIdentityServer(this IServiceCollection serviceCollection, IConfiguration configuration, ILogger logger)
        {
            var cert = LoadCert(configuration, logger).GetAwaiter().GetResult();

            logger.LogInformation($"Cert Name: {cert.FriendlyName} / {cert.Thumbprint}");

            serviceCollection.AddIdentityServer(x => x.IssuerUri = configuration["IssuerUri"])
                .AddSigningCredential(cert)
                .AddClientStore<ClientStore>()
                .AddResourceStore<ResourceStore>();
                //.AddInMemoryApiResources(IdentityServerConfig.GetApiResources(configuration.GetSection("ApiResources")))
                //.AddInMemoryClients(IdentityServerConfig.GetApiClients(configuration.GetSection("ApiClients")));
        }

        public static void ConfigureAspIdentity(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddIdentity<ApplicationUser, IdentityRole>()
                .AddDefaultTokenProviders();
        }

        private static async Task<X509Certificate2> LoadCert(IConfiguration configuration, ILogger logger)
        {
            logger.LogInformation("Looking for Cert file");
            if (!File.Exists(configuration["Certificate:Filename"]))
            {
                logger.LogInformation("Cert file was not found, copying...");

                var file = new CloudFile(new Uri(configuration["StorageAccount:FileUri"]),
                    new StorageCredentials(configuration["StorageAccount:Name"], configuration["StorageAccount:Key"]));

                await file.DownloadToFileAsync(configuration["Certificate:Filename"], FileMode.Create);

                logger.LogInformation("Cert file was copied successfully");
            }
            return new X509Certificate2(configuration["Certificate:Filename"], configuration["Certificate:Password"]);
        }
    }
}
