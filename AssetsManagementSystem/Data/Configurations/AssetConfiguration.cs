using AssetsManagementSystem.Models.DbSets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetsManagementSystem.Data.Configurations
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            
            builder.HasOne(m => m.Manufacturer)
               .WithMany(a => a.Assets)
               .HasForeignKey(fk => fk.ManufacturerId)
               .IsRequired(true);

          //  builder.HasKey(a => a.SerialNumber);
           // builder.ToTable(nameof(Asset),a=>a.IsTemporal());
        }
    }
}
