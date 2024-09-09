namespace AssetsManagementSystem.Models.DbSets
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string EventType { get; set; }   
        public string EntityName { get; set; } 
        public string EntityId { get; set; }   
        public string Changes { get; set; }  
        public DateTime Timestamp { get; set; }   
        public string PerformedBy { get; set; }   
        public string IPAddress { get; set; }
    }
}
