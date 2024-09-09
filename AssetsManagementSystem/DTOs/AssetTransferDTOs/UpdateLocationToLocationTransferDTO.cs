namespace AssetsManagementSystem.DTOs.AssetTransferDTOs
{
    public class UpdateLocationToLocationTransferDTO
    {
        [Required(ErrorMessage = "Asset ID is required.")]
        public int AssetId { get; set; }

       
        [Required(ErrorMessage = "To Location ID is required.")]
        public int ToLocationId { get; set; }

    }
}
