﻿namespace AssetsManagementSystem.Models.DbSets
{
    public class Asset:BaseWithAuditEntity
    {
        [Required(ErrorMessage = "Asset name is required.")]
        [MaxLength(100, ErrorMessage = "Asset name cannot exceed 100 characters.")]
        public string Name { get; set; }
         
        [Required(ErrorMessage = "Model number is required.")]
        [MaxLength(50, ErrorMessage = "Model number cannot exceed 50 characters.")]
        public string ModelNumber { get; set; }


        [Required(ErrorMessage = "Serial number is required.")]
        [MaxLength(100, ErrorMessage = "Serial number cannot exceed 100 characters.")]  
        public string SerialNumber { get; set; }


        [Required(ErrorMessage = "Purchase date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [PastOrPresentDate(ErrorMessage = "Purchase date cannot be in the future.")]
        public DateOnly PurchaseDate { get; set; }


        [Required(ErrorMessage = "Purchase price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Purchase price must be a positive value.")]
        public decimal PurchasePrice { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [FutureDate("PurchaseDate", ErrorMessage = "Warranty expiry date must be after the purchase date.")]
        public DateOnly WarrantyExpiryDate { get; set; }


        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateOnly DepreciationDate { get; set; }


        [Required(ErrorMessage = "Asset status is required.")]
        public string Status { get; set; }

        [MaxLength(1000, ErrorMessage = "Model number cannot exceed 50 characters.")]
        public string? dicription { get; set; }


        [Required(ErrorMessage = "Location is required.")]
        [ForeignKey("Location")]
        public int LocationId { get; set; }   
        public virtual Location Location { get; set; }


        [Required(ErrorMessage = "Assigned user is required.")]
        [ForeignKey("AssignedUser")]
        public Guid AssignedUserId { get; set; }  
        public virtual  User AssignedUser { get; set; }


        [ForeignKey("Category")]
        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }   
        public virtual Category Category { get; set; }


        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        public virtual Manufacturer  Manufacturer { get; set; }

        public virtual ICollection<AssetsSuppliers> AssetsSuppliers { get; set; }  
        public virtual ICollection<Document> Documents { get; set; }   
        public virtual ICollection<AssetDisposalRecord> AssetDisposalRecords { set; get; }
        public virtual ICollection<AssetMaintenanceRecords> AssetMaintenanceRecords { set; get; }
        public virtual ICollection<AssetTransferRecords>  AssetTransferRecords { set; get; }

        

    }
}
