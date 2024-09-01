namespace AssetsManagementSystem.Models.DbSets
{
    public class SubCategory:BaseWithAuditEntity
    {
        [Required(ErrorMessage = "Asset subtype name is required.")]
        [MaxLength(50, ErrorMessage = "Asset subtype name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Asset type ID is required.")]
        [ForeignKey("MainAssetCategory")]
        public int MainCategoryId { get; set; }
        public virtual Category MainAssetCategory { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
    }
}
