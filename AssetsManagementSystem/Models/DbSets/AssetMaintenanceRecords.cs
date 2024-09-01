namespace AssetsManagementSystem.Models.DbSets
{
    public class AssetMaintenanceRecords:BaseWithAuditEntity
    {
        [Required(ErrorMessage = "Asset ID is required.")]
        [ForeignKey("Asset")]
        public int AssetId { get; set; }   
        public virtual Asset Asset { get; set; }

        [Required(ErrorMessage = "Maintenance description is required.")]
        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }


        [ForeignKey("PerformedBy")]
        public Guid PerformedById { get; set; }

        public virtual User PerformedBy { get; set; }


    }
}
