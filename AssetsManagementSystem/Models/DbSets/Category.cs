namespace AssetsManagementSystem.Models.DbSets
{
    public class Category:BaseWithAuditEntity
    { 

        [Required(ErrorMessage = "Category name is required.")]
        [MaxLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
        [MinLength(2)]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }
        public string SerialCode { get; set; }

        public int? ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }

        public virtual ICollection<Category>  SubCategories { get; set; }

    }
}
