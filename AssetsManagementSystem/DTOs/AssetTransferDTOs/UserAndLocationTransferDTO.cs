namespace AssetsManagementSystem.DTOs.AssetTransferDTOs
{
    public class UserAndLocationTransferDTO
    {
        [Required(ErrorMessage = "Asset Serial Number is required.")]
        public string AssetSerialNumber { get; set; }


        [Required(ErrorMessage = "From User ID is required.")]
        public Guid FromUserId { get; set; }

        [Required(ErrorMessage = "To User ID is required.")]
        public Guid ToUserId { get; set; }

        [Required(ErrorMessage = "From Location ID is required.")]
        public string FromLocationBarcode { get; set; }

        [Required(ErrorMessage = "To Location ID is required.")]
        public string ToLocationBarcode { get; set; }

        
    }
}
