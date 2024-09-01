namespace AssetsManagementSystem.DTOs.SupplierDTOs
{
    public class AddSupplierRequestDTO
    {
        [Required(ErrorMessage = "Supplier name is required.")]
        [MaxLength(100, ErrorMessage = "Supplier name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Contact information is required.")]
        [MaxLength(200, ErrorMessage = "Contact information cannot exceed 200 characters.")]
        public string ContactInfo { get; set; }
    }
}
