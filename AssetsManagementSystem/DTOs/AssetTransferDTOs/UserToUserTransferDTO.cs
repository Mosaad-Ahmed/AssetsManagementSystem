namespace AssetsManagementSystem.DTOs.AssetTransferDTOs
{
    public class UserToUserTransferDTO
    {
        [Required(ErrorMessage = "Asset ID is required.")]
        public int AssetId { get; set; }

        [Required(ErrorMessage = "From User ID is required.")]
        public Guid FromUserId { get; set; }

        [Required(ErrorMessage = "To User ID is required.")]
        public Guid ToUserId { get; set; }

         
    }
}
