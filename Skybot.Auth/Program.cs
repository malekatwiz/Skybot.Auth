using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Skybot.Auth.Data;
using Skybot.Auth.Extensions;

namespace Skybot.Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build()
                .MigrateDbContext<ApplicationDbContext>((_, __) => { }).Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
