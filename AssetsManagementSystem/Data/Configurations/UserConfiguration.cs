using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetsManagementSystem.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var user = new User
            {
                Id = Guid.Parse("BDABCF06-A956-4EF7-8045-3214E68B9B4C"),
                FirstName = "Mosaad",
                LastName = "Ahmed",
                UserName = "Mosaad_Ahmed@Gmail.com",
                Email = "Mosaad_Ahmed@Gmail.com",
                PhoneNumber = "01551251116",
                AddedOnDate = DateTime.Now,
                UserStatus = UserStatus.Active.ToString(),
                NormalizedUserName = "MOSAAD_AHMED@GMAIL.COM",  
                NormalizedEmail = "MOSAAD_AHMED@GMAIL.COM" ,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, "123456789");

             
            builder.HasData(user);
           
        }
    }
}
