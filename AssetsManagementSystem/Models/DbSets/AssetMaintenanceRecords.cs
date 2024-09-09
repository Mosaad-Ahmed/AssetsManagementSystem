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

        [Required(ErrorMessage = "Maintenance date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [PastOrPresentDate(ErrorMessage = "Maintenance date cannot be in the future.")]
        public DateOnly MaintenanceDate { get; set; }

        [ForeignKey("TOWhomOfUser")]
        public Guid TOWhomOfUserId { get; set; }

        public virtual User TOWhomOfUser { get; set; }


        
        [ForeignKey("TOWhomOfSupplier")]
        public int TOWhomOfSupplierId { get; set; }

        public virtual Supplier TOWhomOfSupplier { get; set; }



        [ForeignKey("PerformedBy")]
        public Guid PerformedById { get; set; }

        public virtual User PerformedBy { get; set; }


    }
}
