namespace AssetsManagementSystem.DTOs.CategoryDTOs
{
    public class UpdateCategoryRequestDTO
    {
        [Required(ErrorMessage = "Category name is required.")]
        [MaxLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
        [MinLength(2)]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        public int? ParentCategoryId { get; set; }

    }
}
