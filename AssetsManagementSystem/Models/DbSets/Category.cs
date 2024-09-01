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
        public virtual ICollection<SubCategory>  SubCategories { get; set; }

    }
}
