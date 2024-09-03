namespace AssetsManagementSystem.Models.DbSets
{
    public class Document:BaseWithAuditEntity
    {
 
        [Required(ErrorMessage = "Document title is required.")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        [MinLength(3,ErrorMessage = "Tittle cannot be less than 3 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "File path is required.")]
        [MaxLength(500, ErrorMessage = "File path cannot exceed 500 characters.")]
        [MinLength(5, ErrorMessage = "File path cannot be less than 5 characters.")]

        public string FilePath { get; set; }


        [ForeignKey("UploadedBy")]
        [Required(ErrorMessage = "Uploaded by user ID is required.")]
         public Guid UploadedById { get; set; }
        public virtual User UploadedBy { get; set; }


        [ForeignKey("Asset")]
        [Required(ErrorMessage = "Asset ID is required.")]
         public int AssetId { get; set; }
        public virtual Asset Asset { get; set; }
    }
}
