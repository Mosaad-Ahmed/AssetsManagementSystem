namespace AssetsManagementSystem.Models.DbSets
{
    public class User:IdentityUser<Guid>, IAuditEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
         public DateTime AddedOnDate { set; get; }
        public DateTime? UpdatedDate { set; get; }
        public DateTime? DeletedDate { set; get; }
        public bool? IsDeleted { set; get; }
        public virtual ICollection<Asset> AssignedAssets { get; set; }   

    }
}
