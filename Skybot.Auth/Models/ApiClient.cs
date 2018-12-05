using System;
using System.Collections.Generic;

namespace Skybot.Auth.Models
{
    public class ApiClient
    {
        public Guid Id { get; private set; }
        public string Secret { get; private set; }
        public virtual IList<ApiResource> Resources { get; private set; }

        public ApiClient(string secret)
        {
            Secret = secret;
            Resources = new List<ApiResource>();
        }
    }

    public class ApiResource
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public string Uri { get; private set; }

        public ApiResource(string name, string displayName, string uri)
        {
            Name = name;
            DisplayName = displayName;
            Uri = uri;
        }
    }
}
