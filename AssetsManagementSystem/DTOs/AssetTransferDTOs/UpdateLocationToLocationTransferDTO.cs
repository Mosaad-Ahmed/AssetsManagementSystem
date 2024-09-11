namespace AssetsManagementSystem.DTOs.AssetTransferDTOs
{
    public class UpdateLocationToLocationTransferDTO
    {
        [Required(ErrorMessage = "Asset ID is required.")]
        public string AssetSerialNumber { get; set; }

       
        [Required(ErrorMessage = "To Location ID is required.")]
        public string ToLocationBarcode { get; set; }

    }
}
