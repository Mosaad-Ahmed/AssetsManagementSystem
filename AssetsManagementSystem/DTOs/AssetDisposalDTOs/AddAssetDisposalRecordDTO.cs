using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace AssetsManagementSystem.DTOs.AssetDisposalDTOs
{
    public class AddAssetDisposalRecordDTO
    {
        [Required(ErrorMessage = "Asset ID is required.")]
        public int AssetId { get; set; }

        [Required(ErrorMessage = "Disposal date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [PastOrPresentDate(ErrorMessage = "Disposal date cannot be in the future.")]
        public DateOnly DisposalDate { get; set; }

        [Required(ErrorMessage = "Reason for disposal is required.")]
        [MaxLength(500, ErrorMessage = "Reason cannot exceed 500 characters.")]
        public string Reason { get; set; }

        [Required(ErrorMessage = "Method of disposal is required.")]
        [MaxLength(100, ErrorMessage = "Method cannot exceed 100 characters.")]
        public string Method { get; set; }

        
    }
}
