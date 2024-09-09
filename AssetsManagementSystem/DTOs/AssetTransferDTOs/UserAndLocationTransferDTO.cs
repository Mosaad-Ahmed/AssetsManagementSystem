namespace AssetsManagementSystem.DTOs.AssetTransferDTOs
{
    public class UserAndLocationTransferDTO
    {
        [Required(ErrorMessage = "Asset ID is required.")]
        public int AssetId { get; set; }

        [Required(ErrorMessage = "From User ID is required.")]
        public Guid FromUserId { get; set; }

        [Required(ErrorMessage = "To User ID is required.")]
        public Guid ToUserId { get; set; }

        [Required(ErrorMessage = "From Location ID is required.")]
        public int FromLocationId { get; set; }

        [Required(ErrorMessage = "To Location ID is required.")]
        public int ToLocationId { get; set; }

        
    }
}
