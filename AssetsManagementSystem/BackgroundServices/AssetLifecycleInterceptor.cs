using AssetsManagementSystem.Models.DbSets;

namespace AssetsManagementSystem.BackgroundServices
{
    public class AssetLifecycleInterceptor : SaveChangesInterceptor
    {
        public IHttpContextAccessor HttpContextAccessor { get; }

        private readonly ILogger<AssetLifecycleInterceptor> _logger;
        public string UserId { get; set; }
        public AssetLifecycleInterceptor(IHttpContextAccessor httpContextAccessor, ILogger<AssetLifecycleInterceptor> logger)
        {
            HttpContextAccessor = httpContextAccessor;
            UserId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger = logger;
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            var context = eventData.Context;

            if (context == null) return result;

            var entries = context.ChangeTracker.Entries()
                .Where(e => e.Entity is Asset && (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted));

            foreach (var entry in entries)
            {
                var assetLifecycle = CreateAssetLifecycle(entry);
                context.Set<AssetLifecycle>().Add(assetLifecycle);

                _logger.LogInformation("Asset lifecycle event created: {EventType} for Asset ID: {AssetId}", assetLifecycle.EventType, assetLifecycle.AssetId);
            }

            return base.SavedChanges(eventData, result);
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;

            if (context == null) return new ValueTask<int>(0);

            var entries = context.ChangeTracker
                .Entries()
                .Where(e => e.Entity is Asset);

            foreach (var entry in entries)
            {
                var assetLifecycle = CreateAssetLifecycle(entry);
                context.Set<AssetLifecycle>().Add(assetLifecycle);

                _logger.LogInformation("Asset lifecycle event created: {EventType} for Asset ID: {AssetId}", assetLifecycle.EventType, assetLifecycle.AssetId);
            }

            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

         
        private AssetLifecycle CreateAssetLifecycle(EntityEntry entry)
        {
            var assetLifecycle = new AssetLifecycle
            {
                AssetId = ((Asset)entry.Entity).Id,
                EventDate = DateTime.UtcNow,
                EventType = entry.State.ToString(), // Added, Modified, Deleted
                Notes = GenerateNotes(entry),
                PerformedById = GetCurrentUserId()
            };

            return assetLifecycle;
        }

        private string GenerateNotes(EntityEntry entry)
        {
            var changes = entry.Properties.Where(p => p.IsModified).Select(p => $"{p.Metadata.Name}: {p.OriginalValue} -> {p.CurrentValue}");
            return $"Changes: {string.Join(", ", changes)}";
        }

        private Guid GetCurrentUserId()
        {
            var myid = "44952cd9-2300-4593-5252-08dcc6718761";
            return Guid.Parse(myid);
           // return Guid.Parse(UserId);
        }

        #region

        //public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        //{
        //    var context = eventData.Context;
        //    if (context == null) return result;

        //    ProcessEntries(context);

        //    return base.SavedChanges(eventData, result);
        //}

        //public override async ValueTask<int> SavedChangesAsync(
        //    SaveChangesCompletedEventData eventData, 
        //    int result, CancellationToken cancellationToken = default)
        //{
        //    var context = eventData.Context;
        //    if (context == null) return result;

        //    ProcessEntries(context);

        //    return await base.SavedChangesAsync(eventData, result, cancellationToken);
        //}

        //#region Private Callee Method
        //private void ProcessEntries(DbContext context)
        //{
        //    var entries = context.ChangeTracker.Entries()
        //        .Where(IsTrackedEntity);

        //    foreach (var entry in entries)
        //    {
        //        var assetLifecycle = new AssetLifecycle
        //        {
        //            EventDate = DateTime.UtcNow,
        //            EventType = entry.State.ToString(),
        //            Notes = GenerateNotes(entry),
        //            AssetId = GetAssetId(entry),
        //            PerformedById = Guid.Parse(GetCurrentUserId())
        //        };

        //        context.Set<AssetLifecycle>().Add(assetLifecycle);
        //    }
        //}

        //private bool IsTrackedEntity(EntityEntry entry)
        //{
        //    return entry.Entity is Asset || entry.Entity is AssetMaintenanceRecords ||
        //           entry.Entity is AssetTransferRecords || entry.Entity is AssetDisposalRecord ||
        //           entry.Entity is Document;
        //}

        //private string GenerateNotes(EntityEntry entry)
        //{
        //    return $"{entry.Entity.GetType().Name} was {entry.State.ToString().ToLower()}";
        //}

        //private int GetAssetId(EntityEntry entry)
        //{
        //    return entry.Entity switch
        //    {
        //        Asset asset => asset.Id,
        //        AssetMaintenanceRecords maintenance => maintenance.AssetId,
        //        AssetTransferRecords transfer => transfer.AssetId,
        //        AssetDisposalRecord disposal => disposal.AssetId,
        //        Document document => document.AssetId,
        //         _ => 0
        //    };
        //}

        //private string GetCurrentUserId()
        //{
        //    return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        //}
        //#endregion


        #endregion
    }
}