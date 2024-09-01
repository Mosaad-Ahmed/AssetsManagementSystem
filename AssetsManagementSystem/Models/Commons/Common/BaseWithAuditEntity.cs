namespace AssetsManagementSystem.Models.Commons.Common
{
    public abstract class BaseWithAuditEntity : AuditEntity, IBaseEntity
    {
        public int Id { get ; set; }
    }
}
