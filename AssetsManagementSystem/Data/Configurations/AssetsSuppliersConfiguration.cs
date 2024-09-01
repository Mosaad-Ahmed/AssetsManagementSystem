using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetsManagementSystem.Data.Configurations
{
    public class AssetsSuppliersConfiguration : IEntityTypeConfiguration<AssetsSuppliers>
    {
        public void Configure(EntityTypeBuilder<AssetsSuppliers> builder)
        {
            builder.HasOne(s => s.Supplier)
                 .WithMany(As => As.AssetsSuppliers)
                 .HasForeignKey(fk=>fk.SupplierId);

            builder.HasOne(s => s.Asset)
                    .WithMany(As => As.AssetsSuppliers)
                    .HasForeignKey(fk => fk.AssetId);

        }
    }
}
