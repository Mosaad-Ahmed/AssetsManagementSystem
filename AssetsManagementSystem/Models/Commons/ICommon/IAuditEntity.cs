namespace AssetsManagementSystem.Models.Commons.ICommon
{
    public interface IAuditEntity : IBaseEntityForGeneric
    {
        public DateTime AddedOnDate { set; get; }
        public DateTime? UpdatedDate { set; get; }
        public DateTime? DeletedDate { set; get; }
        public bool? IsDeleted { set; get; }
    }
}
