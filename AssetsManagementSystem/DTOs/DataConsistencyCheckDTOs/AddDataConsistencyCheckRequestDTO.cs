namespace AssetsManagementSystem.DTOs.DataConsistencyCheckDTOs
{
    public class AddDataConsistencyCheckRequestDTO
    {
        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Issues found field is required.")]
        public bool IssuesFound { get; set; }


        [MaxLength(1000, ErrorMessage = "Resolution cannot exceed 1000 characters.")]
        public string Resolution { get; set; }
    }
}
