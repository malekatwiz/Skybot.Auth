using Microsoft.EntityFrameworkCore;
using Skybot.Auth.Models;

namespace Skybot.Auth.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApiClient> ApiClients { get; set; }
        public DbSet<ApiResource> ApiResources { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApiClientTypeConfiguration());
            builder.ApplyConfiguration(new ApiResourceTypeConfiguration());
        }
    }
}
