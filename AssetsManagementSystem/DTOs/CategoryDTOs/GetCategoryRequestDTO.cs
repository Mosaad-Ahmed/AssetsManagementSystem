namespace AssetsManagementSystem.DTOs.CategoryDTOs
{
    public class GetCategoryRequestDTO
    {
        public int Id { get; set; }

      
        public string Name { get; set; }

        
        public string Description { get; set; }

        public DateTime AddedOnDate { set; get; }
        public DateTime? UpdatedDate { set; get; }
        public DateTime? DeletedDate { set; get; }
        public bool? IsDeleted { set; get; }
    }
}
