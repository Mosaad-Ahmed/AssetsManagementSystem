namespace AssetsManagementSystem.Models.DbSets
{
  
    public class AssetsSuppliers:IBaseEntityForGeneric
    {

         //public int ID { get; set; }

        [ForeignKey("Asset")]
        public int AssetId { get; set; }

        public virtual Asset Asset { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }


    }
}
