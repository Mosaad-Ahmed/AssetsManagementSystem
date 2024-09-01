namespace AssetsManagementSystem.Models.Commons.Common

{
    public abstract class AuditEntity : IAuditEntity
    {
        public DateTime AddedOnDate { set; get; }
        public DateTime? UpdatedDate { set; get; }
        public DateTime? DeletedDate { set; get; }
        public bool? IsDeleted { set; get; }
    }
}
