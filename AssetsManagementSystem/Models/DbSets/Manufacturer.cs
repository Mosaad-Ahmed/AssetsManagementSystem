namespace AssetsManagementSystem.Models.DbSets
{
    public class Manufacturer:IBaseEntityForGeneric
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Info { get; set; }

        public virtual List<Asset> Assets { get; set; }
    }
}
