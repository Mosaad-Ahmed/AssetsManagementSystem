using AssetsManagementSystem.Models.Commons.Common;

namespace AssetsManagementSystem.Models.DbSets
{
    public class Supplier:BaseWithAuditEntity
    {
        [Required(ErrorMessage = "Supplier name is required.")]
        [MaxLength(100, ErrorMessage = "Supplier name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Contact information is required.")]
        [MaxLength(200, ErrorMessage = "Contact information cannot exceed 200 characters.")]
        [MinLength(10,ErrorMessage = "Contact information cannot less than 10 characters.")]
        public string ContactInfo { get; set; }
        public virtual ICollection<AssetsSuppliers> AssetsSuppliers { get; set; }
    }
}
