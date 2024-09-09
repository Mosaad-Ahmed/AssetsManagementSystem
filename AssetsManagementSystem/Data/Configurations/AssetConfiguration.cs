using AssetsManagementSystem.Models.DbSets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetsManagementSystem.Data.Configurations
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(pk => new { pk.SerialNumber,pk.ManufacturerId });

            builder.HasOne(m => m.Manufacturer)
               .WithOne(a => a.Asset).HasForeignKey<Asset>(fk => fk.ManufacturerId);

        }
    }
}
