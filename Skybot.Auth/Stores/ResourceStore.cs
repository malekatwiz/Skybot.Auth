using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Configuration;

namespace Skybot.Auth.Stores
{
    public class ResourceStore : IResourceStore
    {
        private readonly IConfiguration _configuration;

        public ResourceStore(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            return Task.Run(() =>
            {
                ApiResource resource = null;
                GetApiResources().TryGetValue(name, out resource);
                return resource;
            });
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Task.Run(() =>
            {
                var resources = GetApiResources().Where(x => scopeNames.Contains(x.Key));
                return resources.Select(x => x.Value);
            });
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Task.Run(() =>
            {
                return new List<IdentityResource>().AsEnumerable();
            });
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            return Task.Run(() =>
            {
                return new Resources
                {
                    ApiResources = GetApiResources().Values
                };
            });
        }

        private IDictionary<string, ApiResource> GetApiResources()
        {
            var apiSettings = _configuration.GetSection("ApiResources").Get<Models.ApiResource[]>();

            var apiResources = new Dictionary<string, ApiResource>();
            foreach (var api in apiSettings)
            {
                apiResources.Add(api.Name, new ApiResource(api.Name, api.DisplayName));
            }

            return apiResources;
        }
    }
}
