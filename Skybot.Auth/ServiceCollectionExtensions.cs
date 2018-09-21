using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skybot.Auth.Models;

namespace Skybot.Auth
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureIdentityServer(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            
            serviceCollection.AddIdentityServer(x => x.IssuerUri = "null")
                .AddSigningCredential(LoadCertificate(configuration["CertTumbprint"]))
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddInMemoryClients(IdentityServerConfig.GetApiClients(configuration));
        }

        public static void ConfigureAspIdentity(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddIdentity<ApplicationUser, IdentityRole>()
                .AddDefaultTokenProviders();
        }

        private static X509Certificate2 LoadCertificate(string certThumbprint)
        {
            using (var certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                certStore.Open(OpenFlags.ReadOnly);

                var certCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, certThumbprint, true);

                return certCollection.Count > 0 ? certCollection[0] : null;
            }
        }
    }
}
