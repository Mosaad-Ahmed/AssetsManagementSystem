namespace AssetsManagementSystem.DTOs.AssetDisposalDTOs
{
    public class GetAssetDisposalRecordDTO
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public DateTime DisposalDate { get; set; }
        public string Reason { get; set; }
        public string Method { get; set; }
        public Guid ApprovedById { get; set; }
        public string ApprovedByName { get; set; }
        public DateTime AddedOnDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
