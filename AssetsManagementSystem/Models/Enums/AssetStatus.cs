using System.Runtime.Serialization;

namespace AssetsManagementSystem.Models.Enums
{
    public enum AssetStatus
    {
        [EnumMember(Value = "Active")]
        Active = 0,

        [EnumMember(Value = "In Repair")]
        InRepair = 1,

        [EnumMember(Value = "Retired")]
        Retired = 2
    }
}
