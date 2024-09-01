namespace AssetsManagementSystem.Models.DbSets
{
    public class AssetLifecycle:IBaseEntity
    {
        public int Id { get; set; }
        public int AssetId { get; set; }   
        public virtual Asset Asset { get; set; }   
        public DateTime EventDate { get; set; }   
        public string EventType { get; set; }   
        public string Notes { get; set; }   
        public Guid PerformedById { get; set; }   
        public  virtual User PerformedBy { get; set; }
    }
}
