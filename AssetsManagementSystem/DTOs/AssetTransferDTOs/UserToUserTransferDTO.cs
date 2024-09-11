namespace AssetsManagementSystem.DTOs.AssetTransferDTOs
{
    public class UserToUserTransferDTO
    {
        [Required(ErrorMessage = "Asset serial Number is required.")]
        public string AssetSerialNumber { get; set; }

        [Required(ErrorMessage = "From User ID is required.")]
        public Guid FromUserId { get; set; }

        [Required(ErrorMessage = "To User ID is required.")]
        public Guid ToUserId { get; set; }

         
    }
}
