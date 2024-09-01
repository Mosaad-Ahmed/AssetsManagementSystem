namespace AssetsManagementSystem.Models.DbSets
{
    public class DataConsistencyCheck:IBaseEntity
    {
         public int Id { get; set; }


        [Required(ErrorMessage = "Check date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [PastOrPresentDate(ErrorMessage = "Check date cannot be in the future.")]
        public DateTime CheckDate { get; set; }


        [ForeignKey("PerformedBy")]
        [Required(ErrorMessage = "The ID of the person who performed the check is required.")]
        public Guid PerformedById { get; set; }   
        public virtual User PerformedBy { get; set; }


        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Issues found field is required.")]
        public bool IssuesFound { get; set; }


        [MaxLength(1000, ErrorMessage = "Resolution cannot exceed 1000 characters.")]
        public string Resolution { get; set; }   
    }
}
