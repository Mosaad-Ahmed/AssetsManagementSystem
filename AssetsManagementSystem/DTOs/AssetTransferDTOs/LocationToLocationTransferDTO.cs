namespace AssetsManagementSystem.DTOs.AssetTransferDTOs
{
    public class LocationToLocationTransferDTO
    {
        [Required(ErrorMessage = "Asset ID is required.")]
        public int AssetId { get; set; }

        [Required(ErrorMessage = "From Location ID is required.")]
        public int FromLocationId { get; set; }

        [Required(ErrorMessage = "To Location ID is required.")]
        public int ToLocationId { get; set; }

        [Required(ErrorMessage = "Transfer date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [PastOrPresentDate(ErrorMessage = "Transfer date cannot be in the future.")]
        public DateTime TransferDate { get; set; }
    }
}
