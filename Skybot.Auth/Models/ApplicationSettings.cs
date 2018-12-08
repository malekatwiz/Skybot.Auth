namespace Skybot.Auth.Models
{
    public class ApiResource
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public class ApiClient
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public string[] Scopes { get; set; }
    }
}
