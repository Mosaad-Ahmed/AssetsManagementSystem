namespace AssetsManagementSystem.DTOs.SubCategoryDTOs
{
    public class GetSubCategoryRequestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MainCategoryId { get; set; }
        public string MainCategoryName { get; set; }

        public DateTime AddedOnDate { set; get; }
        public DateTime? UpdatedDate { set; get; }
        public DateTime? DeletedDate { set; get; }
        public bool? IsDeleted { set; get; }
    }
}
