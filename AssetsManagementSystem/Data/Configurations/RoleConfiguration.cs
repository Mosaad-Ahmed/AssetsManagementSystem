using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetsManagementSystem.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            Role admin = new Role
            {
                Id =Guid.Parse("fc05f613-0e97-444e-b19b-018a223a7484"),
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            Role user = new Role
            {
                Id = Guid.Parse("846e3679-1537-487d-969c-3a6116fc3b2d"),
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            Role manager = new Role
            {
                Id = Guid.Parse("d9c0c478-adf7-40db-ade3-2b7810d9659f"),
                Name = "Manager",
                NormalizedName = "MANAGER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            Role auditor = new Role
            {
                Id = Guid.Parse("62474474-f91b-483d-b0d8-2742c01146f0"),
                Name = "Auditor",
                NormalizedName = "AUDITOR",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            builder.HasData(admin, user, manager, auditor);
        }
    }
}
