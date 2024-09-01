namespace AssetsManagementSystem.DTOs.AssetMaintenanceDTOs
{
    public class GetAssetMaintenanceRecordDTO
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string Description { get; set; }
        public DateTime AddedOnDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
