using AssetsManagementSystem.Models.DbSets;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AssetsManagementSystem.Data.Context
{
    public class ApplicationDbContext:IdentityDbContext<User,Role,Guid>
    {
        //private readonly AssetLifecycleInterceptor _assetLifecycleInterceptor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optionsBuilder)//, AssetLifecycleInterceptor assetLifecycleInterceptor)
            : base(optionsBuilder) 
        {
        //    _assetLifecycleInterceptor = assetLifecycleInterceptor;

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
           .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.AddInterceptors(_assetLifecycleInterceptor);
        //}
        public virtual DbSet<User> users { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<AssetDisposalRecord> AssetDisposalRecords { get; set; }
        public virtual DbSet<AssetMaintenanceRecords> AssetMaintenanceRecords { get; set; }
        public virtual DbSet<AssetTransferRecords>  AssetTransferRecords { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Category> Categories  { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<DataConsistencyCheck> DataConsistencyChecks { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<AssetLifecycle> AssetLifecycles { get; set; }

    }
}
