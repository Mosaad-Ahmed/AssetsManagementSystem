namespace AssetsManagementSystem.DTOs.DocumentDTOs
{
    public class GetDocumentResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public Guid UploadedById { get; set; }
        public string UploadedByName { get; set; }
        public int AssetId { get; set; }
        public string AssetName { get; set; }

        public DateTime AddedOnDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
