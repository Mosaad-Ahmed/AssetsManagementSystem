namespace AssetsManagementSystem.Models.DbSets
{
    public class Location:BaseWithAuditEntity
    {

        [Required(ErrorMessage = "Location name is required.")]
        [MaxLength(200, ErrorMessage = "Location name cannot exceed 200 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(500, ErrorMessage = "Address cannot exceed 500 characters.")]
        public string Address { get; set; }

         public virtual ICollection<Asset> Assets { get; set; }
    }
}
