using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skybot.Auth.Models;

namespace Skybot.Auth.Data
{
    public class ApiClientTypeConfiguration : IEntityTypeConfiguration<ApiClient>
    {
        public void Configure(EntityTypeBuilder<ApiClient> builder)
        {
            builder.ToTable("ApiClients");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
        }
    }

    public class ApiResourceTypeConfiguration : IEntityTypeConfiguration<ApiResource>
    {
        public void Configure(EntityTypeBuilder<ApiResource> builder)
        {
            builder.ToTable("ApiResources");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
        }
    }
}
