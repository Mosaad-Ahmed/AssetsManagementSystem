using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetsManagementSystem.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {


            builder.HasMany(c => c.SubCategories).WithOne(sc => sc.ParentCategory).HasForeignKey(fk => fk.ParentCategoryId);



        }
    }
}
