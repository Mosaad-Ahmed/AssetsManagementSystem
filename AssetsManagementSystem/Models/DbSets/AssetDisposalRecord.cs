namespace AssetsManagementSystem.Models.DbSets
{
    public class AssetDisposalRecord:BaseWithAuditEntity
    {
        [Required(ErrorMessage = "Asset ID is required.")]
        [ForeignKey("Asset")]
        public int AssetId { get; set; }   
        public virtual Asset Asset { get; set; }

       
        [Required(ErrorMessage = "Reason for disposal is required.")]
        [MaxLength(500, ErrorMessage = "Reason cannot exceed 500 characters.")]
        public string Reason { get; set; }


        [Required(ErrorMessage = "Method of disposal is required.")]
        [MaxLength(100, ErrorMessage = "Method cannot exceed 100 characters.")]
        public string Method { get; set; }


        [Required(ErrorMessage = "Approved by is required.")]
        [ForeignKey("ApprovedBy")]
        public Guid ApprovedById { get; set; }     
        public virtual User ApprovedBy { get; set; }  
    }
}
