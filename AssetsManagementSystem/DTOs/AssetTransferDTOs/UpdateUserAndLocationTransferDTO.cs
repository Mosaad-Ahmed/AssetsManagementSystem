namespace AssetsManagementSystem.DTOs.AssetTransferDTOs
{
    public class UpdateUserAndLocationTransferDTO
    {
        

        [Required(ErrorMessage = "To User ID is required.")]
        public Guid ToUserId { get; set; }

      

        [Required(ErrorMessage = "To Location ID is required.")]
        public int ToLocationId { get; set; }
    }
}
