namespace AssetsManagementSystem.DTOs.AssetTransferDTOs
{
    public class UpdateAssetTransferRecordRequestDTO:AddAssetTransferRecordRequestDTO
    {
       

        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [PastOrPresentDate(ErrorMessage = "Approval date cannot be in the future.")]
        public DateOnly? ApprovalDate { get; set; }

 
    }
}
