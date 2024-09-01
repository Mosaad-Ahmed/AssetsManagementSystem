namespace AssetsManagementSystem.DTOs.SubCategoryDTOs
{
    public class AddSubCategoryRequestDTO
    {
        [Required(ErrorMessage = "Asset subtype name is required.")]
        [MaxLength(50, ErrorMessage = "Asset subtype name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Main category ID is required.")]
        public int MainCategoryId { get; set; }
    }
}
