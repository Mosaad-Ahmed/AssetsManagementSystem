namespace AssetsManagementSystem.DTOs.AssetTransferDTOs
{
    public class UpdateUserToUserTransferDTO
    {
       

        [Required(ErrorMessage = "To User ID is required.")]
        public Guid ToUserId { get; set; }

    }
}
