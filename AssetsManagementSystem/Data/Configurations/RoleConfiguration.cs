using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetsManagementSystem.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            Role admin = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            Role user = new Role
            {
                Id = Guid.NewGuid(),
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            Role manager = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Manager",
                NormalizedName = "MANAGER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            Role auditor = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Auditor",
                NormalizedName = "AUDITOR",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            builder.HasData(admin, user, manager, auditor);
        }
    }
}
