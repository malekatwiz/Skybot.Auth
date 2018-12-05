using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Skybot.Auth.Extensions
{
    public static class IWebHostExtensions
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost,
            Action<TContext, IServiceProvider> seeder)
            where TContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<TContext>();

                try
                {
                    var retry = Policy.Handle<SqlException>().WaitAndRetry(new[]
                    {
                        TimeSpan.FromSeconds(3),
                        TimeSpan.FromSeconds(7),
                        TimeSpan.FromSeconds(15)
                    });

                    retry.Execute(() => { context.Database.Migrate(); });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }

            return webHost;
        }
    }
}
