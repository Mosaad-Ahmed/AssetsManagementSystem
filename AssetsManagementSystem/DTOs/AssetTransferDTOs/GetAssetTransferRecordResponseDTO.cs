namespace AssetsManagementSystem.DTOs.AssetTransferDTOs
{
    public class GetAssetTransferRecordResponseDTO
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string AssetName { get; set; }   

        public Guid FromUserId { get; set; }
        public string FromUserName { get; set; }  

        public Guid ToUserId { get; set; }
        public string ToUserName { get; set; }   

        public int FromLocationId { get; set; }
        public string FromLocationName { get; set; }   

        public int ToLocationId { get; set; }
        public string ToLocationName { get; set; }   

        public DateTime TransferDate { get; set; }
        public TransferStatus Status { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string RejectionReason { get; set; }

        public DateTime AddedOnDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
